// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Security.Cryptography.X509Certificates;
using gemini_server;
using HackerNews.Handlers;
using Microsoft.Extensions.DependencyInjection;

var certPath = "cert/mycert.pfx";
var serverCertificate = new X509Certificate2(certPath, "asdf");

var port = 1965;

var ipAddress = IPAddress.Parse("127.0.0.1");

var serviceCollection = new ServiceCollection();
serviceCollection.AddScoped<FrontPageHandler>();

var serviceProvider = serviceCollection.BuildServiceProvider();

var requestHandler = new RequestRouter();
requestHandler.RegisterHandler("/", req => serviceProvider.GetRequiredService<FrontPageHandler>().Handle(req));

var server = new Server(serverCertificate, port, ipAddress, requestHandler);

server.Start();