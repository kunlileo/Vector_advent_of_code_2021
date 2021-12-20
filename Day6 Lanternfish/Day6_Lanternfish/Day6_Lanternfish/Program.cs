using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6_Lanternfish
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day6 Lanternfish\Day6_Lanternfish\Day6_Lanternfish\InputFile.txt";

    private static int days = 256;
    static void Main(string[] args)
    {
      var lanternfishes = File.ReadAllText(inputFilePath).Split(",").Select(i=>int.Parse(i)).ToList();

      // part1
      //for (int i = 0; i < days; i++)
      //{
      //  OneRun(lanternfishes);
      //  //Console.WriteLine(string.Join(" ", lanternfishes));
      //}
      //Console.WriteLine("Ans part1: " + lanternfishes.Count);

      // part2
      Dictionary<int, long> allFishesDict = new Dictionary<int, long>()
      {
        {0, 0}, {1, 0}, {2, 0}, {3, 0}, {4, 0}, {5, 0}, {6, 0}, {7, 0}, {8, 0}
      };
      File.ReadAllText(inputFilePath).Split(",").Select(i=>int.Parse(i)).ToList().ForEach(i=>allFishesDict[i]++);
      for (int i = 0; i < days; i++)
      {
        OneRunEfficient(allFishesDict);
      }

      long ans = allFishesDict.Values.Sum();
      Console.WriteLine("Ans part2: "+ ans);
      Console.ReadKey();
    }

    static void OneRun(List<int> lanternfishes)
    {
      List<int> newFishes = new List<int>();
      for (int i = 0; i < lanternfishes.Count; i++)
      {
        if (lanternfishes[i] == 0)
        {
          newFishes.Add(8);
        }
        lanternfishes[i]--;
        if (lanternfishes[i] < 0)
        {
          lanternfishes[i] = 6;
        }
      }
      lanternfishes.AddRange(newFishes);
    }

    static void OneRunEfficient(Dictionary<int, long> dict)
    {
      Dictionary<int, long> tmp = dict.ToDictionary(i=>i.Key, i=>i.Value);
      long reborn = 0;

      foreach (int key in tmp.Keys)
      {
        if (key - 1 >= 0)
        {
          dict[key - 1] = tmp[key];
        }
        else
        {
          reborn += tmp[key];
          dict[8]= tmp[key];
        }
      }
      dict[6] += reborn;
    }
  }
}
