using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Day10_Syntax_Scoring
{
  class Program
  {

    private static string inputFilePath = @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day10 Syntax Scoring\Day10_Syntax_Scoring\Day10_Syntax_Scoring\InputFile.txt";
    private static Dictionary<char, int> penalties = new Dictionary<char, int>()
    {
      {')', 3},{']', 57},{'}', 1197},{'>', 25137}
    };

    private static Dictionary<char, long> scores = new Dictionary<char, long>()
    {
      {')', 1},{']', 2},{'}', 3},{'>', 4}
    };

    private static Dictionary<char, char> paired = new Dictionary<char, char>()
    {
      {'(', ')'}, {'[', ']'}, {'{', '}'}, {'<', '>'}
    };

    private static Dictionary<char, char> pairedReverse = new Dictionary<char, char>()
    {
      {')', '('}, {']', '['}, {'}', '{'}, {'>', '<'}
    };

    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);
      // part1
      int totalPenalties = lines.Sum(l => GetPenaltyForLine(l));
      Console.WriteLine("Ans Part1 : " + totalPenalties);

      // part2
      var allScores = lines.Select(l => GetRemainingStr(l)).Where(l => !string.IsNullOrEmpty(l)).Select(l=>GetCompletionScore(l))
        .OrderBy(i=>i).ToArray();
      Console.WriteLine("Ans Part2 : " + allScores[allScores.Length/2]);
      //Console.WriteLine(string.Join("\n", lines.Select(l=>GetRemainingStr(l)).Where(l=>!string.IsNullOrEmpty(l))));
      Console.ReadKey();
    }

    static int GetPenaltyForLine(string line)
    {
      Stack<char> coll = new Stack<char>();
      foreach (char c in line)
      {
        if (pairedReverse.ContainsKey(c))
        {
          if (coll.Count < 1 || coll.Peek() != pairedReverse[c])
          {
            return penalties[c];
          }
          else
          {
            coll.Pop();
          }
        }
        else
        {
          coll.Push(c);
        }
      }
      return 0;
    }

    static string GetRemainingStr(string line)
    {
      Stack<char> coll = new Stack<char>();
      foreach (char c in line)
      {
        if (pairedReverse.ContainsKey(c))
        {
          if (coll.Count < 1 || coll.Peek() != pairedReverse[c])
          {
            return "";
          }
          else
          {
            coll.Pop();
          }
        }
        else
        {
          coll.Push(c);
        }
      }
      return string.Concat(coll.Select(i=>paired[i]));
    }

    static long GetCompletionScore(string line)
    {
      return line.Aggregate((long)0, (pre, cur)=>pre = pre*5+scores[cur]);
    }
  }
}
