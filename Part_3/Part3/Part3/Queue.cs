using System.Reflection;

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
                    ship.Expectation = time.Current_Time - ship.ActualArrival;
                    break;
                case CargoType.Liquid:
                    queue_liquid.Add(ship);
                    ship.Expectation = time.Current_Time - ship.ActualArrival;
                    break;
                case CargoType.Container:
                    queue_container.Add(ship);
                    ship.Expectation = time.Current_Time - ship.ActualArrival;
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
            if (queue_bulk.Count > 0)
            {
                Console.WriteLine("\n -------------");
                Console.WriteLine("| Bulk Queue: |");
                Console.WriteLine(" -------------\n");
                foreach (var ship in queue_bulk)
                {
                    Console.WriteLine($"Ship: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation:dd\\:hh\\:mm\\:ss}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2} " +
                        $"Left Unloading: {ship.LeftUnloadingTimeBOM:F2}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\n -------------");
                Console.WriteLine("| Bulk Queue: |  IS EMPTY!");
                Console.WriteLine(" -------------");
            }
            if (queue_liquid.Count > 0)
            {
                Console.WriteLine("\n ---------------");
                Console.WriteLine("| Liquid Queue: |");
                Console.WriteLine(" ---------------\n");
                foreach (var ship in queue_liquid)
                {
                    Console.WriteLine($"Ship: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation:dd\\:hh\\:mm\\:ss}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2} " +
                        $"Left Unloading: {ship.LeftUnloadingTimeBOM:F2}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(" ---------------");
                Console.WriteLine("| Liquid Queue: |  IS EMPTY!");
                Console.WriteLine(" ---------------");
            }
            if (queue_container.Count > 0)
            {
                Console.WriteLine(" ------------------");
                Console.WriteLine("| Container Queue: |");
                Console.WriteLine(" ------------------\n");
                foreach (var ship in queue_container)
                {
                    Console.WriteLine($"Ship: {ship.Name}, Arrival: {ship.ActualArrival}, Expectation: {ship.Expectation:dd\\:hh\\:mm\\:ss}, UnloadingTimeBOM: {ship.UnloadingTimeBOM:F2}, " +
                        $"Left Unloading: {ship.LeftUnloadingTimeBOM:F2}");
                }
            }
            else
            {
                Console.WriteLine(" ------------------");
                Console.WriteLine("| Container Queue: | IS EMPTY!");
                Console.WriteLine(" ------------------");
                Console.WriteLine();
            }


        }
        
        // Метод для обработки очереди
        public void ProcessQueue(List<Ship> queue, Port port, Time time, CargoType cargoType)
        {
            decimal remainingTime = 24;  // Изначально у нас 24 часа
            //decimal tmp; //Переменная для переноса времени
            foreach (var ship in queue.ToList()) // Преобразуем коллекцию в `ToList()` для безопасной итерации
            {
                if (ship.LeftUnloadingTimeBOM <= remainingTime)
                {
                    remainingTime -= ship.LeftUnloadingTimeBOM;
                    ship.LeftUnloadingTimeBOM = 0;  // Корабль разгрузился

                    Console.WriteLine($"Ship {ship.Name} finished unloading and is removed from queue.");
                    port.actualShips.Remove(ship);
                    queue.Remove(ship);

                    if (time.Current_Time - ship.ActualArrival - TimeSpan.FromDays(ship.PlannedStayDays) > TimeSpan.Zero)
                    {
                        ship.DelayInPort = time.Current_Time - ship.ActualArrival - TimeSpan.FromDays(ship.PlannedStayDays);
                    }
                    else { ship.DelayInPort = TimeSpan.Zero; }
                    port.servedShips.Add(ship);//Разгруженные корабли
                    
                   
                    do
                    {
                        var nextShip = port.actualShips
                        .FirstOrDefault(s => s.CargoType == cargoType && !IsShipInQueue(s));
                        if (nextShip != null)
                        {
                            Console.WriteLine($"Adding new ship {nextShip.Name} to the queue.");
                            queue.Add(nextShip);
                            TimeSpan remainingTimes = TimeSpan.FromHours((double)remainingTime);
                            nextShip.Expectation = time.Current_Time - ship.ActualArrival - remainingTimes;

                            Console.WriteLine($"Ship: {nextShip.Name}, Arrival: {nextShip.ActualArrival}, Expectation: {nextShip.Expectation:dd\\:hh\\:mm\\:ss}, UnloadingTimeBOM: {nextShip.UnloadingTimeBOM:F2}");
                            if (nextShip.LeftUnloadingTimeBOM - remainingTime > 0)
                            {
                                nextShip.LeftUnloadingTimeBOM -= remainingTime;
                                break;
                            }
                            else
                            {
                                remainingTime -= nextShip.LeftUnloadingTimeBOM;
                                nextShip.LeftUnloadingTimeBOM = 0;
                                Console.WriteLine($"Ship {ship.Name} finished unloading and is removed from queue.");
                                port.actualShips.Remove(nextShip);
                                queue.Remove(nextShip);
                                if(time.Current_Time - nextShip.ActualArrival - TimeSpan.FromDays(nextShip.PlannedStayDays) > TimeSpan.Zero)
                                {
                                    nextShip.DelayInPort = time.Current_Time - nextShip.ActualArrival - TimeSpan.FromDays(nextShip.PlannedStayDays);
                                }
                                else { nextShip.DelayInPort = TimeSpan.Zero; }
                                
                                port.servedShips.Add(nextShip);
                            }
                        }
                        else break;
                    } while (remainingTime > 0);
                }
                else
                {
                    // Если времени не хватает на текущий корабль, переносим остаток времени на следующий день
                    ship.LeftUnloadingTimeBOM -= remainingTime;
                    break;  // Дальше уже нечего разгружать в этот день
                }
            }
        }

        public bool IsAnyShipInQueue()
        {
            return queue_bulk.Count > 0 || queue_liquid.Count > 0 || queue_container.Count > 0;
        }
        public ulong GetShipCountInQueueReflection(CargoType cargoType)
        {
            string fieldName = $"queue_{cargoType.ToString().ToLower()}";
            var field = GetType().GetField(fieldName);

            if (field != null && field.GetValue(this) is List<Ship> queue)
            {
                return (ulong)queue.Count;
            }
            else
            {
                throw new ArgumentException("Unknown CargoType or queue not found");
            }
        }
    }
}
