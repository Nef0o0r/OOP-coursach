namespace TourSales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BeachTour beachTour = new BeachTour(1, "Maldives Getaway", new DateTime(2024, 12, 1), 7, 49999.99M);
            CityTour cityTour = new CityTour(2, "Paris Explorer", new DateTime(2024, 5, 15), 5, 34999.99M);
            AdventureTour adventureTour = new AdventureTour(3, "Amazon Jungle Trek", new DateTime(2024, 8, 20), 10, 59999.99M);

            beachTour.Print();
            Console.WriteLine();
            cityTour.Print();
            Console.WriteLine();
            adventureTour.Print();
        }
    }
}
