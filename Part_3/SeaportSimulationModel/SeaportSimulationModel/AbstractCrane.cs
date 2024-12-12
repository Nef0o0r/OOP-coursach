namespace SeaPort
{
    internal abstract class AbstractCrane
    {
        public int SpeedWorkInHour { get; protected set; }
        public CargoType CargoType { get; protected set; }
    }
}
