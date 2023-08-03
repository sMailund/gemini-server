using gemini_server;
using gemini_server.Responses;
using HackerNews.Guards;
using HackerNews.Repositories;

namespace HackerNews.Handlers;

internal class DownvotePostHandler
{
    
    private readonly IPostRepository _posts;

    public DownvotePostHandler(IPostRepository posts)
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
        
        post.Downvote(req.UserThumbprint);
        return new RedirectResponse($"/view-post?{post.PostId.ToString()}");
    }
}