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
serviceCollection.AddScoped<CreatePostHandler>();
serviceCollection.AddScoped<ViewPostHandler>();
serviceCollection.AddScoped<UpvotePostHandler>();
serviceCollection.AddScoped<DownvotePostHandler>();

var serviceProvider = serviceCollection.BuildServiceProvider();


// initialize repository with dummy data
var postRepository = serviceProvider.GetRequiredService<IPostRepository>();
postRepository.AddNewPost(new Post("gemini project main page", "gemini://gemini.circumlunar.space", "mailund",
    Guid.NewGuid(), Guid.NewGuid().ToString(), DateTime.Now.AddDays(-1)));
postRepository.AddNewPost(new Post("amfora wiki", "gemini://makeworld.space/amfora-wiki", "mailund", Guid.NewGuid(),
    Guid.NewGuid().ToString(), DateTime.Now.AddHours(-4)));


var requestHandler = new RequestRouter();
requestHandler.RegisterHandler("/", req => serviceProvider.GetRequiredService<FrontPageHandler>().Handle(req));
requestHandler.RegisterHandler("/create-post", req => serviceProvider.GetRequiredService<CreatePostHandler>().Handle(req));
requestHandler.RegisterHandler("/view-post", req => serviceProvider.GetRequiredService<ViewPostHandler>().Handle(req));
requestHandler.RegisterHandler("/upvote-post", req => serviceProvider.GetRequiredService<UpvotePostHandler>().Handle(req));
requestHandler.RegisterHandler("/downvote-post", req => serviceProvider.GetRequiredService<DownvotePostHandler>().Handle(req));

var server = new Server(serverCertificate, port, ipAddress, requestHandler);

server.Start();


// TODO: make sure usernames cannot be duplicated
// TODO: make sure submitted content cannot contain malicious payloads