using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Day07
{
    class SortedDirectedGraph
    {
        private SortedDictionary<int, Node> nodes;

        public SortedDictionary<int, Node> Nodes { get => nodes; }

        public SortedDirectedGraph()
        {
            nodes = new SortedDictionary<int, Node>();
        }
        public Node AddNode(int id)
        {
            Node newNode = new Node(id);
            Nodes.Add(id, newNode);
            return newNode;
        }
        public void AddEdge(int fromId, int toId)
        {
            Node from, to;
            from = Nodes.ContainsKey(fromId) ? Nodes[fromId] : AddNode(fromId);
            to = Nodes.ContainsKey(toId) ? Nodes[toId] : AddNode(toId);
            AddEdge(from, to);
        }
        public void AddEdge(Node from, Node to)
        {
            from.OutgoingEdges.Add(to.ID, to);
            to.IncomingEdges.Add(from.ID, from);
        }
        public void RemoveEdge(int fromId, int toId)
        {
            RemoveEdge(nodes[fromId], nodes[toId]);
        }
        public void RemoveEdge(Node from, Node to)
        {
            from.OutgoingEdges.Remove(to.ID);
            to.IncomingEdges.Remove(from.ID);
        }
        public void RemoveNode(int id)
        {
            foreach (var e in nodes[id].IncomingEdges)
            {
                e.Value.OutgoingEdges.Remove(id);
            }
            foreach (var e in nodes[id].OutgoingEdges)
            {
                e.Value.IncomingEdges.Remove(id);
            }
            nodes.Remove(id);
        }
        public void RemoveNode(Node n)
        {
            foreach (var e in n.IncomingEdges)
            {
                e.Value.OutgoingEdges.Remove(n.ID);
            }
            foreach (var e in n.OutgoingEdges)
            {
                e.Value.IncomingEdges.Remove(n.ID);
            }
            nodes.Remove(n.ID);
        }

    }

    class Node
    {
        private int nodeId;
        public int ID { get => nodeId; }
        SortedDictionary<int, Node> outgoingEdges;

        public SortedDictionary<int, Node> OutgoingEdges { get => outgoingEdges; }
        SortedDictionary<int, Node> incomingEdges;

        public SortedDictionary<int, Node> IncomingEdges { get => incomingEdges; }

        public Node(int id)
        {
            nodeId = id;
            outgoingEdges = new SortedDictionary<int, Node>();
            incomingEdges = new SortedDictionary<int, Node>();
        }
    }
}