using System;
using System.Collections.Generic;

namespace Day01
{
    class Frequency
    {
        static void Main(string[] args)
        {
            int result = 0;
            HashSet<int> resultingFreqs = new HashSet<int>();
            resultingFreqs.Add(0);
            String[] inputFreqs = System.IO.File.ReadAllLines("input.txt");
            int[] input = new int[inputFreqs.Length];
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = int.Parse(inputFreqs[i]);
                result += input[i];
            } 
            Console.WriteLine("Final frequency: " + result);
            result = 0;
            bool freqRepeated = false;
            do
            {
                for (int i = 0; i < input.Length; i++)
                {
                    result += input[i];
                    if (!resultingFreqs.Add(result))
                    {
                        freqRepeated = true;
                        break;
                    }    
                }
            } while (!freqRepeated);
            Console.WriteLine("First repeating frequency: " + result);
            Console.Read();
        }
    }
}
