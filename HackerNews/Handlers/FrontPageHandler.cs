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
        var greeting = req.IsLoggedIn
            ? $"Welcome back, {req.UserName}!"
            : "Welcome to Gemtalk! Please log in to join the discussion.";

        var sb = new StringBuilder()
            .AppendLine("# gemtalk")
            .AppendLine(greeting)
            .AppendLine()
            .AppendLine();

        var posts = _posts
            .GetTopPosts();

        foreach (var post in posts)
        {
            sb.AppendLine($"## {post.Title}");
            sb.AppendLine($"=> {post.Link} Follow link");
            sb.AppendLine();
        }

        return new SuccessResponse(sb.ToString());
    }
}