namespace SeaPort
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime = new DateTime(2024, 12, 5, 12, 48, 12);
            Simulation simulation = new Simulation();
            Time time = new Time();
            time.StartTime(dateTime);
            Schedule schedule = new Schedule();
            Port port= new Port(1,2,3);
            // Генерация расписания
            schedule.GenerateScheduleShip(10, time.Start_Time);

            // Сортировка по дате прибытия идеального расписания
            var sortedByPlannedArrival = schedule.SortByScheduledArrival();
            //Сортировка по дате прибытия реального расписания
            var sortedByActualArrival = schedule.SortByActualArrival();

            // Вывод отсортированного идеального расписания
            //Console.WriteLine("Ships sorted by Scheduled Arrival:");
            //schedule.PrintSchedule(sortedByPlannedArrival);

            Console.WriteLine();

            // Вывод отсортированного реального расписания
            Console.WriteLine("Ships sorted by Scheduled Really Arrival:");
            schedule.PrintScheduleReally(sortedByActualArrival);

            // Запуск симуляции
            simulation.SimulateDays(31, time, port, schedule);
        }
    }
}