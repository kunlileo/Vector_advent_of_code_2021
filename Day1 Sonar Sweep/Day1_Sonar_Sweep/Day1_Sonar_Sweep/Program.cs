using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Day1_Sonar_Sweep
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day1 Sonar Sweep\Day1_Sonar_Sweep\Day1_Sonar_Sweep\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);
      int[] depths = lines.Select(d => int.Parse(d)).ToArray();

      // part1
      int count = 0;
      for (int i = 1; i < depths.Length; i++)
      {
        if (depths[i] > depths[i - 1])
        {
          count++;
        }
      }
      Console.WriteLine("Ans of part1 : "+count);

      // part2
      count = 0;
      for (int i = 1; i < depths.Length-2; i++)
      {
        if (depths[i] + depths[i + 1] + depths[i + 2] > depths[i - 1] + depths[i] + depths[i + 1])
        {
          count++;
        }
      }
      Console.WriteLine("Ans of part2 : " + count);
      Console.ReadKey();
    }
  }
}
