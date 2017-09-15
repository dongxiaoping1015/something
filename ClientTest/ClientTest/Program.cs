using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(BookingStatus);
            FieldInfo[] fieldArray = t.GetFields();
            foreach (FieldInfo field in fieldArray)
            {
                if (!field.IsSpecialName)
                {
                    Console.WriteLine($"Name: {field.Name}, Value: {field.GetRawConstantValue()}");
                }
            }
            Console.ReadLine();
            //ConsoleKey key;
            //Client c = new Client();
            //string filePath = Environment.CurrentDirectory + "/" + "Client01.jpg";
            //if (File.Exists(filePath))
            //{
            //    c.BeginSendFile(filePath);
            //}
            //c.SendMessage("Hello, readers.Thanks for reading!");
            //Console.WriteLine(@"\n\n输入'Q'退出");
            //do
            //{
            //    key = Console.ReadKey(true).Key;
            //}
            //while (key != ConsoleKey.Q);
            //Console.WriteLine("Client is running ... ");
            //TcpClient client;
            //const int BufferSize = 8192;
            //try
            //{
            //    client = new TcpClient();
            //    //与服务器建立连接
            //    client.Connect(IPAddress.Parse("172.16.230.130"), 8500);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    return;
            //}
            ////打印连接到的服务端信息
            //Console.WriteLine($"Server Connected! Local:{client.Client.LocalEndPoint}" +
            //    $" --> Server:{client.Client.RemoteEndPoint}");
            //NetworkStream streamToServer = client.GetStream();
            //string msg;
            //do
            //{
            //    Console.Write("Sent:");
            //    msg = Console.ReadLine();
            //    if (!String.IsNullOrEmpty(msg) && msg != "Q")
            //    {
            //        byte[] buffer = Encoding.Unicode.GetBytes(msg);
            //        try
            //        {
            //            //发往服务器
            //            streamToServer.Write(buffer, 0, buffer.Length);
            //            int bytesRead;
            //            buffer = new byte[BufferSize];
            //            //接收并显示服务器回传的字符串
            //            bytesRead = streamToServer.Read(buffer, 0, buffer.Length);
            //            if (bytesRead == 0)
            //            {
            //                Console.WriteLine("Server offline");
            //            }
            //            msg = Encoding.Unicode.GetString(buffer, 0, buffer.Length);
            //            Console.WriteLine($"received：{msg} [{buffer.Length} bytes]");
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine(ex.Message); break;
            //        }
            //    }
            //}
            //while (msg != "Q");
            //streamToServer.Dispose();
            //client.Close();
            //NetworkStream streamToServer = client.GetStream();
            //byte[] buffer = Encoding.Unicode.GetBytes(msg);//获得缓存
            //streamToServer.Write(buffer, 0, buffer.Length);//发往服务器
            //Console.WriteLine($"Sent:{msg}");
            //按Q退出
            //Console.WriteLine("\n\n输入\"Q\"键退出。");
            //ConsoleKey key;
            //do
            //{
            //    key = Console.ReadKey(true).Key;
            //}
            //while (key != ConsoleKey.Q);
        }
    }
    public enum BookingStatus
    {
        未提交 = 1,
        已提交,
        已取消,
        已订妥 = 6
    }
}
