namespace StaticBlogGenerator.Lib;

public interface IContext<T>
{
    public void Insert(T obj);
    public void Update(T obj);
    public void Delete(T obj);
    public T GetSingle(Guid id);
    public IEnumerable<T> GetAll();
}
