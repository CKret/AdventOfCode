using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Core;

namespace AdventOfCode._2015
{
    /// <summary>
    /// Santa needs help mining some AdventCoins (very similar to bitcoins) to use
    /// as gifts for all the economically forward-thinking little girls and boys.
    /// 
    /// To do this, he needs to find MD5 hashes which, in hexadecimal, start with
    /// at least five zeroes. The input to the MD5 hash is some secret key (your
    /// puzzle input, given below) followed by a number in decimal.
    /// 
    /// To mine AdventCoins, you must find Santa the lowest positive number (no
    /// leading zeroes: 1, 2, 3, ...) that produces such a hash.
    /// </summary>
    [AdventOfCode(2015, 4, 1, "Find a MD5 hash which, in hexadecimal, start with at least five zeroes.", 254575)]
    public class AdventOfCode2015041 : AdventOfCodeBase
    {
        [SuppressMessage("Microsoft.Cryptography", "CA5351")]
        public override void Solve()
        {
            var key = "bgvyzdsv";
            var md5 = MD5.Create();

            var n = 0;
            while (true)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key + n));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 16)
                {
                    Result = n;
                    break;
                }
                n++;
            }
        }

        public AdventOfCode2015041(string sessionCookie) : base(sessionCookie) { }
    }
}
