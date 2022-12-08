using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StaticBlogGenerator.Lib.BL;
using StaticBlogGenerator.Lib.DAL;

namespace StaticBlogGenerator.App.WindowModels;

public class MainWindowModel : BaseNotification
{
    #region Observable properties

    private Article _article;

    public Article SelectedArticle
    {
        get => _article;
        set => SetField(ref _article, value);
    }

    public ObservableCollection<Article> Articles { get; set; }

    #endregion

    #region Commands

    public LambdaCommand CommandConnect { get; set; }

    #endregion


    private readonly ArticlesContext _articlesContext;

    public MainWindowModel()
    {
        _article = new Article();
        _articlesContext = new ArticlesContext(new ArticlesDbContext());
        Articles = new ObservableCollection<Article>();

        CommandConnect = new LambdaCommand(
            execute: async _ => await InitArticles(),
            canExecute: _ => Articles.Count == 0
            );
    }

    private async Task InitArticles()
    {
        Articles.Clear();

        await foreach (var article in _articlesContext.GetAllAsync())
        {
            Articles.Add(article);
        }
        CommandConnect.OnCanExecuteChanged();
    }
}
