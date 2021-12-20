using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15_Chiton
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day15 Chiton\Day15_Chiton\Day15_Chiton\InputFile.txt";

    //private static int[] deltaX = new int[] { 0, -1, 0, 1 };
    //private static int[] deltaY = new int[] { -1, 0, 1, 0 };
    private static int[] deltaX = new int[] { 0, 1 };
    private static int[] deltaY = new int[] { 1, 0 };
    static void Main(string[] args)
    {
      var riskField = File.ReadAllLines(inputFilePath).Select(i => i.Select(o => o - '0').ToArray()).ToArray();
      HashSet<string> allRisks = new HashSet<string>();
      bool[,] detected = new bool[riskField.Length, riskField[0].Length];
      int[,] totalRiskFields = new int[riskField.Length, riskField[0].Length];
      Run(0, 0, 0, riskField, totalRiskFields);
      Console.WriteLine("Ans part1: " + (totalRiskFields[riskField.Length - 1, riskField[0].Length - 1] - riskField[0][0]));
      Console.ReadKey();
    }

    static void Run(int i, int j, int currentTotalRisk, int[][] field, int[,] totalRiskField)
    {
      if (i < 0 || i >= field.Length || j < 0 || j >= field[i].Length)
      {
        return;
      }

      currentTotalRisk += field[i][j];
      if (totalRiskField[i, j] == 0)
      {
        totalRiskField[i, j] = currentTotalRisk;
      }
      else
      {
        totalRiskField[i, j] = Math.Min(totalRiskField[i, j], currentTotalRisk);
      }

      for (int k = 0; k < 2; k++)
      {
        Run(i + deltaX[k], j + deltaY[k], totalRiskField[i, j], field, totalRiskField);
      }
    }

    static int GetRiskInField(int i, int j, int[][] field)
    {
      if (i < 0 || i >= field.Length || j < 0 || j >= field[i].Length)
      {
        return int.MaxValue;
      }

      return field[i][j];
    }
  }
}
