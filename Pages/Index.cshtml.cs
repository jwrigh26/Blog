using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages;

public class IndexModel : PageModel
{
    public List<BlogPost> Posts { get; set; } = new();
    public bool HasMore { get; set; }

    public async Task OnGetAsync()
    {
        // Load initial 5 posts for first render
        Posts = await GetPostsFromDb(page: 1, pageSize: 25);
        HasMore = true;
    }

    // Handler called via fetch('/Index?handler=MorePosts&page=2')
    public async Task<IActionResult> OnGetMorePostsAsync(int page = 1)
    {
        // TRY and Catch needed ? Maybe not? What is best practice?
        int pageSize = 25;
        var posts = await GetPostsFromDb(page, pageSize);

        if (!posts.Any())
        {
            return StatusCode(204); // Return 204 if no more posts exist
        }

        // Returns _BlogPostCard.cshtml partial populated with posts
        return Partial("_BlogPostCard", posts);
    }

    private async Task<List<BlogPost>> GetPostsFromDb(int page, int pageSize)
    {
        // Simulate network database delay asynchronously
        await Task.Delay(50);

        return Enumerable
            .Range((page - 1) * pageSize + 1, pageSize)
            .Select(i => 
            {
                var framework = (i % 3) switch
                {
                    0 => FrameworkType.Vue,
                    1 => FrameworkType.React,
                    _ => FrameworkType.Razor
                };

                return new BlogPost
                {
                Id = i,
                Title = $"Blog Post #{i}",
                Framework = framework,
                Slug = i % 3 == 0 ? "vue-post" : i % 3 == 1 ? "react-post" : "razor-post"
                };
            })
            .ToList();
    }
}

public enum FrameworkType
{
    Unkonwn,
    Vue,
    React,
    Razor
}

public class BlogPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public FrameworkType Framework { get; set; }

    public string Summary => Framework switch
    {
        FrameworkType.Vue => "A demo Vue page.",
        FrameworkType.React => "A demo React page.",
        FrameworkType.Razor => "A demo Razor page.",
        _ => "A web development page."
    };
}