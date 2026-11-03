using System;
using System.Text;
namespace TextTokenizationLab
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Введите текст:");
            string input = Console.ReadLine() ?? "";
            Console.WriteLine(input);
        }
    }
}