namespace SeaPort
{
    internal class Queue
    {
        public List<Ship> queue_bulk;
        public List<Ship> queue_liquid;
        public List<Ship> queue_container;
        private int HoursDuration;

        public Queue()
        {
            queue_bulk = new List<Ship>();
            queue_liquid = new List<Ship>();
            queue_container = new List<Ship>();
        }
        public void AddQueu(Ship ship, Time time)
        {
            switch (ship.CargoType)
            {
                case CargoType.Bulk:
                    queue_bulk.Add(ship);
                    ship.Expectation = time.Current_Time - ship.ActualArrival.AddDays(1);
                    break;
                case CargoType.Liquid:
                    queue_liquid.Add(ship);
                    ship.Expectation = time.Current_Time - ship.ActualArrival.AddDays(1);
                    break;
                case CargoType.Container:
                    queue_container.Add(ship);
                    ship.Expectation = time.Current_Time - ship.ActualArrival.AddDays(1);
                    break;
                default:
                    Console.WriteLine("Unknown Cargotype (AddQueue)");
                    return;
            }
        }
        public void RemoveQueu(Ship ship)
        {
            switch (ship.CargoType)
            {
                case CargoType.Bulk:
                    queue_bulk.Remove(ship);
                    break;
                case CargoType.Liquid:
                    queue_liquid.Remove(ship);
                    break;
                case CargoType.Container:
                    queue_container.Remove(ship);
                    break;
                default:
                    Console.WriteLine("Unknown CargoType (RemoveQueue)");
                    return;
            }
        }
        // Проверяет, находится ли корабль в списке bulk
        public bool IsShipInBulkQueue(Ship ship)
        {
            return queue_bulk.Contains(ship);
        }

        // Проверяет, находится ли корабль в списке liquid
        public bool IsShipInLiquidQueue(Ship ship)
        {
            return queue_liquid.Contains(ship);
        }

        // Проверяет, находится ли корабль в списке container
        public bool IsShipInContainerQueue(Ship ship)
        {
            return queue_container.Contains(ship);
        }

        //Проверяет, находится ли корабль в очереди
        public bool IsShipInQueue(Ship ship)
        {
            switch (ship.CargoType)
            {
                case (CargoType.Bulk):
                    return queue_bulk.Contains(ship);
                case CargoType.Liquid:
                    return queue_liquid.Contains(ship);
                case CargoType.Container:
                    return queue_container.Contains(ship);
                default:
                    Console.WriteLine("Error IsShipInQueue");
                    return false;
            }
        }
        // Вывод всех кораблей в очередях
        public void PrintAllShipsInQueue()
        {   
            Console.WriteLine("\nBulk Queue:");
            foreach (var ship in queue_bulk)
            {
                Console.WriteLine($"\nShip: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2} " +
                    $"Left Unloading: {ship.LeftUnloadingTimeBOM:F2}");
            }

            Console.WriteLine("Liquid Queue:");
            foreach (var ship in queue_liquid)
            {
                Console.WriteLine($"\nShip: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2} " +
                    $"Left Unloading: {ship.LeftUnloadingTimeBOM:F2}");
            }
                Console.WriteLine("Container Queue:");
                foreach (var ship in queue_container)
                {
                    Console.WriteLine($"\nShip: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2}, " +
                        $"Left Unloading: {ship.LeftUnloadingTimeBOM:F2}");
                }
        }
        // Метод для обработки очереди
        public void ProcessQueue(List<Ship> queue, Port port, Time time, CargoType cargoType)
        {
            decimal remainingTime = 24;  // Изначально у нас 24 часа

            foreach (var ship in queue.ToList()) // Преобразуем коллекцию в `ToList()` для безопасной итерации
            {
                if (ship.LeftUnloadingTimeBOM <= remainingTime)
                {
                    remainingTime -= ship.LeftUnloadingTimeBOM;
                    //ship.LeftUnloadingTimeBOM = 0;  // Корабль разгрузился

                    Console.WriteLine($"Ship {ship.Name} finished unloading and is removed from queue.");
                    port.actualShips.Remove(ship);
                    queue.Remove(ship);
                    port.servedShips.Add(ship);

                    //Вот тут он доолжен искать не один скорее всего
                    // Пытаемся найти следующий корабль того же типа
                    var nextShip = port.actualShips
                        .FirstOrDefault(s => s.CargoType == cargoType && !IsShipInQueue(s));

                    if (nextShip != null)
                    {
                        Console.WriteLine($"Adding new ship {nextShip.Name} to the queue.");
                        queue.Add(nextShip);
                        nextShip.LeftUnloadingTimeBOM -= ship.LeftUnloadingTimeBOM;  // Обновляем время разгрузки
                    }
                }
                else
                {
                    // Если времени не хватает на текущий корабль, переносим остаток времени на следующий день
                    ship.LeftUnloadingTimeBOM -= remainingTime;
                    break;  // Дальше уже нечего разгружать в этот день
                }
            }
        }


    }
}
