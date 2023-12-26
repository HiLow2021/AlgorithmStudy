using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Utilities;

namespace AlgorithmStudy.Algorithm
{
    /// <summary>
    /// 数学に関係する静的メソッドを提供します。
    /// </summary>
    public static class Mathematics
    {
        /// <summary>
        /// 指定する整数の絶対値を返します。
        /// </summary>
        /// <param name="n">整数。</param>
        /// <returns>絶対値。</returns>
        public static int Absolute(int n)
        {
            return (int)Absolute((long)n);
        }

        /// <summary>
        /// 指定する整数の絶対値を返します。
        /// </summary>
        /// <param name="n">整数。</param>
        /// <returns>絶対値。</returns>
        public static long Absolute(long n)
        {
            // 正の数は、負の数の補数(ビットごとの論理否定)に 1 を足すことで求められます。
            return (0 <= n) ? n : ~n + 1;
        }

        /// <summary>
        /// 指定する数値の絶対値を返します。
        /// </summary>
        /// <param name="n">数値。</param>
        /// <returns>絶対値。</returns>
        public static double Absolute(double n)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 指定する数値の平方根を返します。
        /// </summary>
        /// <remarks>
        /// バビロニア人の方法と呼ばれるアルゴリズムで実装しています。
        /// </remarks>
        /// <param name="s">数値。</param>
        /// <returns>平方根。</returns>
        public static double SquareRoot(double s)
        {
            if (s < 0) { return double.NaN; }

            double x = 0.5 * s;
            double temp = 0;

            while (x != temp)
            {
                temp = x;
                x = 0.5 * (x + s / x);
            }

            return x;
        }

        /// <summary>
        /// 与えられた数値を指定の範囲内に収めます。
        /// </summary>
        /// <param name="value">数値。</param>
        /// <param name="min">範囲の下限。</param>
        /// <param name="max">範囲の上限。</param>
        /// <returns>範囲内の数値。</returns>
        public static double Clamp(double value, double min, double max)
        {
            if (min > max) { throw new ArgumentOutOfRangeException(nameof(min) + "が" + nameof(max) + "より大きい値です。"); }

            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 与えられた数値を指定の範囲内で正規化します。
        /// </summary>
        /// <param name="value">数値。</param>
        /// <param name="min">数値が取り得る最小の値。</param>
        /// <param name="max">数値が取り得る最大の値。</param>
        /// <param name="normalizedMin">正規化の範囲の下限。</param>
        /// <param name="normalizedMax">正規化の範囲の上限。</param>
        /// <returns>正規化された数値。</returns>
        public static double Normalize(double value, double min, double max, double normalizedMin, double normalizedMax)
        {
            if (min >= max) { throw new ArgumentOutOfRangeException(nameof(min) + "が" + nameof(max) + "以上の値です。"); }
            if (normalizedMin > normalizedMax) { throw new ArgumentOutOfRangeException(nameof(normalizedMin) + "が" + nameof(normalizedMax) + "より大きい値です。"); }

            return (normalizedMax - normalizedMin) * (value - min) / (max - min) + normalizedMin;
        }

        /// <summary>
        /// 2つの整数の最小公倍数を返します。
        /// </summary>
        /// <remarks>
        /// 有名なユークリッドの互除法を用いて実装しています。
        /// </remarks>
        /// <param name="a">1番目の整数。</param>
        /// <param name="b">2番目の整数。</param>
        /// <returns>最小公倍数。</returns>
        public static int LeastCommonMultiple(int a, int b)
        {
            return Absolute(a * b / GreatestCommonDivisor(a, b));
        }

        /// <summary>
        /// 2つの整数の最大公約数を返します。
        /// </summary>
        /// <remarks>
        /// 有名なユークリッドの互除法を用いて実装しています。
        /// </remarks>
        /// <param name="a">1番目の整数。</param>
        /// <param name="b">2番目の整数。</param>
        /// <returns>最大公約数。</returns>
        public static int GreatestCommonDivisor(int a, int b)
        {
            CommonUtility.Swap(ref a, ref b, (x, y) => x < y);

            int r = a % b;

            while (r != 0)
            {
                a = b;
                b = r;
                r = a % b;
            }

            return Absolute(b);
        }

        /// <summary>
        /// 指定する自然数が、素数であるかどうかを示す値を返します。
        /// </summary>
        /// <remarks>
        /// 指定された数 n が合成数である場合、その平方根以下で必ず割り切れる数が存在します。
        /// なぜなら、合成数なら n = q * r (q、r は整数)が成立するからです。
        /// したがって、n の平方根以下の範囲で n が割り切れるかどうかを判断すれば良いことになります。
        /// また、メソッド内では処理の高速化のために、先に n が 2 で割り切れるかどうかを判断して繰り返し文の回数を減らす工夫をしています。
        /// </remarks>
        /// <param name="n">自然数。</param>
        /// <returns>素数である場合は True。そうでなければ False。</returns>
        public static bool IsPrimeNumber(int n)
        {
            if (n < 2) { throw new ArgumentOutOfRangeException(nameof(n) + "を2以上の整数にしてください。"); }
            if (n == 2) { return true; }

            // これは、((n % 2) == 1)と同様の意味になります。
            if ((n & 1) == 0)
            {
                return false;
            }
            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 指定する自然数以下の素数を入れた配列を返します。
        /// </summary>
        /// <remarks>
        /// エラトステネスの篩と呼ばれる素数判定のアルゴリズムを使用しています。
        /// 指定された数 n 以下のすべての素数を発見するアルゴリズムです。 
        /// 古代ギリシャの哲学者エラトステネスが考案したため、その名を冠しています。 
        /// また、メソッド内では処理の高速化のために、先に 2 の倍数だけ除外して繰り返し文の回数を減らす工夫をしています。
        /// </remarks>
        /// <param name="n">自然数。</param>
        /// <returns>素数の配列。</returns>
        public static int[] GetPrimeNumbers(int n)
        {
            if (n < 2) { throw new ArgumentOutOfRangeException(nameof(n) + "を2以上の整数にしてください。"); }

            var Searchs = Enumerable.Repeat(true, n - 1).ToArray();

            for (int i = 2; i <= n - 2; i += 2)
            {
                Searchs[i] = false;
            }
            for (int i = 3; i * i <= n; i += 2)
            {
                if (Searchs[i - 2])
                {
                    for (int j = i * i; j <= n; j += i)
                    {
                        Searchs[j - 2] = false;
                    }
                }
            }

            var Primes = new List<int>();

            for (int i = 0; i < Searchs.Length; i++)
            {
                if (Searchs[i])
                {
                    Primes.Add(i + 2);
                }
            }

            return Primes.ToArray();
        }

        /// <summary>
        /// 指定する整数の階乗を返します。
        /// </summary>
        /// <remarks>
        /// 階乗とは 1 から n までのすべての整数の積です。0 の階乗は 1 になります。
        /// </remarks>
        /// <param name="n">整数。</param>
        /// <returns>階乗数。</returns>
        public static long Factorial(int n)
        {
            if (n < 0) { throw new ArgumentOutOfRangeException(nameof(n) + "を0以上の整数にしてください。"); }

            if (n == 0)
            {
                return 1;
            }
            for (int i = n - 1; i > 0; i--)
            {
                n *= i;
            }

            return n;
        }

        /// <summary>
        /// 指定の整数を指定する回数で累乗した結果を返します。
        /// </summary>
        /// <param name="x">累乗対象の整数。</param>
        /// <param name="n">累乗回数。</param>
        /// <returns>累乗結果。</returns>
        public static long Power(int x, int n)
        {
            // x^n 計算量O(logn)
            long res = 1;

            while (n > 0)
            {
                // 2のn乗の剰余がほしい場合は、2のn乗 - 1 で論理積をかければいいので、
                // これは、((n % 2) == 1)と同様の意味になります。
                // ただし、nが負の場合は、誤りが生じる可能性があります。
                if ((n & 1) == 1)
                {
                    res = res * x;
                }

                x = x * x; // 一周する度にx, x^2, x^4, x^8となる
                n >>= 1; // 桁をずらす n = n >> 1
            }

            return res;
        }

        /// <summary>
        /// 指定する整数番目のフィボナッチ数を返します。
        /// </summary>
        /// <remarks>
        /// ある項が、直前の 2 つの項の和となるフィボナッチ数列の n 番目の項を求めます。最初の 2 項は 0、1 です。
        /// 再帰関数を用いる方式の方が一般的ですが、処理速度を考慮して繰り返し文で実装しています。
        /// </remarks>
        /// <param name="n">整数。</param>
        /// <returns>フィボナッチ数。</returns>
        public static long Fibonacci(int n)
        {
            long a = 0;
            long b = 1;

            for (int i = 0; i < n; i++)
            {
                long temp = a;
                a = b;
                b = temp + a;
            }

            return a;
        }

        /// <summary>
        /// 指定する整数番目の N ナッチ数を返します。
        /// </summary>
        /// <remarks>
        /// ある項が、直前の N 個の項の和となる N ナッチ数列の n 番目の項を求めます。最初の N 項は 0、0、...、1 です。
        /// 再帰関数を用いる方式は現実的ではないので、繰り返し文で実装します。
        /// </remarks>
        /// <param name="N"><paramref name="N"/>ナッチ数列。</param>
        /// <param name="n">整数。</param>
        /// <returns>フィボナッチ数。</returns>
        public static long Nnacci(int N, int n)
        {
            if (N < 2) { throw new ArgumentOutOfRangeException(nameof(N) + "を2以上の整数にしてください。"); }

            long[] arr = new long[N];

            for (int i = 0; i < N - 1; i++)
            {
                arr[i] = 0;
            }

            arr[N - 1] = 1;

            for (int i = 0; i < n; i++)
            {
                long sum = 0;

                for (int j = 0; j < N - 1; j++)
                {
                    sum += arr[j];
                    arr[j] = arr[j + 1];
                }

                arr[N - 1] += sum;
                sum = 0;
            }

            return arr[0];
        }
    }
}
