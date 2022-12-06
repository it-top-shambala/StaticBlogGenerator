namespace StaticBlogGenerator.Lib;

public interface IContext<T>
{
    public Task InsertAsync(T obj);
    public Task UpdateAsync(T obj);
    public Task DeleteAsync(T obj);
    public Task<T> GetSingleAsync(Guid id);
    public IAsyncEnumerable<T> GetAllAsync();
}
