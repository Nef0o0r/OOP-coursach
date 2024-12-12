using SeaPort;

namespace SeaPort.Crane
{
    internal class ContainerCrane : AbstractCrane
    {
        public ContainerCrane()
        {
            CargoType = CargoType.Container;
            SpeedWorkInHour = 100; //100 контейнеров в час
        }
    }
}