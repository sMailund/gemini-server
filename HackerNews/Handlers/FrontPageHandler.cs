using gemini_server;
using gemini_server.Responses;

namespace HackerNews.Handlers;

public class FrontPageHandler
{
    public IResponse Handle(Request req)
    {
        return new SuccessResponse($"handler works!!! And your name is {req.UserName}");
    }
}