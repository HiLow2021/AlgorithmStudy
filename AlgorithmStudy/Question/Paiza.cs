using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlgorithmStudy.Question
{
    public static class Paiza
    {
        /// <summary>
        /// Leet文字列。
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string C016(string source)
        {
            source = source.Replace('A', '4');
            source = source.Replace('E', '3');
            source = source.Replace('G', '6');
            source = source.Replace('I', '1');
            source = source.Replace('O', '0');
            source = source.Replace('S', '5');
            source = source.Replace('Z', '2');

            return source;
        }

        /// <summary>
        /// 最遅出社時刻。
        /// </summary>
        /// <returns></returns>
        public static DateTime B013(int a, int b, int c, IList<DateTime> trainTimes)
        {
            var n = trainTimes.Count;
            var start = DateTime.MinValue;

            for (int i = n - 1; i >= 0; i--)
            {
                var kaisha = new DateTime(1, 1, 1, 8, 59, 0);

                kaisha = kaisha.AddMinutes(-c);

                if (kaisha - trainTimes[i].AddMinutes(b) >= TimeSpan.Zero)
                {
                    start = trainTimes[i].AddMinutes(-a);

                    break;
                }
            }

            return start;
        }

        /// <summary>
        /// 複数形への変換。
        /// </summary>
        /// <returns></returns>
        public static string B021(string source)
        {
            var match1 = new Regex(@"(s|sh|ch|o|x)$");
            var match2 = new Regex(@"(f|fe)$");
            var match3 = new Regex(@"(?<!a|i|u|e|o)y$");

            if (match1.IsMatch(source))
            {
                source = match1.Replace(source, "$1es");
            }
            else if (match2.IsMatch(source))
            {
                source = match2.Replace(source, "ves");
            }
            else if (match3.IsMatch(source))
            {
                source = match3.Replace(source, "$1ies");
            }
            else
            {
                source += "s";
            }

            return source;
        }

        /// <summary>
        /// オススメのお店。
        /// </summary>
        /// <returns></returns>
        public static IList<int> B084(IList<int> myRanks, IList<int[]> rankList, int k)
        {
            var n = myRanks.Count;
            var m = rankList.Count;
            var ranksList = new List<List<int>>();
            var results = new List<int>();

            for (int i = 0; i < m; i++)
            {
                if (ranksList[i].Where((x, j) => x == 3 && myRanks[j] == x).Count() >= k)
                {
                    var rec = ranksList[i].Select((x, j) => x == 3 && myRanks[j] == 0);

                    for (int j = 0; j < n; j++)
                    {
                        if (ranksList[i][j] == 3 && myRanks[j] == 0)
                        {
                            results.Add(j + 1);
                        }
                    }
                }
            }

            results = results.Distinct().OrderBy(x => x).ToList();

            return results;
        }

        /// <summary>
        /// 週休2日制。
        /// </summary>
        /// <returns></returns>
        public static int A023(int[] source)
        {
            var days = source;
            var n = days.Length;
            var daysOfWeek = 7;
            var okWeek = 5;
            var count = 0;
            var maxCount = 0;

            for (int i = 0; i <= n - daysOfWeek;)
            {
                var sum = 0;

                for (int k = 0; k < daysOfWeek; k++)
                {
                    sum += days[i + k];
                }

                if (sum > okWeek)
                {
                    count = 0;
                    i++;
                    continue;
                }

                int j;

                if (n - i >= daysOfWeek * 2)
                {
                    j = daysOfWeek;
                }
                else
                {
                    j = n - i;
                }

                count += j;

                if (count > maxCount)
                {
                    maxCount = count;
                }

                i += j;
            }

            return maxCount;
        }

        /// <summary>
        /// 試験の作成。
        /// </summary>
        /// <remarks>
        /// bit全探索で、2^N 乗通りの組み合わせを取得する方法を採用したのですが、
        /// 指数が int 型や long 型でサポートできる範囲を越えてしまう可能性に気付かず、
        /// 正解することができませんでした。
        /// bit全探索は効率的で強力ですが、動的計画法を使った方が良かったかもしれません。
        /// </remarks>
        /// <returns></returns>
        public static int[] A035(int[] points)
        {
            var n = points.Length;
            var setList = new List<IList<int>>();

            for (int bit = 0; bit < (1 << n); bit++)
            {
                var set = new List<int>();

                for (int i = 0; i < n; i++)
                {
                    if ((bit & (1 << i)) > 0)
                    {
                        set.Add(points[i]);
                    }
                }

                setList.Add(set);
            }

            var sums = setList.Select(x => x.Sum()).Distinct().OrderBy(x => x).ToArray();

            return sums;
        }
    }
}
