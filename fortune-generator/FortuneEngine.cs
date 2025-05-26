using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyFortuneGenerator
{
    public class FortuneEngine
    {
        private Random random;
        private List<Fortune> fortuneHistory;
        private Dictionary<string, List<string>> fortuneTemplates;
        private string[] luckyColors = { "Red", "Blue", "Green", "Purple", "Gold", "Silver", "Orange", "Pink" };

        public FortuneEngine()
        {
            random = new Random();
            fortuneHistory = new List<Fortune>();
            InitializeFortuneTemplates();
        }

        private void InitializeFortuneTemplates()
        {
            fortuneTemplates = new Dictionary<string, List<string>>
            {
                ["Love"] = new List<string>
                {
                    "Love is in the air for you, {0}! A special someone may enter your life when you least expect it.",
                    "Your heart will guide you to happiness, {0}. Trust your romantic instincts today.",
                    "A meaningful connection awaits you, {0}. Be open to new relationships and deeper bonds.",
                    "Romance blooms where kindness grows, {0}. Your compassionate nature will attract love.",
                    "The stars align for matters of the heart, {0}. Love will find its way to you."
                },
                ["Career"] = new List<string>
                {
                    "Professional success is on the horizon, {0}! Your hard work will soon pay off.",
                    "A new opportunity will present itself to you, {0}. Be ready to seize the moment.",
                    "Your leadership qualities will shine bright today, {0}. Others will look to you for guidance.",
                    "Innovation and creativity will be your keys to success, {0}. Think outside the box.",
                    "A mentor or colleague will offer valuable advice, {0}. Listen carefully to their wisdom."
                },
                ["Health"] = new List<string>
                {
                    "Your body and mind are in perfect harmony today, {0}. Embrace this positive energy.",
                    "A healthy lifestyle choice you make today will benefit you greatly, {0}.",
                    "Listen to your body's needs, {0}. Rest and rejuvenation are calling to you.",
                    "Physical activity will bring you joy and vitality, {0}. Move your body with purpose.",
                    "Mental clarity and emotional balance are yours to claim, {0}. Meditate on your goals."
                },
                ["Finance"] = new List<string>
                {
                    "Financial abundance flows toward you, {0}! A wise investment decision awaits.",
                    "Your practical approach to money will serve you well, {0}. Trust your financial instincts.",
                    "An unexpected source of income may surprise you, {0}. Keep your eyes open for opportunities.",
                    "Saving money today will lead to greater prosperity tomorrow, {0}. Be mindful of your spending.",
                    "A financial goal you've been working toward is closer than you think, {0}."
                },
                ["General"] = new List<string>
                {
                    "Today brings new possibilities and fresh perspectives, {0}. Embrace the unknown with confidence.",
                    "Your positive attitude will attract wonderful experiences, {0}. Smile and the world smiles with you.",
                    "A pleasant surprise awaits you before the day ends, {0}. Stay alert for unexpected joys.",
                    "Your intuition is particularly strong today, {0}. Trust your inner voice to guide you.",
                    "Good fortune follows those who help others, {0}. Your kindness will be rewarded.",
                    "The universe conspires to help you achieve your dreams, {0}. Take that first step forward."
                },
                ["Adventure"] = new List<string>
                {
                    "An exciting journey begins with a single step, {0}. Adventure calls your name today!",
                    "Explore new territories, both literal and metaphorical, {0}. Discovery awaits the brave.",
                    "Your adventurous spirit will lead you to amazing experiences, {0}. Say yes to new opportunities.",
                    "A thrilling challenge will test your courage, {0}. You have the strength to overcome it.",
                    "The path less traveled holds special rewards for you, {0}. Dare to be different."
                }
            };
        }

        public Fortune GenerateFortune(UserInput input)
        {
            var templates = fortuneTemplates[input.Category];
            var selectedTemplate = templates[random.Next(templates.Count)];
            var message = string.Format(selectedTemplate, input.Name);

            // Apply mood influence
            message = ApplyMoodInfluence(message, input.Mood);

            var fortune = new Fortune
            {
                UserName = input.Name,
                Category = input.Category,
                Mood = input.Mood,
                Message = message,
                LuckyNumber = random.Next(1, input.LuckyNumberRange + 1),
                LuckyColor = luckyColors[random.Next(luckyColors.Length)],
                ConfidenceLevel = CalculateConfidenceLevel(input),
                Timestamp = DateTime.Now
            };

            fortuneHistory.Add(fortune);
            return fortune;
        }

        private string ApplyMoodInfluence(string message, string mood)
        {
            var moodModifiers = new Dictionary<string, string[]>
            {
                ["Happy"] = new[] { " Your joyful energy amplifies this fortune!", " Happiness multiplies your blessings!" },
                ["Sad"] = new[] { " Remember, after every storm comes sunshine.", " This fortune brings hope to brighten your day." },
                ["Excited"] = new[] { " Your enthusiasm will make this fortune even more powerful!", " Channel your excitement into positive action!" },
                ["Anxious"] = new[] { " Take deep breaths and trust in this guidance.", " Let this fortune calm your worries." },
                ["Calm"] = new[] { " Your peaceful nature enhances this fortune's wisdom.", " Serenity will guide you to success." },
                ["Adventurous"] = new[] { " Your bold spirit will unlock hidden opportunities!", " Adventure awaits those who dare to dream!" }
            };

            if (moodModifiers.ContainsKey(mood))
            {
                var modifiers = moodModifiers[mood];
                message += modifiers[random.Next(modifiers.Length)];
            }

            return message;
        }

        private int CalculateConfidenceLevel(UserInput input)
        {
            int baseConfidence = 70;
            
            // Mood influences confidence
            switch (input.Mood)
            {
                case "Happy":
                case "Excited":
                case "Adventurous":
                    baseConfidence += random.Next(10, 21);
                    break;
                case "Calm":
                    baseConfidence += random.Next(5, 16);
                    break;
                case "Sad":
                case "Anxious":
                    baseConfidence += random.Next(-10, 11);
                    break;
            }

            // Lucky number range affects confidence
            if (input.LuckyNumberRange > 50)
                baseConfidence += random.Next(0, 11);

            return Math.Max(50, Math.Min(95, baseConfidence));
        }

        public List<Fortune> GetFortuneHistory()
        {
            return fortuneHistory.OrderByDescending(f => f.Timestamp).ToList();
        }
    }
}
