using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using gemini_server;
using gemini_server.Responses;

class MyTcpListener
{
    
    public static void Main()
    {
        // TODO find solution to EOF problem
        
        var certPath = "cert/mycert.pfx";

        X509Certificate2 serverCertificate = new X509Certificate2(certPath, "asdf");
        int reloads = 0;

        TcpListener server = null;
        try
        {
            // Set the TcpListener on port 13000.
            Int32 port = 1965;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            server = new TcpListener(localAddr, port);

            // Start listening for client requests.
            server.Start();

            var requestHandler = new RequestHandler();

            requestHandler.RegisterHandler("/test", req => new SuccessResponse("handler works"));
            requestHandler.RegisterHandler("/input", req => new InputResponse("test input"));

            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                new Thread((() =>
                {
                    var readBuffer = new byte[1024];
                    reloads++;
                    Console.WriteLine("Connected!");
                    var stream = client.GetStream();
                    SslStream sslStream = new SslStream(
                        stream, false);
                    try
                    {
                        sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired: false,
                            checkCertificateRevocation: true);
                        
                        sslStream.Read(readBuffer, 0, 1024); 
                        var uriString = Encoding.UTF8.GetString(readBuffer, 0, 1024)
                            .Split("\r\n")[0];
                        
                        Console.WriteLine(uriString);

                        var uri = new Uri(uriString);
                        
                        Console.WriteLine("authenticated");
                        Console.WriteLine("path: " + uri.LocalPath);
                        Console.WriteLine("query: " + uri.Query);

                        var request = new Request()
                        {
                            Uri = uri
                        };

                        var response = requestHandler.HandleRequest(request);

                        var body = "";
                        if (response is SuccessResponse)
                        {
                            body = ((SuccessResponse) response).Body;
                        } 
                        
                        byte[] header = Encoding.UTF8.GetBytes($"{response.GetStatusCode()} {response.GetMeta()}\r\n");
                        
                        byte[] message = Encoding.UTF8.GetBytes(body);
                        
                        var payload = header.Concat(message).ToArray();
                        sslStream.Write(payload);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error:");
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        Console.WriteLine("closing stream");
                        sslStream.Close();
                        sslStream.Flush();
                    }
                    
                    Console.WriteLine("closing client");
                    stream.Close();
                    stream.Dispose();
                    client.Close();
                    client.Dispose();
                })).Start();
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            server.Stop();
        }

        Console.WriteLine("\nHit enter to continue...");
        Console.Read();
    }

}