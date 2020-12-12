using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day12
    {
        public static int One()
        {
            var instructions = InputParser.GetInputLines("day12.txt");
            Dictionary<char, Direction> directions = new Dictionary<char, Direction>();
            char currentDirection = 'E';
            Point currentLocation = new Point(0, 0);

            directions.Add('N', new Direction(0, -1));
            directions.Add('S', new Direction(0, 1));
            directions.Add('E', new Direction(1, 0));
            directions.Add('W', new Direction(-1, 0));

            List<char> directionListRight = new List<char>() { 'N', 'E', 'S', 'W' };
            List<char> directionListLeft = new List<char>() { 'N', 'W', 'S', 'E' };

            foreach (var instruction in instructions)
            {
                var instructionLetter = instruction[0];
                var instructionNumber = int.Parse(instruction.Substring(1));
                var movePlaces = 0;
                var currentPlace = 0;
                Direction direction;
                switch (instructionLetter)
                {
                    case 'L':
                        currentPlace = directionListLeft.IndexOf(currentDirection);
                        movePlaces = (instructionNumber / 90);
                        currentDirection = directionListLeft[(currentPlace + movePlaces) % 4];
                        break;
                    case 'R':
                        currentPlace = directionListRight.IndexOf(currentDirection);
                        movePlaces = (instructionNumber / 90);
                        currentDirection = directionListRight[(currentPlace + movePlaces) % 4];
                        break;
                    case 'F':
                        direction = directions[currentDirection];
                        currentLocation.X += (direction.X * instructionNumber);
                        currentLocation.Y += (direction.Y * instructionNumber);
                        break;
                    default:
                        direction = directions[instructionLetter];
                        currentLocation.X += (direction.X * instructionNumber);
                        currentLocation.Y += (direction.Y * instructionNumber);
                        break;
                }
            }
            return Math.Abs(currentLocation.X) + Math.Abs(currentLocation.Y);  
        }

        public static int Two()
        {
            var instructions = InputParser.GetInputLines("day12.txt");
            Dictionary<char, Direction> directions = new Dictionary<char, Direction>();
            Point currentLocation = new Point(0, 0);

            directions.Add('N', new Direction(0, -1));
            directions.Add('S', new Direction(0, 1));
            directions.Add('E', new Direction(1, 0));
            directions.Add('W', new Direction(-1, 0));

            Direction wayPoint = new Direction(10, -1);

            foreach (var instruction in instructions)
            {
                var instructionLetter = instruction[0];
                var instructionNumber = int.Parse(instruction.Substring(1));
                var movePlaces = 0;
                Direction direction;
                switch (instructionLetter)
                {
                    case 'L':
                        movePlaces = (instructionNumber / 90);
                        for (int i = 1; i <= movePlaces; i++)
                        {
                            var x = wayPoint.X;
                            var y = wayPoint.Y;
                            wayPoint.X = y;
                            wayPoint.Y = -1 * x;
                        }
                        break;
                    case 'R':
                        movePlaces = (instructionNumber / 90);
                        for (int i = 1; i <= movePlaces; i++)
                        {
                            var x = wayPoint.X;
                            var y = wayPoint.Y;
                            wayPoint.X = -1 * y;
                            wayPoint.Y = x;
                        }
                        break;
                    case 'F':
                        currentLocation.X += (wayPoint.X * instructionNumber);
                        currentLocation.Y += (wayPoint.Y * instructionNumber);
                        break;
                    default:
                        direction = directions[instructionLetter];
                        wayPoint.X += (direction.X * instructionNumber);
                        wayPoint.Y += (direction.Y * instructionNumber);
                        break;
                }
            }
            return Math.Abs(currentLocation.X) + Math.Abs(currentLocation.Y);
        }
    }
}
