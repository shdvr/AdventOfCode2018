using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day07
{
    class SleighInstructions
    {
        static void Main(string[] args)
        {
            Regex instructionRegex = new Regex(@"Step ([A-Z]) must be finished before step ([A-Z]) can begin\.");
            //read the input
            MatchCollection instructions = instructionRegex.Matches(System.IO.File.ReadAllText("input.txt"));
            SortedDirectedGraph g = new SortedDirectedGraph();
            //parse it as directed edges of a graph with sorted nodes
            foreach (Match insturction in instructions)
            {
                g.AddEdge((int)(insturction.Groups[1].Value[0]), (int)(insturction.Groups[2].Value[0]));
            }
            List<char> order = new List<char>(g.Nodes.Count);
            //traverse the sorted graph looking for the first node with no incoming edges
            while (g.Nodes.Count > 0)
            {
                foreach (var n in g.Nodes.Values)
                {
                    if(n.IncomingEdges.Count == 0)
                    {
                        //once such node is found, add it to the output list, remove it from the graph and find another one like it
                        order.Add((char)n.ID);
                        g.RemoveNode(n);
                        break;
                    }
                }
                //repeat until no nodes are left in the graph
            }
            Console.WriteLine("First answer: " + new string(order.ToArray()));

            //TODO: part 2
            //Console.WriteLine("Second answer: " + 0);

            Console.Read();
        }
    }
}
