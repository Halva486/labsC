using System;
using System.Linq;
using System.Text;

namespace TextTokenizationLab
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (args.Contains("--test"))
                return Lab4Tests.RunAll();

            Console.WriteLine("=== PROJECT 04: CONCORDANCE ===");
            Console.WriteLine("Введите текст построчно.");
            Console.WriteLine("Для завершения нажмите Enter на пустой строке.\n");

            var input = new StringBuilder();
            while (true)
            {
                string? line = Console.ReadLine();
                if (string.IsNullOrEmpty(line)) break;
                if (input.Length > 0) input.Append('\n');
                input.Append(line);
            }

            if (string.IsNullOrWhiteSpace(input.ToString()))
            {
                Console.WriteLine("Ошибка: текст пустой!");
                return 1;
            }

            Text text = TextParser.Parse(input.ToString());
            Console.WriteLine("\n=== РЕЗУЛЬТАТ ===");
            text.PrintConcordance();
            Console.WriteLine("\nПрограмма завершена.");
            return 0;
        }
    }
}
