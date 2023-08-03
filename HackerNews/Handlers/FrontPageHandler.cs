using System.Text;
using gemini_server;
using gemini_server.Responses;
using HackerNews.Repositories;

namespace HackerNews.Handlers;

internal class FrontPageHandler
{
    private readonly IPostRepository _posts;

    public FrontPageHandler(IPostRepository posts)
    {
        _posts = posts;
    }

    public IResponse Handle(Request req)
    {
        var sb = new StringBuilder()
            .AppendLine("# gemtalk");

        AppendGreeting(sb, req)
            .AppendLine()
            .AppendLine();


        sb.AppendLine("## posts");

        var posts = _posts
            .GetTopPosts(10);

        foreach (var post in posts)
        {
            sb.AppendLine($"### {post.Title}");
            sb.AppendLine($"Posted by {post.PostedByUser} at {post.PostedAt.ToString()}.");
            sb.AppendLine($"{post.Points} point(s).");
            sb.AppendLine($"=> {post.Link} Follow link");
            sb.AppendLine($"=> /view-post?{post.PostId} View ({post.Comments.Count}) comment(s)");
            
            if (req.IsLoggedIn)
            {
                var thumbprint = req.UserThumbprint;
                sb.AppendLine($"=> /upvote-post?{post.PostId} Upvote post " + (post.UserHasUpvoted(thumbprint) ? "[x]" : ""));
                sb.AppendLine($"=> /downvote-post?{post.PostId} Downvote post " + (post.UserHasDownvoted(thumbprint) ? "[x]" : ""));
            }

            sb.AppendLine();
        }

        return new SuccessResponse(sb.ToString());
    }

    private static StringBuilder AppendGreeting(StringBuilder sb, Request req)
    {
        if (!req.IsLoggedIn)
        {
            return sb.AppendLine("Welcome to Gemtalk! Please log in to join the discussion.");
        }

        sb.AppendLine($"Welcome back, {req.UserName}!")
            .AppendLine("=> /create-post Create a new post");

        return sb;
    }
}