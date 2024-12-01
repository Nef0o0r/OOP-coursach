namespace TourSales
{
    internal class CityTour : Tour
    {
        public CityTour(int id, string name, DateTime startDate, int duration, decimal price)
            : base(id, name, startDate, duration, price) { }

        public override void Print()
        {
            base.Print();
            Console.WriteLine("Type: City Tour");
        }
    }
}