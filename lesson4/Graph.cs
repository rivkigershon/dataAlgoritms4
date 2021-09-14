using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson4
{
    public class Edge<T>
    {
        public Vertice<T> To { get; set; }
        public int Weight { get; set; }
        public Edge(Vertice<T> to, int weight)
        {
            this.To = to;
            this.Weight = weight;
        }
    }
    public class Edge1<T>
    {
        public T From { get; set; }
        public T To { get; set; }
        public int Weight { get; set; }
        public Edge1(T from, T to, int weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }
    }
    public class Vertice<T>
    {
        private static int counter = 0;
        public int Id { get; }
        public T Name { get; set; }
        public bool IsVisited { get; set; }
        public List<Edge<T>> Adjacency { get; set; }
        public int Dist { get; set; }
        public Vertice<T> Prev { get; set; }
        public Vertice(T name, bool visited)
        {
            this.Id = counter++;
            this.Name = name;
            this.IsVisited = visited;
            this.Adjacency = new List<Edge<T>>();
        }
        public void addAdjacency(Vertice<T> adj, int weight)
        {
            this.Adjacency.Add(new Edge<T>(adj, weight));
        }
    }
    public class Graph<T>
    {
        public static LinkedList<Vertice<T>> Dijkstra(Edge1<T>[] edgesArr, T srcName)
        {
            Dictionary<T, Vertice<T>> d = new Dictionary<T, Vertice<T>>();
            for (int i = 0; i < edgesArr.Length; i++)
            {
                if (!d.ContainsKey(edgesArr[i].From))
                    d.Add(edgesArr[i].From, new Vertice<T>(edgesArr[i].From, false));
                if (!d.ContainsKey(edgesArr[i].To))
                    d.Add(edgesArr[i].To, new Vertice<T>(edgesArr[i].To, false));
                d[edgesArr[i].From].addAdjacency(d[edgesArr[i].To], edgesArr[i].Weight);
                //d[edgesArr[i].To.Id].addAdjacency(edgesArr[i].From, edgesArr[i].Weight);
            }
            Vertice<T>[] verArr = new Vertice<T>[d.Count];
            Vertice<T> src = null;
            foreach (var item in d)
            {
                verArr[item.Value.Id] = item.Value;
                if (item.Value.Name.Equals(srcName))
                    src = item.Value;
            }
            return Dijkstra(verArr, src);
        }
        public static LinkedList<Vertice<T>> Dijkstra(Vertice<T>[] verArr, Vertice<T> src)
        {
            LinkedList<Vertice<T>> retGraph = new LinkedList<Vertice<T>>();
            if (src != null)
            {
                for (int i = 0; i < verArr.Length; i++)
                {
                    verArr[i].Prev = null;
                    verArr[i].Dist = int.MaxValue;
                }
                verArr[src.Id].Dist = 0;
                MinHeap<T> min = new MinHeap<T>();
                min.BuildHeap(verArr, verArr.Length);
                Vertice<T> u;
                while (!min.IsEmpty())
                {
                    u = min.ExtractMin();
                    foreach (Edge<T> item in u.Adjacency)
                    {
                        if (item.To.Dist > u.Dist + item.Weight)
                        {
                            item.To.Dist = u.Dist + item.Weight;
                            item.To.Prev = u;
                        }
                    }
                    min.ChangePriority();
                    retGraph.AddLast(u);
                }
            }
            return retGraph;
        }
    }
}
