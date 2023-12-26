using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy.Algorithm.Search
{
    /// <summary>
    /// グラフ探索に関する静的メソッドを提供します。
    /// </summary>
    public static class GraphSearch
    {
        /// <summary>
        /// 指定した隣接行列の始点ノードから終点ノードまでの最短距離を示す数値を返します。
        /// </summary>
        /// <param name="graph">隣接行列。</param>
        /// <param name="start">始点ノード。</param>
        /// <param name="destination">終点ノード。</param>
        /// <returns>最短距離を示す数値。</returns>
        public static int DijkstraSearch(int[,] graph, int start, int destination)
        {
            if (graph == null) { throw new ArgumentNullException(nameof(graph)); }

            var n = graph.GetLength(0);
            var vertex = new My.Collections.PriorityQueue<int, int>();
            var shortestPath = Enumerable.Repeat(int.MaxValue, n).ToArray();

            shortestPath[start] = 0;
            vertex.Enqueue(start, shortestPath[start]);

            while (vertex.Count != 0)
            {
                var v = vertex.Dequeue();

                for (int i = 0; i < n; i++)
                {
                    if (0 < graph[v, i] && shortestPath[v] + graph[v, i] < shortestPath[i])
                    {
                        shortestPath[i] = shortestPath[v] + graph[v, i];

                        if (!vertex.Contains(i))
                        {
                            vertex.Enqueue(i, shortestPath[i]);
                        }
                    }
                }
            }

            return shortestPath[destination];
        }
    }
}
