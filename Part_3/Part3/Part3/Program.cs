namespace SeaPort
{
    class Program
    {
        static void Main(string[] args)
        {
            Simulation simulation = new Simulation();
            Time time = new Time();
            Schedule schedule = new Schedule();
            Port port= new Port(3,3,3);
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