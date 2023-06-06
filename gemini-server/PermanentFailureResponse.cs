namespace gemini_server;

public class PermanentFailureResponse : IResponse
{
    public string Reason { get; init; } = "unknown failure";
    
    public virtual int GetStatusCode() => 50;

    public virtual string GetMeta() => Reason;
}