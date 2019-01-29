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

            //set up graph, workers, list of current tasks, and timer
            g = new SortedDirectedGraph();
            foreach (Match insturction in instructions)
            {
                g.AddEdge((int)(insturction.Groups[1].Value[0]), (int)(insturction.Groups[2].Value[0]));
            }
            Worker[] workers = new Worker[5];
            List<Node> tasksInProgress = new List<Node>();
            int stopWatch = -1;
            //assign tasks until we're done
            while (g.Nodes.Count > 0)
            {
                for (int i = 0; i < workers.Length; i++)
                {
                    //check if any tasks are done
                    if ((workers[i].timeleft <= 0) && (workers[i].task != null)) //if just finished a task, mark it as done
                    {
                        g.RemoveNode(workers[i].task);
                        tasksInProgress.Remove(workers[i].task);
                        workers[i].task = null;
                    }
                }
                //assign tasks to free workers
                for (int i = 0; i < workers.Length; i++)
                {
                    if (workers[i].timeleft <= 0)
                    {
                        //assign work if available
                        foreach (var n in g.Nodes.Values)
                        {
                            if (n.IncomingEdges.Count == 0 && !tasksInProgress.Contains(n))
                            {
                                workers[i].task = n;
                                workers[i].timeleft = n.ID - 4; //ASCII math example: for letter A, the ASCII char value is 65, we need 61
                                tasksInProgress.Add(n);
                                break;
                            }
                        }
                    }
                }
               
                //wait a unit of time to let workers work on tasks
                stopWatch++;
                for (int i = 0; i < workers.Length; i++)
                {
                    workers[i].timeleft--;
                }
            }
            //report total time elapsed
            Console.WriteLine("Second answer: " + stopWatch + " seconds");

            Console.Read();
        }

        struct Worker
        {
            public int timeleft;
            public Node task;
        }
    }
}
