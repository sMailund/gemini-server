namespace gemini_server.Responses;

public sealed class BadRequestResponse : PermanentFailureResponse
{
    public override int GetStatusCode() => 59;

    public override string GetMeta() => "Bad Request.";
}