using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest
{
    public class Server
    {
        private TcpClient client;
        private NetworkStream streamToclient;
        private const int BufferSize = 8192;
        private byte[] buffer;
        //private RequestHandler handler;
        public Server(TcpClient client)
        {
            this.client = client;
            //打印连接到的客户端信息
            Console.WriteLine($"\nClient Connected! Local:{client.Client.LocalEndPoint}" +
                $" <-- Client:{client.Client.RemoteEndPoint}");
            //获得流
            streamToclient = client.GetStream();
            buffer = new byte[BufferSize];
            //设置RequestHandler
            handler = new RequestHandler();
            //在构造函数中就开始准备读取
            AsyncCallback callback = new AsyncCallback(ReadComplete);
            streamToclient.BeginRead(buffer, 0, BufferSize, callback, null);
        }
        //在读取完成时回调
        private void ReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                bytesRead = streamToclient.EndRead(ar);
                if (bytesRead == 0)
                {
                    Console.WriteLine("Client offline");return;
                }
                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Array.Clear(buffer, 0, buffer.Length);//清空缓存，避免脏读
                string[] msgArray = handler.GetActualString(msg);
                //遍历得到的字符串
                foreach (string m in msgArray)
                {
                    Console.WriteLine($"Received: {m} [{bytesRead} bytes]");
                    string back = m.ToUpper();
                    //将得到的字符串改为大写并重新发送
                    byte[] temp = Encoding.Unicode.GetBytes(back);
                    streamToclient.Write(temp, 0, temp.Length);
                    streamToclient.Flush();
                    Console.WriteLine($"Sent: {back}");
                }
                //再次调用BeginRead(),完成时调用自身，形成无限循环
                AsyncCallback callback = new AsyncCallback(ReadComplete);
                streamToclient.BeginRead(buffer, 0, BufferSize, callback, null);
            }
            catch (Exception ex)
            {
                if (streamToclient != null)
                {
                    streamToclient.Dispose();
                }
                client.Close();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
