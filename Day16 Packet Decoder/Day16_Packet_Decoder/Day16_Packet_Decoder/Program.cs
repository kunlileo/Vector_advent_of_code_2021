using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;

namespace Day16_Packet_Decoder
{

  class Program
  {
    private static string inputFilePath =
      @"C:\Users\kli\source\repos\Vector_advent_of_code_2021\Day16 Packet Decoder\Day16_Packet_Decoder\Day16_Packet_Decoder\InputFile.txt";

    private static readonly string literalTypeId = Convert.ToString(4, 2).PadLeft(3,'0');
    private static readonly string sumTypeId = Convert.ToString(0, 2).PadLeft(3, '0');
    private static readonly string productTypeId = Convert.ToString(1, 2).PadLeft(3, '0');
    private static readonly string minTypeId = Convert.ToString(2, 2).PadLeft(3, '0');
    private static readonly string maxTypeId = Convert.ToString(3, 2).PadLeft(3, '0');
    private static readonly string greaterTypeId = Convert.ToString(5, 2).PadLeft(3, '0');
    private static readonly string lessTypeId = Convert.ToString(6, 2).PadLeft(3, '0');
    private static readonly string equalTypeId = Convert.ToString(7, 2).PadLeft(3, '0');

    private static Dictionary<char, string> hexDight2Binary = new Dictionary<char, string>()
    {
      {'0', "0000"},{'1', "0001"},{'2', "0010"},{'3', "0011"},{'4', "0100"},{'5', "0101"},{'6', "0110"},{'7', "0111"},
      {'8', "1000"},{'9', "1001"},{'A', "1010"},{'B', "1011"},{'C', "1100"},{'D', "1101"},{'E', "1110"},{'F', "1111"}
    };
    static void Main(string[] args)
    {
      var hexStr = File.ReadAllText(inputFilePath);
      var binStr = string.Concat(hexStr.Select(i => hexDight2Binary[i]));

      // part1
      Dictionary<long, string> colls = new Dictionary<long, string>();
      Parser(binStr, 0, colls);

      long versionSum = colls.Sum(i => Convert.ToInt64(i.Value.Split(" ")[0], 2));
      Console.WriteLine("Ans part1: " + versionSum);

      // part22
      long res2 = GetReuslt(colls);
      Console.WriteLine("Ans part2: "+res2);
      Console.ReadKey();
    }

    static void Parser(string input, long level, Dictionary<long, string> colls)
    {
      if (string.IsNullOrEmpty(input) || input.Length < 8)
      {
        return;
      }

      //string version = input.Substring(0, 3);
      string TypeId = input.Substring(3, 3);
      if (TypeId == literalTypeId)
      {
        string restString = LiteralValueParser(input, level, colls);
        Parser(restString, level + 1, colls);
      }
      else
      {
        char LengthTypeId = input[6];
        if (LengthTypeId == '0')
        {
          string restString = OperatorLengthTypeId0Parser(input, level, colls);
          Parser(restString, level + 1, colls);
        }
        else
        {
          string restString = OperatorLengthTypeId1Parser(input, level, colls);
          Parser(restString, level + 1, colls);
        }
      }

    }

    static string LiteralValueParser(string input, long level, Dictionary<long, string> colls)
    {
      string version = input.Substring(0, 3);
      string typeId = input.Substring(3, 3);
      List<string> values = new List<string>();
      string restString = input.Substring(6);
      while (restString[0] != '0')
      {
        values.Add(restString.Substring(0, 5));
        restString = restString.Substring(5);
      }
      values.Add(restString.Substring(0, 5));
      restString = restString.Substring(5);

      UpdateDictionary(colls, level, version+ " " + typeId + " " + string.Join(" ", values));

      return restString;
    }

    static string OperatorLengthTypeId0Parser(string input, long level, Dictionary<long, string> colls)
    {
      string version = input.Substring(0, 3);
      string typeId = input.Substring(3, 3);
      string lengthTypeId = input.Substring(6, 1);
      string subpacketLengthStr = input.Substring(7, 15);
      long subpacketLength = Convert.ToInt64(subpacketLengthStr, 2);
      UpdateDictionary(colls, level, version + " " + typeId + " " + lengthTypeId + " " + subpacketLengthStr);
      return input.Substring(22);
    }

    private static string OperatorLengthTypeId1Parser(string input, long level, Dictionary<long, string> colls)
    {
      string version = input.Substring(0, 3);
      string typeId = input.Substring(3, 3);
      string lengthTypeId = input.Substring(6, 1);
      string subpacketNumberStr = input.Substring(7, 11);
      long numbers = Convert.ToInt64(subpacketNumberStr, 2);
      UpdateDictionary(colls, level, version + " " + typeId + " " + lengthTypeId + " " + subpacketNumberStr);
      return input.Substring(18);
    }

    static void UpdateDictionary(Dictionary<long, string> colls, long level, string decodedStr)
    {
      colls.Add(level, decodedStr);
    }

    static long GetReuslt(Dictionary<long, string> colls)
    {
      Stack<string> cmds = new Stack<string>(colls.Values);
      Stack<(long, long)> dealtedCmds = new Stack<(long, long)>();
      while (cmds.Count > 0)
      {
        string lastCmd = cmds.Pop();
        var lastCmdComponents = lastCmd.Split(" ");
        if (lastCmdComponents[1] == literalTypeId)
        {
          dealtedCmds.Push((GetLiteraValue(lastCmd), GetStringLength(lastCmd)));
        }
        else
        {
          List<(long, long)> relevantLiteralValues = GetRelevantLiteralValues(lastCmdComponents, dealtedCmds);
          var result = DoOperation(lastCmdComponents, relevantLiteralValues);
          dealtedCmds.Push(result);
        }
      }

      return dealtedCmds.First().Item1;
    }

    static long GetLiteraValue(string input)
    {
      return Convert.ToInt64(string.Concat(input.Split(" ").Skip(2).Select(i=>i.Substring(1))), 2);
    }

    static long GetStringLength(string input)
    {
      return input.Count(c => c != ' ');
    }

    static List<(long, long)> GetRelevantLiteralValues(string[] cmdComponents, Stack<(long, long)> dealtedCmds)
    {
      List<(long, long)> relevantLiteralValues = new List<(long, long)>();
      if (cmdComponents[2] == "1")
      {
        long numberOfRelevantLiteralValues = Convert.ToInt64(cmdComponents[3], 2);
        while (numberOfRelevantLiteralValues > 0)
        {
          var neededLietralValue = dealtedCmds.Pop();
          relevantLiteralValues.Add(neededLietralValue);
          numberOfRelevantLiteralValues--;
        }
      }
      else
      {
        long lengthOfRelevantLiteralValues = Convert.ToInt64(cmdComponents[3], 2), totalLength = 0;
        while (totalLength < lengthOfRelevantLiteralValues)
        {
          var neededLiteralValue = dealtedCmds.Pop();
          relevantLiteralValues.Add(neededLiteralValue);
          totalLength += neededLiteralValue.Item2;
        }
        Debug.Assert(totalLength == lengthOfRelevantLiteralValues);
      }
      return relevantLiteralValues;
    }

    static (long, long) DoOperation(string[] cmdComponents, List<(long, long)> relevantLiteralVaules)
    {
      long totalLength = relevantLiteralVaules.Sum(i => i.Item2)+cmdComponents.Sum(i=>i.Length);
      if (cmdComponents[1] == sumTypeId)
      {
        return (relevantLiteralVaules.Sum(i => i.Item1), totalLength);
      }
      else if (cmdComponents[1] == productTypeId)
      {
        return (relevantLiteralVaules.Aggregate((long)1, (pre, cur)=>pre*=cur.Item1), totalLength);
      }
      else if (cmdComponents[1] == minTypeId)
      {
        return (relevantLiteralVaules.Min(i=>i.Item1), totalLength);
      }
      else if (cmdComponents[1] == maxTypeId)
      {
        return (relevantLiteralVaules.Max(i => i.Item1), totalLength);
      }
      else if (cmdComponents[1] == greaterTypeId)
      {
        Debug.Assert(relevantLiteralVaules.Count==2);
        return (relevantLiteralVaules[0].Item1 > relevantLiteralVaules[1].Item1 ? 1 : 0, totalLength);
      }
      else if (cmdComponents[1] == lessTypeId)
      {
        Debug.Assert(relevantLiteralVaules.Count == 2);
        return (relevantLiteralVaules[0].Item1 < relevantLiteralVaules[1].Item1 ? 1 : 0, totalLength);
      }
      else if (cmdComponents[1] == equalTypeId)
      {
        Debug.Assert(relevantLiteralVaules.Count == 2);
        return (relevantLiteralVaules[0].Item1 == relevantLiteralVaules[1].Item1 ? 1 : 0, totalLength);
      }
      return (0,0);
    }
  }
}
