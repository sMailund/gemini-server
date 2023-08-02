namespace HackerNews.Repositories;

internal interface IPostRepository
{
    List<Post> GetTopPosts();
    void AddNewPost(Post post);
}