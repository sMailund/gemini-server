namespace gemini_server.Responses;

public class SuccessResponse : IResponse
{
   public SuccessResponse(string body)
   {
      Body = body;
   }
   
   public string Body { get; init; }
   
   public int GetStatusCode() => 20;

   public string GetMeta() => "text/gemini; charset=utf-8";
}