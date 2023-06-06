namespace gemini_server.Responses;

public interface IResponse
{
   public int GetStatusCode();
   public string GetMeta();
}