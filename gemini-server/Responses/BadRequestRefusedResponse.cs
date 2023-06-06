namespace gemini_server.Responses;

public sealed class BadRequestRefusedResponse : PermanentFailureResponse
{
    public override int GetStatusCode() => 59;

    public override string GetMeta() => "Bad Request Refused.";
}