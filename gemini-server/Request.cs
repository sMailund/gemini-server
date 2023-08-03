namespace gemini_server;

public record Request
{
    public Uri Uri { get; init; }
    public bool IsLoggedIn { get; init; } = false;
    public string? UserName { get; init; }
    public string? UserThumbprint { get; set; }
}