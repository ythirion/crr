namespace Blog;

using TimeProvider = Func<DateTime>;

public class Article
{
    private readonly string _name;
    private readonly string _content;
    private readonly TimeProvider _timeProvider;
    public List<Comment> Comments { get; }

    private Article(string name, string content, TimeProvider timeProvider)
    {
        _name = name;
        _content = content;
        _timeProvider = timeProvider;
        Comments = [];
    }

    public static Article Create(string name, string content, TimeProvider timeProvider) =>
        new(name, content, timeProvider);

    private void AddComment(
        string text,
        string author,
        DateOnly creationDate)
    {
        var comment = new Comment(text, author, creationDate);
        if (Comments.Contains(comment))
        {
            throw new CommentAlreadyExistException();
        }

        Comments.Add(comment);
    }

    public void AddComment(string text, string author)
        => AddComment(text, author, DateOnly.FromDateTime(_timeProvider().Date));
}

public record Comment(string Text, string Author, DateOnly CreationDate);

public class CommentAlreadyExistException : ArgumentException;