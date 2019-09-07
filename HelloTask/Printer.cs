using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    interface IPrinter
    {
        void Print(string message, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.Gray);
    }

    class Printer : IPrinter
    {
        private readonly object _syncObject = new object();
        public void Print(string message, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.Gray)
        {
            lock (_syncObject)
            {
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor();
            }
        }
    }
}
