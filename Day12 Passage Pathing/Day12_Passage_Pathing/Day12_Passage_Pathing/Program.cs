using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12_Passage_Pathing
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day12 Passage Pathing\Day12_Passage_Pathing\Day12_Passage_Pathing\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);
      Dictionary<string, HashSet<string>> nodes2NeigbhoursMap = new Dictionary<string, HashSet<string>>();
      foreach (string line in lines)
      {
        var nodes = line.Split("-");
        UpdataDict(nodes2NeigbhoursMap, nodes[0], nodes[1]);
        UpdataDict(nodes2NeigbhoursMap, nodes[1], nodes[0]);
      }


      // part1
      HashSet<string> foundedPaths = new HashSet<string>();
      FindingPath("start", nodes2NeigbhoursMap, new List<string>() { "start" }, foundedPaths);
      //Console.WriteLine(string.Join("\n", foundedPaths));
      Console.WriteLine("Ans part1: " + foundedPaths.Count);

      // part2
      List<string> smallCaves =
        nodes2NeigbhoursMap.Keys.Where(o => o != "start" && o != "end" && IsSmallCave(o)).ToList();
      foundedPaths.Clear();
      foreach (string smallCave in smallCaves)
      {
        FindingPathWithSingleAmazingSmallCave("start", smallCave, nodes2NeigbhoursMap, new List<string>() { "start" }, foundedPaths);
        //Console.WriteLine(string.Join("\n", foundedPaths.OrderBy(o=>o)));
      }

      int ans2 = foundedPaths.Count;
      Console.WriteLine("Ans part2 : "+ans2);
      Console.ReadKey();
    }

    static void UpdataDict(Dictionary<string, HashSet<string>> dict, string key, string value)
    {
      if (dict.ContainsKey(key))
      {
        dict[key].Add(value);
      }
      else
      {
        dict.Add(key, new HashSet<string>() { value });
      }
    }

    static bool IsSmallCave(string cave)
    {
      return cave.ToLower() == cave;
    }

    static void FindingPath(string currentNode, Dictionary<string, HashSet<string>> dict, List<string> visitedNode, HashSet<string> foundedPath)
    {
      HashSet<string> nodesToVisit = dict[currentNode];
      foreach (string node in nodesToVisit)
      {
        if (node == "end")
        {
          visitedNode.Add(node);
          foundedPath.Add(string.Join(",", visitedNode));
        }
        else
        {
          if (!IsSmallCave(node) || (IsSmallCave(node) && !visitedNode.Contains(node)))
          {
            List<string> vistedCpy = visitedNode.Select(i => i).ToList();
            vistedCpy.Add(node);
            FindingPath(node, dict, vistedCpy, foundedPath);
          }
        }
      }
    }

    static void FindingPathWithSingleAmazingSmallCave(string currentNode, string amazingSmallCave, Dictionary<string, HashSet<string>> dict, List<string> visitedNode, HashSet<string> foundedPath)
    {
      HashSet<string> nodesToVisit = dict[currentNode];
      foreach (string node in nodesToVisit)
      {
        if (node == "end")
        {
          visitedNode.Add(node);
          foundedPath.Add(string.Join(",", visitedNode));
        }
        else
        {
          
          if (!IsSmallCave(node)|| (node != amazingSmallCave && !visitedNode.Contains(node)) ||
                   (node == amazingSmallCave && visitedNode.Count(n => n == node) < 2))
          {
            List<string> vistedCpy = visitedNode.Select(i => i).ToList();
            vistedCpy.Add(node);
            FindingPathWithSingleAmazingSmallCave(node, amazingSmallCave, dict, vistedCpy, foundedPath);
          }
        }
      }
    }
  }
}
