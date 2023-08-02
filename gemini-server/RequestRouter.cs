using gemini_server.Responses;

namespace gemini_server;

public class RequestRouter
{
    private Dictionary<string, Func<Request, IResponse>> _handlers = new();
    
    public void RegisterHandler(string path, Func<Request, IResponse> handler)
    {
        _handlers[path] = handler;
    }

    public IResponse HandleRequest(Request request)
    {
        var handler = _handlers.GetValueOrDefault(request.Uri.LocalPath);

        if (handler == null)
        {
            return new NotFoundResponse();
        }
        
        return handler(request);
    }
}