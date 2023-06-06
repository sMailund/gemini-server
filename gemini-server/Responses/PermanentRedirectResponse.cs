namespace gemini_server.Responses;

public class PermanentRedirectResponse : RedirectResponse
{
    public PermanentRedirectResponse(string path) : base(path)
    {
    }

    public override int GetStatusCode() => 31;
}