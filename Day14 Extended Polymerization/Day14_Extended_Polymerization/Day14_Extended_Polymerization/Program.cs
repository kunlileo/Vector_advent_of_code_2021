using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14_Extended_Polymerization
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day14 Extended Polymerization\Day14_Extended_Polymerization\Day14_Extended_Polymerization\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath).Where(l => !string.IsNullOrEmpty(l)).ToArray();
      string template = lines[0];
      Dictionary<string, string> insertionRule =
        lines.Skip(1).Select(i => i.Split(" -> ").ToArray()).ToDictionary(o => o[0], o => o[1]);

      // part1 Brute Force
      //string pattern = string.Join("|", insertionRule.Keys.Select(k => "(?=(" + k + "))"));
      //for (int i = 0; i < 4; i++)
      //{
      //  var matches = Regex.Matches(template, @pattern);
      //  int offset = 0;
      //  foreach (Match m in matches)
      //  {
      //    template = template.Insert(m.Index + 1 + offset, insertionRule[template.Substring(m.Index + offset, 2)]);
      //    offset++;
      //  }
      //}
      //Dictionary<char, int> charFreqMap = template.GroupBy(o => o).ToDictionary(o => o.Key, o => o.Count());
      //Console.WriteLine("Ans part1: Len: " + template.Length + " res: " + (charFreqMap.Values.Max() - charFreqMap.Values.Min()));

      // part2
      Dictionary<string, long> pattern2CountMap = new Dictionary<string, long>();
      template = lines[0];
      for (int i = 0; i < template.Length - 1; i++)
      {
        string key = template.Substring(i, 2);
        UpdateDictionary(pattern2CountMap, key);
      }

      for (int i = 0; i < 40; i++)
      {
        //var allKeys = pattern2CountMap.Keys.Select(i=>i).ToList();
        Dictionary<string, long> newPattern2CountMap = new Dictionary<string, long>();
        foreach (string key in pattern2CountMap.Keys)
        {
          if (insertionRule.ContainsKey(key))
          {
            long increment = pattern2CountMap[key];
            UpdateDictionary(newPattern2CountMap, key.Substring(0, 1) + insertionRule[key], increment);
            UpdateDictionary(newPattern2CountMap, insertionRule[key] + key.Substring(1, 1), increment);
          }
        }

        pattern2CountMap = newPattern2CountMap;
      }

      var ans = ConvertPatternCount2CharCount(pattern2CountMap);
      long res = ans.Values.Max() - ans.Values.Min();
      Console.WriteLine("Ans part1: "+ res);
      Console.ReadKey();
    }


    static void UpdateDictionary<T>(Dictionary<T, long> dict, T key, long increment = 1)
    {
      if (dict.ContainsKey(key))
      {
        dict[key]+=increment;
      }
      else
      {
        dict.Add(key, increment);
      }
    }

    static Dictionary<char, long> ConvertPatternCount2CharCount(Dictionary<string, long> dict)
    {
      Dictionary<char, long> charFreqCount = new Dictionary<char, long>();
      foreach (string key in dict.Keys)
      {
        foreach (char c in key)
        {
          UpdateDictionary(charFreqCount, c, dict[key]);
        }
      }

      var allLetters = charFreqCount.Keys.Select(i => i).ToArray();
      foreach (char letter in allLetters)
      {
        charFreqCount[letter] = (charFreqCount[letter] + 1) / 2;
      }

      return charFreqCount;
    }
  }
}
