using System;
using System.Linq;

namespace TextTokenizationLab
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("--test"))
            {
                Environment.ExitCode = Lab3Tests.RunAll();
                return;
            }
            Console.WriteLine("=== ЛАБОРАТОРНАЯ №3: ТОКЕНИЗАЦИЯ ТЕКСТА ===");
            ConsoleMenu.Run();
        }
    }
}
