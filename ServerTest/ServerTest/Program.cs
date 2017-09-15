using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running ... ");
            IPAddress ip = new IPAddress(new byte[] { 192, 168, 1, 91 });
            TcpListener listener = new TcpListener(ip, 8500);
            listener.Start();
            Console.WriteLine("Start Listening ... ");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                //Server wapper = new Server(client);
                Server wapper = new Server(client);
                wapper.BeginRead();
            }
            //const int BufferSize = 8192;
            //Console.WriteLine("Server is running ... ");
            //IPAddress ip = new IPAddress(new byte[] { 172, 16, 230, 130 });
            //TcpListener listener = new TcpListener(ip, 8500);
            //listener.Start();//开始侦听
            //Console.WriteLine("Start Listening ... ");
            ////获取一个连接，中断方法
            //TcpClient remoteClient = listener.AcceptTcpClient();
            ////打印连接到的客户端信息
            //Console.WriteLine($"Client Connected!Local:{remoteClient.Client.LocalEndPoint}" +
            //    $" <-- Client:{remoteClient.Client.RemoteEndPoint}");
            ////获得流，并写入buffer中
            //NetworkStream streamToClient = remoteClient.GetStream();
            //do
            //{
            //    try
            //    {
            //        byte[] buffer = new byte[BufferSize];
            //        int bytesRead = streamToClient.Read(buffer, 0, BufferSize);
            //        if (bytesRead == 0)
            //        {
            //            Console.WriteLine("Client offline"); break;
            //        }
            //        //获得请求的字符串
            //        string msg = Encoding.Unicode.GetString(buffer, 0, BufferSize);
            //        Console.WriteLine($"Received:{msg} [{bytesRead} bytes]");
            //        //转换成大写并发送
            //        msg = msg.ToUpper();
            //        buffer = Encoding.Unicode.GetBytes(msg);
            //        streamToClient.Write(buffer, 0, buffer.Length);
            //        Console.WriteLine($"Sent: {msg}");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message); break;
            //    }
            //}
            //while (true);
            //streamToClient.Dispose();
            //remoteClient.Close();
            ////byte[] buffer = new byte[BufferSize];
            ////int bytesRead = streamToClient.Read(buffer, 0, BufferSize);
            //////获得请求的字符串
            ////string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
            ////Console.WriteLine($"Received: {msg} [{bytesRead}bytes]");
            ////按Q退出
            //Console.WriteLine("\n\n输入\"Q\"键退出。");
            //ConsoleKey key;
            //do
            //{
            //    key = Console.ReadKey(true).Key;
            //}
            //while (key != ConsoleKey.Q);
        }
    }
}
