using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        private ProtocolHandler handler;
        public Server(TcpClient client)
        {
            this.client = client;
            //打印连接到的客户端信息
            Console.WriteLine($"\nClient Connected! Local:{client.Client.LocalEndPoint}" +
                $" <-- Client:{client.Client.RemoteEndPoint}");
            //获得流
            streamToclient = client.GetStream();
            buffer = new byte[BufferSize];
            ////设置RequestHandler
            //handler = new RequestHandler();
            handler = new ProtocolHandler();
            //在构造函数中就开始准备读取
            //AsyncCallback callback = new AsyncCallback(ReadComplete);
            //streamToclient.BeginRead(buffer, 0, BufferSize, callback, null);
        }
        //开始进行读取
        public void BeginRead()
        {
            AsyncCallback callBack = new AsyncCallback(OnReadComplete);
            streamToclient.BeginRead(buffer, 0, BufferSize, callBack, null);
        }
        //在读取完成时进行回调
        private void OnReadComplete(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                bytesRead = streamToclient.EndRead(ar);
                Console.WriteLine($"Reading data, {bytesRead} bytes ...");
                if (bytesRead == 0)
                {
                    Console.WriteLine("Client offline");
                    return;
                }
                string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
                Array.Clear(buffer, 0, buffer.Length);//清空缓存，避免脏读
                //获取protocol数组
                string[] protocolArray = handler.GetProtocol(msg);
                foreach (string pro in protocolArray)
                {
                    //这里异步调用，不然可能会比较耗时
                    ParameterizedThreadStart start =
                        new ParameterizedThreadStart(handleProtocol);
                    start.BeginInvoke(pro, null, null);
                }
                //再次调用BeginRead()，完成调用自身，形成无限循环
                AsyncCallback callBack = new AsyncCallback(OnReadComplete);
                streamToclient.BeginRead(buffer, 0, BufferSize, callBack, null);
            }
            catch (Exception ex)
            {
                if (streamToclient != null)
                {
                    streamToclient.Dispose();
                }
                client.Close();
                Console.WriteLine(ex.Message);//捕获异常时退出程序
            }
        }
        //处理protocol
        private void handleProtocol(object obj)
        {
            string pro = obj as string;
            ProtocolHelper helper = new ProtocolHelper(pro);
            FileProtocol protocol = helper.GetProtocol();
            if (protocol.Mode == FileRequestMode.Send)
            {
                //客户端发送文件，对服务端来说则是接收文件
                receiveFile(protocol);
            }
            else if (protocol.Mode == FileRequestMode.Receive)
            {
                //客户端接收文件，对服务端来说则是发送文件
                //sendFile(protocol);
            }
        }
        //接收文件
        private void receiveFile(FileProtocol protocol)
        {
            //获取远程客户端的位置
            IPEndPoint endpoint = client.Client.RemoteEndPoint as IPEndPoint;
            IPAddress ip = endpoint.Address;
            //使用新端口号，获得远程用于接收文件的端口
            endpoint = new IPEndPoint(ip, protocol.Port);
            //连接到远程客户端
            TcpClient localClient;
            try
            {
                localClient = new TcpClient();
                localClient.Connect(endpoint);
            }
            catch
            {
                Console.WriteLine($"无法连接到客户端 --> {endpoint}");
                return;
            }
            //获取发送文件的流
            NetworkStream streamToClient = localClient.GetStream();
            //随机生成一个在当前目录下的文件名称
            string path =
                Environment.CurrentDirectory + "/" + generateFileName(protocol.FileName);
            byte[] fileBuffer = new byte[1024];//每次接收1KB
            FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
            //从缓存Buffer中读入到文件流中
            int bytesRead;
            int totalBytes = 0;
            do
            {
                bytesRead = streamToclient.Read(buffer, 0, BufferSize);
                fs.Write(buffer, 0, bytesRead);
                totalBytes += bytesRead;
                Console.WriteLine($"Receiving {totalBytes} bytes ...");
            }
            while (bytesRead > 0);
            Console.WriteLine($"Total {totalBytes} bytes received, Done!");
            streamToClient.Dispose();
            fs.Dispose();
            localClient.Close();
        }
        //随机获取一个图片名称
        private string generateFileName(string fileName)
        {
            DateTime now = DateTime.Now;
            return $"{now.Minute}_{now.Second}_{now.Millisecond}_{fileName}";
        }
        //在读取完成时回调
        //private void ReadComplete(IAsyncResult ar)
        //{
        //    int bytesRead = 0;
        //    try
        //    {
        //        bytesRead = streamToclient.EndRead(ar);
        //        if (bytesRead == 0)
        //        {
        //            Console.WriteLine("Client offline");return;
        //        }
        //        string msg = Encoding.Unicode.GetString(buffer, 0, bytesRead);
        //        Array.Clear(buffer, 0, buffer.Length);//清空缓存，避免脏读
        //        string[] msgArray = handler.GetActualString(msg);
        //        //遍历得到的字符串
        //        foreach (string m in msgArray)
        //        {
        //            Console.WriteLine($"Received: {m} [{bytesRead} bytes]");
        //            string back = m.ToUpper();
        //            //将得到的字符串改为大写并重新发送
        //            byte[] temp = Encoding.Unicode.GetBytes(back);
        //            streamToclient.Write(temp, 0, temp.Length);
        //            streamToclient.Flush();
        //            Console.WriteLine($"Sent: {back}");
        //        }
        //        //再次调用BeginRead(),完成时调用自身，形成无限循环
        //        AsyncCallback callback = new AsyncCallback(ReadComplete);
        //        streamToclient.BeginRead(buffer, 0, BufferSize, callback, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (streamToclient != null)
        //        {
        //            streamToclient.Dispose();
        //        }
        //        client.Close();
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
    public class RequestHandler
    {
        private string temp = string.Empty;
        public string[] GetActualString(string input)
        {
            return GetActualString(input, null);
        }
        private string[] GetActualString(string input, List<string> outputList)
        {
            if (outputList == null)
            {
                outputList = new List<string>();
            }
            if (!String.IsNullOrEmpty(temp))
            {
                input = temp + input;
            }
            string output = "";
            string pattern = @"(?<=^\[length=)(\d+)(?=\])";
            int length;
            if (Regex.IsMatch(input, pattern))
            {
                Match m = Regex.Match(input, pattern);
                //获取消息字符串实际应有的长度
                length = Convert.ToInt32(m.Groups[0].Value);
                //获取需要进行截取的位置
                int startIndex = input.IndexOf(']') + 1;
                //获取从此位置开始后所有字符的长度
                output = input.Substring(startIndex);
                if (output.Length == length)
                {
                    //如果output的长度与消息字符串的应有长度相等
                    //说明刚好是完整的一条信息
                    outputList.Add(output);
                    temp = "";
                }
                else if (output.Length < length)
                {
                    //如果之后的长度小于应有的长度
                    //说明没有发完整，则应将整条信息，包括元数据，全部缓存
                    //与下一条数据合并起来再进行处理
                    temp = input;
                    //此时程序应该退出，因为需要等待下一条数据到来才能继续处理
                }
                else if (output.Length > length)
                {
                    //如果之后的长度大于应有长度
                    //说明消息发完整了，但是有多余的数据
                    //多余的数据可能是截断消息，也可能是多条完整消息
                    //截取字符串
                    output = output.Substring(0, length);
                    outputList.Add(output);
                    temp = "";
                    //缩短input的长度
                    input = input.Substring(startIndex + length);
                    //递归调用
                    GetActualString(input, outputList);
                }
            }
            else//说明"[","]"就不完整
            {
                temp = input;
            }
            return outputList.ToArray();
        }
    }
}
