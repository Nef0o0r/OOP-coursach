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
/*
1
2
3
4
5
6
7
8
9
10
11
12
END
1 7
2 10
2 12
3 8
3 10
3 12
4 10
4 11
5 10
5 11
6 8
6 9
6 10
END

*/