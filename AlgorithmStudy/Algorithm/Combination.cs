using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy.Algorithm
{
    /// <summary>
    /// 組み合わせに関する実装例の集まりです。
    /// </summary>
    public static class Combination
    {
        /// <summary>
        /// 与えられた配列から 2 個を選んで並べる組み合わせの全パターンを返します。
        /// </summary>
        /// <param name="Sources">配列。</param>
        /// <returns>組み合わせの全パターン。</returns>
        public static List<int[]> CombinationTwo(int[] Sources)
        {
            var n = Sources.Length;
            var Result = new List<int[]>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    Result.Add(new int[] { Sources[i], Sources[j] });
                }
            }

            return Result;
        }

        /// <summary>
        /// 与えられた配列から 3 個を選んで並べる組み合わせの全パターンを返します。
        /// </summary>
        /// <param name="Sources">配列。</param>
        /// <returns>組み合わせの全パターン。</returns>
        public static List<int[]> CombinationThree(int[] Sources)
        {
            var n = Sources.Length;
            var Result = new List<int[]>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        Result.Add(new int[] { Sources[i], Sources[j], Sources[k] });
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// 与えられた配列から 2 個を選んで並べる順列の全パターンを返します。
        /// </summary>
        /// <param name="Sources">配列。</param>
        /// <returns>順列の全パターン。</returns>
        public static List<int[]> PermutationTwo(int[] Sources)
        {
            var n = Sources.Length;
            var Result = new List<int[]>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        Result.Add(new int[] { Sources[i], Sources[j] });
                }
            }

            return Result;
        }

        /// <summary>
        /// 与えられた配列から 3 個を選んで並べる順列の全パターンを返します。
        /// </summary>
        /// <param name="Sources">配列。</param>
        /// <returns>順列の全パターン。</returns>
        public static List<int[]> PermutationThree(int[] Sources)
        {
            var n = Sources.Length;
            var Result = new List<int[]>();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (i != j && j != k && k != i)
                            Result.Add(new int[] { Sources[i], Sources[j], Sources[k] });
                    }
                }
            }

            return Result;
        }

        /// <summary>
        /// 文字列の配列の直積集合(デカルト積)をサブシーケンス群として返します。
        /// </summary>
        /// <param name="Source">元の文字列の配列。</param>
        /// <param name="RepeatCount">繰り返す回数。</param>
        /// <returns>直積集合を表すサブシーケンス群。</returns>
        public static IEnumerable<string> ProductByLinq(string[] Sources, int RepeatCount)
        {
            return Enumerable.Repeat(Sources, RepeatCount).Aggregate(Enumerable.Repeat("", 1), (a, ca) => from s in a from c in ca select s + c);
        }
    }
}
