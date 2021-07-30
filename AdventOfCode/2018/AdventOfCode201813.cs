using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// --- Day 13: Mine Cart Madness ---
    /// 
    /// A crop of this size requires significant logistics to transport produce,
    /// soil, fertilizer, and so on. The Elves are very busy pushing things around
    /// in carts on some kind of rudimentary system of tracks they've come up with.
    /// 
    /// Seeing as how cart-and-track systems don't appear in recorded history for
    /// another 1000 years, the Elves seem to be making this up as they go along.
    /// They haven't even figured out how to avoid collisions yet.
    /// 
    /// You map out the tracks (your puzzle input) and see where you can help.
    /// 
    /// Tracks consist of straight paths (| and -), curves (/ and \), and
    /// intersections (+). Curves connect exactly two perpendicular pieces of
    /// track; for example, this is a closed loop:
    /// 
    /// /----\
    /// |    |
    /// |    |
    /// \----/
    /// 
    /// Intersections occur when two perpendicular paths cross. At an intersection,
    /// a cart is capable of turning left, turning right, or continuing straight.
    /// Here are two loops connected by two intersections:
    /// 
    /// /-----\
    /// |     |
    /// |  /--+--\
    /// |  |  |  |
    /// \--+--/  |
    /// |     |
    /// \-----/
    /// 
    /// Several carts are also on the tracks. Carts always face either up (^), down
    /// (v), left (<), or right (>). (On your initial map, the track under each
    /// cart is a straight path matching the direction the cart is facing.)
    /// 
    /// Each time a cart has the option to turn (by arriving at any intersection),
    /// it turns left the first time, goes straight the second time, turns right
    /// the third time, and then repeats those directions starting again with left
    /// the fourth time, straight the fifth time, and so on. This process is
    /// independent of the particular intersection at which the cart has arrived -
    /// that is, the cart has no per-intersection memory.
    /// 
    /// Carts all move at the same speed; they take turns moving a single step at a
    /// time. They do this based on their current location: carts on the top row
    /// move first (acting from left to right), then carts on the second row move
    /// (again from left to right), then carts on the third row, and so on. Once
    /// each cart has moved one step, the process repeats; each of these loops is
    /// called a tick.
    /// 
    /// For example, suppose there are two carts on a straight track:
    /// 
    /// |  |  |  |  |
    /// v  |  |  |  |
    /// |  v  v  |  |
    /// |  |  |  v  X
    /// |  |  ^  ^  |
    /// ^  ^  |  |  |
    /// |  |  |  |  |
    /// 
    /// First, the top cart moves. It is facing down (v), so it moves down one
    /// square. Second, the bottom cart moves. It is facing up (^), so it moves up
    /// one square. Because all carts have moved, the first tick ends. Then, the
    /// process repeats, starting with the first cart. The first cart moves down,
    /// then the second cart moves up - right into the first cart, colliding with
    /// it! (The location of the crash is marked with an X.) This ends the second
    /// and last tick.
    /// 
    /// Here is a longer example:
    /// 
    /// /->-\        
    /// |   |  /----\
    /// | /-+--+-\  |
    /// | | |  | v  |
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /-->\        
    /// |   |  /----\
    /// | /-+--+-\  |
    /// | | |  | |  |
    /// \-+-/  \->--/
    /// \------/   
    /// 
    /// /---v        
    /// |   |  /----\
    /// | /-+--+-\  |
    /// | | |  | |  |
    /// \-+-/  \-+>-/
    /// \------/   
    /// 
    /// /---\        
    /// |   v  /----\
    /// | /-+--+-\  |
    /// | | |  | |  |
    /// \-+-/  \-+->/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /----\
    /// | /->--+-\  |
    /// | | |  | |  |
    /// \-+-/  \-+--^
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /----\
    /// | /-+>-+-\  |
    /// | | |  | |  ^
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /----\
    /// | /-+->+-\  ^
    /// | | |  | |  |
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /----<                               >
    /// | /-+-->-\  |
    /// | | |  | |  |
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /---<\                               >
    /// | /-+--+>\  |
    /// | | |  | |  |
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /--<-\                               >
    /// | /-+--+-v  |
    /// | | |  | |  |
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /-<--\                               >
    /// | /-+--+-\  |
    /// | | |  | v  |
    /// \-+-/  \-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /<---\                               >
    /// | /-+--+-\  |
    /// | | |  | |  |
    /// \-+-/  \-<--/                               >
    /// \------/   
    /// 
    /// /---\        
    /// |   |  v----\
    /// | /-+--+-\  |
    /// | | |  | |  |
    /// \-+-/  \<+--/                               >
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /----\
    /// | /-+--v-\  |
    /// | | |  | |  |
    /// \-+-/  ^-+--/
    /// \------/   
    /// 
    /// /---\        
    /// |   |  /----\
    /// | /-+--+-\  |
    /// | | |  X |  |
    /// \-+-/  \-+--/
    /// \------/
    /// 
    /// After following their respective paths for a while, the carts eventually
    /// crash. To help prevent crashes, you'd like to know the location of the
    /// first crash. Locations are given in X,Y coordinates, where the furthest
    /// left column is X=0 and the furthest top row is Y=0:
    /// 
    /// 111
    /// 0123456789012
    /// 0/---\        
    /// 1|   |  /----\
    /// 2| /-+--+-\  |
    /// 3| | |  X |  |
    /// 4\-+-/  \-+--/
    /// 5  \------/
    /// 
    /// In this example, the location of the first crash is 7,3.
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
    [AdventOfCode(2018, 13, "Mine Cart Madness", "100,21", "113,109")]
    public class AdventOfCode201813 : AdventOfCodeBase
    {
        public AdventOfCode201813(string sessionCookie) : base(sessionCookie) { }

        protected override object SolvePart1()
        {
            var map = File.ReadAllText(@"2018\AdventOfCode201813.txt");
            var mapWidth = map.Split('\n').First().Length + 1;
            var carts = GetCarts(map, mapWidth);

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
                        return $"{collision.First().X},{collision.First().Y}";
                    }
                }
            }
        }

        protected override object SolvePart2()
        {
            var map = File.ReadAllText(@"2018\AdventOfCode201813.txt");
            var mapWidth = map.Split('\n').First().Length + 1;
            var carts = AdventOfCode201813.GetCarts(map, mapWidth);

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

            return $"{carts.First().X},{carts.First().Y}";
        }

        public static List<Cart> GetCarts(string map, int mapWidth)
        {
            return map.Select((c, i) =>
                      {
                          var x = i % mapWidth;
                          var y = i / mapWidth;
                          var nextTurn = 0;
                          var direction = -1;

                          switch (c)
                          {
                              case '<':
                                  direction = 0;
                                  break;
                              case '^':
                                  direction = 1;
                                  break;
                              case '>':
                                  direction = 2;
                                  break;
                              case 'v':
                                  direction = 3;
                                  break;
                          }

                          return new Cart { X = x, Y = y, Turn = nextTurn, Direction = direction };
                      })
                      .Where(c => c.Direction != -1)
                      .ToList();
        }
    }

    public class Cart
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Turn { get; set; }
        public int Direction { get; set; }
    }
}
