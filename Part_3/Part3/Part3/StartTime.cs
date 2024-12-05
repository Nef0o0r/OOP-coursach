namespace SeaPort
{
    internal class Time
    {
        public DateTime Start_Time { get; set; }
        public DateTime Current_Time { get; set; }
        public DateTime End_Time { get; set; }
        public Time()
        {
            Start_Time = new DateTime(2024, 1, 1);
            Current_Time = Start_Time;
            End_Time = Start_Time.AddDays(31);
        }
        public void StartTime(DateTime startDateTime)
        {
            Start_Time = startDateTime;
            Current_Time = startDateTime;
            End_Time = Start_Time.AddDays(31);
        }
        public void CurrentTime(int PassedTimeHours)
        {
            Current_Time.AddHours(PassedTimeHours);
        }
    }
}
