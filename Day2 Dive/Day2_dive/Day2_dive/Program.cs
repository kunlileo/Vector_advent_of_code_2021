using System;
using System.IO;

namespace Day2_dive
{
  class Program
  {
    private static string inputFilePath = @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day2 Dive\Day2_dive\Day2_dive\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);
      long horizontal = 0, depth = 0;

      // part1
      foreach (string line in lines)
      {
        string[] commands = line.Split(" ");
        long val = long.Parse(commands[1]);
        if (commands[0] == "forward")
        {
          horizontal += val;
        }
        else if (commands[0] == "up")
        {
          depth -= val;
        }
        else if (commands[0] == "down")
        {
          depth += val;
        }
      }
      Console.WriteLine("Ans part1 : "+horizontal*depth);


      // part2
      horizontal = 0;
      depth = 0;
      long aim = 0;
      foreach (string line in lines)
      {
        string[] commands = line.Split(" ");
        long val = long.Parse(commands[1]);
        if (commands[0] == "forward")
        {
          horizontal += val;
          depth += aim * val;
        }
        else if (commands[0] == "up")
        {
          aim -= val;
        }
        else if (commands[0] == "down")
        {
          aim += val;
        }
      }
      Console.WriteLine("Ans part1 : " + horizontal * depth);
      Console.ReadKey();
    }
  }
}
