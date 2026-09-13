using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // Add DbSet properties here as database tables are added
}
