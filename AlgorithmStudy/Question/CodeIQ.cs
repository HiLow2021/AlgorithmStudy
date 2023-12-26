using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using My.Extensions;
using My.Text;
using My.Utilities;
using AlgorithmStudy.Algorithm;

namespace AlgorithmStudy.Question
{
    public static class CodeIQ
    {
        public static int Question1()
        {
            for (int i = 10; ; i++)
            {
                var str2 = Convert.ToString(i, 2);
                var str8 = Convert.ToString(i, 8);
                var str10 = i.ToString();

                if (str2 == string.Join("", str2.Reverse())
                    && str8 == string.Join("", str8.Reverse())
                    && str10 == string.Join("", str10.Reverse()))
                {
                    return i;
                }
            }
        }

        public static IEnumerable<string> Question2()
        {
            var oparators = new string[] { "+", "-", "*", "/", "" };

            for (int i = 1000; i < 10000; i++)
            {
                var patterns = oparators.Product(4)
                                        .Select(xs => string.Join("", xs.Zip(i.ToString(), (x, y) => x + y)))
                                        .Where(x => int.TryParse(x[0].ToString(), out int Result))
                                        .Where(x => 4 < x.Length);

                foreach (var p in patterns)
                {
                    if (string.Join("", TextConverter.Evaluate(p).ToString().Reverse()) == i.ToString())
                    {
                        yield return p;
                    }
                }
            }
        }

        public static IEnumerable<int> Question3()
        {
            var Cards = new bool[101];

            for (int i = 2; i < Cards.Length; i++)
            {
                for (int j = i; j < Cards.Length; j += i)
                {
                    Cards[j] = !Cards[j];
                }
            }
            for (int i = 1; i < Cards.Length; i++)
            {
                if (!Cards[i])
                {
                    yield return i;
                }
            }
        }

        public static int Question4(int n, int m)
        {
            var Count = n - 1;
            var i = 0;

            for (; 0 <= Count; i++)
            {
                Count -= CommonUtility.Min(m, (int)Math.Pow(2, i));
            }

            return i;
        }

        public static int Question5(int Money, int Max)
        {
            var Coins = new int[] { 10, 50, 100, 500 };
            var Count = 0;

            for (int i = 0; i <= Max; i++)
            {
                var Patterns = Coins.CombinationIncludingDuplication(i);

                foreach (var p in Patterns)
                {
                    if (p.Sum() == Money)
                    {
                        Count++;
                    }
                }
            }

            return Count;
        }

        public static int Question6()
        {
            var Count = 0;

            for (int i = 2; i <= 10000; i += 2)
            {
                for (int j = i * 3 + 1; j != 1;)
                {
                    j = ((j & 1) == 0) ? j >> 1 : j * 3 + 1;

                    if (j == i)
                    {
                        Count++;

                        break;
                    }
                }
            }

            return Count;
        }

        public static IEnumerable<string> Question7()
        {
            var dt1 = new DateTime(1964, 10, 10);
            var dt2 = new DateTime(2020, 7, 23);

            while (dt1 <= dt2)
            {
                var str = dt1.ToString("yyyyMMdd");
                var str2 = Convert.ToString(int.Parse(str), 2);
                var str10 = Convert.ToInt32(string.Join(string.Empty, str2.Reverse()), 2).ToString();

                if (str == str10)
                {
                    yield return dt1.ToString("yyyy/MM/dd");
                }

                dt1 = dt1.AddDays(1);
            }
        }

        public static int Question8(int n)
        {
            var Map = new bool[n * 2 + 1, n * 2 + 1];
            var vx = new int[] { 0, 1, 0, -1 };
            var vy = new int[] { -1, 0, 1, 0 };
            var Count = 0;

            Move(n, n, 0);

            return Count;

            void Move(int x, int y, int m)
            {
                Map[y, x] = true;

                if (m == n)
                {
                    Count++;

                    return;
                }

                for (int i = 0; i < vx.Length; i++)
                {
                    var px = x + vx[i];
                    var py = y + vy[i];

                    if (!Map[py, px])
                    {
                        Move(px, py, m + 1);
                        Map[py, px] = false;
                    }
                }
            }
        }

        public static int Question9(int m, int w)
        {
            var Path = new int[++m, ++w];

            Path[0, 0] = 1;

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i != j && m - i != w - j)
                    {
                        if (0 < i)
                        {
                            Path[i, j] += Path[i - 1, j];
                        }
                        if (0 < j)
                        {
                            Path[i, j] += Path[i, j - 1];
                        }
                    }
                }
            }

            return Path[m - 2, w - 1] + Path[m - 1, w - 2];
        }

        public static int Question10()
        {
            var European = new int[]
            {
                0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26
            };
            var American = new int[]
            {
                0, 28, 9, 26, 30, 11, 7, 20, 32, 17, 5, 22, 34, 15, 3, 24, 36, 13, 1, 0, 27, 10, 25, 29, 12, 8, 19, 31, 18, 6, 21, 33, 16, 4, 23, 35, 14, 2
            };
            var n = European.Length;
            var m = American.Length;
            var Count = 0;

            for (int i = 2; i <= 36; i++)
            {
                var Sum1 = 0;
                var Sum2 = 0;

                for (int j = 0; j <= m; j++)
                {
                    var s1 = 0;
                    var s2 = 0;

                    for (int k = 0; k < i; k++)
                    {
                        s1 += European[(j + k) % n];
                        s2 += American[(j + k) % m];
                    }

                    Sum1 = CommonUtility.Max(Sum1, s1);
                    Sum2 = CommonUtility.Max(Sum2, s2);
                }
                if (Sum1 < Sum2)
                {
                    Count++;
                }
            }

            return Count;
        }

        public static IEnumerable<long> Question11()
        {
            var Count = 0;

            for (int i = 3; Count < 6 + 5; i++)
            {
                var a = Mathematics.Fibonacci(i);
                var b = a.ToString().Select(x => int.Parse(x.ToString())).Sum();

                if (a % b == 0)
                {
                    Count++;

                    yield return a;
                }
            }
        }

        public static IEnumerable<int> Question12()
        {
            for (int i = 2; ; i++)
            {
                var sr = Mathematics.SquareRoot(i);
                var significand = Regex.Replace(sr.ToString(), @"\.", string.Empty);

                if (Calculate(significand) == 9)
                {
                    yield return i;
                    break;
                }
            }
            for (int i = 2; ; i++)
            {
                var sr = Mathematics.SquareRoot(i);
                var significand = Regex.Replace(sr.ToString(), @"^\d+\.", string.Empty);

                if (Calculate(significand) == 9)
                {
                    yield return i;
                    break;
                }
            }

            int Calculate(string str)
            {
                var flag = new bool[10];

                for (int j = 0; j < str.Length; j++)
                {
                    flag[int.Parse(str[j].ToString())] = true;

                    if (flag.All(x => x))
                    {
                        return j;
                    }
                }

                return -1;
            }
        }

        public static int Question13()
        {
            var arr = new string[] { "A", "D", "E", "K", "I", "L", "R", "S", "T", "W" };
            var Table = arr.Permutation(arr.Length)
                           .Select(x => x.ToArray())
                           .Where(x => x[0] != "R" && x[0] != "S" && x[0] != "T" && x[0] != "W")
                           .Select(xs =>
                           {
                               var d = new Dictionary<string, string>();
                               var i = 0;

                               foreach (var x in xs)
                               {
                                   d.Add(x, (i).ToString());
                                   i++;
                               }

                               return d;
                           });

            var Count = 0;

            foreach (var t in Table)
            {
                var Sum = int.Parse(t["R"] + t["E"] + t["A"] + t["D"]) +
                    int.Parse(t["W"] + t["R"] + t["I"] + t["T"] + t["E"]) +
                    int.Parse(t["T"] + t["A"] + t["L"] + t["K"]) -
                    int.Parse(t["S"] + t["K"] + t["I"] + t["L"] + t["L"]);

                if (Sum == 0)
                {
                    Count++;
                }
            }

            return Count;
        }

        public static int Question14()
        {
            var Countries = new string[]
            {
                "Brazil", "Croatia", "Mexico", "Cameroon", "Spain", "Netherlands", "Chile", "Australia", "Colombia",
                "Greece", "Cote d'lvoire", "Japan", "Uruguay", "Costa Rica", "England", "Italy", "Switzerland",
                "Ecuador", "France", "Honduras", "Argentina", "Bosnia and Herzegovina", "Iran", "Nigeria", "Germany",
                "Portugal", "Ghana", "USA", "Belgium", "Algeria", "Russia", "Korea Republic"
            };
            var Used = new bool[Countries.Length];
            var Max = 0;

            Countries = Countries.Select(x => x.ToLower()).ToArray();

            for (int i = 0; i < Countries.Length; i++)
            {
                Used[i] = true;
                Shiritori(Countries[i], 1);
                Used[i] = false;
            }

            return Max;

            void Shiritori(string Current, int n)
            {
                for (int i = 0; i < Countries.Length; i++)
                {
                    if (Countries[i][0] == Current[Current.Length - 1] && !Used[i])
                    {
                        Used[i] = true;
                        Shiritori(Countries[i], n + 1);
                        Used[i] = false;
                    }
                }

                Max = CommonUtility.Max(Max, n);
            }
        }

        public static int Question15(int n, int Step)
        {
            var Memo = new int[n + 1, n + 1];

            Memo.SetAll(-1);

            return Move(0, n);

            int Move(int a, int b)
            {
                if (b < a) { return 0; }
                else if (a == b) { return 1; }
                else if (Memo[a, b] != -1) { return Memo[a, b]; }

                var Count = 0;

                for (int i = 1; i <= Step; i++)
                {
                    for (int j = 1; j <= Step; j++)
                    {
                        Count += Move(a + i, b - j);
                    }
                }

                return Memo[a, b] = Count;
            }
        }

        public static int Question16(int n)
        {
            var List = new List<double>();

            for (int m = 4; m <= n; m += 4)
            {
                var SquareSize = (m / 4) * (m / 4);

                for (int i = 1; i < m / 2; i++)
                {
                    var RectangleSize1 = i * (m / 2 - i);

                    for (int j = 1; j < m / 2; j++)
                    {
                        var RectangleSize2 = j * (m / 2 - j);

                        if (RectangleSize1 + RectangleSize2 == SquareSize)
                        {
                            List.Add(RectangleSize2 / (double)RectangleSize1 + SquareSize / (double)RectangleSize1);
                        }
                    }
                }
            }

            return List.Distinct().Count() / 2;
        }

        public static int Question17(int n)
        {
            var Memo = new int[2, n + 1];

            Memo.SetAll(-1);

            return Align(0, 1) + Align(1, 1);

            int Align(int Gender, int m)
            {
                if (m == n) { return 1; }
                else if (Memo[Gender, m] != -1)
                {
                    return Memo[Gender, m];
                }

                var Count = 0;

                Count += Align(0, m + 1);

                if (Gender == 0)
                {
                    Count += Align(1, m + 1);
                }

                return Memo[Gender, m] = Count;
            }
        }

        public static int Question18()
        {
            for (int n = 2; ; n++)
            {
                var Used = new bool[n + 1];

                Used[1] = true;

                if (Cut(1, 1, n, Used))
                {
                    return n;
                }
            }

            bool Cut(int prev, int k, int n, bool[] Used)
            {
                if (k == n) { return IsInteger(Mathematics.SquareRoot(prev + 1)); }

                for (int i = 2; i <= n; i++)
                {
                    if (!Used[i] && IsInteger(Mathematics.SquareRoot(prev + i)))
                    {
                        Used[i] = true;

                        if (Cut(i, k + 1, n, Used))
                        {
                            return true;
                        }

                        Used[i] = false;
                    }
                }

                return false;
            }

            bool IsInteger(double a)
            {
                return a - Math.Floor(a) == 0;
            }
        }

        // 解を求めるまでの時間が掛かりすぎるために、このアルゴリズムは好ましくありません。
        public static int Question19(int n, int Step)
        {
            Step = CommonUtility.Min(n, ++Step);

            for (int m = 1; ; m++)
            {
                var Patterns = GetCompositeNumbers(m).Combination(n);

                foreach (var p in Patterns)
                {
                    if (Connect(p, p, 0))
                    {
                        return m;
                    }
                }
            }

            bool Connect(IEnumerable<int> Friens, IEnumerable<int> Rest, int k)
            {
                if (k == Step) { return true; }

                foreach (var f in Friens)
                {
                    Rest = Rest.Where(x => x != f);

                    var NextFriends = Rest.Where(x => Mathematics.GreatestCommonDivisor(x, f) != 1);
                    var NextRest = Rest.Except(NextFriends);

                    if (Connect(NextFriends, NextRest, k + 1))
                    {
                        return true;
                    }
                }

                return false;
            }

            int[] GetCompositeNumbers(int m)
            {
                if (m < 2) { return new int[0]; }

                var Numbers = Enumerable.Range(2, m - 1);
                var Primes = Mathematics.GetPrimeNumbers(m);

                return Numbers.Except(Primes).ToArray();
            }
        }

        public static int Question20()
        {
            var MagicSquare = new int[,]
            {
                { 1, 14, 14, 4 },
                { 11, 7, 6, 9 },
                { 8, 10, 10, 5 },
                { 13, 2, 3, 15 },
            };
            var Array = MagicSquare.Cast<int>().ToArray();
            var Sums = new Dictionary<int, int>();

            for (int i = 1; i <= Array.Length; i++)
            {
                var Patterns = Array.Combination(i).Select(x => x.Sum());

                foreach (var p in Patterns)
                {
                    if (!Sums.ContainsKey(p))
                    {
                        Sums.Add(p, 1);
                    }
                    else
                    {
                        Sums[p]++;
                    }
                }
            }

            return Sums.FirstOrDefault(x => x.Value == Sums.Values.Max()).Key;
        }
    }
}
