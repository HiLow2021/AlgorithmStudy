using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy.Demo
{
    public static class Utility
    {
        public static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        public static IList<int> ReadInts()
        {
            return Console.ReadLine().Split(new[] { ' ' }).Select(x => int.Parse(x)).ToList();
        }

        public static void Show<T>(IEnumerable<T> Source)
        {
            foreach (var x in Source)
            {
                Console.WriteLine("{0} ", x);
            }

            Console.WriteLine();
        }

        public static void Show<T>(IEnumerable<IEnumerable<T>> Source)
        {
            foreach (var b in Source)
            {
                foreach (var x in b)
                {
                    Console.Write("{0} ", x);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void Show<T>(IEnumerable<T[]> Source)
        {
            foreach (var b in Source)
            {
                foreach (var x in b)
                {
                    Console.Write("{0} ", x);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void Show<T>(T[,] Source)
        {
            for (int i = Source.GetLowerBound(0); i <= Source.GetUpperBound(0); i++)
            {
                for (int j = Source.GetLowerBound(1); j <= Source.GetUpperBound(1); j++)
                {
                    Console.Write("{0} ", Source[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
