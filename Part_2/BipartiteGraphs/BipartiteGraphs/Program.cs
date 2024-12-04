namespace BipartiteGraphs;

class Program
{
    static void Main(string[] args)
    {
        BipartiteGraphs graphs = new BipartiteGraphs();
        graphs.InputAllVertex();
        graphs.InputAllEdge();
        Console.WriteLine("Ваш введенный граф:\n");
        graphs.PrintAdjacencyList();
        //graphs.PrintSortedMatchingsBySizeDescending();
        graphs.PrintMaxMatching();
        Console.WriteLine($"Число совершенных паросочетаний: {graphs.IsPerfectMatching()}");
    }
}
