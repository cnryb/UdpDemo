using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client");
            int port = 443;
            string ip = "52.175.49.29";
            //ip = "127.0.0.1";
            var remote = new IPEndPoint(IPAddress.Parse(ip), port);

            UdpClient udpClient = new UdpClient();
            //udpClient.Connect(remote);

            byte[] bs = Encoding.ASCII.GetBytes("hi");
            udpClient.Send(bs, bs.Length, remote);


            ThreadPool.QueueUserWorkItem(async q =>
            {
                while (true)
                {
                    UdpReceiveResult res = await udpClient.ReceiveAsync();
                    var bbs = res.Buffer;
                    //var bs = udpClient.Receive(ref remote);
                    var msg = Encoding.ASCII.GetString(bbs);
                    Console.WriteLine(msg);
                }
            });
            //while (true)
            //{
            //    var msg = Console.ReadLine();
            //    bs = Encoding.ASCII.GetBytes(msg);
            //    udpClient.Send(bs, bs.Length, remote);
            //    //await udpClient.SendAsync(bs, bs.Length);
            //}

            for (int i = 0; i < 10000; i++)
            {
                //var msg = Console.ReadLine();
                bs = Encoding.ASCII.GetBytes(i.ToString());
                udpClient.Send(bs, bs.Length, remote);
                Thread.Sleep(50);
            }
        }
    }
}
