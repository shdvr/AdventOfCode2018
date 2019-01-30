using System;
using System.Linq;

namespace Day08
{
    class License
    {
        static void Main(string[] args)
        {
            //read input into an array
            String[] input = System.IO.File.ReadAllText("input.txt").Split(' ');
            int[] license = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                license[i] = int.Parse(input[i]);
            }
            int checksum = 0;
            int index = 0 ; //bookmark in the license
            //read the first node to kick off the recursive read
            LicenseNode root = ReadNode(ref index, ref license, ref checksum);
            Console.WriteLine("First answer: " + checksum);
            //walk the tree to get root's value
            checksum = GetNodeValue(root);
            Console.WriteLine("Second answer: " + checksum);
            Console.Read();
        }

        //a recursive function to read in a node
        private static LicenseNode ReadNode(ref int index, ref int[] license, ref int checksum)
        {
            //read number of child nodes and metadata
            LicenseNode temp = new LicenseNode(license[index],license[index + 1]);
            index += 2;
            //read nodes (if present)
            for (int i = 0; i < temp.Nodes.Length; i++)
            {
                temp.Nodes[i] = ReadNode(ref index, ref license, ref checksum);
            }
            //read metadata
            for (int i = 0; i < temp.Metadata.Length; i++)
            {
                temp.Metadata[i] = license[index];
                checksum += license[index];
                index++;
            }
            return temp;
        }

        //a recursive function to get a node's value
        private static int GetNodeValue(LicenseNode n)
        {
            if(n.Nodes.Length==0)
            {
                return n.Metadata.Sum();
            }
            else
            {
                int sum = 0;
                foreach (int ptr in n.Metadata)
                {
                    if(ptr>0 && ptr<=n.Nodes.Length)
                    {
                        sum += GetNodeValue(n.Nodes[ptr - 1]);
                    }
                }
                return sum;
            }
        }
    }

    class LicenseNode
    {
        int[] metadata;
        LicenseNode[] nodes;

        public int[] Metadata { get => metadata; set => metadata = value; }
        public LicenseNode[] Nodes { get => nodes; set => nodes = value; }

        public LicenseNode(int nodeCount, int metadataCount)
        {
            nodes = new LicenseNode[nodeCount];
            metadata = new int[metadataCount];
        }
    }

}
