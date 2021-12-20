using System;
using System.IO;
using System.Linq;

namespace Day11_Dumbo_Octopus
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day11 Dumbo Octopus\Day11_Dumbo_Octopus\Day11_Dumbo_Octopus\InputFile.txt";
    static void Main(string[] args)
    {
      var field = File.ReadAllLines(inputFilePath).Select(l => l.Select(i => i - '0').ToArray()).ToArray();

      // part1
      int ans1 = 0;
      for (int i = 0; i < 100; i++)
      {
        RunOneStep(field);
        ans1 += GetLighted(field);
      }
      Console.WriteLine("Ans part1: "+ans1);

      // part2
      int step = 0;
      field = File.ReadAllLines(inputFilePath).Select(l => l.Select(i => i - '0').ToArray()).ToArray();
      while (GetLighted(field) != field.Length * field[0].Length)
      {
        RunOneStep(field);
        step++;
      }
      Console.WriteLine("Ans part2: " + step);
      Console.ReadKey();
    }

    static void RunOneStep(int[][] field)
    {
      bool[,] alreadyLigthed = new bool[field.Length, field[0].Length];
      for (int i = 0; i < field.Length; i++)
      {
        for (int j = 0; j < field[i].Length; j++)
        {
          Update(i, j, field, alreadyLigthed);
        }
      }

      //Print(field);
    }

    static void LightUpSurrending(int i, int j, int[][] field, bool[,] alreadyLighted)
    {
      if (i - 1 >= 0 && j - 1 >= 0)
      {
        Update(i - 1, j - 1, field, alreadyLighted);
      }

      if (i - 1 >= 0)
      {
        Update(i - 1, j, field, alreadyLighted);
      }

      if (i - 1 >= 0 && j + 1 < field[i].Length)
      {
        Update(i - 1, j + 1, field, alreadyLighted);
      }

      if (j - 1 >= 0)
      {
        Update(i, j - 1, field, alreadyLighted);
      }

      if (j + 1 < field[i].Length)
      {
        Update(i, j + 1, field, alreadyLighted);
      }

      if (i + 1 < field.Length && j - 1 >= 0)
      {
        Update(i + 1, j - 1, field, alreadyLighted);
      }

      if (i + 1 < field.Length)
      {
        Update(i + 1, j, field, alreadyLighted);
      }

      if (i + 1 < field.Length && j + 1 < field[i].Length)
      {
        Update(i + 1, j + 1, field, alreadyLighted);
      }
    }

    static void Update(int i, int j, int[][] field, bool[,] alreadyLighted)
    {
      if (!alreadyLighted[i,j])
      {
        field[i][j]++;
        field[i][j] %= 10;
        if (field[i][j] == 0)
        {
          alreadyLighted[i, j] = true;
          LightUpSurrending(i, j, field, alreadyLighted);
        }
      }
    }

    static void Print(int[][] field)
    {
      Console.WriteLine("-------------------------");
      for (int i = 0; i < field.Length; i++)
      {
        for (int j = 0; j < field[i].Length; j++)
        {
          Console.Write(field[i][j]+" ");
        }
        Console.WriteLine();
      }
    }

    static int GetLighted(int[][] fields)
    {
      return fields.Sum(l => l.Count(o => o == 0));
    }
  }
}
