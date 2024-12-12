using SeaPort;

namespace SeaPort.Crane
{
    internal class LiquedCrane : AbstractCrane
    {
        public LiquedCrane()
        {
            CargoType = CargoType.Liquid;
            SpeedWorkInHour = 3000; //3000 кг в час
        }
    }
}