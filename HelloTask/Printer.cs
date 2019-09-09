using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloTaskApp
{
    public interface IPrinter
    {
        void Print(string message, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.Gray);
    }

    class Printer : IPrinter
    {
        private readonly object _syncObject = new object();
        public void Print(string message, int x = -1, int y = -1, ConsoleColor color = ConsoleColor.Gray)
        {
            lock (_syncObject)
            {
                Console.CursorLeft = x != -1 ? x : Console.CursorLeft;
                Console.CursorTop = y != -1 ? y : Console.CursorTop;
                Console.ForegroundColor = color;
                Console.Write(message);
                Console.ResetColor();
            }
        }
    }
}
