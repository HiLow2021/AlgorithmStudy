using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;
using My.Utilities;

namespace AlgorithmStudy.Algorithm
{
    /// <summary>
    /// 動的計画法(DynamicPrograming)で、問題解決を図っている実装例の集まりです。
    /// </summary>
    public static class DynamicPrograming
    {
        /// <summary>
        /// 与えられた、それぞれ1つ限りの品物を、<paramref name="W"/>の容量のナップザックに詰め込む場合に、
        /// 最大の価値を示す値を返します。
        /// </summary>
        /// <remarks>
        /// 実際は、動的計画法のメモ化再帰と分枝限定法のハイブリッドで実装しています。
        /// </remarks>
        /// <param name="W">ナップザックの容量。</param>
        /// <param name="Values">品物の価値を示す配列。</param>
        /// <param name="Weights">品物の重さを示す配列。</param>
        /// <returns>最大の価値を示す値。</returns>
        public static int KnapsackWithFiniteItemByRecursion(int W, int[] Values, int[] Weights)
        {
            var N = Values.Length;
            var Max = 0;
            var Memo = new int[N + 1, W + 1];

            Memo.SetAll(-1);
            Knapsack(0, N, W);

            return Max;

            void Knapsack(int Sum, int n, int w)
            {
                if (w < 0 || Sum <= Memo[n, w])
                {
                    return;
                }

                Max = CommonUtility.Max(Max, Sum);
                Memo[n, w] = Sum;

                for (int i = n - 1; i >= 0; i--)
                {
                    Knapsack(Sum + Values[i], i, w - Weights[i]);
                }
            }
        }

        /// <summary>
        /// 与えられた、それぞれ1つ限りの品物を、<paramref name="W"/>の容量のナップザックに詰め込む場合に、
        /// 最大の価値を示す値を返します。
        /// </summary>
        /// <param name="W">ナップザックの容量。</param>
        /// <param name="Values">品物の価値を示す配列。</param>
        /// <param name="Weights">品物の重さを示す配列。</param>
        /// <returns>最大の価値を示す値。</returns>
        public static int KnapsackWithFiniteItemByNonRecursion(int W, int[] Values, int[] Weights)
        {
            var N = Values.Length;
            var Sum = new int[N + 1, W + 1];

            for (int n = 0; n <= N; n++)
            {
                for (int w = 0; w <= W; w++)
                {
                    if (n == 0 || w == 0)
                    {
                        Sum[n, w] = 0;
                    }
                    else if (Weights[n - 1] <= w)
                    {
                        Sum[n, w] = CommonUtility.Max(Values[n - 1] + Sum[n - 1, w - Weights[n - 1]], Sum[n - 1, w]);
                    }
                    else
                    {
                        Sum[n, w] = Sum[n - 1, w];
                    }
                }
            }

            return Sum[N, W];
        }

        /// <summary>
        /// 与えられた、それぞれ無限個の品物を、<paramref name="W"/>の容量のナップザックに詰め込む場合に、
        /// 最大の価値を示す値を返します。
        /// </summary>
        /// <remarks>
        /// 実際は、動的計画法のメモ化再帰と分枝限定法のハイブリッドで実装しています。
        /// </remarks>
        /// <param name="W">ナップザックの容量。</param>
        /// <param name="Values">品物の価値を示す配列。</param>
        /// <param name="Weights">品物の重さを示す配列。</param>
        /// <returns>最大の価値を示す値。</returns>
        public static int KnapsackWithInfiniteItemByRecursion(int W, int[] Values, int[] Weights)
        {
            var Max = 0;
            var Memo = Enumerable.Repeat(-1, W + 1).ToArray();

            Knapsack(0, W);

            return Max;

            void Knapsack(int Sum, int w)
            {
                if (w < 0 || Sum <= Memo[w])
                {
                    return;
                }

                Max = CommonUtility.Max(Max, Sum);
                Memo[w] = Sum;

                for (int i = 0; i < Values.Length; i++)
                {
                    Knapsack(Sum + Values[i], w - Weights[i]);
                }
            }
        }

        /// <summary>
        /// 与えられた、それぞれ無限個の品物を、<paramref name="W"/>の容量のナップザックに詰め込む場合に、
        /// 最大の価値を示す値を返します。
        /// </summary>
        /// <param name="W">ナップザックの容量。</param>
        /// <param name="Values">品物の価値を示す配列。</param>
        /// <param name="Weights">品物の重さを示す配列。</param>
        /// <returns>最大の価値を示す値。</returns>
        public static int KnapsackWithInfiniteItemByNonRecursion(int W, int[] Values, int[] Weights)
        {
            var N = Values.Length;
            var Sum = new int[N + 1, W + 1];

            for (int n = 0; n <= N; n++)
            {
                for (int w = 0; w <= W; w++)
                {
                    if (n == 0 || w == 0)
                    {
                        Sum[n, w] = 0;
                    }
                    else if (Weights[n - 1] <= w)
                    {
                        Sum[n, w] = CommonUtility.Max(Values[n - 1] + Sum[n, w - Weights[n - 1]], Sum[n - 1, w]);
                    }
                    else
                    {
                        Sum[n, w] = Sum[n - 1, w];
                    }
                }
            }

            return Sum[N, W];
        }

        /// <summary>
        /// 与えられたコインを使って、お金を両替する組み合わせの総数を返します。
        /// </summary>
        /// <param name="Coins">両替に使うコインの配列。</param>
        /// <param name="Money">両替するお金。</param>
        /// <returns>組み合わせの総数。</returns>
        public static int CoinChange(int[] Coins, int Money)
        {
            var n = Coins.Length;
            var Way = new int[n + 1, Money + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= Money; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        Way[i, j] = 0;
                    }
                    else if (Coins[i - 1] == j)
                    {
                        Way[i, j] = Way[i - 1, j] + 1;
                    }
                    else if (Coins[i - 1] < j)
                    {
                        Way[i, j] = Way[i - 1, j] + Way[i, j - Coins[i - 1]];
                    }
                    else
                    {
                        Way[i, j] = Way[i - 1, j];
                    }
                }
            }

            return Way[n, Money];
        }

        /// <summary>
        /// 与えられた配列の部分集合の合計で、最大のものを返します。
        /// </summary>
        /// <param name="Source">配列。</param>
        /// <returns>最大の合計。</returns>
        public static int LargestSumContiguousSubarray(int[] Source)
        {
            var n = Source.Length;
            var Sum = 0;
            var Stock = Source[0];
            var Max = Source[0];

            for (int i = 0; i < n; i++)
            {
                Sum += Source[i];

                if (Sum < Source[i])
                {
                    Stock = Source[i];
                    Sum = Stock;
                }

                Max = CommonUtility.Max(Max, Sum);
            }

            return Max;
        }

        /// <summary>
        /// 与えられた配列の部分集合で、指定する合計になるかどうかを示す値を返します。
        /// </summary>
        /// <param name="Source">配列。</param>
        /// <param name="Sum">合計。</param>
        /// <returns>なる場合は True。そうでなければ False。</returns>
        public static bool IsSubsetSum(int[] Source, int Sum)
        {
            var n = Source.Length;
            var Table = new bool[n + 1, Sum + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= Sum; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        Table[i, j] = false;
                    }
                    else if (Source[i - 1] == j)
                    {
                        Table[i, j] = true;
                    }
                    else if (Source[i - 1] < j)
                    {
                        Table[i, j] = Table[i - 1, j] | Table[i - 1, j - Source[i - 1]];
                    }
                    else
                    {
                        Table[i, j] = Table[i - 1, j];
                    }
                }
            }

            return Table[n, Sum];
        }

        /// <summary>
        /// 与えられた地図の左上から右下に移動する場合の最小のコストを返します。
        /// 移動は右、右下、下の三通りとします。
        /// </summary>
        /// <remarks>
        /// いわゆる、最短経路問題です。
        /// 一般的には、グラフアルゴリズムのダイクストラ法などを考えますが、移動方向が限定的な場合は、動的計画法のボトムアップ方式で十分です。
        /// </remarks>
        /// <param name="Map">地図。</param>
        /// <returns>最小のコスト。</returns>
        public static int MinimumCostPathEasy(int[,] Map)
        {
            var n = Map.GetLength(0);
            var m = Map.GetLength(1);
            var Table = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Table[i, j] = Map[i, j];
                    }
                    else if (i == 0)
                    {
                        Table[i, j] = Table[i, j - 1] + Map[i, j];
                    }
                    else if (j == 0)
                    {
                        Table[i, j] = Table[i - 1, j] + Map[i, j];
                    }
                    else
                    {
                        Table[i, j] = CommonUtility.Min(Table[i - 1, j], Table[i, j - 1], Table[i - 1, j - 1]) + Map[i, j];
                    }
                }
            }

            return Table[n - 1, m - 1];
        }

        /// <summary>
        /// 与えられた正の整数の配列から、数を並び替えて可能な最大値を返します。
        /// </summary>
        /// <remarks>
        /// このメソッドでは動的計画法で全ての組み合わせの中から最大値を見つけていますが、他にも数値を文字列に変換してソートする方法もあります。
        /// </remarks>
        /// <param name="Sources">正の整数の配列。</param>
        /// <returns>最大値。</returns>
        public static int MaxByCombination(int[] Sources)
        {
            var n = Sources.Length;
            var Max = 0;
            var Selected = new int[n];

            Select(0);

            return Max;

            void Select(int Sum)
            {
                if (!Selected.Contains(0))
                {
                    Max = CommonUtility.Max(Max, Sum);

                    return;
                }

                for (int i = 0; i < n; i++)
                {
                    if (Selected[i] == 0)
                    {
                        Selected[i] = 1;
                        Select(Combine(Sum, Sources[i]));
                        Selected[i] = 0;
                    }
                }
            }

            int Combine(int a, int b)
            {
                return int.Parse(a.ToString() + b.ToString());
            }
        }
    }
}
