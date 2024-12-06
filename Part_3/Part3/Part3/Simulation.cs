namespace SeaPort
{
    internal class Simulation
    {
        private int QuantityBulkCrane;//Количество контейнеров Сыпучих
        private int QuantityLinquidCrane;
        private int QuantityContainerCrane;


        Queue queue = new Queue();
        public void SimulateDays(int durationInDays, Time time, Port port, Schedule schedule)
        {
            QuantityBulkCrane = port.BulkCranes;
            QuantityLinquidCrane = port.LiquidCranes;
            QuantityContainerCrane = port.ContainerCranes;
            //HoursDuration = 24;

            for (int day = 0; day < durationInDays; day++)
            {
                time.Current_Time = time.Start_Time.AddDays(day);
                if (port.actualShips.Count > 0)
                {
                    foreach (var ship in port.actualShips) {
                        if ((ship.CargoType == CargoType.Bulk) && (queue.queue_bulk.Count < QuantityBulkCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        if ((ship.CargoType == CargoType.Liquid) && (queue.queue_liquid.Count < QuantityLinquidCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        if ((ship.CargoType == CargoType.Container) && (queue.queue_container.Count < QuantityContainerCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        //Console.WriteLine($"\n\nUnloadTime: {ship.ExtraDelay}\n\n");
                    }

                }
                Console.WriteLine("\n__________________________________________________________");
                Console.WriteLine($"Day: {time.Current_Time}");
                port.AddActualShipWithTime(time.Current_Time, schedule); // Моделируем один день
                //port.PrintActualShips();
                queue.PrintAllShipsInQueue();
                //foreach (var ship in queue.queue_bulk) ship.LeftUnloadingTimeBOM -= HoursDuration;
                //foreach (var ship in queue.queue_liquid) ship.LeftUnloadingTimeBOM -= HoursDuration;
                //foreach (var ship in queue.queue_bulk) ship.LeftUnloadingTimeBOM -= HoursDuration;

                // Обработка списков с учетом LeftUnloadingTimeBOM
                queue.ProcessQueue(queue.queue_bulk, port, time, CargoType.Bulk);
                queue.ProcessQueue(queue.queue_liquid, port, time, CargoType.Liquid);
                queue.ProcessQueue(queue.queue_container, port, time, CargoType.Container);
            }
            //port.RemoveShipInPortByName("Ship1", time);
        }
    }
}
