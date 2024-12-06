namespace SeaPort
{
    internal class Schedule
    {
        private List<Ship> scheduleShip = new List<Ship>(); // Расписание всех кораблей на месяц

        // Генерация расписания судов
        public void GenerateScheduleShip(int numberOfShips, DateTime startDate)
        {
            int cargoTypeCount = Enum.GetValues(typeof(CargoType)).Length; // Количество типов груза

            Random random = new Random();
            for (int i = 0; i < numberOfShips; i++)
            {
                string name = "Ship" + (i + 1); // Имя судна
                CargoType cargoType = (CargoType)(random.Next(0, cargoTypeCount)); // Тип груза
                double cargoWeight = random.Next(20000, 100010); // Случайный вес груза
                DateTime arrivalDate = startDate.AddDays(random.Next(0, 31)); // Прибытие в течение месяца
                int plannedStayDays = random.Next(6, 14); // Планируемое время стоянки

                // Создание судна
                Ship ship = new Ship(name, cargoType, cargoWeight, arrivalDate, plannedStayDays);

                // Добавление судна в расписание
                scheduleShip.Add(ship);
            }
        }

        // Общая функция сортировки по произвольному ключу
        public List<Ship> SortSchedule(Func<Ship, object> keySelector)
        {
            return scheduleShip.OrderBy(keySelector).ToList();
        }

        // Функция сортировки по ScheduledArrival
        public List<Ship> SortByScheduledArrival()
        {
            return SortSchedule(ship => ship.ScheduledArrival);
        }
        public List<Ship> SortByActualArrival()
        {
            return SortSchedule(ship => ship.ActualArrival);
        }

        // Функция для вывода списка судов
        public void PrintSchedule(List<Ship> ships)
        {
            if (ships == null || ships.Count == 0)
            {
                Console.WriteLine("No ships to display.");
                return;
            }

            Console.WriteLine("Schedule of Ships:");
            foreach (var ship in ships)
            {
                Console.WriteLine($"Name: {ship.Name}, Cargo Type: {ship.CargoType}, Cargo Weight: {ship.CargoWeight} tons, " +
                                  $"Scheduled Arrival: {ship.ScheduledArrival:dd-MM-yyyy}, Planned Stay: {ship.PlannedStayDays} days, " +
                                  $"Departure Date: {ship.DepartureDate:dd-MM-yyyy}");
            }
        }

        public void PrintScheduleReally(List<Ship> ships)
        {
            if (ships == null || ships.Count == 0)
            {
                Console.WriteLine("No ships to display.");
                return;
            }

            Console.WriteLine("Schedule of Ships:");
            foreach (var ship in ships)
            {
                Console.WriteLine($"Name: {ship.Name}, Cargo Type: {ship.CargoType}, Cargo Weight: {ship.CargoWeight} tons, " +
                                  $"Scheduled Actual Arrival: {ship.ActualArrival:dd-MM-yyyy}, Planned Stay: {ship.PlannedStayDays} days, " +
                                  $"Departure Date: {ship.DepartureDate:dd-MM-yyyy}");
            }
        }

        // Пример: вывод полного расписания
        public void PrintFullSchedule()
        {
            PrintSchedule(scheduleShip);
        }
    }
}
