using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Blog.TagHelpers;

[HtmlTargetElement("vite-script", Attributes = "src")]
public class ViteScriptTagHelper : TagHelper
{
    private readonly IWebHostEnvironment _env;
    private readonly string _devBaseUrl;

    public ViteScriptTagHelper(IWebHostEnvironment env, IConfiguration config)
    {
        _env = env;
        var configBaseUrl = config["Vite:BaseUrl"];
        _devBaseUrl = !string.IsNullOrEmpty(configBaseUrl) ? configBaseUrl : "http://localhost:5173";
    }

    [HtmlAttributeName("src")]
    public string Src { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "script";
        output.Attributes.SetAttribute("type", "module");

        var cleanSrc = Src.StartsWith("~/") ? Src[2..] : Src;

        if (_env.IsDevelopment())
        {
            // Point directly to Vite dev server on local port 5173
            output.Attributes.SetAttribute("src", $"{_devBaseUrl}/{cleanSrc}");
        }
        else
        {
            // Production: Map source entrypoints to compiled wwwroot/dist assets
            var prodSrc = cleanSrc
                .Replace("src/islands/", "dist/island-")
                .Replace("src/apps/game/main.ts", "dist/app-game.js")
                .Replace("src/apps/game/main.js", "dist/app-game.js");

            // Ensure .ts extension becomes .js for production bundles
            if (prodSrc.EndsWith(".ts"))
            {
                prodSrc = prodSrc[..^3] + ".js";
            }

            output.Attributes.SetAttribute("src", $"~/{prodSrc}");
            output.Attributes.SetAttribute("asp-append-version", "true");
        }
    }
}