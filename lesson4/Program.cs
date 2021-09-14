using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lesson4
{
    class Program
    {
        static void Main(string[] args)
        {
            Edge1<string>[] e = new Edge1<string>[] {
                new Edge1<string>("Herzliya", "Modi'in", 2),
                new Edge1<string>("Herzliya","Tel Aviv", 10),
                new Edge1<string>( "Modi'in","Jerusalem", 8),
                new Edge1<string>( "Modi'in","Tel Aviv", 6),
                new Edge1<string>("Tel Aviv","Jerusalem", 1)
            };

            LinkedList<Vertice<string>> retGraph = Graph<string>.Dijkstra(e, "Herzliya");
            if (retGraph.Count != 4) Console.WriteLine("expect retGraph.Count to be 4 and actual: " + retGraph.Count);
            foreach (var item in retGraph)
            {
                switch (item.Name)
                {
                    case "Herzliya":
                        if (item.Dist != 0) Console.WriteLine("Herzliya dist not equals 0");
                        break;
                    case "Modi'in":
                        if (item.Dist != 2) Console.WriteLine("Modi'in dist not equals 2");
                        break;
                    case "Tel Aviv":
                        if (item.Dist != 8) Console.WriteLine("Tel Aviv dist not equals 8");
                        break;
                    case "Jerusalem":
                        if (item.Dist != 9) Console.WriteLine("Jerusalem dist not equals 9");
                        break;
                    default:
                        Console.WriteLine("invalid value");
                        break;
                }
            }
            //הרצליה מודיעין תל אביב ירושלים
        }
    }
}
