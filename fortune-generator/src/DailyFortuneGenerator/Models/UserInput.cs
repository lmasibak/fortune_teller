namespace DailyFortuneGenerator.Models
{
    /// <summary>
    /// Represents user input for fortune generation.
    /// </summary>
    public class UserInput
    {
        /// <summary>
        /// Gets or sets the user's name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the fortune category.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's current mood.
        /// </summary>
        public string Mood { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the maximum range for lucky number generation.
        /// </summary>
        public int LuckyNumberRange { get; set; }

        /// <summary>
        /// Gets or sets whether sound effects are enabled.
        /// </summary>
        public bool EnableSound { get; set; }
    }
}
