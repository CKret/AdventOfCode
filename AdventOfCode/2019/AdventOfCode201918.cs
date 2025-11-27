using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode.Mathematics.PathFinding;
using AdventOfCode.Mathematics.PathFinding.Core;

namespace AdventOfCode._2019
{
    /// <summary>
    /// </summary>
    [AdventOfCode(2019, 18, "Many-Worlds Interpretation", 3270, 1628)]
    public class AdventOfCode201918 : AdventOfCodeBase
    {
        public AdventOfCode201918(string sessionCookie) : base(sessionCookie) { }

        // Optimized implementation using precomputed distances between "interesting" nodes (entrance + keys)
        protected override async Task<object> SolvePart1(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // parse grid into map
            var map = new Dictionary<(int x, int y), char>();
            int y = 0;
            foreach (var line in Input)
            {
                for (int x = 0; x < line.Length; x++)
                    map[(x, y)] = line[x];
                y++;
            }

            // find entrance and keys
            var entrancePos = map.Single(p => p.Value == '@').Key;
            var keyPositions = map.Where(p => char.IsLower(p.Value)).ToDictionary(p => p.Value, p => p.Key);
            int K = keyPositions.Count;

            // assign indices: 0 = entrance, 1..K = keys
            var keyList = keyPositions.Keys.OrderBy(c => c).ToList(); // deterministic order
            var nodePositions = new List<(int x, int y)>(K + 1) { entrancePos };
            var keyIndex = new Dictionary<char, int>();
            for (int i = 0; i < keyList.Count; i++)
            {
                keyIndex[keyList[i]] = i; // 0-based for keys
                nodePositions.Add(keyPositions[keyList[i]]);
            }

            // For each node compute reachable keys with distance and required key-mask (doors encountered)
            // edges[fromNode] = list of (toNode, distance, requiredMask)
            var edges = new List<List<(int to, int dist, int reqMask)>>();
			for (int i = 0; i < nodePositions.Count; i++)
			{
				cancellationToken.ThrowIfCancellationRequested();
				var pos = nodePositions[i];
				// BFS
				var q = new Queue<((int x, int y) p, int dist, int mask)>();
				var visited = new HashSet<(int x, int y)> { pos };
				q.Enqueue((pos, 0, 0));

				var list = new List<(int to, int dist, int reqMask)>();
				while (q.Count > 0)
				{
					cancellationToken.ThrowIfCancellationRequested();
					var cur = q.Dequeue();
					var (cx, cy) = cur.p;
					var curDist = cur.dist;
					var curMask = cur.mask;

					var neighs = new (int x, int y)[] { (cx + 1, cy), (cx - 1, cy), (cx, cy + 1), (cx, cy - 1) };
					foreach (var n in neighs)
					{
						if (visited.Contains(n)) continue;
						if (!map.TryGetValue(n, out var ch)) continue;
						if (ch == '#') continue;

						var nextMask = curMask;
						if (char.IsUpper(ch))
						{
							// require the corresponding key
							var lower = char.ToLowerInvariant(ch);
							if (keyIndex.TryGetValue(lower, out var idx))
								nextMask |= (1 << idx);
						}

						visited.Add(n);

						if (char.IsLower(ch))
						{
							// record edge from i to this key node
							var toNode = 1 + keyIndex[ch];
							list.Add((toNode, curDist + 1, nextMask));
						}

						q.Enqueue((n, curDist + 1, nextMask));
					}
				}

				edges.Add(list);
			}

            // Dijkstra over (node, collectedMask)
            int allMask = (1 << K) - 1;
            var pq = new PriorityQueue<(int node, int mask), int>();
            var distDict = new Dictionary<(int node, int mask), int>();

            pq.Enqueue((0, 0), 0);
            distDict[(0, 0)] = 0;

            while (pq.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var curr = pq.Dequeue();
                var key = curr;
                var d = distDict[key];
                var (node, mask) = key;
                if (mask == allMask)
                    return d;

                foreach (var e in edges[node])
                {
                    int bit = 1 << (e.to - 1);
                    if ((mask & bit) != 0) continue; // already collected
                    // check required keys (doors) are satisfied
                    if ((e.reqMask & ~mask) != 0) continue;

                    var newMask = mask | bit;
                    var nd = d + e.dist;
                    var nk = (e.to, newMask);
                    if (!distDict.TryGetValue(nk, out var existing) || nd < existing)
                    {
                        distDict[nk] = nd;
                        pq.Enqueue(nk, nd);
                    }
                }
            }

            return null;
        }

        protected override async Task<object> SolvePart2(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // parse grid into map
            var map = new Dictionary<(int x, int y), char>();
            int maxY = 0;
            foreach (var line in Input)
            {
                for (int x = 0; x < line.Length; x++)
                    map[(x, maxY)] = line[x];
                maxY++;
            }

            // find original entrance
            var entrancePos = map.Single(p => p.Value == '@').Key;
            var (cx, cy) = entrancePos;

            // mutate center to 4-entrances per puzzle instructions
            // positions: center and cardinal become walls, diagonals become entrances
            var deltas = new (int dx, int dy)[] { (0,0), (1,0), (-1,0), (0,1), (0,-1), (1,1), (-1,1), (1,-1), (-1,-1) };
            // set center and cardinal to '#'
            map[(cx, cy)] = '#';
            map[(cx+1, cy)] = '#';
            map[(cx-1, cy)] = '#';
            map[(cx, cy+1)] = '#';
            map[(cx, cy-1)] = '#';
            // set diagonals to '@'
            map[(cx-1, cy-1)] = '@';
            map[(cx+1, cy-1)] = '@';
            map[(cx-1, cy+1)] = '@';
            map[(cx+1, cy+1)] = '@';

            // collect start positions and keys
            var starts = new List<(int x, int y)>();
            starts.Add((cx-1, cy-1));
            starts.Add((cx+1, cy-1));
            starts.Add((cx-1, cy+1));
            starts.Add((cx+1, cy+1));

            var keyPositions = map.Where(p => char.IsLower(p.Value)).ToDictionary(p => p.Value, p => p.Key);
            int K = keyPositions.Count;
            var keyList = keyPositions.Keys.OrderBy(c => c).ToList();
            var keyIndex = new Dictionary<char, int>();
            for (int i = 0; i < keyList.Count; i++) keyIndex[keyList[i]] = i;

            // Build node positions: starts then keys
            int S = starts.Count;
            var nodePositions = new List<(int x, int y)>();
            nodePositions.AddRange(starts);
            foreach (var k in keyList) nodePositions.Add(keyPositions[k]);

            // For each node compute reachable keys with BFS
            var edges = new List<List<(int keyIdx, int dist, int reqMask)>>();
            for (int i = 0; i < nodePositions.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var pos = nodePositions[i];
                var q = new Queue<((int x, int y) p, int dist, int mask)>();
                var visited = new HashSet<(int x, int y)> { pos };
                q.Enqueue((pos, 0, 0));
                var list = new List<(int keyIdx, int dist, int reqMask)>();

                while (q.Count > 0)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    var cur = q.Dequeue();
                    var (cx2, cy2) = cur.p;
                    var curDist = cur.dist;
                    var curMask = cur.mask;

                    foreach (var n in new (int x, int y)[] { (cx2+1, cy2), (cx2-1, cy2), (cx2, cy2+1), (cx2, cy2-1) })
                    {
                        if (visited.Contains(n)) continue;
                        if (!map.TryGetValue(n, out var ch)) continue;
                        if (ch == '#') continue;

                        var nextMask = curMask;
                        if (char.IsUpper(ch))
                        {
                            var lower = char.ToLowerInvariant(ch);
                            if (keyIndex.TryGetValue(lower, out var idx))
                                nextMask |= (1 << idx);
                        }

                        visited.Add(n);

                        if (char.IsLower(ch))
                        {
                            var kid = keyIndex[ch];
                            list.Add((kid, curDist + 1, nextMask));
                        }

                        q.Enqueue((n, curDist + 1, nextMask));
                    }
                }

                edges.Add(list);
            }

            // Dijkstra over state: positions of 4 robots (indices into nodePositions) and collected mask
            int allMask = (1 << K) - 1;
            var pq = new PriorityQueue<(int[] positions, int mask), int>(Comparer<int>.Default);
            // Use tuple key string for visited distances to keep simple
            var dist = new Dictionary<string, int>();

            int[] startPosIndices = new int[S];
            for (int i = 0; i < S; i++) startPosIndices[i] = i;
            string KeyFor(int[] poss, int mask) => string.Join(',', poss) + '|' + mask.ToString();

            var startKey = KeyFor(startPosIndices, 0);
            pq.Enqueue((startPosIndices, 0), 0);
            dist[startKey] = 0;

            while (pq.Count > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var state = pq.Dequeue();
                var positions = state.positions;
                var mask = state.mask;
                var keyStr = KeyFor(positions, mask);
                var dcur = dist[keyStr];

                if (mask == allMask) return dcur;

                // For each robot, try to move to any reachable key not yet collected
                for (int r = 0; r < S; r++)
                {
                    int srcIndex = positions[r];
                    foreach (var e in edges[srcIndex])
                    {
                        int bit = 1 << e.keyIdx;
                        if ((mask & bit) != 0) continue; // already collected
                        // required doors must be satisfied
                        if ((e.reqMask & ~mask) != 0) continue;

                        // move robot r to key node index = S + e.keyIdx
                        var newPositions = (int[])positions.Clone();
                        newPositions[r] = S + e.keyIdx;
                        var newMask = mask | bit;
                        var nk = KeyFor(newPositions, newMask);
                        var nd = dcur + e.dist;
                        if (!dist.TryGetValue(nk, out var existing) || nd < existing)
                        {
                            dist[nk] = nd;
                            pq.Enqueue((newPositions, newMask), nd);
                        }
                    }
                }

                // Additionally, if a robot is currently on a key node, allow moves from that key node as well
                for (int r = 0; r < S; r++)
                {
                    int srcIndex = positions[r];
                    if (srcIndex < S) continue; // not on a key
                    int keyIdx = srcIndex - S;
                    // edges from this key node were computed at index srcIndex
                    foreach (var e in edges[srcIndex])
                    {
                        int bit = 1 << e.keyIdx;
                        if ((mask & bit) != 0) continue;
                        if ((e.reqMask & ~mask) != 0) continue;

                        var newPositions = (int[])positions.Clone();
                        newPositions[r] = S + e.keyIdx;
                        var newMask = mask | bit;
                        var nk = KeyFor(newPositions, newMask);
                        var nd = dcur + e.dist;
                        if (!dist.TryGetValue(nk, out var existing) || nd < existing)
                        {
                            dist[nk] = nd;
                            pq.Enqueue((newPositions, newMask), nd);
                        }
                    }
                }
            }

            return null;
        }

        public static List<Vertex<(int, int)>> GetPathCosts(Dictionary<(int, int), char> map, Dictionary<(int X, int Y), char> possibleLocations)
        {

            var pathCosts = new List<Vertex<(int, int)>>();
            foreach (var source in possibleLocations)
            {
                var destination = (source.Key.X + 1, source.Key.Y);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new Vertex<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }

                destination = (source.Key.X - 1, source.Key.Y);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new Vertex<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }

                destination = (source.Key.X, source.Key.Y + 1);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new Vertex<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }

                destination = (source.Key.X, source.Key.Y - 1);
                if (possibleLocations.ContainsKey(destination))
                {
                    var pfb = new Vertex<(int, int)>
                    {
                        Source = source.Key,
                        Destination = destination,
                        Cost = 1
                    };

                    pathCosts.Add(pfb);
                }
            }

            return pathCosts;
        }
    }
}
