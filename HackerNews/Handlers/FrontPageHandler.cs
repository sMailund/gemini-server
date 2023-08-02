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
            .GetTopPosts();

        foreach (var post in posts)
        {
            sb.AppendLine($"### {post.Title}");
            sb.AppendLine($"Posted by {post.PostedByUser}.");
            sb.AppendLine($"=> {post.Link} Follow link");
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