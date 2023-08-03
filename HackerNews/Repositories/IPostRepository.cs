namespace HackerNews.Repositories;

internal interface IPostRepository
{
    List<Post> GetTopPosts(int limi);
    void AddNewPost(Post post);
    Post? GetPostById(Guid postId);
}