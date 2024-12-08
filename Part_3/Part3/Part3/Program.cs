namespace SeaPort
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong QueneTime;
            DateTime dateTime = new DateTime(2024, 1, 1);
            Simulation simulation = new Simulation();
            Time time = new Time();
            time.StartTime(dateTime);
            Schedule schedule = new Schedule();
            Port port= new Port(1,1,1);
            // Генерация расписания
            //schedule.GenerateScheduleShip(100, time.Start_Time);
            
            schedule.Otladka();

            // Сортировка по дате прибытия идеального расписания
            var sortedByPlannedArrival = schedule.SortByScheduledArrival();
            //Сортировка по дате прибытия реального расписания
            var sortedByActualArrival = schedule.SortByActualArrival();

            // Вывод отсортированного идеального расписания
            //Console.WriteLine("Ships sorted by Scheduled Arrival:");
            schedule.PrintSchedule(sortedByPlannedArrival);

            Console.WriteLine();

            // Вывод отсортированного реального расписания
            //Console.WriteLine("Ships sorted by Scheduled Really Arrival:");
            //schedule.PrintScheduleReally(sortedByActualArrival);
            // Запуск симуляции
            simulation.SimulateDays(10, time, port, schedule);
            port.PrintServedShips();
            Console.WriteLine();

            Console.WriteLine($"Общая суммма штрафа: {port.FullFine}");
            Console.WriteLine($"Число разгруженных судов: {port.servedShips.Count}");
            QueneTime = 0;
            foreach (var ship in port.actualShips.Concat(port.servedShips))
            {
                QueneTime += (ulong)ship.Expectation.TotalMinutes;
            }
            QueneTime /= (ulong)(port.actualShips.Count + port.servedShips.Count);
            Console.WriteLine($"Среднее время ожидания в очереди в минутах: {QueneTime} (в часах пирмерно {(decimal)QueneTime / 60 :F2})");
            var maxExpectation = port.actualShips.Concat(port.servedShips).Max(ship => ship.Expectation.TotalHours);
            Console.WriteLine($"Максимальное время ожидания в часах: {maxExpectation:F2} (в днях примерно = {maxExpectation/24:F2})");
            foreach (var pair in simulation.hashTable)
            {
                Console.WriteLine($"Средняя очередь за месяц для кораблей с грузом {pair.Key}: {pair.Value:F2}");
            }
        }
    }
}