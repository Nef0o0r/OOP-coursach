namespace TourSales
{
    internal abstract class Tour : IPrintable
    {
        protected readonly int _id; // ID тура
        protected readonly string _name; // Название тура
        protected readonly DateTime _startDate; // Дата начала тура
        protected readonly int _duration; // Продолжительность тура в днях
        protected readonly decimal _price; // Цена тура

        public int ID { get { return _id; } }
        public string Name { get { return _name; } }
        public DateTime StartDate { get { return _startDate; } }
        public int Duration { get { return _duration; } }
        public decimal Price { get { return _price; } }

        public Tour(int id, string name, DateTime startDate, int duration, decimal price)
        {
            _id = id;
            _name = name;
            _startDate = startDate;
            _duration = duration;
            _price = price;
        }

        public virtual void Print()
        {
            Console.WriteLine("Tour ID: {0}\nName: {1}\nStart Date: {2}\nDuration: {3} days\nPrice: {4} Р",
                ID, Name, StartDate.ToShortDateString(), Duration, Price);
        }
    }
}
