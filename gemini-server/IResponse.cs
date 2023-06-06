namespace gemini_server;

public interface IResponse
{
   public int GetStatusCode();
   public string GetMeta();
}