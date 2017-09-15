using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientTest
{
    class Client
    {
        private const int BufferSize = 8192;
        private byte[] buffer;
        private TcpClient client;
        private NetworkStream streamToServer;
        public Client()
        {
            try
            {
                client = new TcpClient();
                client.Connect(new IPAddress(new byte[] { 192, 168, 1, 91 }), 8500);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            buffer = new byte[BufferSize];
            //打印连接到的服务端信息
            Console.WriteLine($"Server Connected!Local:{client.Client.LocalEndPoint} --> " +
                $"Server:{client.Client.RemoteEndPoint}");
            streamToServer = client.GetStream();
        }
        //连续发送三条消息到服务端
        public void SendMessage(string msg)
        {
            byte[] temp = Encoding.Unicode.GetBytes(msg);
            try
            {
                streamToServer.Write(temp, 0, temp.Length);
                Console.WriteLine($"Sent: {msg}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //msg = $"[length={msg.Length}]{msg}";
            //for (int i = 0; i <= 2; i++)
            //{
            //    byte[] temp = Encoding.Unicode.GetBytes(msg);
            //    try
            //    {
            //        streamToServer.Write(temp, 0, temp.Length);
            //        Console.WriteLine($"Sent:{msg}");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        break;
            //    }
            //    AsyncCallback callback = new AsyncCallback(ReadComplete);
            //    streamToServer.BeginRead(buffer, 0, BufferSize, callback, null);
            //}
        }
        //读取完成时的回调方法
        private void ReadComplete(IAsyncResult ar)
        {
            int bytesRead;
            try
            {
                bytesRead = streamToServer.EndRead(ar);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Server offline"); return;
                }
                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received:{msg} [{bytesRead} bytes]");
                Array.Clear(buffer, 0, buffer.Length);
                AsyncCallback callback = new AsyncCallback(ReadComplete);
                streamToServer.BeginRead(buffer, 0, BufferSize, callback, null);
            }
            catch (Exception ex)
            {
                if (streamToServer != null)
                {
                    streamToServer.Dispose();
                }
                client.Close();
                Console.WriteLine(ex.Message);
            }
        }
        //发送文件 - 异步方法
        public void BeginSendFile(string filePath)
        {
            ParameterizedThreadStart start = new ParameterizedThreadStart(BeginSendFile);
            start.BeginInvoke(filePath, null, null);
        }
        private void BeginSendFile(object obj)
        {
            string filePath = obj as string;
            SendFile(filePath);
        }
        //发送文件
        public void SendFile(string filePath)
        {
            IPAddress ip = IPAddress.Parse("192.168.1.91");
            TcpListener listener = new TcpListener(ip, 0);
            listener.Start();
            //获取本地侦听的端口号
            IPEndPoint endPoint = listener.LocalEndpoint as IPEndPoint;
            int listeningPort = endPoint.Port;
            //获取发送的协议字符串
            string fileName = Path.GetFileName(filePath);
            FileProtocol protocol = new FileProtocol
                (FileRequestMode.Send, listeningPort, fileName);
            string pro = protocol.ToString();
            SendMessage(pro);//发送协议到服务器
            //中断，等待远程连接
            TcpClient localClient = listener.AcceptTcpClient();
            Console.WriteLine("Start sending file...");
            NetworkStream stream = localClient.GetStream();
            //创建文件流
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            byte[] fileBuffer = new byte[1024];//每次传1kb
            int bytesRead;
            int totalBytes = 0;
            //创建获取文件发送状态的类
            SendStatus status = new SendStatus(filePath);
            //将文件流转写入网络流
            try
            {
                do
                {
                    Thread.Sleep(10);//模拟远程传输的视觉效果，暂停10毫秒
                    bytesRead = fs.Read(fileBuffer, 0, fileBuffer.Length);
                    stream.Write(fileBuffer, 0, bytesRead);
                    totalBytes += bytesRead;
                    status.PrintStatus(totalBytes);
                }
                while (bytesRead > 0);
                Console.WriteLine($"Total {totalBytes} bytes sent, Done!");
            }
            catch
            {
                Console.WriteLine("Server has lost...");
            }
            finally
            {
                stream.Dispose();
                fs.Dispose();
                localClient.Close();
                listener.Stop();
            }
        }
    }
    public class SendStatus
    {
        private FileInfo info;
        private long fileBytes;
        public SendStatus(string filePath)
        {
            info = new FileInfo(filePath);
            fileBytes = info.Length;
        }
        public void PrintStatus(int sent)
        {
            string percent = GetPercent(sent);
            Console.WriteLine($"Sending {sent} bytes, {percent}%...");
        }
        //获得发送文件的百分比
        public string GetPercent(int sent)
        {
            decimal allBytes = Convert.ToDecimal(fileBytes);
            decimal currentSent = Convert.ToDecimal(sent);
            decimal percent = (currentSent / allBytes) * 100;
            percent = Math.Round(percent, 1);//保留一位小数
            if (percent.ToString() == "100.0")
            {
                return "100";
            }
            else
            {
                return percent.ToString();
            }
        }
    }
    public enum FileRequestMode
    {
        Send = 0,
        Receive
    }
    public struct FileProtocol
    {
        private readonly FileRequestMode mode;
        private readonly int port;
        private readonly string fileName;
        public FileProtocol(FileRequestMode mode, int port, string fileName)
        {
            this.mode = mode;
            this.port = port;
            this.fileName = fileName;
        }
        public FileRequestMode Mode { get { return mode; } }
        public int Port { get { return port; } }
        public string FileName { get { return fileName; } }
        public override string ToString()
        {
            return $"<protocol><file name=\"{fileName}\" " +
                $"mode=\"{mode}\" port=\"{port}\" /></protocol>";
        }
    }
}
