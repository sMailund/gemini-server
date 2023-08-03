namespace HackerNews;

internal class Post
{
    public Post(string title, string link, string postedByUser, Guid postId, string postedByUserId, DateTime postedAt)
    {
        Title = title;
        Link = link;
        PostedByUser = postedByUser;
        PostId = postId;
        PostedByUserId = postedByUserId;
        PostedAt = postedAt;
        _upvotedBy.Add(postedByUserId);
    }

    public void Upvote(string userId)
    {
        _downvotedBy.Remove(userId);
        _upvotedBy.Add(userId);
    }
    
    public void Downvote(string userId)
    {
        _downvotedBy.Add(userId);
        _upvotedBy.Remove(userId);
    }
    
    public int Points => _upvotedBy.Count - _downvotedBy.Count;

    public string Title { get; init; }
    public string Link { get; init; }
    public string PostedByUser { get; init; }
    public Guid PostId { get; init; }
    public string PostedByUserId { get; init; }
    public DateTime PostedAt { get; init; }

    private readonly HashSet<string> _upvotedBy = new();
    
    private readonly HashSet<string> _downvotedBy = new();

}