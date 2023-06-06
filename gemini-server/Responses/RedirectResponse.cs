namespace gemini_server.Responses;

public class RedirectResponse : IResponse
{
   public RedirectResponse(string path)
   {
      Path = path;
   }
   
   public string Path { get; init; }
   
   public virtual int GetStatusCode() => 30;

   public string GetMeta() => Path;
}