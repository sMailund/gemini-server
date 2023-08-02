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

        var requestHandler = new RequestRouter();
        requestHandler.RegisterHandler("/test", req => new SuccessResponse("handler works"));
        requestHandler.RegisterHandler("/input", req => new InputResponse("test input"));
        requestHandler.RegisterHandler("/test-redirect", req => new RedirectResponse("/redirect-works"));
        requestHandler.RegisterHandler("/redirect-works", request => new SuccessResponse("redirect works"));
        
        var certPath = "cert/mycert.pfx";

        Start(certPath, requestHandler);
    }

    private static void Start(string certPath, RequestRouter requestRouter)
    {
        X509Certificate2 serverCertificate = new X509Certificate2(certPath, "asdf");

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


            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                var client = server.AcceptTcpClient();
                new Thread(() => HandleRequest(client, serverCertificate, requestRouter))
                    .Start();
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

    private static void HandleRequest(TcpClient client, X509Certificate2 serverCertificate,
        RequestRouter requestRouter)
    {
        var readBuffer = new byte[1024];
        Console.WriteLine("Connected!");
        var stream = client.GetStream();
        SslStream sslStream = new SslStream(
            stream, false, ValidateCertificate);
        try
        {
            sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired: true,
                checkCertificateRevocation: true);


            sslStream.Read(readBuffer, 0, 1024);
            var uriString = Encoding.UTF8.GetString(readBuffer, 0, 1024)
                .Split("\r\n")[0];

            Console.WriteLine(uriString);

            var uri = new Uri(uriString);

            Console.WriteLine("authenticated");
            Console.WriteLine("path: " + uri.LocalPath);
            Console.WriteLine("query: " + uri.Query);


            // Check if the handshake was successful and the client certificate is available
            if (sslStream is { IsAuthenticated: true, RemoteCertificate: not null })
            {
                Console.WriteLine("reading client certificate...");
                var clientCertificate = new X509Certificate2(sslStream.RemoteCertificate);
                Console.WriteLine("Client certificate: " + clientCertificate.Subject);
            }

            var request = new Request()
            {
                Uri = uri
            };

            var response = requestRouter.HandleRequest(request);

            var body = "";
            if (response is SuccessResponse)
            {
                body = ((SuccessResponse)response).Body;
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
    }

    private static bool ValidateCertificate(object sender, X509Certificate? certificate, X509Chain? chain,
        SslPolicyErrors sslPolicyErrors)
    {
        // If you want to trust any certificate, return true
        // This essentially disables certificate validation.
        return true;

        // If you want to trust only specific self-signed certificates,
        // you can add additional checks here, such as checking the
        // certificate's thumbprint or Common Name (CN).
        // Example:
        // return certificate.GetCertHashString() == "YOUR_CERT_THUMBPRINT";
    }
}