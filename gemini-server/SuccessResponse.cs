namespace gemini_server;

public class SuccessResponse : Response
{
   public SuccessResponse(string body)
   {
      Body = body;
   }
   
   public string Body { get; init; }
}