using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day11
    {
        private static int _xMinBound = 0;
        private static int _xMaxBound = 0;
        private static int _yMinBound = 0;
        private static int _yMaxBound = 0;
        public static int One()
        {
            var inputLines = InputParser.GetInputLines("day11.txt");
            var seatMatrix = GetInputMatrix(inputLines);


            _xMinBound = seatMatrix.Keys.Min(p => p.X);
            _yMinBound = seatMatrix.Keys.Min(p => p.Y);
            _xMaxBound = seatMatrix.Keys.Max(p => p.X);
            _yMaxBound = seatMatrix.Keys.Max(p => p.Y);


            var previousSeatMatrixCopy = new Dictionary<Point, bool>();

            while (!AreDictionariesEqual(seatMatrix, previousSeatMatrixCopy))
            {
                previousSeatMatrixCopy = seatMatrix.ToDictionary(entry => entry.Key, entry => entry.Value);

                for (int y = 0; y < inputLines.Count; y++)
                {
                    for (int x = 0; x < inputLines[y].Length; x++)
                    {
                        var newPoint = new Point(x, y);
                        if (seatMatrix.ContainsKey(newPoint))
                        {
                            var numberOfOccupiedAdjacentSeats = NumberOfOccupiedAdjacentSeats(newPoint, previousSeatMatrixCopy, false);
                            if (numberOfOccupiedAdjacentSeats == 0)
                            {
                                seatMatrix[newPoint] = true;
                            }
                            if (numberOfOccupiedAdjacentSeats >= 4)
                            {
                                seatMatrix[newPoint] = false;
                            }
                        }
                    }

                }
            }
            return seatMatrix.Count(kvp=>kvp.Value);
        }

        private static bool AreDictionariesEqual(Dictionary<Point, bool> dic1, Dictionary<Point, bool> dic2)
        {
            return dic1.Keys.All(k => dic2.ContainsKey(k) && object.Equals(dic2[k], dic1[k]));
        }

        private static int NumberOfOccupiedAdjacentSeats(Point point, Dictionary<Point, bool> seatMatrix, bool visibleAdjacent)
        {
            var count = 0;
            List<Point> adjacentPoints = new List<Point>();
            if (visibleAdjacent)
            {
                adjacentPoints = GetVisibleAdjacentPoints(point, seatMatrix);
            }else
            {
                adjacentPoints = GetAdjacentPoints(point);
            }

            foreach (var adjacentPoint in adjacentPoints)
            {
                if (seatMatrix.ContainsKey(adjacentPoint) && seatMatrix[adjacentPoint])
                {
                    count++;
                }
            }
            return count;
        }

        private static List<Point> GetVisibleAdjacentPoints(Point point, Dictionary<Point, bool> seatMatrix)
        {
            List<Direction> directions = new List<Direction>() { new Direction(-1, -1), new Direction(0, -1), new Direction(1, -1), new Direction(1, 0), new Direction(1, 1), new Direction(0, 1), new Direction(-1, 1), new Direction(-1, 0) };
            List<Point> adjacentVisiblePoints = new List<Point>();
            foreach (var direction in directions)
            {
                bool result = false;
                var newPoint = point;
                while (!result)
                {
                    newPoint = new Point(newPoint.X + direction.X, newPoint.Y + direction.Y);
                    if (!IsOutOfBound(newPoint, seatMatrix))
                    {
                        if (seatMatrix.ContainsKey(newPoint))
                        {
                            adjacentVisiblePoints.Add(newPoint);
                            result = true;
                        }
                            
                    }
                    else
                    {
                        adjacentVisiblePoints.Add(newPoint);
                        result = true;
                    }
                }
            }
            return adjacentVisiblePoints;
        }

        private static bool IsOutOfBound(Point point, Dictionary<Point, bool> seatMatrix)
        {
            return point.X < _xMinBound || point.X > _xMaxBound || point.Y < _yMinBound || point.Y > _yMaxBound;
        }

        private static List<Point> GetAdjacentPoints(Point point)
        {
            return new List<Point>() { new Point(point.X - 1, point.Y - 1), new Point(point.X, point.Y - 1), new Point(point.X + 1, point.Y - 1), new Point(point.X + 1, point.Y),
                new Point(point.X + 1, point.Y + 1), new Point(point.X, point.Y + 1), new Point(point.X - 1, point.Y + 1), new Point(point.X - 1, point.Y)};
        }

        private static Dictionary<Point, bool> GetInputMatrix(List<string> inputLines)
        {
            var result = new Dictionary<Point, bool>();
            for (int y = 0; y < inputLines.Count; y++)
            {
                for (int x = 0; x < inputLines[y].Length; x++)
                {
                    if (inputLines[y][x] != '.')
                    {
                        result.Add(new Point(x, y), false);
                    }
                    
                }
            }
            return result;
        }

        public static int Two()
        {
            var inputLines = InputParser.GetInputLines("day11.txt");
            var seatMatrix = GetInputMatrix(inputLines);

            _xMinBound = seatMatrix.Keys.Min(p => p.X);
            _yMinBound = seatMatrix.Keys.Min(p => p.Y);
            _xMaxBound = seatMatrix.Keys.Max(p => p.X);
            _yMaxBound = seatMatrix.Keys.Max(p => p.Y);

            var previousSeatMatrixCopy = new Dictionary<Point, bool>();

            while (!AreDictionariesEqual(seatMatrix, previousSeatMatrixCopy))
            {
                previousSeatMatrixCopy = seatMatrix.ToDictionary(entry => entry.Key, entry => entry.Value);

                for (int y = 0; y < inputLines.Count; y++)
                {
                    for (int x = 0; x < inputLines[y].Length; x++)
                    {
                        var newPoint = new Point(x, y);
                        if (seatMatrix.ContainsKey(newPoint))
                        {
                            var numberOfOccupiedAdjacentSeats = NumberOfOccupiedAdjacentSeats(newPoint, previousSeatMatrixCopy, true);
                            if (numberOfOccupiedAdjacentSeats == 0)
                            {
                                seatMatrix[newPoint] = true;
                            }
                            if (numberOfOccupiedAdjacentSeats >= 5)
                            {
                                seatMatrix[newPoint] = false;
                            }
                        }
                    }

                }
            }
            return seatMatrix.Count(kvp => kvp.Value);
        }
    }

    public struct Direction
    {
        public Direction(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
