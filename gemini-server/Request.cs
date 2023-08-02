namespace gemini_server;

public class Request
{
    public Uri Uri { get; init; }
    public bool IsLoggedIn { get; init; } = false;
    public string? UserName { get; init; }
}