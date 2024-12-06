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

            for (int day = 0; day < durationInDays; day++)
            {
                time.Current_Time = time.Start_Time.AddDays(day);
                if (port.actualShips.Count > 0)
                {
                    foreach (var ship in port.actualShips) {
                        if ((ship.CargoType == CargoType.Bulk) && (queue.queue_bulk.Count < QuantityBulkCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        if ((ship.CargoType == CargoType.Liquid) && (queue.queue_container.Count < QuantityLinquidCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        if ((ship.CargoType == CargoType.Container) && (queue.queue_container.Count < QuantityContainerCrane) && !queue.IsShipInQueue(ship)) queue.AddQueu(ship, time);
                        //Console.WriteLine($"\n\nUnloadTime: {ship.ExtraDelay}\n\n");
                    }

                }
                Console.WriteLine($"Day: {time.Current_Time}");
                port.AddActualShipWithTime(time.Current_Time, schedule); // Моделируем один день
                //port.PrintActualShips();
                queue.PrintAllShipsInQueue();
            }
            port.RemoveShipInPortByName("Ship1", time);
        }
    }
}
