using gemini_server;
using gemini_server.Responses;
using HackerNews.Guards;
using HackerNews.Repositories;

namespace HackerNews.Handlers;

internal class AddCommentHandler
{
    
    private readonly IPostRepository _posts;

    public AddCommentHandler(IPostRepository posts)
    {
        _posts = posts;
    }

    public IResponse Handle(Request req)
    {
        var error = AssertUserNotNull.Assert(req);
        if (error is not null)
        {
            return error;
        }

        var unparsedPostId =
            req.Uri.LocalPath
                .Split("?")[0]
                .Trim('/')
                .Split('/')
                .Last();

        Guid postId;
        try
        {
            postId = Guid.Parse(unparsedPostId);
        }
        catch
        {
            return new BadRequestResponse()
            {
                Reason = "Invalid post id."
            };
        }
        
        var post = _posts.GetPostById(postId);
        if (post is null)
        {
            return new NotFoundResponse();
        }
        
        var query = req.Uri.Query;

        if (query.Equals(""))
        {
            return new InputResponse("Enter your comment");
        }
        
        post.AddComment(req.UserName, req.UserThumbprint, query);
        return new RedirectResponse($"/view-post?{postId.ToString()}");
    }
}