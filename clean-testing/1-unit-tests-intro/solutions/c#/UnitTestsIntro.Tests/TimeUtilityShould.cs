using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace UnitTestsIntro.Tests
{
    public class TimeUtilityShould
    {
        [TestCase(0, "Night")]
        [TestCase(4, "Night")]
        [TestCase(6, "Morning")]
        [TestCase(9, "Morning")]
        [TestCase(12, "Afternoon")]
        [TestCase(17, "Afternoon")]
        [TestCase(18, "Evening")]
        [TestCase(23, "Evening")]
        public void GetADescriptionAtAnyTime(int hour, string expectedDescription)
        {
            var clockMock = new Mock<IClock>();
            clockMock.Setup(c => c.Now())
                .Returns(hour.ToDateTime());
            
            new TimeUtility(clockMock.Object)
                .GetTimeOfDay()
                .Should()
                .Be(expectedDescription);
        }
        
        [Test]
        public void ReturnMorningFor6Am()
        {
            var clockMock = new Mock<IClock>();
            clockMock.Setup(c => c.Now())
                .Returns(new DateTime(2022, 12, 1, 6, 5, 0, 0));
            
            new TimeUtility(clockMock.Object)
                .GetTimeOfDay()
                .Should()
                .Be("Morning");
        }
    }

    internal static class TestExtensions
    {
        public static DateTime ToDateTime(this int hour) => new(2022, 12, 1, hour, 0, 0, 0);
    }
}