using SeaPort.Crane;

namespace SeaPort
{
    internal class Port
    {
        public int BulkCranes { get; set; } //Количество "сыпучих" кранов
        public int LiquidCranes { get; set; } //Количество "жидких" кранов
        public int ContainerCranes { get; set; } //Количество "контейнерных" кранов

        public List<Ship> actualShips = new List<Ship>(); //Корабли находящиеся в порту на данный момент

        public List<Ship> servedShips = new List<Ship>();

        public Port()
        {
            BulkCranes = 0;
            LiquidCranes = 0;
            ContainerCranes = 0;
            actualShips = new List<Ship>();
        }
        public Port(int bulk_crane, int liquid_crane, int container_crane)
        {
            BulkCranes = bulk_crane;
            LiquidCranes = liquid_crane;
            ContainerCranes = container_crane;
            actualShips = new List<Ship>();
        }
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

        // Функция для удаления корабля из списка по имени
        public bool RemoveShipInPortByName(string shipName, Time time)
        {
            // Найти корабль по имени
            var shipToRemove = actualShips.FirstOrDefault(ship => ship.Name.Equals(shipName, StringComparison.OrdinalIgnoreCase));



            if (shipToRemove != null)
            {
                shipToRemove.DayInPort = time.Current_Time - shipToRemove.ActualArrival;
                // Удалить корабль
                actualShips.Remove(shipToRemove);
                Console.WriteLine($"Ship '{shipName}' has been removed from the port.");
                Console.WriteLine($"Day in Port: {shipToRemove.DayInPort}");
                return true; // Успешно удален
            }
            else
            {
                Console.WriteLine($"Ship '{shipName}' not found in the port.");
                return false; // Не найден
            }
        }
    }
}