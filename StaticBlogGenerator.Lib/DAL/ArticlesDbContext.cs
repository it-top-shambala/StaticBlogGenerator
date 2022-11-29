using Microsoft.EntityFrameworkCore;

namespace StaticBlogGenerator.Lib.DAL;

public sealed class ArticlesDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }

    private readonly string _connectionString;

    public ArticlesDbContext(string connectionString = "Data Source=articles.db;")
    {
        _connectionString = connectionString;
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }
}
