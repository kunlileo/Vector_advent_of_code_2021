using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day3_Binary_Diagnostic
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day3 Binary Diagnostic\Day3_Binary_Diagnostic\Day3_Binary_Diagnostic\InputFile.txt";
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines(inputFilePath);
      int totalRows = lines.Length;
      int totalCols = lines[0].Length;
      // part1
      string gamma = "", epsilon = "";
      for (int j = 0; j < totalCols; j++)
      {
        int sum = 0;
        for (int i = 0; i < totalRows; i++)
        {
          sum += (lines[i][j] - '0');
        }

        if (sum > totalRows / 2)
        {
          gamma += '1';
          epsilon += '0';
        }
        else
        {
          gamma += '0';
          epsilon += '1';
        }
      }
      Console.WriteLine("Ans part1: " + Convert.ToInt32(gamma, 2)* Convert.ToInt32(epsilon, 2));

      // part2 1: find o2
      var stringsForO2Rating = lines;
      int colIdx = 0;
      while (stringsForO2Rating.Length > 1)
      {
        int rowSum = 0;
        for (int i = 0; i < stringsForO2Rating.Length; i++)
        {
          rowSum += (stringsForO2Rating[i][colIdx] - '0');
        }

        if (rowSum >= stringsForO2Rating.Length- rowSum)
        {
          stringsForO2Rating = stringsForO2Rating.Where(l => l[colIdx] == '1').ToArray();
        }
        else
        {
          stringsForO2Rating = stringsForO2Rating.Where(l => l[colIdx] == '0').ToArray();
        }
        colIdx++;
      }
      Console.WriteLine(stringsForO2Rating[0]);

      // part2 2: find co2
      var stringsForCO2Rating = lines;
      colIdx = 0;
      while (stringsForCO2Rating.Length > 1)
      {
        int rowSum = 0;
        for (int i = 0; i < stringsForCO2Rating.Length; i++)
        {
          rowSum += (stringsForCO2Rating[i][colIdx] - '0');
        }

        if (rowSum < stringsForCO2Rating.Length - rowSum)
        {
          stringsForCO2Rating = stringsForCO2Rating.Where(l => l[colIdx] == '1').ToArray();
        }
        else
        {
          stringsForCO2Rating = stringsForCO2Rating.Where(l => l[colIdx] == '0').ToArray();
        }

        colIdx++;
      }
      Console.WriteLine(stringsForCO2Rating[0]);
      Console.WriteLine("Ans part2: "+Convert.ToInt32(stringsForO2Rating[0],2)*Convert.ToInt32(stringsForCO2Rating[0], 2));
      Console.ReadKey();
    }
  }
}
