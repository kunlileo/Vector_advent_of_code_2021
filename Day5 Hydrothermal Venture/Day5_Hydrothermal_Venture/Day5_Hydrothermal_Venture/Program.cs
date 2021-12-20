using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace Day5_Hydrothermal_Venture
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day5 Hydrothermal Venture\Day5_Hydrothermal_Venture\Day5_Hydrothermal_Venture\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);
      var coordinatesAll = lines.Select(l => Regex.Matches(l, @"\d+").Cast<Match>().Select(i=>int.Parse(i.Value)).ToArray()).ToArray();
      int maxRow = Math.Max(coordinatesAll.Max(o => o[0]), coordinatesAll.Max(o => o[2]));
      int maxCol = Math.Max(coordinatesAll.Max(o => o[1]), coordinatesAll.Max(o => o[3]));
      int[,] field = new int[maxRow+1, maxCol+1];

      // part1
      for (int i = 0; i < coordinatesAll.Length; i++)
      {
        DrawStraightLine(field, coordinatesAll[i]);
      }
      Console.WriteLine("Ans part1: "+ CountGreater(field, 2));

      // part2
      //field = new int[maxRow + 1, maxCol + 1];
      for (int i = 0; i < coordinatesAll.Length; i++)
      {
        //DrawStraightLine(field, coordinatesAll[i]);
        DrawDiagLine(field, coordinatesAll[i]);
      }
      Console.WriteLine("Ans part2: " + CountGreater(field, 2));
      Console.ReadKey();
    }

    static void Print(int[,] field)
    {
      Console.WriteLine("Printing Field: ...................................");
      for (int i = 0; i < field.GetLength(0); i++)
      {
        for (int j = 0; j < field.GetLength(1); j++)
        {
          Console.Write((field[i,j]==0 ? "." : field[i, j].ToString()) +" ");
        }
        Console.WriteLine();
      }
    }

    static void DrawStraightLine(int[,] field, int[] coordinates)
    {
      int x1 = coordinates[0], y1 = coordinates[1], x2 = coordinates[2], y2 = coordinates[3];
      if (x1 == x2)
      {
        for (int i = Math.Min(y1,y2); i <= Math.Max(y1, y2); i++)
        {
          field[x1, i]++;
        }
      }
      else if (y1 == y2)
      {
        for (int i = Math.Min(x1, x2); i <= Math.Max(x1, x2); i++)
        {
          field[i, y1]++;
        }
      }
    }

    static void DrawDiagLine(int[,] field, int[] coordinates)
    {
      int x1 = coordinates[0], y1 = coordinates[1], x2 = coordinates[2], y2 = coordinates[3];
      if (Math.Abs(x1-x2)==Math.Abs(y1-y2))
      {
        int minX = Math.Min(x1, x2), yForMinX = 0, stepY = 1;
        if (minX == x1)
        {
          yForMinX = y1;
          stepY = y1 < y2 ? 1 : -1;
        }
        else
        {
          yForMinX = y2;
          stepY = y2 < y1 ? 1 : -1;
        }

        for (int i = minX; i <= Math.Max(x1, x2); i++)
        {
          field[i, yForMinX]++;
          yForMinX += stepY;
        }
      }
    }

    static int CountGreater(int[,] field, int target)
    {
      int count = 0;
      for (int i = 0; i < field.GetLength(0); i++)
      {
        for (int j = 0; j < field.GetLength(1); j++)
        {
          if (field[i, j] >= target)
          {
            count++;
          }
        }
      }

      return count;
    }
  }
}
