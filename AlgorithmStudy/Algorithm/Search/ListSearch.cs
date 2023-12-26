using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmStudy.Algorithm.Search
{
    /// <summary>
    /// リストの線形探索に関する静的メソッドを提供します。
    /// </summary>
    public static class ListSearch
    {
        /// <summary>
        /// 指定した要素が、対象のリスト内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">対象のリスト。</param>
        /// <param name="source">探索する要素。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool LinearSearch<T>(IList<T> list, T source)
        {
            int n = list.Count - 1;

            for (int i = 0; i <= n; i++)
            {
                if (list[i].Equals(source))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 指定した要素が、対象のリスト内に存在するかどうかを示す値を返します。
        /// リストが整列済み(昇順or降順)でない場合、結果が正しく返らないので注意してください。
        /// </summary>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">対象のリスト。</param>
        /// <param name="source">探索する要素。</param>
        /// <param name="isAscendingOrder">リストが昇順で整列されているかどうか。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool BinarySearch<T>(IList<T> list, T source, bool isAscendingOrder)
            where T : IComparable<T>
        {
            int left = 0;
            int right = list.Count - 1;

            while (left <= right)
            {
                // シフト演算で右に1ずらすことは、2で割ったことと同じ意味になります。
                int mid = (left + right) >> 1;

                if (list[mid].CompareTo(source) < 0)
                {
                    if (isAscendingOrder)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }
                else if (list[mid].CompareTo(source) > 0)
                {
                    if (isAscendingOrder)
                    {
                        right = mid - 1;
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
