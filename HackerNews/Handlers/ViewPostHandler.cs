using System.Text;
using gemini_server;
using gemini_server.Responses;
using HackerNews.Guards;
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
        var (error, post) = AssertPostExists.GetOrFail(_posts, req);
        if (error is not null)
        {
            return error;
        }

        var sb = new StringBuilder()
            .AppendLine($"# {post.Title}")
            .AppendLine($"posted by {post.PostedByUser} ({post.PostedByUserId})")
            .AppendLine($"=> {post.Link} Follow link");
        
        return new SuccessResponse(sb.ToString());
    }
    
}