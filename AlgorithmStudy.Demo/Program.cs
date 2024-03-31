using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using AlgorithmStudy.Question;

namespace AlgorithmStudy.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();

            do
            {
                var input = Console.ReadLine();
                if(IsApplicationExit(input))
                {
                    break;
                }

                sw.Restart();

                var result = "";
                Console.WriteLine(result);

                sw.Stop();
                Console.WriteLine($"経過時間:{sw.Elapsed.TotalSeconds}秒");
            }
            while (true);
        }

        static bool IsApplicationExit(string? input)
        {
            return Regex.IsMatch(input ?? string.Empty, @"^(exit|e)", RegexOptions.IgnoreCase);
        }
    }
}