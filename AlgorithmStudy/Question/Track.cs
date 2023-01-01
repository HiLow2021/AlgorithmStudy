using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy.Question
{
    public static class Track
    {
        /// <summary>
        /// レーベンシュタイン試験です。
        /// レーベンシュタイン距離で、置換のみを考えて距離を算出します。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Levenshtein(string input)
        {
            var answer = "levenshtein";
            int result;

            if (input == answer)
            {
                result = 0;
            }
            else if (input.Length != answer.Length)
            {
                result = -1;
            }
            else
            {
                var count = 0;

                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] != answer[i])
                    {
                        count++;
                    }
                }

                result = count;
            }

            return result;
        }

        /// <summary>
        /// 単語学習です。
        /// 与えられた文字の部分集合を求めます。
        /// ただし、空文字と重複文字は省きます。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int StudyWord(string input)
        {
            var n = input.Length;
            var results = new List<string>();
            var used = new bool[n];

            Recursive(string.Empty, 0);
            results = results.Distinct().Where(x => x.Length > 0).ToList();

            return results.Count;

            void Recursive(string s, int m)
            {
                if (m == n)
                {
                    results.Add(s);

                    return;
                }

                results.Add(s);

                for (int i = m; i < n; i++)
                {
                    if (!used[i])
                    {
                        s += input[i];
                        used[i] = true;
                        Recursive(s, i + 1);
                        s = s.Substring(0, s.Length - 1);
                        used[i] = false;
                    }
                }
            }
        }

        /// <summary>
        /// 単語学習の別解です。
        /// 与えられた文字の部分集合を求めます。
        /// ただし、空文字と重複文字は省きます。
        /// </summary>
        /// <remarks>
        /// bit全探索を使ったアプローチです。
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int StudyWordAnother(string input)
        {
            var n = input.Length;
            var results = new List<string>();

            for (int i = 0; i < (1 << n); i++)
            {
                var s = string.Empty;

                for (int j = 0; j < n; j++)
                {
                    if ((i & 1 << j) > 0)
                    {
                        s += input[j];
                    }
                }

                results.Add(s);
            }

            results = results.Distinct().Where(x => x.Length > 0).ToList();

            return results.Count;
        }
    }
}
