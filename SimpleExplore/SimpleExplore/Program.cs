using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo;
using System.Reflection;
using System.Runtime.Remoting;

namespace SimpleExplore
{
    
    class Program
    {
        public static void AssemblyExplore()
        {
            StringBuilder sb = new StringBuilder();
            Assembly asm = Assembly.Load("Demo");
            sb.AppendLine($"FullName(全名):{asm.FullName}");
            sb.AppendLine($"Location(路径):{asm.Location}");
            Module[] modules = asm.GetModules();
            foreach (Module module in modules)
            {
                sb.AppendLine($"模块:{module}");
                Type[] types = module.GetTypes();
                foreach (Type t in types)
                {
                    sb.AppendLine($"    类型:{t}");
                }
            }
            Console.WriteLine(sb.ToString());
        }
        public static void MemberExplore(Type t)
        {
            StringBuilder sb = new StringBuilder();
            MemberInfo[] memberInfo = t.GetMembers();
            sb.Append($"查看类型{t.Name}的成员信息:");
            foreach (MemberInfo mi in memberInfo)
            {
                sb.Append($"成员: {mi.ToString().PadRight(40)} " +
                    $"类型: {mi.MemberType}\n");
            }
            Console.WriteLine(sb.ToString());
        }
        static void Main(string[] args)
        {
            Type t = typeof(Calculator);
            Calculator c = new Calculator(3, 5);
            int result =
                (int)t.InvokeMember("Add", BindingFlags.InvokeMethod, null, c, null);
            Console.WriteLine($"The result is {result}");

            object[] parameters = { 6, 9 };
            t.InvokeMember("Add", BindingFlags.InvokeMethod, null, t, parameters);

            MethodInfo mi = t.GetMethod("Add", BindingFlags.Instance | BindingFlags.Public);
            mi.Invoke(c, null);

            mi = t.GetMethod("Add", BindingFlags.Static | BindingFlags.Public);
            mi.Invoke(null, parameters);

            //Assembly asm = Assembly.GetExecutingAssembly();
            //Object obj = asm.CreateInstance("SimpleExplore.Calculator", true);

            //ObjectHandle handler = Activator.CreateInstance(null, " SimpleExplore.Calculator");
            //obj = handler.Unwrap();
            //有参数构造函数创建对象
            //Assembly asm = Assembly.GetExecutingAssembly();
            //Object[] parameters = new Object[2];//定义构造函数需要的参数
            //parameters[0] = 3;
            //parameters[1] = 5;
            //Object obj = asm.CreateInstance("SimpleExplore.Calculator", true, BindingFlags.Default, null, parameters, null, null);

            //Type t = typeof(DemoClass);
            //Console.WriteLine($"下面列出应用于{t}的RecordAttribute属性:", t);
            ////获取所有的RecordAttris属性
            //object[] records = t.GetCustomAttributes(typeof(RecordAttribute), false);
            //foreach (RecordAttribute record in records)
            //{
            //    Console.WriteLine($"    {record}");
            //    Console.WriteLine($"      类型:{record.RecordType}");
            //    Console.WriteLine($"      作者:{record.Author}");
            //    Console.WriteLine($"        日期:{record.Date.ToShortDateString()}");
            //    if (!String.IsNullOrEmpty(record.Memo))
            //    {
            //        Console.WriteLine($"        备注:{record.Memo}");
            //    }
            //}
            //DemoClass demo = new DemoClass();
            //Console.WriteLine(demo.ToString());
            //Console.WriteLine((AttributeTargets)6140);
            //MemberExplore(typeof(DemoClass));
            //AssemblyExplore();
            Console.ReadLine();
        }
    }
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class RecordAttribute : Attribute
    {
        private string recordType;
        private string author;
        private DateTime date;
        private string memo;
        //构造函数，构造函数的参数在特性中也称为"未知参数"
        public RecordAttribute(string recordType, string author, string date)
        {
            this.recordType = recordType;
            this.author = author;
            this.date = Convert.ToDateTime(date);
        }
        //对于位置参数，通常只提供get访问器
        public string RecordType { get { return recordType; } }
        public string Author {  get { return author; } }
        public DateTime Date {  get { return date; } }
        //构建一个属性，在特性中也叫"命名参数"
        public string Memo
        {
            get { return memo; }
            set { memo = value; }
        }
    }
    [Record("更新", "Dong", "2017-09-07", Memo = "修改 ToString()方法")]
    [Record("创建", "Dong", "2017-09-06")]
    public class DemoClass
    {
        public override string ToString()
        {
            return "This is a demo class"; 
        }
    }
}
