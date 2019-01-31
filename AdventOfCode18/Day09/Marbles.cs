using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Day09
{
    class Marbles
    {
        static void Main(string[] args)
        {
            Regex inputRegex = new Regex(@"([0-9]*) players; last marble is worth ([0-9]*) points");
            MatchCollection inputData = inputRegex.Matches(System.IO.File.ReadAllText("input.txt"));
            int playerCount = int.Parse(inputData[0].Groups[1].Value);
            int marbleCount = int.Parse(inputData[0].Groups[2].Value);
            int[] scores = new int[playerCount]; //set up the scorecard
            LinkedList<int> circle = new LinkedList<int>(new int[] { 0 }); //put the first marble down
            LinkedListNode<int> current = circle.First;
            int nextMarblePoints = 1; //value of the next marble to play
            int currentPlayer = 0; //player that will be playing the next marble
            while (nextMarblePoints <= marbleCount) //play until we run out of marbles
            {
                if(nextMarblePoints % 23 == 0)
                {
                    scores[currentPlayer] += nextMarblePoints;
                    for (int i = 0; i < 6; i++)
                    {
                        current = current.Previous;
                        if (current == null) current = circle.Last;
                    }
                    if(current.Previous == null)
                    {
                        scores[currentPlayer] += circle.First.Value;
                        circle.RemoveFirst();
                    }
                    else
                    {
                        scores[currentPlayer] += current.Previous.Value;
                        circle.Remove(current.Previous);
                    }
                }
                else
                {
                    current = current.Next;
                    if (current == null) current = circle.First;
                    current = circle.AddAfter(current, nextMarblePoints);
                }
                //prepare for next turn
                nextMarblePoints++;
                currentPlayer++;
                if (currentPlayer == playerCount) currentPlayer = 0;
            }
   
            Console.WriteLine("First answer: " + scores.Max());

            //Console.WriteLine("Second answer: " + checksum);
            Console.Read();
        }
    }
}
