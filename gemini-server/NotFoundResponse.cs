namespace gemini_server;

public sealed class NotFoundResponse : PermanentFailureResponse
{
    public override int GetStatusCode() => 51;

    public override string GetMeta() => "Not found";
}