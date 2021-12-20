using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Day8_Seven_Segment_Search
{
  enum Pos
  {
    TopLeft = 6,
    BottomLeft = 4,
    BottomRight = 9
  }
  class Program
  {
    private static string inputFilePath = @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day7 The Treachery of Whales\Day8_Seven_Segment_Search\Day8_Seven_Segment_Search\InputFile.txt";
    private static int[] targetLengths = new int[] {2, 4, 3, 7};

    private static Dictionary<int, int> len2DightMap = new Dictionary<int, int>()
    {
      {2, 1}, {4, 4}, {3, 7}, {7, 8}
    };

    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);

      // part1
      var afterDelis = lines.Select(l => l.Split("|")[1].Split(" ").Where(i => !string.IsNullOrEmpty(i))).ToArray();
      int resP1 = afterDelis.Sum(i => i.Count(o => targetLengths.Contains(o.Length)));
      Console.WriteLine("Ans part1: " + resP1);

      // part2
      int sum = 0;
      foreach (string line in lines)
      {
        var parts = line.Split("|").ToArray();
        Dictionary<string, int> string2DigitMap = GetMapping(parts[0].Split(" ").Where(i => !string.IsNullOrEmpty(i)).ToArray());
        string numberAfterDeli = string.Concat(parts[1].Split(" ").
          Where(i => !string.IsNullOrEmpty(i)).
          Select(i=>string2DigitMap[string.Concat(i.OrderBy(o => o))].ToString()));
        //Console.WriteLine(numberAfterDeli);
        sum += int.Parse(numberAfterDeli);
      }
      Console.WriteLine("Ans part2 : "+sum);
      Console.ReadKey();
    }

    static Dictionary<string, int> GetMapping(string[] input)
    {
      Dictionary<char, int> charFreq = GetFreq(input);
      Dictionary<string, int> ans = new Dictionary<string, int>();
      foreach(string s in input)
      {
        string sSorted = string.Concat(s.OrderBy(i => i));
        if (targetLengths.Contains(s.Length))
        {
          ans.Add(sSorted, len2DightMap[s.Length]);
        }
        else if(s.Length==5)
        {
          if (s.Any(c => charFreq[c]==(int)Pos.TopLeft))
          {
            ans.Add(sSorted, 5);
          }
          else if (s.Any(c => charFreq[c] == (int) Pos.BottomLeft))
          {
            ans.Add(sSorted, 2);
          }
          else
          {
            ans.Add(sSorted, 3);
          }
        }
        else
        {
          if (s.All(c => charFreq[c] != (int) Pos.BottomLeft))
          {
            ans.Add(sSorted, 9);
          }
          else if (s.Count(c=>charFreq[c]==8)==2)
          {
            ans.Add(sSorted, 0);
          }
          else
          {
            ans.Add(sSorted, 6);
          }
        }
      }

      return ans;
    }

    static Dictionary<char, int> GetFreq(string[] input)
    {
      Dictionary<char, int> ans = new Dictionary<char, int>()
      {
        {'a', 0}, {'b', 0}, {'c', 0}, {'d', 0}, {'e', 0}, {'f', 0}, {'g', 0},
      };

      foreach (string s in input)
      {
        foreach (char c in s)
        {
          ans[c]++;
        }
      }
      return ans;
    }
  }
}
