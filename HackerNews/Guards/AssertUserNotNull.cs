using gemini_server;
using gemini_server.Responses;

namespace HackerNews.Guards;

internal static class AssertUserNotNull
{

    public static IResponse? Assert(Request req)
    {
        if (!req.IsLoggedIn)
        {
            return new BadRequestResponse
            {
                Reason = "You need to be a logged in user"
            };
        }
        
        if (req.UserName is null || req.UserThumbprint is null)
        {
            return new BadRequestResponse
            {
                Reason = "Certificate subject name or certificate thumbprint can not be null"
            };
        }

        return null;
    }
    
}