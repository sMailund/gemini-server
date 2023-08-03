using System.Security.Cryptography.X509Certificates;

namespace HackerNews;

internal class Post
{
    public Post(string Title, string Link, string PostedByUser, Guid PostId, string PostedByUserId, DateTime PostedAt, int Points)
    {
        this.Title = Title;
        this.Link = Link;
        this.PostedByUser = PostedByUser;
        this.PostId = PostId;
        this.PostedByUserId = PostedByUserId;
        this.PostedAt = PostedAt;
        this.Points = Points;
    }

    public string Title { get; init; }
    public string Link { get; init; }
    public string PostedByUser { get; init; }
    public Guid PostId { get; init; }
    public string PostedByUserId { get; init; }
    public DateTime PostedAt { get; init; }
    public int Points { get; init; }

    public void Deconstruct(out string Title, out string Link, out string PostedByUser, out Guid PostId, out string PostedByUserId, out DateTime PostedAt, out int Points)
    {
        Title = this.Title;
        Link = this.Link;
        PostedByUser = this.PostedByUser;
        PostId = this.PostId;
        PostedByUserId = this.PostedByUserId;
        PostedAt = this.PostedAt;
        Points = this.Points;
    }
}