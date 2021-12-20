using System;
using System.IO;
using System.Linq;

namespace Day7_The_Treachery_of_Whales
{
  class Program
  {
    static string inputFilePath = @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day7 The Treachery of Whales\Day7_The_Treachery_of_Whales\Day7_The_Treachery_of_Whales\InputFile.txt";
    static void Main(string[] args)
    {
      var numbers = File.ReadAllText(inputFilePath).Split(",").Select(i => int.Parse(i)).ToArray();
      int minFuel = int.MaxValue;
      // part1
      for(int i = 1; i < numbers.Max(); i++)
      {
        int minFuelForI = 0;
        for(int j = 0; j < numbers.Length; j++)
        {
          minFuelForI += Math.Abs(i - numbers[j]);
        }

        if (minFuelForI < minFuel) 
        { 
          minFuel = minFuelForI; 
        }
      }
      Console.WriteLine("Ans part1: "+minFuel);

      // part1
      minFuel = int.MaxValue;
      for (int i = 0; i < numbers.Max(); i++)
      {
        int minFuelForI = 0;
        for (int j = 0; j < numbers.Length; j++)
        {
          int range = Math.Abs(i - numbers[j]);
          minFuelForI += (range+1)*range/2;
        }

        if (minFuelForI < minFuel)
        {
          minFuel = minFuelForI;
        }
      }
      Console.WriteLine("Ans part2: " + minFuel);
      Console.ReadKey();
    }
  }
}
