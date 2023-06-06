namespace gemini_server.Responses;

public sealed class ProxyRequestRefusedResponse : PermanentFailureResponse
{
    public override int GetStatusCode() => 53;

    public override string GetMeta() => "Proxy Request Refused.";
}