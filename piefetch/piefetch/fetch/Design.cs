using System;

namespace fetch
{
    public class Design
    {

        public static string title = "┌────────────────   piefetch   ────────────────┐";

        public static string line = "───────────────────────────────────────────";


        public static void printfunc(string text, string value)
        {
            startline();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{text,-20}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($": {value}");
        }

        public static void startline()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("│   ");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
