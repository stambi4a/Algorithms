using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;
    private HashSet<string> visitedNodes;
    private LinkedList<string> sortedNodes;
    private HashSet<string> cycleNodes;

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
        this.visitedNodes = new HashSet<string>();
        this.sortedNodes = new LinkedList<string>();
        this.cycleNodes = new HashSet<string>();
    }

    public ICollection<string> TopSort()
    {
        this.visitedNodes = new HashSet<string>();
        this.sortedNodes = new LinkedList<string>();
        foreach(var node in this.graph.Keys)
        {
            this.TopSortDfs(node);
        } 

        return this.sortedNodes;
    }

    private void TopSortDfs(string node)
    {
        if (this.cycleNodes.Contains(node))
        {
            throw new InvalidOperationException("A cycle detected in the graph.");
        }
        if (!this.visitedNodes.Contains(node))
        {
            this.visitedNodes.Add(node);
            this.cycleNodes.Add(node);
            if (this.graph.ContainsKey(node))
            {
                foreach (var child in this.graph[node])
                {
                    this.TopSortDfs(child);
                }
            }

            this.cycleNodes.Remove(node);
            this.sortedNodes.AddFirst(node);            
        }
    }
    /* public ICollection<string> TopSort()
     {
         var predecessorCount = new Dictionary<string, int>();
         foreach (var node in this.graph)
         {
             if (!predecessorCount.ContainsKey(node.Key))
             {
                 predecessorCount.Add(node.Key, 0);
             }

             foreach (var childNode in node.Value)
             {
                 if (!predecessorCount.ContainsKey(childNode))
                 {
                     predecessorCount.Add(childNode, 0);
                 }

                 predecessorCount[childNode]++;
             }
         }

         var removedNodes = new List<string>();
         while (true)
         {
             string nodeToRemove = graph.Keys.FirstOrDefault(n => predecessorCount[n] == 0);

             if (nodeToRemove == null)
             {
                 break;
             }

             foreach (var child in this.graph[nodeToRemove])
             {
                 predecessorCount[child]--;
             }

             removedNodes.Add(nodeToRemove);
             this.graph.Remove(nodeToRemove); 
         }

         if (this.graph.Count > 0)
         {
             throw new InvalidOperationException("A cycle detected in the graph.");
         }

         return removedNodes;
     }*/
}
