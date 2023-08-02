namespace gemini_server.Responses;

public class TemporaryFailureResponse : IResponse
{
    public virtual string Reason { get; init; } = "temporary failure";
    
    public virtual int GetStatusCode() => 40;

    public virtual string GetMeta() => Reason;
}