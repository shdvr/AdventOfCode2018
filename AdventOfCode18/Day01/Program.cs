using System;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;
            String[] freqs = System.IO.File.ReadAllLines("input.txt");
            foreach (var freq in freqs)
            {
                result += int.Parse(freq);
            }
            Console.WriteLine(result);
        }
    }
}
