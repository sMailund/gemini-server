namespace gemini_server.Responses;

public sealed class NotFoundResponse : PermanentFailureResponse
{
    public override int GetStatusCode() => 51;

    public override string GetMeta() => "Not found";
}