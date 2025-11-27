using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode._2015
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2015, 4, "The Ideal Stocking Stuffer", 254575, 1038736)]
    public class AdventOfCode201504 : AdventOfCodeBase
    {
        public AdventOfCode201504(string sessionCookie) : base(sessionCookie) { }

        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
        {
            var key = "bgvyzdsv";
            var md5 = MD5.Create();

            var n = 0;
            while (true)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key + n));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] < 16)
                {
                    return n;
                }
                n++;
            }
        }

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
        {
            var key = "bgvyzdsv";
            var md5 = MD5.Create();

            var n = 0;
            while (true)
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(key + n));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    return n;
                }
                n++;
            }
        }
    }
}
