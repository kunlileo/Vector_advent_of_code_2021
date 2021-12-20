using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;

namespace Day17_Trick_Shot
{
  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day16 Packet Decoder\Day17_Trick_Shot\Day17_Trick_Shot\InputFile.txt";
    static void Main(string[] args)
    {
      var text = File.ReadAllText(inputFilePath);
      var xRange = Regex.Matches(Regex.Match(text, @"x\=(-){0,1}\d+\.\.(-){0,1}\d+").Value, @"(-){0,1}\d+").Cast<Match>().Select(i => int.Parse(i.Value))
        .ToArray();
      var yRange = Regex.Matches(Regex.Match(text, @"y\=(-){0,1}\d+\.\.(-){0,1}\d+").Value, @"(-){0,1}\d+").Cast<Match>().Select(i => int.Parse(i.Value))
        .ToArray();

      // part1
      int xVMin = (int) (-1 + Math.Sqrt(1 + 4 * 2*((double)xRange[0] - 1))) / 2, xVMax = (int)(-1 + Math.Sqrt(1 + 4 * 2* ((double)xRange[1]))) / 2;
      int maxHeight = 0;
      for (int i = xVMin + 1; i <= xVMax; i++)
      {
        int tMin = GetFirstTimeInTargetArea(i, (int)xRange[0]);
        int yVmax = GetMaxYVmax(tMin, (int)yRange[0]);
        maxHeight = Math.Max(maxHeight, GetMaxHeight(yVmax));
      }
      Console.WriteLine("Ans part1: "+maxHeight);

      // part2
      int totalPossibleCount = 0;
      for (int i = xVMin + 1; i <= xRange[1]; i++)
      {
        if (i > xVMin && i <= xVMax)
        {
          int tMin = GetFirstTimeInTargetArea(i, (int)xRange[0]);
          totalPossibleCount += GetPossibleYCountForXStaying(tMin, yRange[0], yRange[1]);
        }
        else
        {
          List<int> timepoints = GetTimePointsWhenXIsInRange(i, xRange[0], xRange[1]);
          totalPossibleCount += GetPossibleYCountForXPassingThrough(yRange[0], yRange[1], timepoints);
        }
      }
      Console.WriteLine("Ans part2: "+totalPossibleCount);
      Console.ReadKey();
    }

    static int GetFirstTimeInTargetArea(int xV0, int xMinRange)
    {
      int x = 0, t = 0;
      while (x < xMinRange)
      {
        x += xV0;
        xV0--;
        t++;
      }

      return t;
    }

    static int GetMaxHeight(int vY)
    {
      return (vY + 1) * vY / 2;
    }

    static int GetMaxYVmax(int tLowerLimit, int yLowerLimit)
    {
      yLowerLimit *= 2;
      int ans = 0, t = tLowerLimit;
      while (t<=Math.Abs(yLowerLimit))
      {
        if (yLowerLimit % t == 0)
        {
          int tmp = t + yLowerLimit / t;
          if ((tmp - 1) % 2 == 0)
          {
            ans = (tmp - 1) / 2;
          }
        }

        t++;
      }

      return ans;
    }

    static int GetPossibleYCountForXStaying(int tLowerLimit, int yLowerLimit, int yUpperlimit)
    {
      yLowerLimit *= 2;
      yUpperlimit *= 2;
      HashSet<int> coll = new HashSet<int>();
      for (int i = tLowerLimit; i <= Math.Abs(yLowerLimit); i++)
      {
        GetPossibleYCountForT(i, yLowerLimit, yUpperlimit, coll);
      }

      return coll.Count;
    }

    static int GetPossibleYCountForXPassingThrough(int yLowerLimit, int yUpperlimit, List<int> timePoints)
    {
      yLowerLimit *= 2;
      yUpperlimit *= 2;
      HashSet<int> coll = new HashSet<int>();
      foreach(int t in timePoints)
      {
        GetPossibleYCountForT(t, yLowerLimit, yUpperlimit, coll);
      }
      return coll.Count;
    }

    static void GetPossibleYCountForT(int time, int yLowerLimit, int yUpperlimit, HashSet<int> coll)
    {
      int lower = (int) Math.Ceiling((double) yLowerLimit / time + time),
        upper = (int)Math.Floor((double) yUpperlimit / time + time);
      for (int i =lower; i <= upper; i++)
      {
        if ((i - 1) % 2 == 0)
        {
          coll.Add((i-1)/2);
        }
      } 
    }

    static List<int> GetTimePointsWhenXIsInRange(int xV, int xLower, int xUpper)
    {
      int t = 0, dist = 0;
      List<int> timeInRange = new List<int>();
      while (xV >= 0&&dist<=xUpper)
      {
        dist += xV;
        xV--;
        t++;
        if (dist >= xLower && dist <= xUpper)
        {
          timeInRange.Add(t);
        }
      }

      return timeInRange;
    }
  }
}
