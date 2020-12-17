using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Day01.One());
            //Console.WriteLine(Day01.Two());

            //Console.WriteLine(Day02.One());
            //Console.WriteLine(Day02.Two());

            //Console.WriteLine(Day03.One());
            //Console.WriteLine(Day03.Two());

            //Console.WriteLine(Day04.One());
            //Console.WriteLine(Day04.Two());

            //Console.WriteLine(Day05.One());
            //Console.WriteLine(Day05.Two());

            //Console.WriteLine(Day06.One());
            //Console.WriteLine(Day06.Two());

            //Console.WriteLine(Day08.One());
            //Console.WriteLine(Day08.Two());

            //Console.WriteLine(Day09.One());
            //Console.WriteLine(Day09.Two());

            //Console.WriteLine(Day10.One());
            //Console.WriteLine(Day10.Two());

            //Console.WriteLine(Day11.One());
            //Console.WriteLine(Day11.Two());

            //Console.WriteLine(Day12.One());
            //Console.WriteLine(Day12.Two());

            //Console.WriteLine(Day13.One());
            //Console.WriteLine(Day13.Two());

            //Console.WriteLine(Day14.One());
            //Console.WriteLine(Day14.Two());

            Stopwatch sw = new Stopwatch();
            //sw.Start();
            //Console.WriteLine(Day15.One());
            //Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
            //sw.Restart();
            //Console.WriteLine(Day15.Two());
            //Console.WriteLine($"{sw.ElapsedMilliseconds} ms");

            //sw.Start();
            //Console.WriteLine(Day16.One());
            //Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
            //sw.Restart();
            //Console.WriteLine(Day16.Two());
            //Console.WriteLine($"{sw.ElapsedMilliseconds} ms");

            sw.Start();
            Console.WriteLine(Day17.One());
            Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
            sw.Restart();
            Console.WriteLine(Day17.Two());
            Console.WriteLine($"{sw.ElapsedMilliseconds} ms");


            sw.Stop();
            Console.ReadLine();
        }

        
    }
}
