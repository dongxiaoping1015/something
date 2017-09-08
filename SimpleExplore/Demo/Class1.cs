using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public abstract class BaseClass
    { }
    public struct DemoStruct
    { }
    public delegate void DemoDelegate(Object sender, EventArgs e);
    public enum DemoEnum
    {
        terrible, bad, common = 4, good, wonderful = 8
    }
    public interface IDemoInterface
    {
        void SayGreeting(string name);
    }
    public interface IDemoInterface2
    { }
    public sealed class DemoClass : BaseClass, IDemoInterface, IDemoInterface2
    {
        private string name;
        public string city;
        public readonly string title;
        public const string text = "Const Field";
        public event DemoDelegate myEvent;
        public string Name
        {
            private get { return name; }
            set { name = value; }
        }
        public DemoClass()
        {
            title = "Readonly Field";
        }
        public class NestedClass
        { }
        public void SayGreeting(string name)
        {
            Console.WriteLine($"Morning :{name}");
        }
    }
}
