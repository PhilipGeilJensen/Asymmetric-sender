using System;
using System.Net.Sockets;
using System.Text;

namespace Asymmetric_sender
{
    class Program
    {
        static void Main(string[] args)
        {
            RsaWithXmlKey rsa = new RsaWithXmlKey();

            const string publicKeyPath = "c:\\temp\\publickey.xml";

            while (true)
            {
                Console.WriteLine("Enter a message to send");
                string msg = Console.ReadLine();

                string data = Convert.ToBase64String(rsa.EncryptData(publicKeyPath, Encoding.UTF8.GetBytes(msg)));

                Connect("127.0.0.1", data);
            }
        }

        static void Connect(string server, string message)
        {
            try
            {
                // Create a TcpClient.
                int port = 13000;
                TcpClient client = new TcpClient(server, port);

                byte[] data = Convert.FromBase64String(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer.
                stream.Write(data, 0, data.Length);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
