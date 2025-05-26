using System;

namespace DailyFortuneGenerator
{
    public class UserInput
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Mood { get; set; }
        public int LuckyNumberRange { get; set; }
        public bool EnableSound { get; set; }
    }

    public class Fortune
    {
        public string UserName { get; set; }
        public string Category { get; set; }
        public string Mood { get; set; }
        public string Message { get; set; }
        public int LuckyNumber { get; set; }
        public string LuckyColor { get; set; }
        public int ConfidenceLevel { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            return $"Fortune for {UserName} - {Timestamp:MMM dd, yyyy}\n" +
                   $"Category: {Category} | Mood: {Mood}\n" +
                   $"Message: {Message}\n" +
                   $"Lucky Number: {LuckyNumber} | Lucky Color: {LuckyColor}\n" +
                   $"Confidence: {ConfidenceLevel}%\n" +
                   new string('-', 50);
        }
    }
}
