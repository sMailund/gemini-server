using System.Security.Cryptography.X509Certificates;

namespace HackerNews;

internal record Post(string Title, string Link, string PostedByUser, Guid PostId, string PostedByUserId, DateTime PostedAt, int Points);