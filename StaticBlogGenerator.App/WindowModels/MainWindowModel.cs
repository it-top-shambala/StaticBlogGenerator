using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StaticBlogGenerator.Lib.BL;
using StaticBlogGenerator.Lib.DAL;

namespace StaticBlogGenerator.App.WindowModels;

public class MainWindowModel
{
    public ObservableCollection<Article> Articles { get; set; }

    private readonly ArticlesContext _articlesContext;

    public MainWindowModel()
    {
        _articlesContext = new ArticlesContext(new ArticlesDbContext());
        Articles = new ObservableCollection<Article>();
        InitArticles();
    }

    private async Task InitArticles()
    {
        await foreach (var article in _articlesContext.GetAllAsyncEnumerable())
        {
            Articles.Add(article);
        }
    }
}
