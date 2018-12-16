using System;
using System.Collections.Generic;
using System.Linq;

namespace Day05
{
    class Polymer
    {
        static void Main(string[] args)
        {
            byte[] polymer = System.IO.File.ReadAllBytes("input.txt");
            LinkedList<byte> units = new LinkedList<byte>(polymer);
            units.RemoveLast(); //strip off line feed character
            bool done;
            LinkedListNode<byte> node;
            do
            {
                done = true;
                node = units.First;
                while (node.Next != null)
                {
                    if (System.Math.Abs(node.Value - node.Next.Value) == 32)
                    {
                        done=false;
                        node = node.Next.Next;
                        units.Remove(node.Previous);
                        units.Remove(node.Previous);
                        if (node == null) break;
                    }
                    else
                    {
                        node = node.Next;
                    }
                }
            } while (!done);
            Console.WriteLine("First answer: " + units.Count);
            int minLength = int.MaxValue;
            polymer = units.ToArray();
            for (int i = 65; i < 91; i++)
            {
                units = new LinkedList<byte>(polymer);
                node = units.First;
                while(node != null)
                {
                    if(node.Value == i || node.Value == i+32)
                    {
                        node = node.Next;
                        if(node==null)
                        {
                            units.RemoveLast();
                        }
                        else
                        {
                            units.Remove(node.Previous);
                        }
                    
                        
                    }
                    else
                    {
                        node = node.Next;
                    }
                }
                do
                {
                    done = true;
                    node = units.First;
                    while (node.Next != null)
                    {
                        if (System.Math.Abs(node.Value - node.Next.Value) == 32)
                        {
                            done = false;
                            node = node.Next.Next;
                            units.Remove(node.Previous);
                            units.Remove(node.Previous);
                            if (node == null) break;
                        }
                        else
                        {
                            node = node.Next;
                        }
                    }
                } while (!done);
                if (units.Count < minLength) minLength = units.Count;
            }
            Console.WriteLine("Second answer: " + minLength);
            Console.Read();
        }
    }
}
