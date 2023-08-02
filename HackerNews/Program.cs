// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Security.Cryptography.X509Certificates;
using gemini_server;
using HackerNews;
using HackerNews.Handlers;
using HackerNews.Repositories;
using Microsoft.Extensions.DependencyInjection;

var certPath = "cert/mycert.pfx";
var serverCertificate = new X509Certificate2(certPath, "asdf");

var port = 1965;

var ipAddress = IPAddress.Parse("127.0.0.1");

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IPostRepository, InMemoryPostRepository>();
serviceCollection.AddScoped<FrontPageHandler>();

var serviceProvider = serviceCollection.BuildServiceProvider();


// initialize repository with dummy data
var postRepository = serviceProvider.GetRequiredService<IPostRepository>();
postRepository.AddNewPost(new Post("gemini project main page", "gemini://gemini.circumlunar.space", "mailund"));
postRepository.AddNewPost(new Post("amfora wiki", "gemini://makeworld.space/amfora-wiki", "mailund"));


var requestHandler = new RequestRouter();
requestHandler.RegisterHandler("/", req => serviceProvider.GetRequiredService<FrontPageHandler>().Handle(req));

var server = new Server(serverCertificate, port, ipAddress, requestHandler);

server.Start();