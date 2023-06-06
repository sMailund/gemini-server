namespace gemini_server.Responses;

public class SensitiveInputResponse : InputResponse
{
    public SensitiveInputResponse(string prompt) : base(prompt)
    {
    }

    public override int GetStatusCode() => 11;
}