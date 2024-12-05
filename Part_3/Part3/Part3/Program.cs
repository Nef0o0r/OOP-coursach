namespace SeaPort
{
    class Program
    {
        static void Main(string[] args)
        {
            Time time = new Time();
            Schedule schedule = new Schedule();
            Port port= new Port();
            // Генерация расписания
            schedule.GenerateScheduleShip(10, time.Start_Time);

            // Сортировка по дате прибытия идеального расписания
            var sortedByPlannedArrival = schedule.SortByScheduledArrival();
            //Сортировка по дате прибытия реального расписания
            var sortedByActualArrival = schedule.SortByActualArrival();

            // Вывод отсортированного идеального расписания
            Console.WriteLine("Ships sorted by Scheduled Arrival:");
            schedule.PrintSchedule(sortedByPlannedArrival);

            Console.WriteLine();

            // Вывод отсортированного реального расписания
            Console.WriteLine("Ships sorted by Scheduled Really Arrival:");
            schedule.PrintScheduleReally(sortedByActualArrival);

            for (int day = 0; day < 31; day++)
            {   
                time.Current_Time = time.Start_Time.AddDays(day);
                Console.WriteLine($"Day: {time.Current_Time}");

                port.AddActualShipWithTime(time.Current_Time, schedule);  // Моделируем один день
                port.PrintActualShips();
            }
        }
    }
}