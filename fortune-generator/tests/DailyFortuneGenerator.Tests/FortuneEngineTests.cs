using System;
using System.Linq;
using Xunit;
using DailyFortuneGenerator.Core;
using DailyFortuneGenerator.Models;

namespace DailyFortuneGenerator.Tests
{
    /// <summary>
    /// Unit tests for the FortuneEngine class.
    /// </summary>
    public class FortuneEngineTests
    {
        private readonly FortuneEngine _fortuneEngine;

        public FortuneEngineTests()
        {
            _fortuneEngine = new FortuneEngine();
        }

        [Fact]
        public void GenerateFortune_WithValidInput_ReturnsFortuneWithCorrectUserName()
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "John Doe",
                Category = "General",
                Mood = "Happy",
                LuckyNumberRange = 50,
                EnableSound = true
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.Equal("John Doe", fortune.UserName);
            Assert.Equal("General", fortune.Category);
            Assert.Equal("Happy", fortune.Mood);
        }

        [Fact]
        public void GenerateFortune_WithValidInput_ReturnsFortuneWithValidLuckyNumber()
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Jane Smith",
                Category = "Love",
                Mood = "Excited",
                LuckyNumberRange = 25,
                EnableSound = false
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.InRange(fortune.LuckyNumber, 1, 25);
        }

        [Fact]
        public void GenerateFortune_WithValidInput_ReturnsFortuneWithValidConfidenceLevel()
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Test User",
                Category = "Career",
                Mood = "Calm",
                LuckyNumberRange = 100,
                EnableSound = true
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.InRange(fortune.ConfidenceLevel, 50, 95);
        }

        [Fact]
        public void GenerateFortune_WithValidInput_ReturnsFortuneWithNonEmptyMessage()
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Alice",
                Category = "Health",
                Mood = "Anxious",
                LuckyNumberRange = 75,
                EnableSound = false
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.NotNull(fortune.Message);
            Assert.NotEmpty(fortune.Message);
            Assert.Contains("Alice", fortune.Message);
        }

        [Fact]
        public void GenerateFortune_WithValidInput_ReturnsFortuneWithValidLuckyColor()
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Bob",
                Category = "Finance",
                Mood = "Adventurous",
                LuckyNumberRange = 10,
                EnableSound = true
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.NotNull(fortune.LuckyColor);
            Assert.NotEmpty(fortune.LuckyColor);
        }

        [Fact]
        public void GenerateFortune_MultipleGenerations_AddsToHistory()
        {
            // Arrange
            var userInput1 = new UserInput
            {
                Name = "User1",
                Category = "General",
                Mood = "Happy",
                LuckyNumberRange = 50,
                EnableSound = false
            };

            var userInput2 = new UserInput
            {
                Name = "User2",
                Category = "Love",
                Mood = "Excited",
                LuckyNumberRange = 30,
                EnableSound = true
            };

            // Act
            _fortuneEngine.GenerateFortune(userInput1);
            _fortuneEngine.GenerateFortune(userInput2);
            var history = _fortuneEngine.GetFortuneHistory();

            // Assert
            Assert.Equal(2, history.Count);
            Assert.Equal("User2", history.First().UserName); // Should be ordered by newest first
            Assert.Equal("User1", history.Last().UserName);
        }

        [Theory]
        [InlineData("Love")]
        [InlineData("Career")]
        [InlineData("Health")]
        [InlineData("Finance")]
        [InlineData("General")]
        [InlineData("Adventure")]
        public void GenerateFortune_WithDifferentCategories_ReturnsFortuneWithCorrectCategory(string category)
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Test User",
                Category = category,
                Mood = "Happy",
                LuckyNumberRange = 50,
                EnableSound = false
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.Equal(category, fortune.Category);
            Assert.NotNull(fortune.Message);
            Assert.NotEmpty(fortune.Message);
        }

        [Theory]
        [InlineData("Happy")]
        [InlineData("Sad")]
        [InlineData("Excited")]
        [InlineData("Anxious")]
        [InlineData("Calm")]
        [InlineData("Adventurous")]
        public void GenerateFortune_WithDifferentMoods_ReturnsFortuneWithCorrectMood(string mood)
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Test User",
                Category = "General",
                Mood = mood,
                LuckyNumberRange = 50,
                EnableSound = false
            };

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);

            // Assert
            Assert.Equal(mood, fortune.Mood);
        }

        [Fact]
        public void GetFortuneHistory_WhenEmpty_ReturnsEmptyList()
        {
            // Arrange & Act
            var history = _fortuneEngine.GetFortuneHistory();

            // Assert
            Assert.NotNull(history);
            Assert.Empty(history);
        }

        [Fact]
        public void GenerateFortune_WithValidInput_SetsTimestampToCurrentTime()
        {
            // Arrange
            var userInput = new UserInput
            {
                Name = "Time Test",
                Category = "General",
                Mood = "Happy",
                LuckyNumberRange = 50,
                EnableSound = false
            };
            var beforeGeneration = DateTime.Now;

            // Act
            var fortune = _fortuneEngine.GenerateFortune(userInput);
            var afterGeneration = DateTime.Now;

            // Assert
            Assert.InRange(fortune.Timestamp, beforeGeneration, afterGeneration);
        }
    }
}
