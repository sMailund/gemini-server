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
            .AppendLine($"Posted by {post.PostedByUser}")
            .AppendLine($"({post.Points}) point(s)")
            .AppendLine();

        if (req.IsLoggedIn)
        {
            var thumbprint = req.UserThumbprint;
            sb.AppendLine(
                $"=> /upvote-post?{post.PostId} Upvote post " + (post.UserHasUpvoted(thumbprint) ? "[x]" : ""));
            sb.AppendLine($"=> /downvote-post?{post.PostId} Downvote post " + (post.UserHasDownvoted(thumbprint) ? "[x]" : ""));
        }

        sb.AppendLine($"=> {post.Link} Follow link");

        return new SuccessResponse(sb.ToString());
    }
}