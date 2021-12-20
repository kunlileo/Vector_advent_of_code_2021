using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4_Giant_Squid
{
  class Program
  {
    static string inputFilePath = @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day4 Giant Squid\Day4 Giant Squid\Day4 Giant Squid\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllText(inputFilePath);
      var content = lines.Split("\r\n\r\n");
      var numberOrder = content[0].Split(",").Select(i=>int.Parse(i)).ToArray();
      var matrixes = content.Skip(1).Select(c => c.Split("\r\n").
        Select(l=>l.Split(" ").Where(o=>!string.IsNullOrEmpty(o)).Select(i=>int.Parse(i)).ToArray()).ToArray()).ToArray();

      // part1
      int idx = 0, totalNumbers = numberOrder.Length, score = 0;
      while (idx < totalNumbers && score==0)
      {
        for (int i = 0; i < matrixes.GetLength(0); i++)
        {
          MarkeNumber(matrixes[i], numberOrder[idx]);

          if (IsBingo(matrixes[i]))
          {
            score = numberOrder[idx] * SumUnMarked(matrixes[i]);
          }
        }

        idx++;
      }
      Console.WriteLine("Ans part1: " + score);

      // part2
      var matrixesPart2 = content.Skip(1).Select(c => c.Split("\r\n").
        Select(l => l.Split(" ").Where(o => !string.IsNullOrEmpty(o)).Select(i => int.Parse(i)).ToArray()).ToArray()).ToArray();
      idx = 0;
      score = 0;
      Dictionary<int, bool> isABingoMatp = new Dictionary<int, bool>();
      Enumerable.Range(0, totalNumbers).ToList().ForEach(o=>isABingoMatp.Add(o, false));
      while (idx < totalNumbers && !isABingoMatp.Values.All(o=>o))
      {
        for (int i = 0; i < matrixesPart2.GetLength(0); i++)
        {
          if (!isABingoMatp[i])
          {
            MarkeNumber(matrixesPart2[i], numberOrder[idx]);
            bool isABingo = IsBingo(matrixesPart2[i]);
            if (isABingo)
            {
              isABingoMatp[i] = true;
              score = numberOrder[idx] * SumUnMarked(matrixesPart2[i]);
            }
          }
        }

        idx++;
      }
      Console.WriteLine("Ans part2: " + score);
      Console.ReadKey();
    }

    static void MarkeNumber(int[][] matrix, int target)
    {
      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] == target)
          {
            matrix[i][j] = -1;
          }
        }
      }
    }

    static bool IsBingo(int[][] matrix)
    {
      return CheckRow(matrix) || CheckCol(matrix);
    }

    static bool CheckRow(int[][] matrix)
    {
      for (int i = 0; i < matrix.Length; i++)
      {
        if (matrix[i][0] == -1)
        {
          bool isABingo = true;
          for (int j = 1; j < matrix[i].Length; j++)
          {
            if (matrix[i][j] != -1)
            {
              isABingo = false;
              break;
            }
          }

          if (isABingo)
          {
            return true;
          }
        }
      }

      return false;
    }

    static bool CheckCol(int[][] matrix)
    {
      for (int j = 0; j < matrix[0].Length; j++)
      {
        if (matrix[0][j] == -1)
        {
          bool isABingo = true;
          for (int i = 1; i < matrix.Length; i++)
          {
            if (matrix[i][j] != -1)
            {
              isABingo = false;
              break;
            }
          }

          if (isABingo)
          {
            return true;
          }
        }
      }

      return false;
    }

    static int SumUnMarked(int[][] matrix)
    {
      int sum = 0;
      for (int i = 0; i < matrix.Length; i++)
      {
        for (int j = 0; j < matrix[i].Length; j++)
        {
          if (matrix[i][j] >=0)
          {
            sum+=matrix[i][j];
          }
        }
      }
      return sum;
    }

    static void Print(int[][] matrix)
    {
      Console.WriteLine("Print Matrix:................................");
      for (int i = 0; i < matrix.Length; i++)
      {
        Console.WriteLine(string.Join(" ", matrix[i]));
      }
    }
  }
}
