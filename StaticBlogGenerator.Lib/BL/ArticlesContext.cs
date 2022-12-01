using StaticBlogGenerator.Lib.DAL;

namespace StaticBlogGenerator.Lib.BL;

public class ArticlesContext : IContext<Article>
{
    private readonly IContext<Article> _context;

    public ArticlesContext(IContext<Article> context)
    {
        _context = context;
    }

    public void Insert(Article obj)
    {
        _context.Insert(obj);
    }

    public void Update(Article obj)
    {
        _context.Update(obj);
    }

    public void Delete(Article obj)
    {
        _context.Delete(obj);
    }

    public Article GetSingle(Guid id)
    {
        return _context.GetSingle(id);
    }

    public IEnumerable<Article> GetAll()
    {
        return _context.GetAll();
    }
}
