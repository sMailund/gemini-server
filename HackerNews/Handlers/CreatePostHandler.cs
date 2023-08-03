using System.Web;
using gemini_server;
using gemini_server.Responses;
using HackerNews.Repositories;

namespace HackerNews.Handlers;

internal class CreatePostHandler
{
    private readonly IPostRepository _posts;

    public CreatePostHandler(IPostRepository posts)
    {
        _posts = posts;
    }

    public IResponse Handle(Request req)
    {
        if (!req.IsLoggedIn)
        {
            return new BadRequestResponse
            {
                Reason = "You need to be a logged in user in order to post"
            };
        }
        
        var query = req.Uri.Query;

        if (query.Equals(""))
        {
            return new InputResponse("Enter your URI, followed by your title");
        }

        var parts = query.Split("%20");
        if (parts.Length < 2)
        {
            return new BadRequestResponse
            {
                Reason = "Submission must contain both link and title"
            };
        }

        if (req.UserName is null || req.UserThumbprint is null)
        {
            return new BadRequestResponse
            {
                Reason = "Certificate subject name or certificate thumbprint can not be null"
            };
        }
        
        var link = parts[0];
        var title = string.Join(" ", parts, 1, parts.Length - 1);
        var postId = Guid.NewGuid();
        var post = new Post(title, link, req.UserName, postId, req.UserThumbprint);
        _posts.AddNewPost(post);

        // TODO: handle post
        return new RedirectResponse($"/view-post?{postId.ToString()}");
    }
}