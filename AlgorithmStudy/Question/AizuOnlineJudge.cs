using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Extensions;

namespace AlgorithmStudy.Question
{
    public static class AizuOnlineJudge
    {
        /// <summary>
        /// AOJ 557の解答を返します。 
        /// </summary>
        /// <remarks>
        /// 動的計画法のメモ化を利用して実装しています。相当に頭を悩ませた問題でした。
        /// </remarks>
        /// <param name="Source">入力。</param>
        /// <returns>出力。</returns>
        public static long AFirstGrader(int[] Source)
        {
            var n = Source.Length - 1;
            var Memo = new long[21, 101];

            Memo.SetAll(-1);

            return (1 < n) ? Calculate(Source[0], 1) : 0;

            long Calculate(int Sum, int Index)
            {
                if (Sum < 0 || 20 < Sum)
                {
                    return 0;
                }
                if (Index == n)
                {
                    if (Sum == Source[Index])
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                if (Memo[Sum, Index] != -1)
                {
                    return Memo[Sum, Index];
                }

                var Add = Calculate(Sum + Source[Index], Index + 1);
                var Sub = Calculate(Sum - Source[Index], Index + 1);

                return Memo[Sum, Index] = Add + Sub;
            }
        }

        /// <summary>
        /// AOJ 1173の解答を返します。
        /// </summary>
        /// <remarks>
        /// 与えられた文字列中の括弧の配置が適切かどうかを判断する問題でした。
        /// </remarks>
        /// <param name="Source">入力。</param>
        /// <returns>出力。</returns>
        public static bool CheckParentheses(string Source)
        {
            var Chars = Source.ToArray();
            var Parentheses = new char[] { '(', ')', '[', ']' };
            var sb = new StringBuilder();

            foreach (var c in Chars)
            {
                if (Parentheses.Contains(c))
                {
                    sb.Append(c);
                }
            }

            sb.Replace("()", string.Empty);
            sb.Replace("[]", string.Empty);

            return sb.Length == 0;
        }
    }
}
