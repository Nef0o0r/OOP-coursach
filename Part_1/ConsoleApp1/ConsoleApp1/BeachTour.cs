namespace TourSales
{
    internal class BeachTour : Tour
    {
        public BeachTour(int id, string name, DateTime startDate, int duration, decimal price)
            : base(id, name, startDate, duration, price) { }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("Type: Beach Tour");
        }
    }
}