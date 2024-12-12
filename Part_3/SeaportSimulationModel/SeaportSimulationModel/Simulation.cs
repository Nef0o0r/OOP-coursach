using System.Collections;

namespace SeaPort
{
    internal class Simulation
    {
        private int QuantityBulkCrane;//Количество контейнеров Сыпучих
        private int QuantityLinquidCrane;
        private int QuantityContainerCrane;
        public ulong Fine = 2000;
        public Dictionary<CargoType, decimal> hashTable = new Dictionary<CargoType, decimal>();

        Queue queue = new Queue();
        
        public void SimulateDays(int durationInDays, Time time, Port port, Schedule schedule)
        {
            QuantityBulkCrane = port.BulkCranes;
            QuantityLinquidCrane = port.LiquidCranes;
            QuantityContainerCrane = port.ContainerCranes;

            foreach (CargoType cargo in Enum.GetValues(typeof(CargoType)))
            {
                hashTable.Add(cargo, 0);
            }
            Console.WriteLine("\n////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            //Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------------------------------------------");
            for (int day = 0; day < durationInDays; day++)
            {
                time.Current_Time = time.Start_Time.AddDays(day);
                port.AddActualShipWithTime(time.Current_Time, schedule);
                Console.WriteLine("=============================");
                Console.WriteLine($"|| Day: {time.Current_Time} ||");
                Console.WriteLine("=============================\n");
                if (port.actualShips.Count > 0)
                {
                    foreach (CargoType cargo in Enum.GetValues(typeof(CargoType)))
                    {
                        hashTable[cargo] += (ulong)port.GetShipCountInPort(cargo) - queue.GetShipCountInQueueReflection(cargo);
                    }
                    foreach (var ship in port.actualShips) {
                        if ((ship.CargoType == CargoType.Bulk) && (queue.queue_bulk.Count < QuantityBulkCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        if ((ship.CargoType == CargoType.Liquid) && (queue.queue_liquid.Count < QuantityLinquidCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        if ((ship.CargoType == CargoType.Container) && (queue.queue_container.Count < QuantityContainerCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);                  
                    }
                }
                 // Моделируем один день
                port.PrintActualShips();
                if (queue.IsAnyShipInQueue())
                {
                    queue.PrintAllShipsInQueue();
                }

                Console.WriteLine("\n**********************************************************************************************\n");
                // Обработка списков с учетом LeftUnloadingTimeBOM
                queue.ProcessQueue(queue.queue_bulk, port, time, CargoType.Bulk);
                queue.ProcessQueue(queue.queue_liquid, port, time, CargoType.Liquid);
                queue.ProcessQueue(queue.queue_container, port, time, CargoType.Container);
                Console.WriteLine("\n////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////\n");
                //Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------");
            }
            foreach (var ship in port.actualShips) {
                port.FullFine += Fine * (ulong)Math.Ceiling(ship.Expectation.TotalMinutes / 3600);
            }
            foreach (var ship in port.servedShips)
            {
                port.FullFine += Fine * (ulong)ship.DelayInPort.TotalDays;
            }
            foreach (CargoType cargo in Enum.GetValues(typeof(CargoType)))
            {
                hashTable[cargo] /= (decimal)durationInDays;
            }

        }
    }
}
