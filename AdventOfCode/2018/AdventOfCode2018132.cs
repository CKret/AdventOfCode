using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 13: Mine Cart Madness ---
    /// 
    /// --- Part Two ---
    /// 
    /// There isn't much you can do to prevent crashes in this ridiculous system.
    /// However, by predicting the crashes, the Elves know where to be in advance
    /// and instantly remove the two crashing carts the moment any crash occurs.
    /// 
    /// They can proceed like this for a while, but eventually, they're going to
    /// run out of carts. It could be useful to figure out where the last cart that
    /// hasn't crashed will end up.
    /// 
    /// For example:
    /// 
    /// />-<\  
    /// |   |  
    /// | /<+-\
    /// | | | v
    /// \>+</ |
    /// |   ^
    /// \<->/
    /// 
    /// /---\  
    /// |   |  
    /// | v-+-\
    /// | | | |
    /// \-+-/ |
    /// |   |
    /// ^---^
    /// 
    /// /---\  
    /// |   |  
    /// | /-+-\
    /// | v | |
    /// \-+-/ |
    /// ^   ^
    /// \---/
    /// 
    /// /---\  
    /// |   |  
    /// | /-+-\
    /// | | | |
    /// \-+-/ ^
    /// |   |
    /// \---/
    /// 
    /// After four very expensive crashes, a tick ends with only one cart
    /// remaining; its final location is 6,4.
    /// 
    /// What is the location of the last cart at the end of the first tick where it
    /// is the only cart left?
    /// </summary>
    [AdventOfCode(2018, 13, "Mine Cart Madness - Part 2", "113,109")]
    public class AdventOfCode2018132 : AdventOfCodeBase
    {
        public override void Solve()
        {
            var map = File.ReadAllText(@"2018\AdventOfCode201813.txt");
            var mapWidth = map.Split('\n').First().Length + 1;
            var carts = AdventOfCode2018131.GetCarts(map, mapWidth);

            while (true)
            {
                carts = carts
                        .OrderBy(cart => cart.X)
                        .ThenBy(cart => cart.Y)
                        .ToList();

                for (var i = 0; i < carts.Count; i++)
                {
                    var cart = carts[i];
                    switch (cart.Direction)
                    {
                        case 0:
                            cart.X--;
                            break;
                        case 1:
                            cart.Y--;
                            break;
                        case 2:
                            cart.X++;
                            break;
                        case 3:
                            cart.Y++;
                            break;
                    }

                    switch (map[(cart.Y * mapWidth) + cart.X])
                    {
                        case '\\':
                            cart.Direction = (5 - cart.Direction) % 4;
                            break;
                        case '/':
                            cart.Direction = 3 - cart.Direction;
                            break;
                        case '+':
                            switch (cart.Turn)
                            {
                                case 0:
                                    cart.Direction = (4 + cart.Direction - 1) % 4;
                                    break;
                                case 2:
                                    cart.Direction = (4 + cart.Direction + 1) % 4;
                                    break;
                            }
                            cart.Turn = (3 + cart.Turn + 1) % 3;
                            break;
                    }

                    var collision = carts.GroupBy(c => (c.X, c.Y)).Where(g => g.Count() > 1).SelectMany(c => c);
                    if (collision.Any())
                    {
                        foreach (var c in collision)
                        {
                            if (carts.IndexOf(c) <= i) i--;
                            carts.Remove(c);
                        }

                    }
                }

                if (carts.Count == 1) break;
            }

            Result = $"{carts.First().X},{carts.First().Y}";
        }

        public AdventOfCode2018132(string sessionCookie) : base(sessionCookie) { }
    }
}
