﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;

//piefetch author ~ Piwka
namespace fetch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "piefetch";
            CommandInput();
        }

        public static void StartFetch()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Design.title);
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                GetUserInformation();
                GetOSInformation();
                GetRamUsage();
                PrintAscii();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

        public static void GetUserInformation()
        {
            
            Design.startline();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Environment.UserName + "'s PC");
            Design.startline();
            Console.WriteLine(Design.line);

        }

        public static void GetOSInformation()
        {
            Design.printfunc("OS", $"{RuntimeInformation.OSDescription}");
            Design.printfunc("Architecture", $"{RuntimeInformation.OSArchitecture}");
            Design.printfunc("Process Architecture", $"{RuntimeInformation.ProcessArchitecture}");
            Version version = Environment.OSVersion.Version;
            Design.printfunc("Major", $"{version.Major}");
            Design.printfunc("Minor", $"{version.Minor}");
        }

        public static void GetRamUsage()
        {
            var searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem");
            foreach (var ram in searcher.Get())
            {
                ulong total = Convert.ToUInt64(ram["TotalVisibleMemorySize"]);
                ulong free = Convert.ToUInt64(ram["FreePhysicalMemory"]);
                Design.printfunc("RAM", $"{(total/1024) - (free/1024)} MiB / {total/1024} MiB");
                PrintRamBar(total, free);
            }

        }

        public static void PrintRamBar(double t, double f)
        {
            int free = (int)((f / t) * 10);
            Design.startline();
            Console.Write('[');
            for (int i = 0; i < (10 - free) * 2; i++)
            {
                Console.Write('█');
            }
            for (int i = 0; i < free * 2;  i++)
            {
                Console.Write('-');
            }
            Console.Write(']');
            Console.WriteLine();
        }

        public static void PrintAscii()
        {
            Design.startline();
            Console.WriteLine();
            string path = @"ascii\win.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    Design.startline();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(sr.ReadLine());
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public static void CommandInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Environment.UserName.ToLowerInvariant() + "@" + Environment.UserDomainName.ToLowerInvariant() + " ~ ");
            Console.ForegroundColor = ConsoleColor.White;
            string read = Console.ReadLine()?.Trim().ToLower();


            if (commands.ContainsKey(read))
            {
                commands[read]();
                CommandInput();
            }
            else
            {
                Console.WriteLine($"'{read}' is not recognized as an internal or external command.");
                CommandInput();
            }
        }

        static Dictionary<string, Action> commands = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase) 
        {
            { "f", () => StartFetch() },
            { "whoami", () => Console.WriteLine(Environment.UserDomainName.ToLowerInvariant() + @"\" + Environment.UserName.ToLowerInvariant()) },
            { "exit", () => Environment.Exit(0) },
            { "help", () => HelpCommand()},
        };

        public static void HelpCommand()
        {
            foreach (var command in commands)
            {
                Console.WriteLine(command.Key);
            }
        }
    }
}
