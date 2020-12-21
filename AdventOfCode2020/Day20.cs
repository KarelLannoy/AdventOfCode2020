using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day20
    {
        public static long One()
        {
            var input = InputParser.GetInputText("day20.txt");
            var tiles = ParseTiles(input.Split(Environment.NewLine + Environment.NewLine));

            foreach (Tile tile in tiles)
            {
                var differentTiles = tiles.Where(t => t != tile).ToList();
                foreach (var differentTile in differentTiles)
                {
                    tile.AddNeighbour(differentTile);
                }
            }

            var cornerTiles = tiles.Where(t => t.NeigbouringIds.Count() == 2).ToList();
            return cornerTiles.Select(t => t.Id).Aggregate((a, x) => a * x);
        }

        private static List<Tile> ParseTiles(string[] tileString)
        {
            var tiles = new List<Tile>();
            foreach (var singleTileString in tileString)
            {
                Tile tile = new Tile();
                var lines = singleTileString.Split(Environment.NewLine);
                tile.Lines = lines.Skip(1).Take(10).ToList();
                tile.Id = long.Parse(lines[0].Replace("Tile ", "").Replace(":", ""));
                tile.Sides.Add(new Side() { SideString = lines[1], Rotation = Rotation.Up });
                tile.Sides.Add(new Side() { SideString = new string(lines[10].Reverse().ToArray()), Rotation = Rotation.Down });
                tile.Sides.Add(new Side() { SideString = new string(lines.Skip(1).Take(10).Select(l => l[9]).ToArray()), Rotation = Rotation.Right });
                tile.Sides.Add(new Side() { SideString = new string(lines.Skip(1).Take(10).Select(l => l[0]).Reverse().ToArray()), Rotation = Rotation.Left });
                tiles.Add(tile);
            }
            return tiles;
        }

        public static long Two()
        {
            var input = InputParser.GetInputText("day20.txt");
            var tiles = ParseTiles(input.Split(Environment.NewLine + Environment.NewLine));

            var firstTile = tiles.First();
            //firstTile.FlipVertical();
            firstTile.RotatedCorrectly = true;
            OrientTilesCorrect(firstTile, tiles);

            foreach (Tile tile in tiles)
            {
                var differentTiles = tiles.Where(t => t != tile).ToList();
                foreach (var differentTile in differentTiles)
                {
                    tile.AddNeighbour(differentTile);
                }
            }

            Dictionary<Point, Tile> grid = new Dictionary<Point, Tile>();
            var startTile = tiles.Where(t => t.NeigbouringIds.Count() == 2 && (t.NeigbouringIds.All(n => n.Value == Rotation.Right || n.Value == Rotation.Down))).First();
            var p = new Point(0, 0);
            grid.Add(p, startTile);
            AddNeighboursToGrid(startTile, p, grid, tiles);
            tiles.ForEach(t => t.DropEdgeOfLines());

            var ocean = CreateTileFromGrid(grid);

            for (int i = 0; i < 4; i++)
            {
                ocean.Rotate(1);
                for (int x = 0; x < 2; x++)
                {
                    ocean.FlipVertical();
                    for (int y = 0; y < 2; y++)
                    {
                        ocean.FlipHorizontal();
                        var numberOfSeaMonsters = CountNumberOfSeaMonsters(ocean);
                        if (numberOfSeaMonsters > 0)
                        {
                            return ocean.Lines.Sum(s => s.Count(c => c == '#')) - (numberOfSeaMonsters * 15); //points in a seamonster;

                        }
                    }
                    
                }
                
            }
            return 0;
        }

        private static int CountNumberOfSeaMonsters(Tile ocean)
        {
            var counter = 0;
            var seaMonsterLength = 20;
            var seaMonsterHeight = 3;
            var seaMonsterPoints = new List<Point>()
            {
                new Point(0,0),
                new Point(1,1),
                new Point(4,1),
                new Point(5,0),
                new Point(6,0),
                new Point(7,1),
                new Point(10,1),
                new Point(11,0),
                new Point(12,0),
                new Point(13,1),
                new Point(16,1),
                new Point(17,0),
                new Point(18,0),
                new Point(18,-1),
                new Point(19,0)
            };

            for (int y = 1; y < ocean.Lines.Count - seaMonsterHeight + 1; y++)
            {
                for (int x = 0; x < ocean.Lines[y].Length - seaMonsterLength + 1; x++)
                {
                    var yes = true;
                    foreach (var p in seaMonsterPoints)
                    {
                        var pointToCheck = new Point(x, y);
                        pointToCheck.X += p.X;
                        pointToCheck.Y += p.Y;
                        if (ocean.Lines[pointToCheck.Y][pointToCheck.X] == '#')
                        {
                            continue;
                        }
                        else
                        {
                            yes = false;
                            break;
                        }
                    }
                    if (yes)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private static Tile CreateTileFromGrid(Dictionary<Point, Tile> grid)
        {
            List<string> lines = new List<string>();

            var gridLowerBoundX = grid.Keys.Min(k => k.X);
            var gridUpperBoundX = grid.Keys.Max(k => k.X);
            var gridLowerBoundY = grid.Keys.Min(k => k.Y);
            var gridUpperBoundY = grid.Keys.Max(k => k.Y);

            var yIndex = 0;
            for (int y = gridLowerBoundY; y <= gridUpperBoundY; y++)
            {
                for (int x = gridLowerBoundX; x <= gridUpperBoundX; x++)
                {
                    var tile = grid[new Point(x, y)];
                    for (int z = 0; z < tile.Lines.Count; z++)
                    {
                        if (lines.Count < yIndex + (z + 1))
                        {
                            lines.Add(tile.Lines[z]);
                        }
                        else
                        {
                            lines[yIndex + z] += tile.Lines[z];
                        }
                    }
                }
                yIndex += grid.Values.First().Lines.Count;
            }

            return new Tile() { Lines = lines };
        }

        private static void OrientTilesCorrect(Tile firstTile, List<Tile> tiles)
        {
            foreach (var side in firstTile.Sides)
            {
                var matchingSideTile = tiles.FirstOrDefault(t => t.Id != firstTile.Id && (t.Sides.Any(s => s.SideString == side.SideString || s.SideString == new string(side.SideString.Reverse().ToArray()))));
                if (matchingSideTile != null && !matchingSideTile.RotatedCorrectly)
                {
                    var correspondingSide = matchingSideTile.Sides.First(s => s.SideString == side.SideString || s.SideString == new string(side.SideString.Reverse().ToArray()));

                    var rotateTo = (Rotation)(((int)side.Rotation + 2) % 4);
                    var stepsToRotate = (4 + (rotateTo - correspondingSide.Rotation)) % 4;
                    matchingSideTile.Rotate(stepsToRotate);

                    if (side.SideString == correspondingSide.SideString)
                    {
                        if (side.Rotation == Rotation.Down || side.Rotation == Rotation.Up)
                        {
                            matchingSideTile.FlipVertical();
                            matchingSideTile.Rotate(2);
                        }
                        else
                        {
                            matchingSideTile.FlipHorizontal();
                            matchingSideTile.Rotate(2);
                        }
                    }
                    matchingSideTile.RotatedCorrectly = true;
                    OrientTilesCorrect(matchingSideTile, tiles);
                }
            }
        }

        private static void AddNeighboursToGrid(Tile tile, Point p, Dictionary<Point, Tile> grid, List<Tile> tiles)
        {
            var keyList = tile.NeigbouringIds.Keys.ToList();
            foreach (var item in keyList)
            {
                var newPoint = new Point(p.X, p.Y);
                switch (tile.NeigbouringIds[item])
                {
                    case Rotation.Up:
                        newPoint.Y--;
                        break;
                    case Rotation.Right:
                        newPoint.X++;
                        break;
                    case Rotation.Down:
                        newPoint.Y++;
                        break;
                    case Rotation.Left:
                        newPoint.X--;
                        break;
                }
                if (grid.ContainsKey(newPoint))
                {
                    continue;
                }
                var neigbour = tiles.First(t => t.Id == item);
                grid.Add(newPoint, neigbour);
                AddNeighboursToGrid(neigbour, newPoint, grid, tiles);
            }
        }
    }

    public class Tile
    {
        public Tile()
        {
            Sides = new List<Side>();
            NeigbouringIds = new Dictionary<long, Rotation>();
            Lines = new List<string>();
            Rotation = Rotation.Up;
            RotatedCorrectly = false;
        }
        public long Id { get; set; }
        public List<Side> Sides { get; set; }
        public List<string> Lines { get; set; }
        public Dictionary<long, Rotation> NeigbouringIds { get; set; }
        public Rotation Rotation { get; set; }
        public bool RotatedCorrectly { get; set; }

        public void AddNeighbour(Tile neigbour)
        {
            foreach (var side in neigbour.Sides)
            {
                var matchingSide = Sides.FirstOrDefault(s => s.SideString == side.SideString);
                if (matchingSide != null)
                {
                    NeigbouringIds.Add(neigbour.Id, matchingSide.Rotation);
                }
                else
                {
                    matchingSide = Sides.FirstOrDefault(s => s.SideString == new string(side.SideString.Reverse().ToArray()));
                    if (matchingSide != null)
                    {
                        NeigbouringIds.Add(neigbour.Id, matchingSide.Rotation);
                    }
                }
            }
        }

        public void Rotate(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                foreach (var side in Sides)
                {
                    side.Rotation = (Rotation)(((int)side.Rotation + 1) % 4);
                }

                var rotateResult = new List<List<char>>();
                for (int x = 0; x < Lines.Count; x++)
                {
                    List<char> newLine = new List<char>();
                    for (int y = 0; y < Lines.Count; y++)
                    {
                        newLine.Add(Lines[Lines.Count - 1 - y][x]);
                    }
                    rotateResult.Add(newLine);
                }
                Lines = new List<string>();
                foreach (var line in rotateResult)
                {
                    Lines.Add(new string(line.ToArray()));
                }

            }
        }
        public void FlipVertical()
        {
            if (Sides.Count > 0)
            {
                string up = Sides.First(s => s.Rotation == Rotation.Up).SideString;
                string right = Sides.First(s => s.Rotation == Rotation.Right).SideString;
                string down = Sides.First(s => s.Rotation == Rotation.Down).SideString;
                string left = Sides.First(s => s.Rotation == Rotation.Left).SideString;
                Sides.First(s => s.Rotation == Rotation.Up).SideString = new string(down.Reverse().ToArray());
                Sides.First(s => s.Rotation == Rotation.Down).SideString = new string(up.Reverse().ToArray());
                Sides.First(s => s.Rotation == Rotation.Left).SideString = new string(left.Reverse().ToArray());
                Sides.First(s => s.Rotation == Rotation.Right).SideString = new string(right.Reverse().ToArray()); 
            }

            var newLines = new List<string>();
            for (int i = 0; i < Lines.Count; i++)
            {
                newLines.Add(Lines[Lines.Count - 1 - i]);
            }
            Lines = newLines;
        }

        public void FlipHorizontal()
        {
            if (Sides.Count > 0)
            {
                string up = Sides.First(s => s.Rotation == Rotation.Up).SideString;
                string right = Sides.First(s => s.Rotation == Rotation.Right).SideString;
                string down = Sides.First(s => s.Rotation == Rotation.Down).SideString;
                string left = Sides.First(s => s.Rotation == Rotation.Left).SideString;
                Sides.First(s => s.Rotation == Rotation.Up).SideString = new string(up.Reverse().ToArray());
                Sides.First(s => s.Rotation == Rotation.Down).SideString = new string(down.Reverse().ToArray());
                Sides.First(s => s.Rotation == Rotation.Left).SideString = new string(right.Reverse().ToArray());
                Sides.First(s => s.Rotation == Rotation.Right).SideString = new string(left.Reverse().ToArray());
            }
            var newLines = new List<string>();
            for (int i = 0; i < Lines.Count; i++)
            {
                newLines.Add(new string(Lines[i].Reverse().ToArray()));
            }
            Lines = newLines;
        }

        public void DropEdgeOfLines()
        {
            var newLines = new List<string>();
            for (int i = 1; i < Lines.Count - 1; i++)
            {
                newLines.Add(Lines[i].Substring(1, Lines[i].Length - 2));
            }
            Lines = newLines;
        }
    }

    public class Side
    {
        public string SideString { get; set; }
        public Rotation Rotation { get; set; }
    }
    public enum Rotation
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }
}
