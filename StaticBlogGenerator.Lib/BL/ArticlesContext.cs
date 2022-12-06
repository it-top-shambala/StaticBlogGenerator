using StaticBlogGenerator.Lib.DAL;

namespace StaticBlogGenerator.Lib.BL;

public class ArticlesContext : IContext<Article>
{
    private readonly IContext<Article> _context;

    public ArticlesContext(IContext<Article> context)
    {
        _context = context;
    }

    public async Task InsertAsync(Article obj)
    {
        await _context.InsertAsync(obj);
    }

    public async Task UpdateAsync(Article obj)
    {
        await _context.UpdateAsync(obj);
    }

    public async Task DeleteAsync(Article obj)
    {
        await _context.DeleteAsync(obj);
    }

    public async Task<Article> GetSingleAsync(Guid id)
    {
        return await _context.GetSingleAsync(id);
    }

    public IAsyncEnumerable<Article> GetAllAsyncEnumerable()
    {
        return _context.GetAllAsyncEnumerable();
    }
}
