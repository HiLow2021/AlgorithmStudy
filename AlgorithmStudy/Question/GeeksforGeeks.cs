using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy.Question
{
    public static class GeeksforGeeks
    {
        /// <summary>
        /// 指定する整数を連続する自然数の和で表現できる場合、そのパターンを示す文字列の配列を返します。
        /// </summary>
        /// <param name="n">整数。</param>
        /// <returns>パターンを示す文字列の配列。</returns>
        public static string[] ConsecutiveNumbers(int n)
        {
            var List = new List<string>();

            for (int i = 2, min = n / 2; 0 < min; i++, min = n / i - (i - 1) / 2)
            {
                int sum = min; // minは、連続する自然数の中で最小のもの。
                StringBuilder sb = new StringBuilder(min.ToString());

                for (int j = min + 1; j < min + i; j++)
                {
                    sum += j;
                    sb.Append("+" + j.ToString());
                }
                if (sum == n)
                {
                    List.Add(sb.ToString());
                }
            }

            List.Reverse();

            return List.ToArray();
        }
    }
}
