using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using StaticBlogGenerator.Lib.BL;
using StaticBlogGenerator.Lib.DAL;

namespace StaticBlogGenerator.App.WindowModels;

public class MainWindowModel : BaseNotification
{
    #region Observable properties

    private Article? _article;

    public Article? SelectedArticle
    {
        get => _article;
        set
        {
            SetField(ref _article, value);

            Title = _article?.Title;
            Content = _article?.Content;

            State = StateOptions.Editing;
        }
    }

    public ObservableCollection<Article> Articles { get; set; }

    private string? _title;
    public string? Title
    {
        get => _title;
        set
        {
            SetField(ref _title, value);
            CommandCancel.OnCanExecuteChanged();
        }
    }

    private string? _content;
    public string? Content
    {
        get => _content;
        set
        {
            SetField(ref _content, value);
            CommandCancel.OnCanExecuteChanged();
        }
    }

    #endregion

    #region Commands

    public LambdaCommand CommandConnect { get; set; }
    public LambdaCommand CommandSave { get; set; }
    public LambdaCommand CommandCancel { get; set; }

    public LambdaCommand CommandNew { get; set; }

    #endregion

    #region StateValue


    private StateOptions _state;

    private StateOptions State
    {
        get => _state;
        set
        {
            _state = value;
            CommandSave.OnCanExecuteChanged();
        }
    }

    #endregion

    private readonly ArticlesContext _articlesContext;

    public MainWindowModel()
    {
        _articlesContext = new ArticlesContext(new ArticlesDbContext());
        Articles = new ObservableCollection<Article>();

        _state = StateOptions.Unknown;

        CommandConnect = new LambdaCommand(
            execute: async _ => await InitArticles(),
            canExecute: _ => Articles.Count == 0
        );

        CommandCancel = new LambdaCommand(
            execute: _ =>
            {
                SelectedArticle = null;

                State = StateOptions.Unknown;
            },
            canExecute: _ => !string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Title)
        );

        CommandSave = new LambdaCommand(
            execute: async _ =>
            {
                switch (State)
                {
                    case StateOptions.Editing:
                        await UpdateArticle();
                        break;
                    case StateOptions.New:
                        await NewArticle();
                        break;
                    case StateOptions.Unknown:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            },
            canExecute: _ => State is StateOptions.Editing or StateOptions.New
        );

        CommandNew = new LambdaCommand(
            execute: _ =>
            {
                SelectedArticle = new Article();

                State = StateOptions.New;
            }
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

    private async Task UpdateArticle()
    {
        SelectedArticle.Title = Title;
        SelectedArticle.Content = Content;
        await _articlesContext.UpdateAsync(SelectedArticle);
    }

    private async Task NewArticle()
    {
        var article = new Article { Title = this.Title, Content = this.Content };
        await _articlesContext.InsertAsync(article);

        await InitArticles();
    }
}
