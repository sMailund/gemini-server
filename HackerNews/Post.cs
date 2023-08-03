using System.Security.Cryptography.X509Certificates;

namespace HackerNews;

internal class Post
{
    public Post(string title, string link, string postedByUser, Guid postId, string postedByUserId, DateTime postedAt, int points)
    {
        Title = title;
        Link = link;
        PostedByUser = postedByUser;
        PostId = postId;
        PostedByUserId = postedByUserId;
        PostedAt = postedAt;
        Points = points;
    }

    public string Title { get; init; }
    public string Link { get; init; }
    public string PostedByUser { get; init; }
    public Guid PostId { get; init; }
    public string PostedByUserId { get; init; }
    public DateTime PostedAt { get; init; }
    public int Points { get; init; }

    public void Deconstruct(out string title, out string link, out string postedByUser, out Guid postId, out string postedByUserId, out DateTime postedAt, out int points)
    {
        title = Title;
        link = Link;
        postedByUser = PostedByUser;
        postId = PostId;
        postedByUserId = PostedByUserId;
        postedAt = PostedAt;
        points = Points;
    }
}