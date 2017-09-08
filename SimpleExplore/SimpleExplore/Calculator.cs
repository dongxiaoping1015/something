using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleExplore
{
    class Calculator
    {
        private int x;
        private int y;
        public Calculator()
        {
            x = 0;
            y = 0;
            Console.WriteLine("Calculator() invoked");
        }
        public Calculator(int x, int y)
        {
            this.x = x;
            this.y = y;
            Console.WriteLine("Calculator(int x, int y) invoked");
        }
        public int Add()
        {
            int total = 0;
            total = x + y;
            Console.WriteLine("Invoke Instance Method: ");
            Console.WriteLine($"[Add]: {x} plus {y} equals to {total}");
            return total;
        }
        public static void Add(int x, int y)
        {
            int total = x + y;
            Console.WriteLine("Invode Static Method: ");
            Console.WriteLine($"[Add]: {x} plus {y} equals to {total}");
        }
    }
}
