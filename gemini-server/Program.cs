using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;

class MyTcpListener
{
    public static void Main()
    {
        // TODO https://serverfault.com/questions/611120/failed-tls-handshake-does-not-contain-any-ip-sans
        var certPath = "cert/mycert.pfx";

        X509Certificate2 serverCertificate = new X509Certificate2(certPath,"asdf");


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

            // Buffer for reading data
            Byte[] bytes = new Byte[256];
            String data = null;

            // Enter the listening loop.
            while (true)
            {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    using TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    SslStream sslStream = new SslStream(
                        client.GetStream(), false);
                    try
                    {
                        sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired: false,
                            checkCertificateRevocation: true);
                        
                        Console.WriteLine("authenticated");

                        byte[] message = Encoding.UTF8.GetBytes("Hello world!<EOF>");

                        sslStream.Write(message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("error:");
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        sslStream.Close();
                    }
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