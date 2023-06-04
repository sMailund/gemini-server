namespace gemini_server;

public class RequestHandler
{
    private Dictionary<string, Func<Request, Response>> _handlers = new();
    
    public void RegisterHandler(string path, Func<Request, Response> handler)
    {
        _handlers[path] = handler;
    }

    public Response HandleRequest(Request request)
    {
        var handler = _handlers.GetValueOrDefault(request.Uri.LocalPath);

        if (handler == null)
        {
            return new SuccessResponse("not found"); // todo fix not found
        }
        
        return handler(request);
    }
}