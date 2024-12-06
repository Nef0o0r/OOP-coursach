using SeaPort.Crane;

namespace SeaPort
{
    internal class Ship : AbstractShip
    {
        protected decimal UnloadingTime; //Время выгрузки без происшествий
        protected decimal UnloadingTimeBOM; //Время выгрузки с происшествиями

        private Random _random = new Random();
        public DateTime ActualArrival { get; private set; } // Реальное время прибытия
        public DateTime ActualDeparture { get; private set; } // Реальное время отъезда
        public decimal ExtraDelay { get; private set; } // Дополнительная задержка
        public TimeSpan DayInPort { get; set; }

        public Ship(string name, CargoType cargoType, double cargoWeight, DateTime scheduledArrival, int plannedStayDays)
            : base(name, cargoType, cargoWeight, scheduledArrival, plannedStayDays)
        {
            DayInPort = TimeSpan.Zero;

            // Рассчитываем случайные отклонения и задержки
            int arrivalDeviationDays; // Отклонение от расписания прибытия (-2 до +9 дней)
            int delayDeviationDays; // Задержка выгрузки (0-5 часов)

            int probability_arrivals = _random.Next(0, 2); //Вероятность задержки прибытия
            int probability_BOM = _random.Next(0, 2); //Вероятность неожиданной задержки выгрузки

            if (probability_arrivals == 0) arrivalDeviationDays = _random.Next(-2, 4);
            else arrivalDeviationDays = 0;

            if (probability_BOM == 0) delayDeviationDays = _random.Next(1, 6);
            else delayDeviationDays = 0;

            ActualArrival = scheduledArrival.AddDays(arrivalDeviationDays);
            ExtraDelay = delayDeviationDays;

            switch (cargoType)
            {
                case CargoType.Bulk:
                    BulkCrane bulkCrane = new BulkCrane();
                    UnloadingTime = (decimal)cargoWeight / bulkCrane.SpeedWorkInHour;
                    break;
                case CargoType.Liquid:
                    LiquedCrane liquedCrane = new LiquedCrane();
                    UnloadingTime = (decimal) cargoWeight / liquedCrane.SpeedWorkInHour;
                    break;
                case CargoType.Container:
                    ContainerCrane containerCrane = new ContainerCrane();
                    UnloadingTime = (decimal)cargoWeight / containerCrane.SpeedWorkInHour;
                    break;
                default:
                    throw new Exception("Unknown cargo type");
            }
        }
        // Переопределяем PrintShip, чтобы добавить информацию о времени выгрузки
        public override void PrintShip()
        {
            // Выводим общую информацию о судне с помощью базового метода
            base.PrintShip();

            Console.WriteLine($"ActualArrival: {ActualArrival}");
            // Выводим информацию о времени выгрузки

            Console.WriteLine($"Unloading Time: {UnloadingTime:F2} hours");

            Console.WriteLine($"ExtraDelay: {ExtraDelay} hours");

            Console.WriteLine();
        }
    }
}
