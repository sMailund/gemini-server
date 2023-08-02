// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Security.Cryptography.X509Certificates;
using gemini_server;
using gemini_server.Responses;

var requestHandler = new RequestHandler();
requestHandler.RegisterHandler("/test", req => new SuccessResponse($"handler works!!! And your name is {req.UserName}"));
requestHandler.RegisterHandler("/input", req => new InputResponse("test input"));
requestHandler.RegisterHandler("/test-redirect", req => new RedirectResponse("/redirect-works"));
requestHandler.RegisterHandler("/redirect-works", request => new SuccessResponse("redirect works"));

var certPath = "cert/mycert.pfx";
var serverCertificate = new X509Certificate2(certPath, "asdf");

var port = 1965;

var ipAddress = IPAddress.Parse("127.0.0.1");

var server = new Server(serverCertificate, port, ipAddress, requestHandler);

server.Start();
