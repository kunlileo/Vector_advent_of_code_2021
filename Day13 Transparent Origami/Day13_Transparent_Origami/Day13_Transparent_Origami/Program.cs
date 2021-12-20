using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace Day13_Transparent_Origami
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day13 Transparent Origami\Day13_Transparent_Origami\Day13_Transparent_Origami\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath).Where(l => !string.IsNullOrEmpty(l)).ToArray();
      List<string> coordinates = new List<string>(), foldCmds = new List<string>();
      foreach (string l in lines)
      {
        if (l.Contains("fold"))
        {
          foldCmds.Add(l);
        }
        else
        {
          coordinates.Add(l);
        }
      }

      // part1
      //foreach (string cmd in foldCmds.Take(1))
      //{
      //  if (cmd.Contains("x"))
      //  {
      //    coordinates = FoldByX(coordinates, cmd);
      //  }
      //  else
      //  {
      //    coordinates = FoldByY(coordinates, cmd);
      //  }
      //}
      //Console.WriteLine(string.Join("\n", coordinates.OrderBy(i=>i)));

      // part2
      foreach (string cmd in foldCmds)
      {
        if (cmd.Contains("x"))
        {
          coordinates = FoldByX(coordinates, cmd);
        }
        else
        {
          coordinates = FoldByY(coordinates, cmd);
        }
      }
      Console.WriteLine("Ans part1: "+coordinates.Count);
      Print(coordinates);
      Console.ReadKey();
    }

    static List<string> FoldByX(List<string> coordinates, string foldCmd)
    {
      HashSet<string> newCoordinates = new HashSet<string>();
      int axisNr = int.Parse(Regex.Match(foldCmd, @"\d+").Value);
      foreach (string coordinatesEach in coordinates)
      {
        newCoordinates.Add(GetNewCoordinatesAfterFoldingAlongX(coordinatesEach, axisNr));
      }

      return newCoordinates.ToList();
    }

    static List<string> FoldByY(List<string> coordinates, string foldCmd)
    {
      HashSet<string> newCoordinates = new HashSet<string>();
      int axisNr = int.Parse(Regex.Match(foldCmd, @"\d+").Value);

      foreach (string coordinatesEach in coordinates)
      {
        newCoordinates.Add(GetNewCoordinatesAfterFoldingAlongY(coordinatesEach, axisNr));
      }
      return newCoordinates.ToList();
    }

    static string GetNewCoordinatesAfterFoldingAlongX(string input, int xAxis)
    {
      var coordinates = input.Split(",").Select(i => int.Parse(i)).ToArray();
      int x = coordinates[0], y = coordinates[1];
      x = x < xAxis ? x : 2 * xAxis - x;
      return x + "," + y;
    }

    static string GetNewCoordinatesAfterFoldingAlongY(string input, int yAxis)
    {
      var coordinates = input.Split(",").Select(i => int.Parse(i)).ToArray();
      int x = coordinates[0], y = coordinates[1];
      y = y < yAxis ? y : 2 * yAxis - y;
      return x + "," + y;
    }

    static void Print(List<string> coordinates)
    {
      int xMax = coordinates.Max(i => int.Parse(i.Split(",")[0]));
      int yMax = coordinates.Max(i => int.Parse(i.Split(",")[1]));
      char[,] matrix = new char[yMax+1, xMax+1];
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          matrix[i,j]='.';
        }
      }

      foreach (string coordinatesEach in coordinates)
      {
        var xy = coordinatesEach.Split(",").Select(i => int.Parse(i)).ToArray();
        matrix[xy[1], xy[0]] = '#';
      }

      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          Console.Write(matrix[i, j]);
        }
        Console.WriteLine();
      }
    }
  }
}
