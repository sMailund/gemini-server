namespace gemini_server.Responses;

public class InputResponse : IResponse
{
   public InputResponse(string prompt)
   {
      Prompt = prompt;
   }
   
   public string Prompt { get; init; }
   
   public virtual int GetStatusCode() => 10;

   public string GetMeta() => Prompt;
}