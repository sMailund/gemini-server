namespace gemini_server.Responses;

class ServerUnavailableResponse : TemporaryFailureResponse
{
    public override int GetStatusCode() => 41;
    public override string Reason { get; init; } = "server unavailable";
}