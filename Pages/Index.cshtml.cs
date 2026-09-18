using Blog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages;

public class IndexModel : PageModel
{
    public List<BlogPost> Posts { get; set; } = new();
    public bool HasMore { get; set; }

    public void OnGet()
    {
        // Load initial 5 posts for first render
        Posts = GetPostsFromDb(page: 1, pageSize: 5);
        HasMore = true;
    }

    // Handler called via fetch('/Index?handler=MorePosts&page=2')
    public IActionResult OnGetMorePosts(int page = 1)
    {
        int pageSize = 5;
        var posts = GetPostsFromDb(page, pageSize);

        if (!posts.Any())
        {
            return NoContent(); // Return 204 if no more posts exist
        }

        // Returns _BlogPostCard.cshtml partial populated with posts
        return Parital("_BlogPostCard", posts);
    }

    private List<BlogPost> GetPostsFromDb(int page, int pageSize)
    {
        // Replace with your EF Core / DB query logic
        return Enumerable
            .Range((page - 1) * pageSize + 1, pageSize)
            .Select(i => new BlogPost
            {
                Id = i,
                Title = $"Blog Post  #{i}",
                Summary = "A deep dive into architecture and web development.",
                // Deomonstrate polygot pages
                Slug =
                    i % 3 == 0 ? "vue-post"
                    : i % 3 == 1 ? "react-post"
                    : "razor-post",
            })
            .ToList();
    }

    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}
