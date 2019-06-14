using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Server");
            int port = 443;

            UdpClient udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, port));
            while (true)
            {
                UdpReceiveResult res = await udpClient.ReceiveAsync();
                var msg = Encoding.ASCII.GetString(res.Buffer);
                Console.WriteLine($"from {res.RemoteEndPoint} message {msg}");
                await udpClient.SendAsync(res.Buffer, res.Buffer.Length, res.RemoteEndPoint);
            }

        }
    }
}
