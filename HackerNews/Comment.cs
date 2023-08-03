namespace HackerNews;

public record Comment(string UserName, string UserId, string Text, DateTime CreatedAt);