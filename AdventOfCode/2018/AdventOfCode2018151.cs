using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode._2018
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2018, 15, "", 53576)]
    public class AdventOfCode2018151 : AdventOfCodeBase
    {
        public override void Solve()
        {

            string[] map = File.ReadAllLines(@"2018\AdventOfCode201815.txt");

            Console.WriteLine(new Game(map, 3).RunGame(false));

            for (int attackPower = 4; ; attackPower++)
            {
                int? outcome = new Game(map, attackPower).RunGame(true);
                if (outcome.HasValue)
                {
                    Result = outcome;
                    Console.WriteLine(outcome);
                    break;
                }
            }

            //var s = new Solution();
            //var h = s.Solve();

            //var map = new char[32][];
            //for (var i = 0; i < 32; i++)
            //{
            //    map[i] = new char[32];
            //}

            //var input = File.ReadAllLines(@"2018\AdventOfCode201815.txt");

            //var units = new List<CombatUnit>();

            //var locations = new List<(int X, int Y)>();
            //for (var y = 0; y < input.Length; y++)
            //{
            //    for (var x = 0; x < input[y].Length; x++)
            //    {
            //        var c = input[y][x];
            //        map[y][x] = c;

            //        if (c == 'E')
            //        {
            //            units.Add(new CombatUnit
            //            {
            //                CombatUnitType = CombatUnitType.Elf,
            //                X = x,
            //                Y = y,
            //                AttackPower = 3,
            //                HealthPoints = 200
            //            });
            //        }
            //        else if (c == 'G')
            //        {
            //            units.Add(new CombatUnit
            //            {
            //                CombatUnitType = CombatUnitType.Goblin,
            //                X = x,
            //                Y = y,
            //                AttackPower = 3,
            //                HealthPoints = 200
            //            });

            //        }

            //        if (map[y][x] != '#')
            //        {
            //            locations.Add((x, y));
            //        }
            //    }
            //}

            //var paths = new List<PathFindingBase<(int X, int Y)>>();

            //var mapSize = map[0].Length;
            //foreach (var l in locations)
            //{
            //    var coords = new List<(int X, int Y)>();
            //    if (l.Y > 0 && map[l.Y - 1][l.X] == '.') coords.Add((l.X, l.Y - 1));
            //    if (l.X > 0 && map[l.Y][l.X - 1] == '.') coords.Add((l.X - 1, l.Y));
            //    if (l.X < mapSize - 1 && map[l.Y][l.X + 1] == '.') coords.Add((l.X + 1, l.Y));
            //    if (l.Y < mapSize - 1 && map[l.Y + 1][l.X] == '.') coords.Add((l.X, l.Y + 1));
            //    foreach (var p in coords)
            //    {
            //        // Add all open paths.
            //        paths.Add(new PathFindingBase<(int X, int Y)>
            //        {
            //            Source = (l.X, l.Y),
            //            Destination = (p.X, p.Y),
            //            Cost = 1
            //        });
            //    }
            //}

            //while (true)
            //{
            //    // Sort
            //    units = units.GroupBy(u => new { u.X, u.Y }).SelectMany(u => u).ToList();

            //    foreach (var u in units)
            //    {
            //        var currentPaths = new List<PathFindingBase<(int X, int Y)>>();

            //        (int X, int Y) up = (u.X, u.Y - 1);
            //        (int X, int Y) left = (u.X - 1, u.Y);
            //        (int X, int Y) right = (u.X + 1, u.Y);
            //        (int X, int Y) down = (u.X, u.Y + 1);

            //        if (map[up.Y][up.X] == '.')
            //        {
            //            var path = new PathFindingBase<(int X, int Y)>
            //            {
            //                Source = (u.X, u.Y),
            //                Destination = up
            //            };
            //            currentPaths.Add(path);
            //        }

            //        if (map[left.Y][left.X] == '.')
            //        {
            //            var path = new PathFindingBase<(int X, int Y)>
            //            {
            //                Source = (u.X, u.Y),
            //                Destination = left
            //            };
            //            currentPaths.Add(path);
            //        }

            //        if (map[right.Y][right.X] == '.')
            //        {
            //            var path = new PathFindingBase<(int X, int Y)>
            //            {
            //                Source = (u.X, u.Y),
            //                Destination = right
            //            };
            //            currentPaths.Add(path);
            //        }

            //        if (map[down.Y][down.X] == '.')
            //        {
            //            var path = new PathFindingBase<(int X, int Y)>
            //            {
            //                Source = (u.X, u.Y),
            //                Destination = down
            //            };
            //            currentPaths.Add(path);
            //        }

            //        paths.AddRange(currentPaths);

            //        // Find targets:
            //        LinkedList<PathFindingBase<(int X, int Y)>> shortestPath = null;
            //        var shortestCount = int.MaxValue;
            //        CombatUnit fightUnit = null;
            //        var targets = units.Where(t => t.IsAlive && t.CombatUnitType != u.CombatUnitType);

            //        // Find all target squares
            //        foreach (var t in targets)
            //        {
            //            if (t.InRange(u))
            //            {
            //                var inRange = t.InRange(u);
            //            }
            //            // Check if we are able to attack
            //            var adjacent = t.AdjacentSquares(map);
            //            if (adjacent.Any(ad => (ad.X, ad.Y) == (u.X, u.Y)))
            //            {
            //                shortestPath = null;
            //                break;
            //            }

            //            foreach (var a in adjacent)
            //            {
            //                var currentPath = Dijkstras.CalculateShortestPathBetween((u.X, u.Y), (a.X, a.Y), paths);
            //                if (currentPath.Count > 0 && currentPath.Count < shortestCount)
            //                {
            //                    shortestPath = currentPath;
            //                    shortestCount = currentPath.Count;
            //                }
            //                else if (currentPath.Count == 0)
            //                {
            //                    fightUnit = t;
            //                }
            //            }
            //        }

            //        // Move
            //        if (shortestPath != null && shortestPath.Any())
            //        {
            //            var newPos = shortestPath.First();
            //            u.X = newPos.Destination.X;
            //            u.Y = newPos.Destination.Y;
            //            paths.Remove(newPos);
            //            foreach (var x in currentPaths)
            //                paths.Remove(x);

            //            var p = new PathFindingBase<(int X, int Y)>
            //            {
            //                Source = newPos.Destination,
            //                Destination = newPos.Source,
            //                Cost = newPos.Cost
            //            };
            //            paths.Add(p);
            //        }
            //        // Fight
            //        else
            //        {
            //            //Fight
            //            fightUnit?.TakeDamage(u.AttackPower);
            //            if (fightUnit != null && !fightUnit.IsAlive)
            //            {
            //                var g = 0;
            //            }
            //        }

            //    }
            //}

            //var b = 0;
        }

        public AdventOfCode2018151(string sessionCookie) : base(sessionCookie) { }
    }

    public class CombatUnit
    {
        public CombatUnitType CombatUnitType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int AttackPower { get; set; }
        public int HealthPoints { get; set; }

        public bool IsAlive => HealthPoints > 0;

        public List<(int X, int Y)> AdjacentSquares(char[][] map)
        {
            var mapSize = map[0].Length;
            var coords = new List<(int X, int Y)>();
            if (Y > 0 && map[Y - 1][X] == '.') coords.Add((X, Y - 1));
            if (X > 0 && map[Y][X - 1] == '.') coords.Add((X - 1, Y));
            if (X < mapSize - 1 && map[Y][X + 1] == '.') coords.Add((X + 1, Y));
            if (Y < mapSize - 1 && map[Y + 1][X] == '.') coords.Add((X, Y + 1));

            return coords;
        }

        public void TakeDamage(int damage)
        {
            this.HealthPoints -= damage;
        }

        public bool InRange(CombatUnit other)
        {
            if (Y - 1 == other.Y && X == other.X) return true;
            if (Y == other.Y && X - 1 == other.X) return true;
            if (Y == other.Y && X + 1 == other.X) return true;
            return Y + 1 == other.Y && X == other.X;
        }
    }

    public enum CombatUnitType
    {
        Elf,
        Goblin
    }

    class Solution
    {

        public string GetName() => "Beverage Bandits";

        public IEnumerable<object> Solve()
        {
            yield return PartOne();
            yield return PartTwo();
        }

        int PartOne()
        {
            return Outcome(3, 3).score;
        }

        int PartTwo()
        {
            var elfAp = 3;
            while (true)
            {
                var outcome = Outcome(3, elfAp);
                if (outcome.noElfDied)
                {
                    return outcome.score;
                }
                elfAp++;
            }
        }

        (bool noElfDied, int score) Outcome(int goblinAp, int elfAp)
        {
            var state = Parse(goblinAp, elfAp);
            var elfCount = state.players.Count(player => player.elf);

            var rounds = 0;
            while (Step(state))
            {
                rounds++;
            }

            return (state.players.Count(p => p.elf) == elfCount, (rounds - 1) * state.players.Select(player => player.hp).Sum());
        }

        bool Step(State state)
        {
            var moved = false;
            foreach (var player in state.players.OrderBy(a => a.pos))
            {
                if (player.hp > 0)
                {
                    if (Attack(state, player))
                    {
                        moved = true;
                    }
                    else
                    {
                        moved |= Move(state, player);
                        moved |= Attack(state, player);
                    }
                }
            }
            return moved;
        }

        bool Move(State state, Player player)
        {
            var opponents = ClosestOpponents(state, player);
            if (!opponents.Any())
            {
                return false;
            }
            var opponent = opponents.OrderBy(a => a.player.pos).First();
            var nextPos = opponents.Where(a => a.player == opponent.player).Select(a => a.firstStep).OrderBy(_ => _).First();
            (state.mtx[nextPos.irow, nextPos.icol], state.mtx[player.pos.irow, player.pos.icol]) =
                (state.mtx[player.pos.irow, player.pos.icol], state.mtx[nextPos.irow, nextPos.icol]);
            player.pos = nextPos;
            return true;
        }


        IEnumerable<(Player player, (int irow, int icol) firstStep)> ClosestOpponents(State state, Player player)
        {
            var minDist = int.MaxValue;
            foreach (var (otherPlayer, firstStep, dist) in OpponentsByDistance(state, player))
            {
                if (dist > minDist)
                {
                    break;
                }
                else
                {
                    minDist = dist;
                    yield return (otherPlayer, firstStep);
                }
            }
        }

        IEnumerable<(Player player, (int irow, int icol) firstStep, int dist)> OpponentsByDistance(State state, Player player)
        {
            var seen = new HashSet<(int irow, int icol)>();
            seen.Add(player.pos);
            var q = new Queue<((int irow, int icol) pos, (int drow, int dcol) origDir, int dist)>();

            foreach (var (drow, dcol) in new[] { (-1, 0), (0, -1), (0, 1), (1, 0) })
            {
                var posT = (player.pos.irow + drow, player.pos.icol + dcol);
                q.Enqueue((posT, posT, 1));
            }

            while (q.Any())
            {
                var (pos, firstStep, dist) = q.Dequeue();
                switch (GetBlock(state, pos))
                {
                    case Player otherPlayer when player != otherPlayer && otherPlayer.elf != player.elf:
                        yield return (otherPlayer, firstStep, dist);
                        break;

                    case Wall _:
                        break;

                    case Empty _:
                        foreach (var (drow, dcol) in new[] { (-1, 0), (0, -1), (0, 1), (1, 0) })
                        {
                            var posT = (pos.irow + drow, pos.icol + dcol);
                            if (!seen.Contains(posT))
                            {
                                seen.Add(posT);
                                q.Enqueue((posT, firstStep, dist + 1));
                            }
                        }
                        break;
                }
            }
        }

        bool Attack(State state, Player player)
        {
            var opponents = new List<Player>();

            foreach (var (drow, dcol) in new[] { (-1, 0), (0, -1), (0, 1), (1, 0) })
            {
                var posT = (player.pos.irow + drow, player.pos.icol + dcol);
                var block = GetBlock(state, posT);
                switch (block)
                {
                    case Player otherPlayer when otherPlayer.elf != player.elf:
                        opponents.Add(otherPlayer);
                        break;
                }
            }

            if (!opponents.Any())
            {
                return false;
            }
            var minHp = opponents.Select(a => a.hp).Min();
            var opponent = opponents.First(a => a.hp == minHp);
            opponent.hp -= player.ap;
            if (opponent.hp <= 0)
            {
                state.players.Remove(opponent);
                state.mtx[opponent.pos.irow, opponent.pos.icol] = new Empty();
            }
            return true;
        }


        bool ValidPos(State state, (int irow, int icol) pos)
        {
            return !(pos.irow < 0 || pos.irow >= state.mtx.GetLength(0) || pos.icol < 0 || pos.icol >= state.mtx.GetLength(1));
        }
        Block GetBlock(State state, (int irow, int icol) pos)
        {
            return ValidPos(state, pos) ? state.mtx[pos.irow, pos.icol] : new Wall();
        }

        State Parse(int goblinAp, int elfAp)
        {
            var players = new List<Player>();
            var lines = File.ReadAllLines(@"2018\AdventOfCode201815.txt");
            var mtx = new Block[lines.Length, lines[0].Length];
            for (var irow = 0; irow < lines.Length; irow++)
            {
                for (var icol = 0; icol < lines[0].Length; icol++)
                {
                    switch (lines[irow][icol])
                    {
                        case '#':
                            mtx[irow, icol] = new Wall();
                            break;
                        case '.':
                            mtx[irow, icol] = new Empty();
                            break;
                        case var ch when ch == 'G' || ch == 'E':
                            var player = new Player
                            {
                                elf = ch == 'E',
                                ap = ch == 'E' ? elfAp : goblinAp,
                                pos = (irow, icol)
                            };
                            players.Add(player);
                            mtx[irow, icol] = player;
                            break;
                    }
                }
            }
            return new State { mtx = mtx, players = players };
        }
    }
    class State
    {
        public Block[,] mtx;
        public List<Player> players;
    }
    abstract class Block { }
    class Empty : Block { }
    class Wall : Block { }
    class Player : Block
    {
        public (int irow, int icol) pos;
        public bool elf;
        public int ap = 3;
        public int hp = 200;
    }

    public class Game
    {
        private readonly string[] _map;
        private List<Unit> _units = new List<Unit>();
        public Game(string[] initialMap, int elfAttackPower)
        {
            for (int y = 0; y < initialMap.Length; y++)
            {
                for (int x = 0; x < initialMap[y].Length; x++)
                {
                    if (initialMap[y][x] == 'G')
                        _units.Add(new Unit { X = x, Y = y, IsGoblin = true, Health = 200, AttackPower = 3 });
                    else if (initialMap[y][x] == 'E')
                        _units.Add(new Unit { X = x, Y = y, IsGoblin = false, Health = 200, AttackPower = elfAttackPower });
                }
            }

            _map = initialMap.Select(l => l.Replace('G', '.').Replace('E', '.')).ToArray();
        }

        // Returns outcome of game.
        public int? RunGame(bool failOnElfDeath)
        {
            for (int rounds = 0; ; rounds++)
            {
                _units = _units.OrderBy(u => u.Y).ThenBy(u => u.X).ToList();
                for (int i = 0; i < _units.Count; i++)
                {
                    Unit u = _units[i];
                    List<Unit> targets = _units.Where(t => t.IsGoblin != u.IsGoblin).ToList();
                    if (targets.Count == 0)
                        return rounds * _units.Sum(ru => ru.Health);

                    if (!targets.Any(t => IsAdjacent(u, t)))
                        TryMove(u, targets);

                    Unit bestAdjacent =
                        targets
                        .Where(t => IsAdjacent(u, t))
                        .OrderBy(t => t.Health)
                        .ThenBy(t => t.Y)
                        .ThenBy(t => t.X)
                        .FirstOrDefault();

                    if (bestAdjacent == null)
                        continue;

                    bestAdjacent.Health -= u.AttackPower;
                    if (bestAdjacent.Health > 0)
                        continue;

                    if (failOnElfDeath && !bestAdjacent.IsGoblin)
                        return null;

                    int index = _units.IndexOf(bestAdjacent);
                    _units.RemoveAt(index);
                    if (index < i)
                        i--;
                }
            }
        }

        // Important: ordered in reading order
        private static readonly (int dx, int dy)[] s_neis = { (0, -1), (-1, 0), (1, 0), (0, 1) };
        private void TryMove(Unit u, List<Unit> targets)
        {
            HashSet<(int x, int y)> inRange = new HashSet<(int x, int y)>();
            foreach (Unit target in targets)
            {
                foreach ((int dx, int dy) in s_neis)
                {
                    (int nx, int ny) = (target.X + dx, target.Y + dy);
                    if (IsOpen(nx, ny))
                        inRange.Add((nx, ny));
                }
            }

            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
            Dictionary<(int x, int y), (int px, int py)> prevs = new Dictionary<(int x, int y), (int px, int py)>();
            queue.Enqueue((u.X, u.Y));
            prevs.Add((u.X, u.Y), (-1, -1));
            while (queue.Count > 0)
            {
                (int x, int y) = queue.Dequeue();
                foreach ((int dx, int dy) in s_neis)
                {
                    (int x, int y) nei = (x + dx, y + dy);
                    if (prevs.ContainsKey(nei) || !IsOpen(nei.x, nei.y))
                        continue;

                    queue.Enqueue(nei);
                    prevs.Add(nei, (x, y));
                }
            }

            List<(int x, int y)> getPath(int destX, int destY)
            {
                if (!prevs.ContainsKey((destX, destY)))
                    return null;
                List<(int x, int y)> path = new List<(int x, int y)>();
                (int x, int y) = (destX, destY);
                while (x != u.X || y != u.Y)
                {
                    path.Add((x, y));
                    (x, y) = prevs[(x, y)];
                }

                path.Reverse();
                return path;
            }

            List<(int tx, int ty, List<(int x, int y)> path)> paths =
                inRange
                .Select(t => (t.x, t.y, path: getPath(t.x, t.y)))
                .Where(t => t.path != null)
                .OrderBy(t => t.path.Count)
                .ThenBy(t => t.y)
                .ThenBy(t => t.x)
                .ToList();

            List<(int x, int y)> bestPath = paths.FirstOrDefault().path;
            if (bestPath != null)
                (u.X, u.Y) = bestPath[0];
        }

        private bool IsOpen(int x, int y) => _map[y][x] == '.' && _units.All(u => u.X != x || u.Y != y);
        private bool IsAdjacent(Unit u1, Unit u2) => Math.Abs(u1.X - u2.X) + Math.Abs(u1.Y - u2.Y) == 1;

        private class Unit
        {
            public int X, Y;
            public bool IsGoblin;
            public int Health = 200;
            public int AttackPower;
        }
    }
}
