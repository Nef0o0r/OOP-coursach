namespace SeaPort
{
    internal class Port
    {
        public int BulkCranes { get; set; } //Количество "сыпучих" кранов
        public int LiquidCranes { get; set; } //Количество "жидких" кранов
        public int ContainerCranes { get; set; } //Количество "контейнерных" кранов

        public List<Ship> actualShips = new List<Ship>(); //Корабли находящиеся в порту на данный момент

        // Функция для добавления кораблей с проверкой на совпадение даты ActualArrival
        public void AddActualShipWithTime(DateTime arrivalDate, Schedule schedule)
        {
            // Получаем отсортированный список судов по фактическому времени прибытия
            var sortedByActualArrival = schedule.SortByActualArrival();

            foreach (var ship in sortedByActualArrival)
            {
                // Если дата прибытия судна совпадает с переданной датой
                if (ship.ActualArrival.Date == arrivalDate.Date)
                {
                    // Добавляем судно в список
                    actualShips.Add(ship);
                }
            }
        }
        // Функция для вывода списка судов, которые находятся в порту
        public void PrintActualShips()
        {
            Console.WriteLine("Ships currently in port:");
            if (actualShips.Count == 0)
            {
                Console.WriteLine("No ships in port.");
            }
            else
            {
                foreach (var ship in actualShips)
                {
                    Console.WriteLine($"Ship Name: {ship.Name}, Arrival Date: {ship.ActualArrival.ToShortDateString()}, Cargo Type: {ship.CargoType}");
                }

            }
        }
    }
}