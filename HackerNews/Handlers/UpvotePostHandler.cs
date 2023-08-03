using gemini_server;
using gemini_server.Responses;
using HackerNews.Guards;
using HackerNews.Repositories;

namespace HackerNews.Handlers;

internal class UpvotePostHandler
{
    
    private readonly IPostRepository _posts;

    public UpvotePostHandler(IPostRepository posts)
    {
        _posts = posts;
    }

    public IResponse Handle(Request req)
    {
        var userError = AssertUserNotNull.Assert(req);
        if (userError is not null)
        {
            return userError;
        }
            
        var (postError, post) = AssertPostExists.GetOrFail(_posts, req);
        if (postError is not null)
        {
            return postError;
        }
        
        post.Upvote(req.UserThumbprint);
        return new RedirectResponse($"/view-post?{post.PostId.ToString()}");
    }
}