using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day9_Smoke_Basin
{
  class Program
  {
    private static string inputFilePath = @"C:\Users\kli\source\repos\Day9_Smoke_Basin\Day9_Smoke_Basin\InputFile.txt";
    static void Main(string[] args)
    {
      var fields = File.ReadAllLines(inputFilePath).Select(i => i.Select(i => i - '0').ToArray()).ToArray();

      // part1
      int ans = GetRiskLevel(fields);
      Console.WriteLine("Ans part1: "+ans);

      //part2
      List<int> areas = new List<int>();
      bool[,] detected = new bool[fields.Length, fields[0].Length];
      for (int i = 0; i < fields.Length; i++)
      {
        for (int j = 0; j < fields[0].Length; j++)
        {
          if (!detected[i, j] && fields[i][j] < 9)
          {
            int area = 0;
            GetBasinsArea(i, j, fields, detected, ref area);
            areas.Add(area);
          }
        }
      }
      //Console.WriteLine(string.Join(" ", areas.OrderBy(i=>i)));
      Console.WriteLine("Ans part2 : "+areas.OrderByDescending(o=>o).Take(3).Aggregate((pre, cur)=>cur*=pre));
      Console.ReadKey();
    }

    static int GetRiskLevel(int[][] field)
    {
      int risk = 0;
      for (int i = 0; i < field.Length; i++)
      {
        for (int j = 0; j < field[i].Length; j++)
        {
          bool lowerThanLeft = true, lowerThanTop = true, lowerThanRight = true, lowerThanButtom = true;
          if (j - 1 >= 0)
          {
            lowerThanLeft = field[i][j] < field[i][j - 1];
          }

          if (i - 1 >= 0)
          {
            lowerThanTop = field[i][j] < field[i-1][j];
          }

          if (j + 1 < field[i].Length)
          {
            lowerThanRight = field[i][j] < field[i][j+1];
          }

          if (i + 1 < field.Length)
          {
            lowerThanButtom = field[i][j] < field[i+1][j];
          }

          if (lowerThanLeft && lowerThanTop && lowerThanRight && lowerThanButtom)
          {
            risk += field[i][j] + 1;
          }
        }
      }

      return risk;
    }

    static void GetBasinsArea(int i, int j, int[][] field, bool[,] detected, ref int area)
    {
      if (i < 0|| i >= field.Length || j < 0 || j >= field[i].Length || detected[i,j] || field[i][j] == 9) return;
      detected[i, j] = true;
      area++;
      GetBasinsArea(i - 1, j, field, detected, ref area);
      GetBasinsArea(i + 1, j, field, detected, ref area);
      GetBasinsArea(i, j-1, field, detected, ref area);
      GetBasinsArea(i, j+1, field, detected, ref area);
    }
  }
}
