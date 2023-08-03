using gemini_server;
using gemini_server.Responses;
using HackerNews.Repositories;

namespace HackerNews.Guards;

internal static class AssertPostExists
{
    public static (IResponse?, Post) GetOrFail(IPostRepository repo, Request req)
    {
        var query = req
            .Uri
            .Query[1..]; // first character is always '?', skip this

        var selectedPostId = Guid.Parse(query);

        if (query.Equals(""))
        {
            var error = new BadRequestResponse()
            {
                Reason = "Post id missing"
            };

            return (error, new Post("", "", "", Guid.NewGuid(), "", DateTime.Now));
        }

        var post = repo.GetPostById(selectedPostId);
        if (post is null)
        {
            var error = new NotFoundResponse();
            return (error, new Post("", "", "", Guid.NewGuid(), "", DateTime.Now));
        }

        return (null, post);
    }
}