using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Track のチュートリアル問題です。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IList<int> Tutorial(string input)
        {
            var lines = GetQuestionLine(input);
            var positionsX = new List<long>() { 0 };
            var positionsY = new List<long>() { 0 };
            var result = new List<int>();

            for (int i = 0; i < lines.Count; i++)
            {
                var splits = lines[i].Split(" ");
                var command = splits[0];

                if (command == "MOVE")
                {
                    var moveX = long.Parse(splits[1]);
                    var moveY = long.Parse(splits[2]);
                    var currentX = positionsX[positionsX.Count - 1] + moveX;
                    var currentY = positionsY[positionsY.Count - 1] + moveY;

                    positionsX.Add(currentX);
                    positionsY.Add(currentY);
                }
                if (command == "QUERY_EAST" || command == "QUERY_NORTH")
                {
                    var positions = command == "QUERY_EAST" ? positionsX : positionsY;
                    var street = long.Parse(splits[1]);
                    var count = 0;

                    for (int j = 1; j < positions.Count; j++)
                    {
                        if ((positions[j - 1] < street && street < positions[j]) ||
                        positions[j] < street && street < positions[j - 1])
                        {
                            count++;
                        }
                    }

                    result.Add(count);
                }
            }

            return result;
        }

        public static void Sansan(string input)
        {
            var data = GetQuestionData(input);

            Debug(data);


        }

        private static List<string> GetQuestionLine(string input)
        {
            var lines = input.Trim().Split(Environment.NewLine).Select(x => x.Trim()).ToArray();
            var result = new List<string>();

            foreach (var item in lines)
            {
                result.Add(item);
            }

            return result;
        }

        private static List<List<string>> GetQuestionData(string input)
        {
            var lines = input.Trim().Split(Environment.NewLine).Select(x => x.Trim()).ToArray();
            var result = new List<List<string>>();

            foreach (var item in lines)
            {
                var split = Regex.Split(item, @"\s");

                result.Add(split.ToList());
            }

            return result;
        }

        private static void Debug(dynamic source)
        {
            Console.WriteLine(JsonSerializer.Serialize(source));
        }
    }
}
