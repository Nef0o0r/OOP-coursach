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

        public ulong FullFine { get; set; }

        public Port()
        {
            FullFine = 0;
            BulkCranes = 0;
            LiquidCranes = 0;
            ContainerCranes = 0;
            actualShips = new List<Ship>();
        }
        public Port(int bulk_crane, int liquid_crane, int container_crane)
        {
            FullFine = 0;
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
            if (actualShips.Count == 0)
            {
                Console.WriteLine("~ Ships currently in port: No ships in port! ~");
                Console.WriteLine("----------------------------------------------");
            }
            else
            {
                Console.WriteLine("Ships currently in port:");
                Console.WriteLine("------------------------");
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
                actualShips.Remove(shipToRemove);
                Console.WriteLine($"Ship '{shipName}' has been removed from the port.");
                return true; // Успешно удален
            }
            else
            {
                Console.WriteLine($"Ship '{shipName}' not found in the port.");
                return false; // Не найден
            }
        }
        // Функция для вывода списка обслуженных кораблей
        public void PrintServedShips()
        {
            if (servedShips.Count == 0)
            {
                Console.WriteLine("\n----------------------------------");
                Console.WriteLine("~ No served ships yet! ~");
                Console.WriteLine("----------------------------------");
            }
            else
            {
                Console.WriteLine("\n--------------");
                Console.WriteLine("Served Ships:");
                Console.WriteLine("--------------");
                Console.WriteLine("\n_____________________________________________________________________________________________");
                Console.WriteLine($"|  Ship Name  |   Arrival Date   |    Cargo Type    |    Expactation    |  DelayInPort(day) |");
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                foreach (var ship in servedShips)
                {
                    Console.WriteLine($"|   {ship.Name}     |   {ship.ActualArrival.ToShortDateString()}    |      {ship.CargoType}       |     {ship.Expectation:dd\\:hh\\:mm\\:ss}    |        {ship.DelayInPort.Days}        |");
                    Console.WriteLine("---------------------------------------------------------------------------------------------");
                }
            }
        }

        public void PrintServedShipsToFile(string fileName)
        {
            // Проверка на наличие разгруженных судов
            if (servedShips.Count == 0)
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    writer.WriteLine("\n----------------------------------");
                    writer.WriteLine("~ No served ships yet! ~");
                    writer.WriteLine("----------------------------------");
                }
                Console.WriteLine("No served ships yet. Information written to file.");
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    writer.WriteLine("\n--------------");
                    writer.WriteLine("Served Ships:");
                    writer.WriteLine("--------------");
                    writer.WriteLine("\n_____________________________________________________________________________________________");
                    writer.WriteLine($"|  Ship Name  |   Arrival Date   |    Cargo Type    |    Expactation    |  DelayInPort(day) |");
                    writer.WriteLine("---------------------------------------------------------------------------------------------");


                    foreach (var ship in servedShips)
                    {
                        writer.WriteLine($"|   {ship.Name}     |   {ship.ActualArrival.ToShortDateString()}    |      {ship.CargoType}       |     {ship.Expectation:dd\\:hh\\:mm\\:ss}    |        {ship.DelayInPort.Days}        |");
                        writer.WriteLine("---------------------------------------------------------------------------------------------");
                    }
                }
                Console.WriteLine($"Served ships information successfully written to {fileName}");
            }
        }
        public int GetShipCountInPort(CargoType cargoType)
        {
            return actualShips.Count(ship => ship.CargoType == cargoType);
        }
    }
}