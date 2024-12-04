namespace BipartiteGraphs
{
    internal class BipartiteGraphs
    {
        private Dictionary<string, List<string>> adjacencyList;
        public BipartiteGraphs()
        {
            adjacencyList = new Dictionary<string, List<string>>();
        }
        public void AddVertex(string vertex)
        {
            if (!adjacencyList.ContainsKey(vertex))
            {
                adjacencyList[vertex] = new List<string>();
            }
        }
        public void RemoveVertex(string vertex)
        {
            if (adjacencyList.ContainsKey(vertex))
            {
                foreach (var key in adjacencyList.Keys.ToList())
                {
                    adjacencyList[key].Remove(vertex);
                }
                adjacencyList.Remove(vertex);
            }
        }

        public void AddEdge(string vertex1, string vertex2)
        {
            if (!adjacencyList.ContainsKey(vertex1))
            {
                Console.WriteLine("Vertex1 - не существует, для начала добавьте вершину");
                Console.WriteLine($"Хотите добавить вершину \"{vertex1}\"? (Y/N)");
                string input;
                do
                {
                    input = Console.ReadLine();
                    if (input == "Y")
                    {
                        this.AddVertex(vertex1);
                        Console.WriteLine("Вершина добавлена, можете добавлять ребро к этой вершине");
                        return;
                    }
                    else if (input == "N") return;
                    else Console.WriteLine("Введите \"Y\" или \"N\"");
                } while (true);
                return;
            }
            if (!adjacencyList.ContainsKey(vertex2))
            {
                Console.WriteLine("Vertex2 - не существует, для начала добавьте вершину");
                Console.WriteLine($"Хотите добавить вершину \"{vertex2}\"? (Y/N)");
                string input;
                do
                {
                    input = Console.ReadLine();
                    if (input == "Y")
                    {
                        this.AddVertex(vertex2);
                        Console.WriteLine("Вершина добавлена, можете добавлять ребро к этой вершине");
                        return;
                    }
                    else if (input == "N") return;
                    else Console.WriteLine("Введите \"Y\" или \"N\"");
                } while (true);
                return;
            }
            if (!adjacencyList[vertex1].Contains(vertex2)){
                adjacencyList[vertex1].Add(vertex2);
                adjacencyList[vertex2].Add(vertex1);
            }
        }

        public void RemoveEdge(string vertex1, string vertex2)
        {
            if (!adjacencyList.ContainsKey(vertex1))
            {
                Console.WriteLine($"Вершина {vertex1} не существует.");
                return;
            }

            if (!adjacencyList.ContainsKey(vertex2))
            {
                Console.WriteLine($"Вершина {vertex2} не существует.");
                return;
            }

            if (adjacencyList[vertex1].Contains(vertex2))
            {
                adjacencyList[vertex1].Remove(vertex2); // Удаляем ребро из vertex1
                Console.WriteLine($"Ребро между {vertex1} и {vertex2} удалено.");
            }
            else
            {
                Console.WriteLine($"Ребро между {vertex1} и {vertex2} не существует.");
            }

            if (adjacencyList[vertex2].Contains(vertex1))
            {
                adjacencyList[vertex2].Remove(vertex1); // Удаляем ребро из vertex2
            }
            else
            {
                Console.WriteLine($"Ребро между {vertex2} и {vertex1} не существует.");
            }
        }


        public void InputAllVertex()
        {
            Console.WriteLine("Вводите вершины (для окончания ввода введите \"END\"):");
            string vertex;
            do
            {
                vertex = Console.ReadLine();
                if (vertex != "END") this.AddVertex(vertex);
                else
                {
                    Console.WriteLine(' ');
                    break;
                }
            } while (true);
        }

        public void InputAllEdge()
        {
            Console.WriteLine("Вводите по две ранее добавленные вершины через пробел, разделяя пары \"Enter\",\nмежду которыми находятся ребра (для окончания ввода введите \"END\"):");

            string input;
            do
            {
                input = Console.ReadLine();
                if (input == "END")
                {
                    Console.WriteLine("Ввод завершён.\n");
                    break;
                }

                try
                {
                    // Разбиваем ввод на части
                    var parts = input.Split(' ');
                    if (parts.Length != 2)
                    {
                        throw new FormatException("Нужно ввести ровно две вершины через пробел.");
                    }

                    string vertex1 = parts[0];
                    string vertex2 = parts[1];

                    // Добавляем ребро
                    this.AddEdge(vertex1, vertex2);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
           
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
                }
            } while (true);
        }

        public void PrintAdjacencyList()
        {
            foreach (var vertex in adjacencyList)
            {
                Console.WriteLine($"{vertex.Key}: {string.Join(", ", vertex.Value)}");
                Console.WriteLine();
            }
        }

        private void FindMatchings(List<Tuple<string, string>> currentMatching, List<List<Tuple<string, string>>> allMatchings, HashSet<string> usedVertices, List<Tuple<string, string>> allEdges)
        {
            // Если текущее паросочетание не пусто, добавляем его в список
            if (currentMatching.Count > 0)
            {
                // Чтобы избежать дублирования, проверяем, если такое паросочетание уже существует
                var sortedMatching = currentMatching.OrderBy(m => m.Item1).ThenBy(m => m.Item2).ToList();
                if (!allMatchings.Any(matching => matching.SequenceEqual(sortedMatching)))
                {
                    allMatchings.Add(sortedMatching);
                }
            }

            // Перебираем все рёбра
            foreach (var edge in allEdges)
            {
                var vertex1 = edge.Item1;
                var vertex2 = edge.Item2;

                // Проверяем, были ли уже использованы эти вершины
                if (!usedVertices.Contains(vertex1) && !usedVertices.Contains(vertex2))
                {
                    // Добавляем текущее ребро в паросочетание
                    currentMatching.Add(edge);
                    usedVertices.Add(vertex1);
                    usedVertices.Add(vertex2);

                    // Рекурсивно ищем другие паросочетания
                    FindMatchings(currentMatching, allMatchings, usedVertices, allEdges);

                    // Возвращаемся в предыдущее состояние
                    currentMatching.RemoveAt(currentMatching.Count - 1);
                    usedVertices.Remove(vertex1);
                    usedVertices.Remove(vertex2);
                }
            }
        }
        public List<List<Tuple<string, string>>> FindAllMatchings()
        {
            List<List<Tuple<string, string>>> allMatchings = new List<List<Tuple<string, string>>>();
            List<Tuple<string, string>> allEdges = new List<Tuple<string, string>>();

            // Собираем все рёбра
            foreach (var vertex in adjacencyList.Keys)
            {
                foreach (var neighbor in adjacencyList[vertex])
                {
                    if (vertex.CompareTo(neighbor) < 0)  // Чтобы избежать симметричных рёбер
                    {
                        allEdges.Add(Tuple.Create(vertex, neighbor));
                    }
                }
            }

            FindMatchings(new List<Tuple<string, string>>(), allMatchings, new HashSet<string>(), allEdges);

            return allMatchings;
        }

        // Метод для вывода всех паросочетаний
        public void PrintAllMatchings(List<List<Tuple<string, string>>> allMatchings)
        {
            if (allMatchings.Count == 0)
            {
                Console.WriteLine("Паросочетаний в данном случае нет, введены неверные данные");
                return;
            }

            Console.WriteLine("Найденные паросочетания:");
            foreach (var matching in allMatchings)
            {
                Console.WriteLine(string.Join(", ", matching.Select(m => $"({m.Item1}, {m.Item2})")));
            }
        }

        // Метод для нахождения и вывода всех паросочетаний
        public void FindAndPrintAllMatchings()
        {
            List<List<Tuple<string, string>>> allMatchings = FindAllMatchings(); // Находим все паросочетания
            PrintAllMatchings(allMatchings); // Выводим все паросочетания
        }

        // Функция для сортировки паросочетаний по количеству пар
        public List<List<Tuple<string, string>>> SortMatchingsBySizeAndInside(List<List<Tuple<string, string>>> allMatchings)
        {
            // Сортируем паросочетания по количеству пар
            var sortedMatchings = allMatchings.OrderBy(matching => matching.Count).ToList();

            // Для каждого паросочетания сортируем пары внутри по возрастанию
            foreach (var matching in sortedMatchings)
            {
                matching.Sort((m1, m2) =>
                {
                    int compareFirst = m1.Item1.CompareTo(m2.Item1);
                    if (compareFirst != 0)
                        return compareFirst;
                    return m1.Item2.CompareTo(m2.Item2);
                });
            }

            return sortedMatchings;
        }

        // Функция для вывода отсортированных паросочетаний по количеству пар и сортировке внутри
        public void PrintSortedMatchingsBySizeAndInside()
        {
            List<List<Tuple<string, string>>> allMatchings = FindAllMatchings(); // Находим все паросочетания

            // Сортируем паросочетания по количеству пар и внутри каждого паросочетания
            List<List<Tuple<string, string>>> sortedMatchings = SortMatchingsBySizeAndInside(allMatchings);

            // Выводим отсортированные паросочетания
            PrintAllMatchings(sortedMatchings);
        }
        
        //Метод для вывода одного из больших паросочетаний
        public void PrintMaxMatching()
        {
            List<List<Tuple<string, string>>> allMatchings = FindAllMatchings(); // Находим все паросочетания

            // Сортируем паросочетания по количеству пар и внутри каждого паросочетания
            List<List<Tuple<string, string>>> sortedMatchings = SortMatchingsBySizeAndInside(allMatchings);
            // Проверяем, есть ли хотя бы одно паросочетание
            if (sortedMatchings.Count > 0)
            {
                // Выводим только последнюю строку (последнее паросочетание)
                var lastMatching = sortedMatchings.Last();
                Console.WriteLine($"Одно из наибольших паросочетаний: {lastMatching.Count} пар");
                Console.WriteLine(string.Join(", ", lastMatching.Select(m => $"({m.Item1}, {m.Item2})")));
            }
            else
            {
                Console.WriteLine("Паросочетаний не найдено.");
            }

        }

        public List<List<Tuple<string, string>>> SortMatchingsBySizeDescending(List<List<Tuple<string, string>>> allMatchings)
        {
            // Сортируем паросочетания по количеству пар по возрастанию
            var sortedMatchings = SortMatchingsBySizeAndInside(allMatchings);

            // Инвертируем порядок для сортировки по убыванию
            sortedMatchings.Reverse();

            return sortedMatchings;
        }

        public void PrintSortedMatchingsBySizeDescending()
        {
            List<List<Tuple<string, string>>> allMatchings = FindAllMatchings(); // Находим все паросочетания

            // Сортируем паросочетания по убыванию
            List<List<Tuple<string, string>>> sortedMatchings = SortMatchingsBySizeDescending(allMatchings);

            // Выводим отсортированные паросочетания
            PrintAllMatchings(sortedMatchings);
        }

        //Метод для нахождения числа совершенных паросочетаний
        public int IsPerfectMatching()
        {
            // Находим все паросочетания
            List<List<Tuple<string, string>>> allMatchings = FindAllMatchings();

            // Если нет паросочетаний, возвращаем 0
            if (allMatchings.Count == 0)
            {
                return 0;
            }

            // Находим максимальную длину паросочетания
            int maxLength = allMatchings.Max(matching => matching.Count);

            if (maxLength * 2 != adjacencyList.Count)
            {
                Console.WriteLine("Совершенные паросочетания могут быть только у графов с четным количеством вершин");
                return 0;
            }

            // Подсчитываем количество паросочетаний с максимальной длиной
            int countMaxLengthMatchings = allMatchings.Count(matching => matching.Count == maxLength);

            return countMaxLengthMatchings;
        }
    }
}