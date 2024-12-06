namespace SeaPort
{
    internal class Queue
    {
        public List<Ship> queue_bulk;
        public List<Ship> queue_liquid;
        public List<Ship> queue_container;

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
                Console.WriteLine($"\nShip: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2}");
            }

            Console.WriteLine("\nLiquid Queue:");
            foreach (var ship in queue_liquid)
            {
                Console.WriteLine($"\nShip: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2}");
            }

            Console.WriteLine("\nContainer Queue:");
            foreach (var ship in queue_container)
            {
                Console.WriteLine($"\nShip: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2}");
            }
        }
    }
}
