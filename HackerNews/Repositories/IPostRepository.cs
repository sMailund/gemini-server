namespace HackerNews.Repositories;

internal interface IPostRepository
{
    List<Post> GetTopPosts();
    void AddNewPost(Post post);
    Post? GetPostById(Guid postId);
}