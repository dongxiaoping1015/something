using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thead
{
    class Program
    {
        static string sharedValue;
        static void Main(string[] args)
        {
            string name = "Main  ";
            Thread.CurrentThread.Name = name;
            ThreadStart ts = new ThreadStart(ThreadEntry);
            Thread worker = new Thread(ts);
            worker.IsBackground = true;
            worker.Name = "Worker";
            worker.Start();
            Console.WriteLine($"{name}: Worker Status - {worker.ThreadState}");
            Thread.Sleep(100);
            sharedValue = "setted";
            Console.WriteLine($"{name}: Worker Status - {worker.ThreadState}");
            worker.Resume();
            Console.WriteLine($"{name}: End");
            worker.Join();

            Console.ReadLine();
        }
        static void ThreadEntry()
        {
            string name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name}: Start");
            Thread.CurrentThread.Suspend();
            Console.WriteLine($"{name}: sharedValue={sharedValue}");
            Console.WriteLine($"{name}: End");
        }
    }
}
