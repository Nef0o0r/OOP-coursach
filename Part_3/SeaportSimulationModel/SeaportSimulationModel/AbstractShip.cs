namespace SeaPort
{
    internal abstract class AbstractShip
    {
        public string Name { get; protected set; } //Название судна
        public CargoType CargoType { get; protected set; } //Тип груза
        public double CargoWeight { get; protected set; } //Вес груза
        public DateTime ScheduledArrival { get; protected set; } //Дата прибытия
        public int PlannedStayDays { get; protected set; } //Планируемый срок стоянки
        public DateTime DepartureDate { get; protected set; } //Планируемая дата отбытия

        protected AbstractShip(string name, CargoType cargoType, double cargoWeight, DateTime scheduledArrival, int plannedStayDays)
        {
            Name = name;
            CargoType = cargoType;
            CargoWeight = cargoWeight;
            ScheduledArrival = scheduledArrival;
            PlannedStayDays = plannedStayDays;
            DepartureDate = ScheduledArrival.AddDays(PlannedStayDays);
        }
        public virtual void PrintShip()
        {
            Console.WriteLine("Ship Information:");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Cargo Type: {CargoType}");
            Console.WriteLine($"Cargo Weight (kg) or Quantity (things): {CargoWeight}");
            Console.WriteLine($"Scheduled Arrival: {ScheduledArrival}");
            Console.WriteLine($"Planned Stay (Days): {PlannedStayDays}");
            Console.WriteLine($"Departur Date: {DepartureDate}");
        }
    }
}
