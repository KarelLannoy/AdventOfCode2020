using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day17
    {
        private static List<ThreeDimensionalCube> _threeDimensionalcubeDirections = new List<ThreeDimensionalCube>();
        public static long One()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        _threeDimensionalcubeDirections.Add(new ThreeDimensionalCube(x, y, z));
                    }
                }
            }
            _threeDimensionalcubeDirections.Remove(new ThreeDimensionalCube(0, 0, 0));

            Dictionary<ThreeDimensionalCube, bool> cubes = new Dictionary<ThreeDimensionalCube, bool>();
            var input = InputParser.GetInputLines("day17.txt");
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    cubes.Add(new ThreeDimensionalCube(x, y, 0), input[y][x] == '#');
                }
            }

            for (int i = 0; i < 6; i++)
            {
                cubes = CycleCubes(cubes);
            }

            return cubes.Count(c => c.Value);
        }

        private static Dictionary<ThreeDimensionalCube, bool> CycleCubes(Dictionary<ThreeDimensionalCube, bool> cubes)
        {
            var cubesCopy = cubes.ToDictionary(entry => entry.Key, entry => entry.Value);
            foreach (var cube in cubes)
            {
                var neigbouringCubes = GetNeighbouringCubes(cube.Key);
                foreach (var neigbourCube in neigbouringCubes)
                {
                    if (!cubesCopy.ContainsKey(neigbourCube))
                        cubesCopy.Add(neigbourCube, false);
                }
            }

            var cubesResult = cubesCopy.ToDictionary(entry => entry.Key, entry => entry.Value);
            foreach (var cube in cubesCopy)
            {
                var activeNeighbours = GetNumberOfActiveNeigbours(cube.Key, cubes);
                if (cube.Value)
                {
                    cubesResult[cube.Key] = (activeNeighbours == 2 || activeNeighbours == 3);
                }
                else
                {
                    cubesResult[cube.Key] = (activeNeighbours == 3);
                }
            }
            return cubesResult;
        }

        private static int GetNumberOfActiveNeigbours(ThreeDimensionalCube key, Dictionary<ThreeDimensionalCube, bool> cubes)
        {
            var neigbours = GetNeighbouringCubes(key);
            var counter = 0;
            foreach (var neigbour in neigbours)
            {
                if (cubes.ContainsKey(neigbour) && cubes[neigbour])
                    counter++;
            }
            return counter;
        }

        private static List<ThreeDimensionalCube> GetNeighbouringCubes(ThreeDimensionalCube cube)
        {
            var result = new List<ThreeDimensionalCube>();
            foreach (var cubeDirection in _threeDimensionalcubeDirections)
            {
                result.Add(new ThreeDimensionalCube(cube.X + cubeDirection.X, cube.Y + cubeDirection.Y, cube.Z + cubeDirection.Z));
            }
            return result;
        }

        private static List<FourDimensionalCube> _fourDimensionalcubeDirections = new List<FourDimensionalCube>();
        public static long Two()
        {

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        for (int w = -1; w <= 1; w++)
                        {
                            _fourDimensionalcubeDirections.Add(new FourDimensionalCube(x, y, z, w));
                        }
                    }
                }
            }
            _fourDimensionalcubeDirections.Remove(new FourDimensionalCube(0, 0, 0, 0));

            Dictionary<FourDimensionalCube, bool> cubes = new Dictionary<FourDimensionalCube, bool>();
            var input = InputParser.GetInputLines("day17.txt");
            for (int y = 0; y < input.Count; y++)
            {
                for (int x = 0; x < input[y].Length; x++)
                {
                    cubes.Add(new FourDimensionalCube(x, y, 0, 0), input[y][x] == '#');
                }
            }

            Stopwatch st = new Stopwatch();
            st.Start();
            for (int i = 0; i < 6; i++)
            {
                cubes = CycleFourDimensionalCubes(cubes);
                Console.WriteLine($"Itteration {i} - time: {st.ElapsedMilliseconds} ms");
            }

            return cubes.Count(c => c.Value);
        }

        private static Dictionary<FourDimensionalCube, bool> CycleFourDimensionalCubes(Dictionary<FourDimensionalCube, bool> cubes)
        {
            var cubesCopy = cubes.ToDictionary(entry => entry.Key, entry => entry.Value);
            foreach (var cube in cubes)
            {
                var neigbouringCubes = GetNeighbouringFourDimensionalCubes(cube.Key);
                foreach (var neigbourCube in neigbouringCubes)
                {
                    if (!cubesCopy.ContainsKey(neigbourCube))
                        cubesCopy.Add(neigbourCube, false);
                }
            }

            var cubesResult = cubesCopy.ToDictionary(entry => entry.Key, entry => entry.Value);
            foreach (var cube in cubesCopy)
            {
                var activeNeighbours = GetNumberOfActiveFourDimensionalNeigbours(cube.Key, cubes);
                if (cube.Value)
                {
                    cubesResult[cube.Key] = (activeNeighbours == 2 || activeNeighbours == 3);
                }
                else
                {
                    cubesResult[cube.Key] = (activeNeighbours == 3);
                }
            }

            var ActiveKubes = cubesResult.Where(c => c.Value).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var minOuterBoundX = ActiveKubes.Keys.Min(k => k.X);
            var maxOuterBoundX = ActiveKubes.Keys.Max(k => k.X);
            var minOuterBoundY = ActiveKubes.Keys.Min(k => k.Y);
            var maxOuterBoundY = ActiveKubes.Keys.Max(k => k.Y);
            var minOuterBoundZ = ActiveKubes.Keys.Min(k => k.Z);
            var maxOuterBoundZ = ActiveKubes.Keys.Max(k => k.Z);
            var minOuterBoundW = ActiveKubes.Keys.Min(k => k.W);
            var maxOuterBoundW = ActiveKubes.Keys.Max(k => k.W);

            
            return cubesResult.Where(k=>k.Key.X >= minOuterBoundX && k.Key.X <= maxOuterBoundX && k.Key.Y >= minOuterBoundY && k.Key.Y <= maxOuterBoundY && k.Key.Z >= minOuterBoundZ && k.Key.Z <= maxOuterBoundZ && k.Key.W >= minOuterBoundW && k.Key.W <= maxOuterBoundW).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static List<FourDimensionalCube> GetNeighbouringFourDimensionalCubes(FourDimensionalCube cube)
        {
            var result = new List<FourDimensionalCube>();
            foreach (var cubeDirection in _fourDimensionalcubeDirections)
            {
                result.Add(new FourDimensionalCube(cube.X + cubeDirection.X, cube.Y + cubeDirection.Y, cube.Z + cubeDirection.Z, cube.W + cubeDirection.W));
            }
            return result;
        }

        private static int GetNumberOfActiveFourDimensionalNeigbours(FourDimensionalCube key, Dictionary<FourDimensionalCube, bool> cubes)
        {
            var neigbours = GetNeighbouringFourDimensionalCubes(key);
            var counter = 0;
            foreach (var neigbour in neigbours)
            {
                if (cubes.ContainsKey(neigbour) && cubes[neigbour])
                    counter++;
            }
            return counter;
        }
    }
    public struct ThreeDimensionalCube
    {
        public ThreeDimensionalCube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }

    public struct FourDimensionalCube
    {
        public FourDimensionalCube(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int W { get; set; }
    }
}
