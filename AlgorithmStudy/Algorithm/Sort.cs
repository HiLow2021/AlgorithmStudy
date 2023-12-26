using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Utilities;

namespace AlgorithmStudy.Algorithm
{
    /// <summary>
    /// 整列アルゴリズムを実装した静的な整列メソッドを提供します。
    /// </summary>
    /// <remarks>
    /// 整列メソッドは「安定性」と「外部記憶の必要性」の二つの要素で簡潔に分類しています。
    /// 「安定性」とは、順序的に同等な要素が複数あったときに、その並びが元のまま保たれるかどうかを表します。
    /// 並びが元のまま保たれれば「安定」 そうでなければ「不安定」となります。
    /// 「外部記憶の必要性」とは、整列する際に、配列の中身を入れ替えるだけで実現できるかどうかを表します。
    /// 実現できれば「内部ソート」 整列させたい配列の他に、余分に記憶領域を確保して、 そちらに一時的にデータを保存しなければならない場合は「外部ソート」と呼びます。
    /// </remarks>
    public static class Sort
    {
        /// <summary>
        /// バブルソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// バブルソートです。「安定」な「内部」ソート。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void BubbleSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    CommonUtility.Swap(list, j, j + 1, (x, y) => 0 < x.CompareTo(y));
                }
            }
        }

        /// <summary>
        /// 選択ソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// 選択ソートです。「安定」な「内部」ソート。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void SelectionSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            int n = list.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int min = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (0 < list[min].CompareTo(list[j]))
                    {
                        min = j;
                    }
                }

                CommonUtility.Swap(list, i, min);
            }
        }

        /// <summary>
        /// 挿入ソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// 挿入ソートです。「安定」な「内部」ソート。
        /// 概ね整列済みの配列に対しては高速に動作するために、クイックソートやマージソートなどの別の整列アルゴリズム内部で部分的に活用される場合があります。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void InsertionSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            int n = list.Count;

            for (int i = 1; i < n; i++)
            {
                for (int j = i; 1 <= j; j--)
                {
                    if (list[j].CompareTo(list[j - 1]) < 0)
                    {
                        CommonUtility.Swap(list, j, j - 1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// シェルソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// シェルソートです。「不安定」な「内部」ソート。
        /// 挿入ソートを改良したもので、挿入ソートが整列済みの配列に強いことを利用した効率の良い整列アルゴリズムです。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void ShellSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            int n = list.Count;
            int h = 1;

            while (h < n / 9)
            {
                h = h * 3 + 1;
            }
            for (; 0 < h; h /= 3)
            {
                for (int i = h; i < n; i++)
                {
                    for (int j = i; h <= j; j -= h)
                    {
                        if (list[j].CompareTo(list[j - h]) < 0)
                        {
                            CommonUtility.Swap(list, j, j - h);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// クイックソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// クイックソートです。「不安定」な「内部」ソート。
        /// いわゆる、分割統治法的な考え方に基づいて、軸となる要素以上と以下で配列を大まかにソート → 配列を二つに分割という処理を再帰的に繰り返します。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void QuickSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            QuickSort(list, 0, list.Count - 1);

            void QuickSort(IList<T> listInner, int left, int right)
            {
                if (left < right)
                {
                    var pivot = SelectPivot(listInner, left, right);
                    var p = Partition(listInner, pivot, left, right);

                    QuickSort(listInner, left, p - 1);
                    QuickSort(listInner, p, right);
                }
            }

            int Partition(IList<T> listInner, T pivot, int left, int right)
            {
                var l = left;
                var r = right;

                // 検索が交差するまで繰り返します。
                while (l <= r)
                {
                    // 軸要素以上のデータを探します。
                    while (l < right && listInner[l].CompareTo(pivot) < 0)
                    {
                        l++;
                    }
                    // 軸要素未満のデータを探します。
                    while (left < r && 0 <= listInner[r].CompareTo(pivot))
                    {
                        r--;
                    }
                    if (r < l)
                    {
                        break;
                    }

                    CommonUtility.Swap(listInner, l, r);
                    l++;
                    r--;
                }

                return l;
            }

            T SelectPivot(IList<T> listInner, int left, int right)
            {
                var median = MedianInThreeValues(listInner, left, (left + right) / 2, right);

                return listInner[median];
            }
        }

        /// <summary>
        /// クイックソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// クイックソートです。「不安定」な「内部」ソート。
        /// いわゆる、分割統治法的な考え方に基づいて、軸となる要素以上と以下で配列を大まかにソート → 配列を二つに分割という処理を再帰的に繰り返します。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void QuickSortAnother<T>(IList<T> list)
            where T : IComparable<T>
        {
            QuickSort(list, 0, list.Count - 1);

            void QuickSort(IList<T> listInner, int left, int right)
            {
                if (left < right)
                {
                    var pivot = SelectPivot(listInner, left, right);
                    var p = Partition(listInner, pivot, left, right);

                    QuickSort(listInner, left, p - 1);
                    QuickSort(listInner, p + 1, right);
                }
            }

            int Partition(IList<T> listInner, int pivot, int left, int right)
            {
                CommonUtility.Swap(listInner, pivot, right);

                var store = left;

                for (int i = left; i < right; i++)
                {
                    if (listInner[i].CompareTo(listInner[right]) <= 0)
                    {
                        CommonUtility.Swap(listInner, i, store);
                        store++;
                    }
                }

                CommonUtility.Swap(listInner, store, right);

                return store;
            }

            int SelectPivot(IList<T> listInner, int left, int right)
            {
                return MedianInThreeValues(listInner, left, (left + right) / 2, right);
            }
        }

        /// <summary>
        /// マージソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// マージソートです。「安定」な「外部」ソート。
        /// いわゆる、分割統治法的な考え方に基づいて、配列を再帰的に分割していき、整列させながら再び併合(マージ)していきます。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void MergeSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            var n = list.Count;
            var work = new T[n / 2];

            MergeSort(list, 0, n, work);

            void MergeSort(IList<T> listInner, int left, int right, T[] workInner)
            {
                if (1 < right - left)
                {
                    var mid = (left + right) / 2;

                    MergeSort(listInner, left, mid, workInner);
                    MergeSort(listInner, mid, right, workInner);
                    Merge(listInner, left, mid, right, workInner);
                }
            }

            void Merge(IList<T> listInner, int left, int mid, int right, T[] workInner)
            {
                int i, j, k;

                for (i = left, j = 0; i < mid; i++, j++)
                {
                    workInner[j] = listInner[i];
                }

                mid -= left;

                for (j = 0, k = left; i < right && j < mid; k++)
                {
                    if (listInner[i].CompareTo(workInner[j]) < 0)
                    {
                        listInner[k] = listInner[i];
                        i++;
                    }
                    else
                    {
                        listInner[k] = workInner[j];
                        j++;
                    }
                }
                for (; i < right; i++, k++)
                {
                    listInner[k] = listInner[i];
                }
                for (; j < mid; j++, k++)
                {
                    listInner[k] = workInner[j];
                }
            }
        }

        /// <summary>
        /// ヒープソートで、<see cref="IList{T}"/> 全体内の要素を並べ替えます。
        /// </summary>
        /// <remarks>
        /// ヒープソートです。「不安定」な「内部」ソート。
        /// ヒープ(常に優先度最大の要素を取り出せるデータ構造)を用いて、配列を整列させます。
        /// </remarks>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        public static void HeapSort<T>(IList<T> list)
            where T : IComparable<T>
        {
            MakeHeap(list);
            HeapSortInner(list);

            void MakeHeap(IList<T> listInner)
            {
                var n = list.Count;

                for (int i = 1; i < n; i++)
                {
                    for (int j = i, k; j != 0;)
                    {
                        k = (j - 1) / 2;
                        CommonUtility.Swap(listInner, j, k, (x, y) => 0 < x.CompareTo(y));
                        j = k;
                    }
                }
            }

            void HeapSortInner(IList<T> listInner)
            {
                for (int i = listInner.Count - 1; i > 0; i--)
                {
                    CommonUtility.Swap(listInner, 0, i);

                    for (int j = 0, k; (k = 2 * j + 1) < i;)
                    {
                        if ((k != i - 1) && (listInner[k].CompareTo(listInner[k + 1]) < 0))
                        {
                            k++;
                        }

                        CommonUtility.Swap(listInner, j, k, (x, y) => x.CompareTo(y) < 0);
                        j = k;
                    }
                }
            }
        }

        /// <summary>
        /// 3つの要素の中央値を求めます。
        /// </summary>
        /// <typeparam name="T">要素の型。</typeparam>
        /// <param name="list">リスト。</param>
        /// <param name="a">1番目の要素を示すインデックス。</param>
        /// <param name="b">2番目の要素を示すインデックス。</param>
        /// <param name="c">3番目の要素を示すインデックス。</param>
        /// <returns>中央値の要素を示すインデックス。</returns>
        private static int MedianInThreeValues<T>(IList<T> list, int a, int b, int c)
          where T : IComparable<T>
        {
            if (0 < list[a].CompareTo(list[b])) CommonUtility.Swap(ref a, ref b);
            if (0 < list[a].CompareTo(list[c])) CommonUtility.Swap(ref a, ref c);
            if (0 < list[b].CompareTo(list[c])) CommonUtility.Swap(ref b, ref c);

            return b;
        }
    }
}
