namespace SeaPort
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong QueneTime;
            string filePathStatistic = "Statistic.txt";
            string filePathPlannedSchedule = "PlannedSchedule.txt";
            string filePathActualSchedule = "ActualSchedule.txt";
            string filePathServedShips = "ServedShips.txt";
            DateTime dateTime = new DateTime(2024, 1, 1);
            Simulation simulation = new Simulation();
            Time time = new Time();
            time.StartTime(dateTime);
            Schedule schedule = new Schedule();
            Port port= new Port(1,1,1);

            // Генерация расписания
            schedule.GenerateScheduleShip(100, time.Start_Time);
            
            //schedule.Otladka();

            // Сортировка по дате прибытия идеального расписания
            var sortedByPlannedArrival = schedule.SortByScheduledArrival();
            // Вывод отсортированного идеального расписания
            //!!!schedule.PrintSchedule(sortedByPlannedArrival);

            //Сортировка по дате прибытия реального расписания
            var sortedByActualArrival = schedule.SortByActualArrival();
            // Вывод отсортированного реального расписания
            //!!!schedule.PrintScheduleReally(sortedByActualArrival);
            //!!!Console.WriteLine();

            // Запуск симуляции
            simulation.SimulateDays(31, time, port, schedule);

            //Выводим разгруженные корабли
            //!!!port.PrintServedShips();
            //!!!Console.WriteLine();
            /*
            //Выводим статистику
            Console.WriteLine($"Общая сумма штрафа: {port.FullFine}");
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
            */

            // Проверяем, существует ли файл. Если нет - создаем и записываем в него.
            schedule.PrintScheduleToFile(filePathPlannedSchedule, sortedByPlannedArrival);
            schedule.PrintScheduleToFile(filePathActualSchedule, sortedByActualArrival);
            port.PrintServedShipsToFile(filePathServedShips);
            using (StreamWriter writer = new StreamWriter(filePathStatistic, false))  // false - чтобы перезаписать файл, если он уже существует
            {
                writer.WriteLine($"Общая сумма штрафа: {port.FullFine}");
                writer.WriteLine($"Число разгруженных судов: {port.servedShips.Count}");
                QueneTime = 0;
                foreach (var ship in port.actualShips.Concat(port.servedShips))
                {
                    QueneTime += (ulong)ship.Expectation.TotalMinutes;
                }
                QueneTime /= (ulong)(port.actualShips.Count + port.servedShips.Count);
                writer.WriteLine($"Среднее время ожидания в очереди в минутах: {QueneTime} (в часах примерно {(decimal)QueneTime / 60:F2})");
                var maxExpectation = port.actualShips.Concat(port.servedShips).Max(ship => ship.Expectation.TotalHours);
                writer.WriteLine($"Максимальное время ожидания в часах: {maxExpectation:F2} (в днях примерно = {maxExpectation / 24:F2})");

                foreach (var pair in simulation.hashTable)
                {
                    writer.WriteLine($"Средняя очередь за месяц для кораблей с грузом {pair.Key}: {pair.Value:F2}");
                }
                Console.WriteLine($"Statistics were successfully written to {filePathStatistic}");
            }
        }
    }
}