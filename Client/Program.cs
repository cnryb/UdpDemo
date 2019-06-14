using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client");
            int port = 443;
            UdpClient udpClient = new UdpClient();
            udpClient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            while (true)
            {
                var msg = Console.ReadLine();
                byte[] bs = Encoding.ASCII.GetBytes(msg);
                await udpClient.SendAsync(bs, bs.Length);
                UdpReceiveResult res = await udpClient.ReceiveAsync();
                msg = Encoding.ASCII.GetString(res.Buffer);
                Console.WriteLine(msg);
            }

        }
    }
}
