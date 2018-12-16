using System;
using System.Collections.Generic;

namespace Day02
{
    class Boxes
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
                    if (boxLetters.ContainsKey(letter))
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
            Console.WriteLine("Checksum: " + twoLetterBoxes * threeLetterBoxes);

            int idLength = boxes[0].Length;
            int box1index = 0, box2index = 0;
            //find similar boxes
            for (int i = 0; i < boxes.Length; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    if (Matches(boxes[i].ToCharArray(), boxes[j].ToCharArray()))
                    {
                        box1index = i;
                        box2index = j;
                    }
                }
            }
            //get the common letters
            String matchingLetters = boxes[box1index];
            for (int i = 0; i < idLength; i++)
            {
                if (!(boxes[box1index][i]).Equals(boxes[box2index][i]))
                {
                    matchingLetters = matchingLetters.Remove(i, 1);
                    break;
                }
            }
            Console.WriteLine("Matching letters: " + matchingLetters);
            Console.Read();
        }

        private static bool Matches(char[] l, char[] r)
        {
            bool mismatch = false;
            for (int i = 0; i < l.Length; i++)
            {
                if (l[i] != r[i])
                {
                    if (mismatch) return false;
                    mismatch = true;
                }
            }
            return true;
        }
    }
}