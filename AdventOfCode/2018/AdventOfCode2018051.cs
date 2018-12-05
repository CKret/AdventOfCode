using System.IO;
using System.Runtime.InteropServices;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 5, 1, "", 10804)]
    public class AdventOfCode2018051 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllText(@"2018\AdventOfCode201805.txt");
            Result = React(data).Length;
        }
        private string React(string data)
        {
            for (var i = 0; i < data.Length - 1; i++)
            {
                if (data[i] == data[i + 1] + 0x20 || data[i] + 0x20 == data[i + 1])
                {
                    data = data.Remove(i, 2);
                    i = i == 0 ? -1 : i - 2;
                }
            }

            return data;
        }
    }
}
