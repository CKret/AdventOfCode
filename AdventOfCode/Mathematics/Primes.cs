using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Mathematics
{
    public static class Primes
    {
        private const int maxLimit = 40000000;
        private static readonly bool[] primes = InitializeSieve();

        private static bool[] InitializeSieve()
        {
            // Eratosthenes Sieve
            var p = new bool[maxLimit];
            for (var i = 0; i < maxLimit; i++) p[i] = true;
            p[0] = p[1] = false; // 0 and 1 are not primes.

            for (long i = 2; i < maxLimit; i++)
            {
                for (var j = i * i; j < maxLimit; j += i) p[j] = false;
            }

            return p;
        }

        public static IEnumerable<long> Sequence()
        {
            return Sequence(primes.Length);
        }

        public static IEnumerable<long> Sequence(int limit)
        {
            limit = limit < maxLimit ? limit : maxLimit;

            for (var i = 0; i <= limit; i++)
            {
                if (!primes[i]) continue;

                yield return i;
            }
        }

        public static bool IsPrime(this int number)
        {
            return primes[number];
        }

        public static bool IsPrime(this long number)
        {
            return primes[number];
        }

        public static bool IsPrimeMillerRabin(this long number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            if (number < 9) return true;
            if (number % 3 == 0) return false;
            if (number % 5 == 0) return false;

            var ar = new[] { 2, 3 };
            return ar.All(t => !Witness(t, number));
        }

        private static bool Witness(long a, long n)
        {
            var t = 0;
            var u = n - 1;
            while ((u & 1) == 0)
            {
                t++;
                u >>= 1;
            }

            var xi1 = ModularExp(a, u, n);

            for (var i = 0; i < t; i++)
            {
                var xi2 = xi1 * xi1 % n;
                if ((xi2 == 1) && (xi1 != 1) && (xi1 != (n - 1))) return true;
                xi1 = xi2;
            }
            return xi1 != 1;
        }

        private static long ModularExp(long a, long b, long n)
        {
            var d = 1L;
            var k = 0;
            while ((b >> k) > 0) k++;

            for (int i = k - 1; i >= 0; i--)
            {
                d = d * d % n;
                if (((b >> i) & 1) > 0) d = d * a % n;
            }

            return d;
        }

        public static IEnumerable<int> PrimeFactors(this int source)
        {
            while (source > 1)
            {
                var divider = (from n in Enumerable.Range(2, source)
                               where (source % n == 0) || (source == n)
                               select n).First();

                yield return divider;

                source = source / divider;
            }
        }
    }
}
