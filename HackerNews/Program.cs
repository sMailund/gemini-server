// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Security.Cryptography.X509Certificates;
using gemini_server;
using gemini_server.Responses;
using HackerNews;
using HackerNews.Handlers;


var certPath = "cert/mycert.pfx";
var serverCertificate = new X509Certificate2(certPath, "asdf");

var port = 1965;

var ipAddress = IPAddress.Parse("127.0.0.1");


var posts = new List<Post>()
{
    new Post("This is a test post"),
    new Post("This is another post"),
};

var requestHandler = new RequestHandler();
requestHandler.RegisterHandler("/", req => new FrontPageHandler().Handle(req));

var server = new Server(serverCertificate, port, ipAddress, requestHandler);

server.Start();