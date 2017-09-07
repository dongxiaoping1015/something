using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
                client.Connect(new IPAddress(new byte[] { 172, 16, 230, 130 }), 8500);
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
            msg = $"[length={msg.Length}]{msg}";
            for (int i = 0; i <= 2; i++)
            {
                byte[] temp = Encoding.Unicode.GetBytes(msg);
                try
                {
                    streamToServer.Write(temp, 0, temp.Length);
                    Console.WriteLine($"Sent:{msg}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                AsyncCallback callback = new AsyncCallback(ReadComplete);
                streamToServer.BeginRead(buffer, 0, BufferSize, callback, null);
            }
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
        
    }
}
