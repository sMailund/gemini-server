namespace HackerNews.Repositories;

internal class InMemoryPostRepository : IPostRepository
{
    private readonly List<Post> _posts = new List<Post>();

    public List<Post> GetTopPosts(int limit) => _posts
        .Take(limit)
        .ToList();

    public void AddNewPost(Post post) => _posts.Add(post);

    public Post? GetPostById(Guid postId) => _posts.FirstOrDefault(it => it.PostId.Equals(postId));

}