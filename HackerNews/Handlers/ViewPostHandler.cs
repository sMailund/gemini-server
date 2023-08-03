using System.Text;
using gemini_server;
using gemini_server.Responses;
using HackerNews.Repositories;

namespace HackerNews.Handlers;

internal class ViewPostHandler
{
    
    private readonly IPostRepository _posts;

    public ViewPostHandler(IPostRepository posts)
    {
        _posts = posts;
    }

    public IResponse Handle(Request req)
    {
        var query = req
            .Uri
            .Query[1..]; // first character is always '?', skip this

        var selectedPostId = Guid.Parse(query);
        
        if (query.Equals(""))
        {
            return new BadRequestResponse()
            {
                Reason = "Post id missing"
            };
        }

        var post = _posts.GetPostById(selectedPostId);

        if (post is null)
        {
            return new NotFoundResponse();
        }

        var sb = new StringBuilder()
            .AppendLine($"# {post.Title}")
            .AppendLine($"posted by {post.PostedByUser} ({post.PostedByUserId})")
            .AppendLine($"=> {post.Link} Follow link");
        
        return new SuccessResponse(sb.ToString());
    }
    
}