using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Utilities;

namespace AlgorithmStudy.Algorithm.Search
{
    /// <summary>
    /// 文字列探索に関する静的メソッドを提供します。
    /// </summary>
    public static class StringSearch
    {
        /// <summary>
        /// 指定した部分文字列が、対象の文字列内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// いわゆる、力まかせ法です。部分文字列の探索を、対象の文字列の探索範囲を一文字ずつ右にずらして行います。
        /// </remarks>
        /// <param name="source">対象の文字列。</param>
        /// <param name="pattern">探索する部分文字列。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool BruteForceStringSearch(char[] source, char[] pattern)
        {
            int n = source.Length - 1;
            int m = pattern.Length - 1;

            for (int i = 0; i <= n - m + 1; i++)
            {
                int j = 0;

                while (j <= m && source[i + j] == pattern[j])
                {
                    j++;
                }
                if (m < j)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 指定した部分文字列が、対象の文字列内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// いわゆる、力まかせ法です。部分文字列の探索を、対象の文字列の探索範囲を一文字ずつ右にずらして行います。
        /// </remarks>
        /// <param name="source">対象の文字列。</param>
        /// <param name="pattern">探索する部分文字列。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool BruteForceStringSearch(string source, string pattern)
        {
            return BruteForceStringSearch(source?.ToArray(), pattern?.ToArray());
        }

        /// <summary>
        /// 指定した部分文字列が、対象の文字列内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// クヌース・モリス・プラット法です。
        /// 部分文字列の探索を、文字列の探索範囲の先頭から行い、あらかじめ作成しておいたずらし表を使って、比較回数を減らすように工夫します。
        /// </remarks>
        /// <param name="source">対象の文字列。</param>
        /// <param name="pattern">探索する部分文字列。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool KnuthMorrisPrattStringSearch(char[] source, char[] pattern)
        {
            int n = source.Length - 1;
            int m = pattern.Length - 1;
            var shiftTable = CreateShiftTable(pattern);

            for (int i = 0, j = 0; i <= n - m + 1;)
            {
                while (i <= n && j <= m && source[i] == pattern[j])
                {
                    i++;
                    j++;
                }
                if (j == 0)
                {
                    i++;
                }
                else if (j <= m)
                {
                    j = shiftTable[j];
                }
                else
                {
                    return true;
                }
            }

            return false;

            // ずらし表の作成をします。
            // パターンとなる部分文字列と同じ部分文字列の比較を、開始位置を1つ右にずらして行います。
            // 一致しない時点で何文字一致していたかが、ずらし位置です。一文字目は0とします。
            int[] CreateShiftTable(char[] patternInner)
            {
                int l = patternInner.Length;
                var shiftTableInner = new int[l];

                shiftTableInner[0] = 0;

                for (int i = 1, j = 0; i < l; i++)
                {
                    shiftTableInner[i] = j;

                    if (patternInner[i] == patternInner[j])
                    {
                        j++;
                    }
                    else
                    {
                        j = 0;
                    }
                }

                return shiftTableInner;
            }
        }

        /// <summary>
        /// 指定した部分文字列が、対象の文字列内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// クヌース・モリス・プラット法です。
        /// 部分文字列の探索を、文字列の探索範囲の先頭から行い、あらかじめ作成しておいたずらし表を使って、比較回数を減らすように工夫します。
        /// </remarks>
        /// <param name="source">対象の文字列。</param>
        /// <param name="pattern">探索する部分文字列。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool KnuthMorrisPrattStringSearch(string source, string pattern)
        {
            return KnuthMorrisPrattStringSearch(source?.ToArray(), pattern?.ToArray());
        }

        /// <summary>
        /// 指定した部分文字列が、対象の文字列内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// ボイヤー・ムーア法です。
        /// 部分文字列の探索を、文字列の探索範囲の末尾から行い、一致しなければ、あらかじめ作成しておいたずらし表に従って右にずらします。
        /// </remarks>
        /// <param name="source">対象の文字列。</param>
        /// <param name="pattern">探索する部分文字列。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool BoyerMooreStringSearch(char[] source, char[] pattern)
        {
            int n = source.Length - 1;
            int m = pattern.Length - 1;
            var shiftTable = CreateShiftTable(pattern);

            for (int i = m; i <= n;)
            {
                int j = m;

                while (0 <= j && source[i] == pattern[j])
                {
                    i--;
                    j--;
                }
                if (0 <= j)
                {
                    i += m - j; // 文字列の探索範囲における探索位置をいったん末尾に戻します。その上で、ずらし表に従って右にずらします。
                    i += shiftTable.TryGetValue(source[i], out int Value) ? Value : m + 1;
                }
                else
                {
                    return true;
                }
            }

            return false;

            Dictionary<char, int> CreateShiftTable(char[] patternInner)
            {
                int l = patternInner.Length - 1;
                var shiftTableInner = new Dictionary<char, int>();

                for (int i = l - 1; i >= 0; i--)
                {
                    if (!shiftTableInner.ContainsKey(patternInner[i]))
                    {
                        shiftTableInner.Add(patternInner[i], l - i);
                    }
                }

                return shiftTableInner;
            }
        }

        /// <summary>
        /// 指定した部分文字列が、対象の文字列内に存在するかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// ボイヤー・ムーア法です。
        /// 部分文字列の探索を、文字列の探索範囲の末尾から行い、一致しなければ、あらかじめ作成しておいたずらし表に従って右にずらします。
        /// </remarks>
        /// <param name="source">対象の文字列。</param>
        /// <param name="pattern">探索する部分文字列。</param>
        /// <returns>存在する場合は True。そうでなければ False。</returns>
        public static bool BoyerMooreStringSearch(string source, string pattern)
        {
            return BoyerMooreStringSearch(source?.ToArray(), pattern?.ToArray());
        }

        /// <summary>
        /// 2つの文字列のレーベンシュタイン距離(編集距離)を返します。
        /// </summary>
        /// <remarks>
        /// レーベンシュタイン距離を計算するためには、一般的に、動的計画法のボトムアップ方式(Tabulation)によるアルゴリズムが用いられます。
        /// メソッド内では、挿入・削除・置換のそれぞれのコストを 1 に設定していますが、これらのコストには別々の値を割り振る事も可能です。
        /// たとえば、挿入・削除のみを許可して、置換を禁止するタイプのレーベンシュタイン距離は、
        /// 挿入・削除にコスト 1、置換にコスト 2 が割り振られるレーベンシュタイン距離と等価となります。
        /// </remarks>
        /// <param name="source1">1番目の文字列。</param>
        /// <param name="source2">2番目の文字列。</param>
        /// <returns>レーベンシュタイン距離。</returns>
        public static int LevenshteinDistanceSearch(char[] source1, char[] source2)
        {
            var n = source1.Length;
            var m = source2.Length;
            var table = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i == 0)
                    {
                        table[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        table[i, j] = i;
                    }
                    else
                    {
                        var Cost = (source1[i - 1] == source2[j - 1]) ? 0 : 1;

                        table[i, j] = CommonUtility.Min(table[i - 1, j] + 1, table[i, j - 1] + 1, table[i - 1, j - 1] + Cost);
                    }
                }
            }

            return table[n, m];
        }

        /// <summary>
        /// 2つの文字列のレーベンシュタイン距離(編集距離)を返します。
        /// </summary>
        /// <remarks>
        /// レーベンシュタイン距離を計算するためには、一般的に、動的計画法のボトムアップ方式(Tabulation)によるアルゴリズムが用いられます。
        /// メソッド内では、挿入・削除・置換のそれぞれのコストを 1 に設定していますが、これらのコストには別々の値を割り振る事も可能です。
        /// たとえば、挿入・削除のみを許可して、置換を禁止するタイプのレーベンシュタイン距離は、
        /// 挿入・削除にコスト 1、置換にコスト 2 が割り振られるレーベンシュタイン距離と等価となります。
        /// </remarks>
        /// <param name="source1">1番目の文字列。</param>
        /// <param name="source2">2番目の文字列。</param>
        /// <returns>レーベンシュタイン距離。</returns>
        public static int LevenshteinDistanceSearch(string source1, string source2)
        {
            return LevenshteinDistanceSearch(source1?.ToArray(), source2?.ToArray());
        }

        /// <summary>
        /// 2つの文字列のどちらにも含まれる部分列のうち、最も長い部分列長を返します。
        /// </summary>
        /// <remarks>
        /// いわゆる、最長共通部分列問題(Longest-Common Subsequence Problem)です。
        /// 一般的に、動的計画法のボトムアップ方式(Tabulation)によるアルゴリズムが用いられます。
        /// </remarks>
        /// <param name="source1">1番目の文字列。</param>
        /// <param name="source2">2番目の文字列。</param>
        /// <returns>最も長い部分列長。</returns>
        public static int LongestCommonSubSequenceSearch(char[] source1, char[] source2)
        {
            var n = source1.Length;
            var m = source2.Length;
            var table = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        table[i, j] = 0;
                    }
                    else if (source1[i - 1] == source2[j - 1])
                    {
                        table[i, j] = table[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        table[i, j] = CommonUtility.Max(table[i - 1, j], table[i, j - 1]);
                    }
                }
            }

            return table[n, m];
        }

        /// <summary>
        /// 2つの文字列のどちらにも含まれる部分列のうち、最も長い部分列長を返します。
        /// </summary>
        /// <remarks>
        /// いわゆる、最長共通部分列問題(Longest-Common Subsequence Problem)です。
        /// 一般的に、動的計画法のボトムアップ方式(Tabulation)によるアルゴリズムが用いられます。
        /// </remarks>
        /// <param name="source1">1番目の文字列。</param>
        /// <param name="source2">2番目の文字列。</param>
        /// <returns>最も長い部分列長。</returns>
        public static int LongestCommonSubSequenceSearch(string source1, string source2)
        {
            return LongestCommonSubSequenceSearch(source1?.ToArray(), source2?.ToArray());
        }

        /// <summary>
        /// 2つの文字列のどちらにも含まれる文字列のうち、最も長い文字列長を返します。
        /// </summary>
        /// <remarks>
        /// いわゆる、最長共通部分文字列問題(Longest-Common Substring Problem)です。
        /// 一般的に、動的計画法のボトムアップ方式(Tabulation)によるアルゴリズムが用いられます。
        /// </remarks>
        /// <param name="source1">1番目の文字列。</param>
        /// <param name="source2">2番目の文字列。</param>
        /// <returns>最も長い文字列長。</returns>
        public static int LongestCommonSubStringSearch(char[] source1, char[] source2)
        {
            var n = source1.Length;
            var m = source2.Length;
            var table = new int[n + 1, m + 1];
            var result = 0;

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= m; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        table[i, j] = 0;
                    }
                    else if (source1[i - 1] == source2[j - 1])
                    {
                        table[i, j] = table[i - 1, j - 1] + 1;
                        result = CommonUtility.Max(result, table[i, j]);
                    }
                    else
                    {
                        table[i, j] = 0;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 2つの文字列のどちらにも含まれる文字列のうち、最も長い文字列長を返します。
        /// </summary>
        /// <remarks>
        /// いわゆる、最長共通部分文字列問題(Longest-Common Substring Problem)です。
        /// 一般的に、動的計画法のボトムアップ方式(Tabulation)によるアルゴリズムが用いられます。
        /// </remarks>
        /// <param name="source1">1番目の文字列。</param>
        /// <param name="source2">2番目の文字列。</param>
        /// <returns>最も長い文字列長。</returns>
        public static int LongestCommonSubStringSearch(string source1, string source2)
        {
            return LongestCommonSubStringSearch(source1?.ToArray(), source2?.ToArray());
        }
    }
}
