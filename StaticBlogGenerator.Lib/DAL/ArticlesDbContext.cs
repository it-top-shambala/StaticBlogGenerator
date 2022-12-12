using Microsoft.EntityFrameworkCore;

namespace StaticBlogGenerator.Lib.DAL;

public sealed class ArticlesDbContext : DbContext, IContext<Article>
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

    public async Task InsertAsync(Article obj)
    {
        obj.Guid = Guid.NewGuid();
        obj.CreationDate = DateTime.Now;
        obj.LastModificationDate = obj.CreationDate;
        await Articles.AddAsync(obj);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(Article obj)
    {
        var article = Articles.First(a => a.Guid == obj.Guid);
        article.Title = obj.Title;
        article.Content = obj.Content;
        article.LastModificationDate = DateTime.Now;
        await SaveChangesAsync();
    }

    public async Task DeleteAsync(Article obj)
    {
        Articles.Remove(obj);
        await SaveChangesAsync();
    }

    public async Task<Article> GetSingleAsync(Guid id)
    {
        return await Articles.FirstAsync(a => a.Guid == id);
    }

    public IAsyncEnumerable<Article> GetAllAsync()
    {
        return Articles.AsAsyncEnumerable();
    }
}
