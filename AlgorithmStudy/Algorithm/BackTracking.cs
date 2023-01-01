using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;

namespace AlgorithmStudy.Algorithm
{
    /// <summary>
    /// バックトラック法で、問題解決を図っている実装例の集まりです。
    /// </summary>
    public static class BackTracking
    {
        /// <summary>
        /// 海と島をそれぞれ 0、1 で表現した地図上にある島の数を返します。
        /// </summary>
        /// <remarks>
        /// 2次元配列上の固まったブロックを認識することに応用できるアルゴリズムなので、非常に有用です。
        /// </remarks>
        /// <param name="map">地図。</param>
        /// <returns>地図上の島の数。</returns>
        public static int CountIsland(int[,] Map)
        {
            int[,] map = (int[,])Map.Clone();
            int Count = 0;
            int[] vx = new int[] { 0, 1, 0, -1 }; // 左から順に、北、東、南、西のベクトルを表します。
            int[] vy = new int[] { -1, 0, 1, 0 }; // 同上です。

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (IsIsland(x, y))
                    {
                        RemoveIsland(x, y);
                        Count++;
                    }
                }
            }

            return Count;

            bool IsIsland(int x, int y)
            {
                return map.IsWithinRange(y, x) && map[y, x] == 1;
            }

            void RemoveIsland(int x, int y)
            {
                map[y, x] = 0;

                for (int i = 0; i < vx.Length; i++)
                {
                    if (IsIsland(x + vx[i], y + vy[i]))
                    {
                        RemoveIsland(x + vx[i], y + vy[i]);
                    }
                }
            }
        }

        /// <summary>
        /// グラフ彩色問題の解を全て返します。
        /// </summary>
        /// <remarks>
        /// グラフ彩色問題とは、与えられたグラフの隣接する頂点同士が異なる色になるように彩色する問題です。
        /// </remarks>
        /// <param name="Graph">グラフ。</param>
        /// <param name="m">色の数。</param>
        /// <returns>全ての解。</returns>
        public static List<int[]> GraphColoring(int[,] Graph, int m)
        {
            var n = Graph.GetLength(0);
            var Colors = Enumerable.Repeat(-1, n).ToArray();
            var Result = new List<int[]>();

            for (int i = 0; i < m; i++)
            {
                SetColor(Graph, Colors, 0, i);
            }

            return Result;

            bool IsValid(int[,] graph, int[] colors, int v, int c)
            {
                for (int i = 0; i < n; i++)
                {
                    if (graph[v, i] == 1 && colors[i] != -1 && colors[i] == c)
                    {
                        return false;
                    }
                }

                return true;
            }

            void SetColor(int[,] graph, int[] colors, int v, int c)
            {
                Colors[v] = c;

                if (!colors.Contains(-1))
                {
                    Result.Add((int[])colors.Clone());

                    return;
                }
                for (int i = v + 1; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (IsValid(graph, colors, i, j))
                        {
                            SetColor(graph, colors, i, j);
                            colors[i] = -1;
                        }
                    }
                }
            }
        }
    }
}
