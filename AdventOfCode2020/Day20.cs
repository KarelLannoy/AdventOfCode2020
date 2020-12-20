using System;
using System.Collections.Generic;
using System.Linq;
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
            return cornerTiles.Select(t=>t.Id).Aggregate((a, x) => a * x);
        }

        private static List<Tile> ParseTiles(string[] tileString)
        {
            var tiles = new List<Tile>();
            foreach (var singleTileString in tileString)
            {
                Tile tile = new Tile();
                var lines = singleTileString.Split(Environment.NewLine);
                tile.Id = long.Parse(lines[0].Replace("Tile ", "").Replace(":", ""));
                tile.Sides.Add(lines[1]);
                tile.Sides.Add(new string(lines[10].Reverse().ToArray()));
                tile.Sides.Add(new string(lines.Skip(1).Take(10).Select(l => l[9]).ToArray()));
                tile.Sides.Add(new string(lines.Skip(1).Take(10).Select(l => l[0]).Reverse().ToArray()));
                tiles.Add(tile);
            }
            return tiles;
        }

        public static long Two()
        {
            throw new Exception("No Solution");
        }
    }

    public class Tile
    {
        public Tile()
        {
            Sides = new List<string>();
            NeigbouringIds = new List<long>();
        }
        public long Id { get; set; }
        public List<string> Sides { get; set; }
        public List<long> NeigbouringIds { get; set; }

        public void AddNeighbour(Tile neigbour)
        {
            foreach (var side in neigbour.Sides)
            {
                if (Sides.Contains(side) || Sides.Contains(new string(side.Reverse().ToArray())))
                {
                    NeigbouringIds.Add(neigbour.Id);
                }
            }
        }
    }
}
