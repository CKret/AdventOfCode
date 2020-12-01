using System.Globalization;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// --- Day 1: The Tyranny of the Rocket Equation ---
    ///
    /// --- Part 2 ---
    ///
    /// During the second Go / No Go poll, the Elf in charge of the Rocket Equation
    /// Double-Checker stops the launch sequence. Apparently, you forgot to include
    /// additional fuel for the fuel you just added.
    /// 
    /// Fuel itself requires fuel just like a module - take its mass, divide by
    /// three, round down, and subtract 2. However, that fuel also requires fuel,
    /// and that fuel requires fuel, and so on. Any mass that would require
    /// negative fuel should instead be treated as if it requires zero fuel; the
    /// remaining mass, if any, is instead handled by wishing really hard, which
    /// has no mass and is outside the scope of this calculation.
    /// 
    /// So, for each module mass, calculate its fuel and add it to the total. Then,
    /// treat the fuel amount you just calculated as the input mass and repeat the
    /// process, continuing until a fuel requirement is zero or negative. For
    /// example:
    /// 
    ///  - A module of mass 14 requires 2 fuel. This fuel requires no further
    ///    fuel (2 divided by 3 and rounded down is 0, which would call for a
    ///    negative fuel), so the total fuel required is still just 2.
    ///  - At first, a module of mass 1969 requires 654 fuel. Then, this fuel
    ///    requires 216 more fuel (654 / 3 - 2). 216 then requires 70 more fuel,
    ///    which requires 21 fuel, which requires 5 fuel, which requires no
    ///    further fuel. So, the total fuel required for a module of mass 1969 is
    ///    654 + 216 + 70 + 21 + 5 = 966.
    ///  - The fuel required by a module of mass 100756 and its fuel is:
    ///    33583 + 11192 + 3728 + 1240 + 411 + 135 + 43 + 12 + 2 = 50346.
    /// 
    /// What is the sum of the fuel requirements for all of the modules on your
    /// spacecraft when also taking into account the mass of the added fuel?
    /// (Calculate the fuel requirements for each module separately, then add them
    /// all up at the end.)
    /// </summary>
    [AdventOfCode(2019, 1, 2, "The Tyranny of the Rocket Equation - Part 2", 5183030L)]
    public class AdventOfCode2019012 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var data = File.ReadAllLines(@"2019\AdventOfCode201901.txt");

            Result = data.Sum(mass =>
            {
                var fuel = long.Parse(mass, CultureInfo.CurrentCulture) / 3 - 2;

                var extraFuel = fuel / 3 - 2;
                while (extraFuel > 0)
                {
                    fuel += extraFuel;
                    extraFuel = extraFuel / 3 - 2;
                }

                return fuel;
            });
        }
    }
}
