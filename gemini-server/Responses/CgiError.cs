namespace gemini_server.Responses;

class CgiError : TemporaryFailureResponse
{
    public override int GetStatusCode() => 42;
    public override string Reason { get; init; } = "CGI error";
}