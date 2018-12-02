using System;
using System.Collections.Generic;


namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            int twoLetterBoxes = 0;
            int threeLetterBoxes = 0;
            Dictionary<char, int> boxLetters;
            //read input into array of strings
            String[] boxes = System.IO.File.ReadAllLines("input.txt");

            foreach (String box in boxes)
            {
                //read in into  a dictionary with a letter being the key and value being how many times it's in there
                boxLetters = new Dictionary<char, int>(box.Length);
                foreach (char letter in box.ToCharArray())
                {
                    if(boxLetters.ContainsKey(letter))
                    {
                        boxLetters[letter] = boxLetters[letter] + 1;
                    }
                    else
                    {
                        boxLetters.Add(letter, 1);
                    }
                }
                //if any value is 2, increment the count of boxes with two letters
                if (boxLetters.ContainsValue(2)) twoLetterBoxes++;
                if (boxLetters.ContainsValue(3)) threeLetterBoxes++;
                //if any value is 3, increment the count of boxes with three letters
            }
            //multiply the box counts
            Console.WriteLine("Checksum: " + twoLetterBoxes*threeLetterBoxes);
            Console.Read();
        }
    }
}
