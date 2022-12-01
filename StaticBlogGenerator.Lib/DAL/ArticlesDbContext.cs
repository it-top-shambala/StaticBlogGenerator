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

    public void Insert(Article obj)
    {
        obj.CreationDate = DateTime.Now;
        obj.LastModificationDate = obj.CreationDate;
        Articles.Add(obj);
        SaveChanges();
    }

    public void Update(Article obj)
    {
        var article = Articles.First(a => a.Guid == obj.Guid);
        article.Title = obj.Title;
        article.Content = obj.Content;
        article.LastModificationDate = DateTime.Now;
        SaveChanges();
    }

    public void Delete(Article obj)
    {
        Articles.Remove(obj);
        SaveChanges();
    }

    public Article GetSingle(Guid id)
    {
        return Articles.First(a => a.Guid == id);
    }

    public IEnumerable<Article> GetAll()
    {
        return Articles;
    }
}
