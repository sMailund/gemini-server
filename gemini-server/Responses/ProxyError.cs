namespace gemini_server.Responses;

class ProxyError : TemporaryFailureResponse
{
    public override int GetStatusCode() => 43;
    public override string Reason { get; init; } = "proxy error";
}