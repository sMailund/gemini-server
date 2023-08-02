using System.Text;
using gemini_server;
using gemini_server.Responses;

namespace HackerNews.Handlers;

public class FrontPageHandler
{
    public IResponse Handle(Request req)
    {
        var posts = new List<Post>
        {
            new("This is a test post"),
            new("This is another post"),
        };

        var greeting = req.IsLoggedIn
            ? $"Welcome back, {req.UserName}!"
            : "Welcome to Gemtalk! Please log in to join the discussion.";

        var formattedPosts = posts
            .Select(it => $"## {it.Title}");

        var sb = new StringBuilder()
            .AppendLine("# gemtalk")
            .AppendLine(greeting)
            .AppendLine()
            .AppendLine();

        foreach (var post in formattedPosts)
        {
            sb.AppendLine(post);
            sb.AppendLine();
        }

        return new SuccessResponse(sb.ToString());
    }
}