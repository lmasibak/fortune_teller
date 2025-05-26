using System;

namespace DailyFortuneGenerator.Models
{
    /// <summary>
    /// Represents a generated fortune with all associated data.
    /// </summary>
    public class Fortune
    {
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the fortune category.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's mood when the fortune was generated.
        /// </summary>
        public string Mood { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the fortune message.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the lucky number.
        /// </summary>
        public int LuckyNumber { get; set; }

        /// <summary>
        /// Gets or sets the lucky color.
        /// </summary>
        public string LuckyColor { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the confidence level (50-95%).
        /// </summary>
        public int ConfidenceLevel { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the fortune was generated.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Returns a formatted string representation of the fortune.
        /// </summary>
        /// <returns>Formatted fortune string.</returns>
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
