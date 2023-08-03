namespace HackerNews.Repositories;

internal class InMemoryPostRepository : IPostRepository
{
    private readonly List<Post> _posts = new();

    public List<Post> GetTopPosts(int limit) => _posts
        .OrderByDescending(CalculateScore)
        .Take(limit)
        .ToList();

    public void AddNewPost(Post post) => _posts.Add(post);

    public Post? GetPostById(Guid postId) => _posts.FirstOrDefault(it => it.PostId.Equals(postId));

    /**
     * port of the hacker news ranking algorithm
     */
    private static double CalculateScore(Post post)
    {
        const double gravity = 1.8;
        
        /*
         * in the real HN algorithm, the score is subtracted one point in order to negate the initial upvote from the poster.
         * this requires that users browse the 'new' section of the site in order to gain initial traction.
         * because there is no 'new' section so far, we don't negate the initial posters point
         */
        var points = post.Points; 
        var hoursSinceSubmission = (DateTime.Now - post.PostedAt).TotalHours;

        var calculateScore = points / Math.Pow(hoursSinceSubmission + 2, gravity);
        return calculateScore;
    }

}