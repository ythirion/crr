namespace Blog.Tests;

public class ArticleBuilder
{
    public const string Author = "Pablo Escobar";
    public const string CommentText = "Amazing article !!!";

    private readonly Dictionary<string, string> _comments = new();
    private readonly Bogus.Randomizer _random = new();

    public static readonly DateOnly CreationDate = new(2024, 11, 5);
    private static readonly Func<DateTime> Now = () => CreationDate.ToDateTime(new TimeOnly(13, 0, 1));

    public static ArticleBuilder AnArticle() => new();

    public ArticleBuilder Commented()
    {
        _comments.Add(CommentText, Author);
        return this;
    }

    public Article Build()
    {
        var article = Article.Create(_random.String(), _random.String(), Now);

        _comments
            .ToList()
            .ForEach(comment => article.AddComment(comment.Key, comment.Value));

        return article;
    }
}