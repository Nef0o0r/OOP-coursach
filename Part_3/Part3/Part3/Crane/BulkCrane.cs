using SeaPort;

namespace SeaPort.Crane
{
    internal class BulkCrane : AbstractCrane
    {

        public BulkCrane()
        {
            CargoType = CargoType.Bulk;
            SpeedWorkInHour = 1500; //1500 кг в час
        }
    }
}
