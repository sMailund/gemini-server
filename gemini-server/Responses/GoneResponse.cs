namespace gemini_server.Responses;

public sealed class GoneResponse : PermanentFailureResponse
{
    public override int GetStatusCode() => 52;

    public override string GetMeta() => "Gone.";
}