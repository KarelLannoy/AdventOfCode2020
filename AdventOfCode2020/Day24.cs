using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day24
    {
        public static long One()
        {
            var input = InputParser.GetInputLines("day24.txt");
            var coordinates = GetCoordinates(input);

            var grid = new Dictionary<Point, bool>();
            foreach (var coordinate in coordinates)
            {
                flipTile(coordinate, grid);
            }

            return grid.Count(kvp => kvp.Value);
        }

        private static void flipTile(List<Heading> headings, Dictionary<Point, bool> grid)
        {
            var startPoint = new Point(0, 0);
            foreach (var heading in headings)
            {
                switch (heading)
                {
                    case Heading.East:
                        startPoint.X+=2;
                        break;
                    case Heading.SouthEast:
                        startPoint.X++;
                        startPoint.Y--;
                        break;
                    case Heading.SouthWest:
                        startPoint.X--;
                        startPoint.Y--;
                        break;
                    case Heading.West:
                        startPoint.X-=2;
                        break;
                    case Heading.NorthWest:
                        startPoint.X--;
                        startPoint.Y++;
                        break;
                    case Heading.NorthEast:
                        startPoint.X++;
                        startPoint.Y++;
                        break;
                    default:
                        break;
                }
            }
            if (grid.ContainsKey(startPoint))
                grid[startPoint] = !grid[startPoint];
            else
                grid.Add(startPoint, true);
        }

        private static List<List<Heading>> GetCoordinates(List<string> input)
        {
            List<List<Heading>> coordinates = new List<List<Heading>>();
            foreach (var line in input)
            {
                var index = 0;
                var coordinate = new List<Heading>();
                while (index < line.Length)
                {
                    char c = line[index];
                    if (c == 'e')
                    {
                        coordinate.Add(Heading.East);
                    }
                    else if (c == 'w')
                    {
                        coordinate.Add(Heading.West);
                    }
                    else if (c == 's')
                    {
                        var nextC = line[index + 1];
                        if (nextC == 'e')
                        {
                            coordinate.Add(Heading.SouthEast);
                        }
                        else
                        {
                            coordinate.Add(Heading.SouthWest);
                        }
                        index++;
                    }
                    else
                    {
                        var nextC = line[index + 1];
                        if (nextC == 'e')
                        {
                            coordinate.Add(Heading.NorthEast);
                        }
                        else
                        {
                            coordinate.Add(Heading.NorthWest);
                        }
                        index++;
                    }
                    index++;
                }
                coordinates.Add(coordinate);
            }
            return coordinates;
        }

        public static long Two()
        {
            var input = InputParser.GetInputLines("day24.txt");
            var coordinates = GetCoordinates(input);

            var grid = new Dictionary<Point, bool>();
            foreach (var coordinate in coordinates)
            {
                flipTile(coordinate, grid);
            }

            for (int i = 0; i < 100; i++)
            {
                FlipGrid(grid);
                
            }
            return grid.Count(kvp => kvp.Value);
        }

        private static void FlipGrid(Dictionary<Point, bool> grid)
        {
            var referenceGrid = grid.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (var tile in referenceGrid)
            {
                AddNeigbours(tile.Key, grid);
            }
            referenceGrid = grid.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (var tile in referenceGrid)
            {
                var numberOfBlackNeigbours = GetNumberOfBlackNeighbours(tile.Key, referenceGrid);
                if (tile.Value && (numberOfBlackNeigbours == 0 || numberOfBlackNeigbours > 2))
                {
                    grid[tile.Key] = false;
                }
                if (!tile.Value && numberOfBlackNeigbours == 2)
                {
                    grid[tile.Key] = true;
                }
            }

        }

        private static int GetNumberOfBlackNeighbours(Point key, Dictionary<Point, bool> referenceGrid)
        {
            List<Point> neigbourPoints = GetNeigbourPoints(key);
            var counter = 0;
            foreach (var neighbour in neigbourPoints)
            {
                if (referenceGrid.ContainsKey(neighbour) && referenceGrid[neighbour])
                {
                    counter++;
                }
            }
            return counter;
        }

        private static void AddNeigbours(Point key, Dictionary<Point, bool> grid)
        {
            List<Point> neigbourPoints = GetNeigbourPoints(key);

            foreach (var neigbour in neigbourPoints)
            {
                if (!grid.ContainsKey(neigbour))
                {
                    grid.Add(neigbour, false);
                }
            }
        }

        private static List<Point> GetNeigbourPoints(Point key)
        {
            return new List<Point> { new Point(key.X + 2, key.Y), new Point(key.X + 1, key.Y - 1),
                new Point(key.X - 1, key.Y - 1), new Point(key.X - 2, key.Y), new Point(key.X - 1, key.Y + 1), new Point(key.X + 1, key.Y + 1) };
        }
    }

    public enum Heading
    {
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest,
        NorthEast
    }
}
