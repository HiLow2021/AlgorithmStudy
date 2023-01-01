using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;
using My.Search;
using My.Text;

namespace AlgorithmStudy.Misc
{
    /// <summary>
    /// クラスに所属が決まっていないメソッドを寄せ集めたクラスです。
    /// </summary>
    public static class Misc
    {
        /// <summary>
        /// 指定する整数までの掛け算表を返します。
        /// </summary>
        /// <param name="n">整数。</param>
        /// <returns>掛け算表。</returns>
        public static int[,] MultiplicationTable(int n)
        {
            var Table = new int[n, n];

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    Table[i - 1, j - 1] = i * j;
                }
            }

            return Table;
        }

        /// <summary>
        /// うるう年であるか判定します。
        /// </summary>
        /// <param name="Year">年。</param>
        /// <returns>うるう年なら True。そうでなければ False。</returns>
        public static bool IsLeapYear(int Year)
        {
            if (Year % 4 == 0 && Year % 100 != 0 || Year % 400 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 与えられた秒数から時、分、秒に変換します。
        /// </summary>
        /// <param name="TotalSeconds">秒数。</param>
        /// <returns>時、分、秒の順に格納された配列。</returns>
        public static int[] GetTimeFromTotalSeconds(int TotalSeconds)
        {
            int hour = TotalSeconds / 3600;
            int minute = TotalSeconds / 60 - hour * 60;
            int second = TotalSeconds % 60;

            return new int[3] { hour, minute, second };
        }

        /// <summary>
        /// 配列の合計を返します。
        /// </summary>
        /// <remarks>
        /// 学習のために、再帰処理で合計を求めています。
        /// </remarks>
        /// <param name="Sources">配列。</param>
        /// <returns>合計。</returns>
        public static int SumByRecursion(int[] Sources)
        {
            return Sum(Sources, 0);

            int Sum(int[] sources, int i)
            {
                if (i == sources.Length - 1)
                {
                    return sources[i];
                }

                return sources[i] + Sum(sources, i + 1);
            }
        }

        /// <summary>
        /// 指定する2次元配列が魔方陣かどうかを示す値を返します。
        /// </summary>
        /// <param name="ms">魔方陣。</param>
        /// <returns>魔方陣なら True。そうでなければ False。</returns>
        public static bool IsMagicSquare(int[,] ms)
        {
            var nx = ms.GetLength(1) - 1;
            var ny = ms.GetLength(0) - 1;
            var List = new List<IEnumerable<int>>();

            for (int y = 0; y <= ny; y++)
            {
                List.Add(ms.GetRow(y));
            }
            for (int x = 0; x <= nx; x++)
            {
                List.Add(ms.GetColumn(x));
            }

            List.Add(ms.GetDiagonal(0, 0, true));
            List.Add(ms.GetDiagonal(ny, 0, false));

            var Sum = List[0].Sum();

            foreach (var Item in List)
            {
                if (Sum != Item.Sum())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Nクイーン問題の解を全て返します。
        /// </summary>
        /// <remarks>
        /// バックトラック法を用いて、問題を解いています。
        /// クイーンの数を増やすと、処理時間やメモリ消費量も著しく増すので注意してください。
        /// </remarks>
        /// <param name="n">クイーンの数。</param>
        /// <returns>全ての解。</returns>
        public static List<int[,]> NQueen(int n)
        {
            var Board = new int[n, n];
            var Result = new List<int[,]>();

            Board.SetAll(-1);
            SetQueen(Board, 0);

            return Result;

            void SetQueen(int[,] board, int y)
            {
                if (y == n)
                {
                    Result.Add(board);

                    return;
                }
                for (int x = 0; x < n; x++)
                {
                    if (board[y, x] == -1)
                    {
                        var copy = (int[,])board.Clone();

                        SetQueenArea(copy, y, x, 0);
                        copy[y, x] = 1;
                        SetQueen(copy, y + 1);
                    }
                }
            }

            void SetQueenArea(int[,] board, int Row, int Column, int Chip)
            {
                board.SetRow(Chip, Row);
                board.SetColumn(Chip, Column);
                board.SetDiagonal(Chip, Row, Column, true);
                board.SetDiagonal(Chip, Row, Column, false);
            }
        }

        /// <summary>
        /// ナイト巡回問題の解を1つ返します。
        /// </summary>
        /// <param name="Width">盤の幅。</param>
        /// <param name="Height">盤の高さ。</param>
        /// <returns>解。</returns>
        public static int[,] KnightTour(int Width, int Height)
        {
            var Board = new int[Height, Width];
            var dx = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
            var dy = new int[] { -2, -1, 1, 2, 2, 1, -1, -2 };

            Board.SetAll(-1);
            Move(Board, 0, 0, 0);

            return Board;

            bool Move(int[,] board, int x, int y, int Count)
            {
                board[y, x] = Count;

                if (Count == Width * Height - 1)
                {
                    return true;
                }
                for (int i = 0; i < dx.Length; i++)
                {
                    var px = x + dx[i];
                    var py = y + dy[i];

                    if (board.IsWithinRange(py, px) && board[py, px] == -1)
                    {
                        if (Move(board, px, py, Count + 1))
                        {
                            return true;
                        }

                        board[py, px] = -1;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// 与えられた整数の配列の要素間に、"+"、"-"、または連結した結果が指定する合計となる、全ての組合せのパターンを返します。
        /// </summary>
        /// <remarks>
        /// たとえば、1 から 9 の配列と 100 の合計が指定された場合は、1 + 2 + 34 – 5 + 67 – 8 + 9 = 100という組み合わせが、パターンに当てはまることになります。
        /// </remarks>
        /// <param name="Sources">整数の配列。</param>
        /// <param name="Sum">合計。</param>
        /// <returns>全ての組み合わせのリスト。</returns>
        public static IEnumerable<string> SumPatterns(int[] Sources, int Sum)
        {
            var Numbers = Sources.Select(x => x.ToString()).ToArray();
            var Operators = new string[] { "+", "-" };
            var Patterns = new List<string>();

            SetPatterns(Numbers[0], 1);

            foreach (var Pattern in Patterns)
            {
                var s = Calculate(Pattern);

                if (s == Sum)
                {
                    yield return Pattern + "=" + Sum;
                }
            }

            int Calculate(string Pattern)
            {
                var p = Pattern;
                var sum = 0;

                if (!Operators.Contains(p.Substring(0, 1)))
                {
                    p = Operators[0] + p;
                }
                for (int i = 0, j = 1; i < p.Length;)
                {
                    var s = p.Substring(i, 1);

                    for (; j < p.Length && !Operators.Contains(p.Substring(j, 1)); j++) ;

                    var Num = int.Parse(p.Substring(i + 1, j - i - 1));

                    if (s == Operators[0])
                    {
                        sum += Num;
                    }
                    else
                    {
                        sum -= Num;
                    }

                    i = j++;
                }

                return sum;
            }

            void SetPatterns(string Pattern, int n)
            {
                if (n == Numbers.Length)
                {
                    Patterns.Add(Pattern);

                    return;
                }

                SetPatterns(Pattern + Operators[0] + Numbers[n], n + 1);
                SetPatterns(Pattern + Operators[1] + Numbers[n], n + 1);
                SetPatterns(Pattern + Numbers[n], n + 1);
            }
        }

        /// <summary>
        /// 与えられた地図の出発地から目的地に移動する場合の最小コストを返します。
        /// </summary>
        /// <remarks>
        /// いわゆる最短経路問題です。
        /// 地図を隣接行列に変換した後、ダイクストラ法で最小コストを算出しています。
        /// </remarks>
        /// <param name="Map">地図。</param>
        /// <param name="Start">出発地。</param>
        /// <param name="Destination">目的地。</param>
        /// <param name="IncludingStartCost">出発地の移動コストを含めるかどうか。</param>
        /// <returns>最小コスト。</returns>
        public static int MinimumCostPath(int[,] Map, int Start, int Destination, bool IncludingStartCost)
        {
            var Graph = MapToGraph(Map);
            var StartCost = IncludingStartCost ? Map[Start / Map.GetLength(0), Start % Map.GetLength(0)] : 0;

            return GraphSearch.DijkstraSearch(Graph, Start, Destination) + StartCost;

            int[,] MapToGraph(int[,] map)
            {
                var n = map.GetLength(0);
                var m = map.GetLength(1);
                var l = map.Length;
                var graph = new int[l, l];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (map.IsWithinRange(i - 1, j))
                        {
                            graph[i * n + j, (i - 1) * n + j] = map[i - 1, j];
                        }
                        if (map.IsWithinRange(i, j + 1))
                        {
                            graph[i * n + j, i * n + j + 1] = map[i, j + 1];
                        }
                        if (map.IsWithinRange(i + 1, j))
                        {
                            graph[i * n + j, (i + 1) * n + j] = map[i + 1, j];
                        }
                        if (map.IsWithinRange(i, j - 1))
                        {
                            graph[i * n + j, i * n + j - 1] = map[i, j - 1];
                        }
                    }
                }

                return graph;
            }
        }

        public static void Test(int cardMaxNum, params int[] players)
        {
            var thinkData = new bool[cardMaxNum];

            for (int i = 1; i < players.Length; i++)
            {
                thinkData[players[i] - 1] = true;
            }

            for (int i = 0; ; i++)
            {
                if (Think(i % players.Length))
                {
                    break;
                }
            }

            bool Think(int player)
            {
                bool flag = false;
                int i, j;

                for (i = 0; thinkData[i]; i++) ;
                for (j = 0; thinkData[thinkData.Length - 1 - j]; j++) ;

                if (i >= players.Length - 1)
                {
                    Console.WriteLine($"{player}:Max");
                    flag = true;
                }
                else if (j >= players.Length - 1)
                {
                    Console.WriteLine($"{player}:Min");
                    flag = true;
                }
                else if (i + j >= players.Length - 1)
                {
                    Console.WriteLine($"{player}:Mid");
                    flag = true;
                }
                else
                {
                    Console.WriteLine($"{player}:?");
                    thinkData[player] = true;
                    thinkData[(player + 1) % players.Length] = false;

                }

                return flag;
            }
        }
    }
}
