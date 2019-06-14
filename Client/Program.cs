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
            string ip = "127.0.0.1";
            var remote = new IPEndPoint(IPAddress.Parse(ip), port);

            UdpClient udpClient = new UdpClient();
            //udpClient.Connect(remote);
            while (true)
            {
                var msg = Console.ReadLine();
                byte[] bs = Encoding.ASCII.GetBytes(msg);
                udpClient.Send(bs, bs.Length, remote);
                //await udpClient.SendAsync(bs, bs.Length);
                UdpReceiveResult res = await udpClient.ReceiveAsync();
                msg = Encoding.ASCII.GetString(res.Buffer);
                Console.WriteLine(msg);
            }

        }
    }
}
