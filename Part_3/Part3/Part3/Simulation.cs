namespace SeaPort
{
    internal class Simulation
    {
        public void SimulateDays(int durationInDays, Time time, Port port, Schedule schedule)
        {
            for (int day = 0; day < durationInDays; day++)
            {
                time.Current_Time = time.Start_Time.AddDays(day);
                Console.WriteLine($"Day: {time.Current_Time}");
                port.AddActualShipWithTime(time.Current_Time, schedule); // Моделируем один день
                port.PrintActualShips();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
            }
            port.RemoveShipInPortByName("Ship1", time);
        }
    }
}
