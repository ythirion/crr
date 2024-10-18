using FluentAssertions;
using FsCheck;
using FsCheck.Xunit;
using Xunit;
using static Blog.Tests.ArticleBuilder;

namespace Blog.Tests;

public class ArticleTests
{
    private readonly ArticleBuilder _article = AnArticle();
    private readonly Bogus.Randomizer _random = new();

    [Fact]
    public void Should_Add_Comment_In_An_Article()
    {
        var article = _article.Build();
        article.AddComment(CommentText, Author);

        article.Comments.Should().HaveCount(1);
        AssertComment(article.Comments[0], CommentText, Author);
    }

    [Fact]
    public void Should_Add_Comment_In_An_Article_Containing_Already_A_Comment()
    {
        var newComment = _random.String(10);
        var newAuthor = _random.String(3);

        var article = _article.Commented().Build();

        article.AddComment(newComment, newAuthor);
        article.Comments.Should().HaveCount(2);
        AssertComment(article.Comments[1], newComment, newAuthor);
    }

    private static void AssertComment(Comment comment, string expectedComment, string expectedAuthor)
    {
        comment.Text.Should().Be(expectedComment);
        comment.Author.Should().Be(expectedAuthor);
        comment.CreationDate.Should().Be(CreationDate);
    }

    public class Fail
    {
        [Fact]
        public void When_Adding_An_Existing_Comment()
        {
            var article = AnArticle().Build();
            article.AddComment(CommentText, Author);

            var act = () => article.AddComment(CommentText, Author);
            act.Should().Throw<CommentAlreadyExistException>();
        }

        private static readonly Arbitrary<string> NonEmptyString =
            Arb.Default
                .String()
                .MapFilter(s => s, s => !string.IsNullOrWhiteSpace(s));

        [Property]
        public Property When_Adding_An_Existing_Comment_Then_It_Should_Fail()
            => Prop.ForAll(NonEmptyString, NonEmptyString,
                (comment, author) =>
                    HasThrownException<CommentAlreadyExistException>(() =>
                    {
                        var article = AnArticle().Build();
                        article.AddComment(comment, author);
                        article.AddComment(comment, author);
                    }));

        private static bool HasThrownException<TException>(Action action)
            where TException : Exception
        {
            try
            {
                action();
                return false;
            }
            catch (TException)
            {
                return true;
            }
        }
    }
}