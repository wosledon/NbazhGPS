using System;
using NbazhGPS.Protocol.Enums;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test
{
    public class TimeZoneLanguageTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public TimeZoneLanguageTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var tzl = new TimeZoneLanguageModel()
            {
                TimeZoneTime = 8,
                TimeZone = TimeZones.东
            };
            var d = 0x0320;
            _testOutputHelper.WriteLine(Convert.ToString(tzl.Serialize(), 2));
            _testOutputHelper.WriteLine(tzl.Serialize().ToString("x8"));
            Assert.Equal(tzl.Serialize().ToString("x8"), d.ToString("x8"));
        }

        [Fact]
        public void Test2()
        {
            ushort tzl = 0x0320;

            _testOutputHelper.WriteLine(new TimeZoneLanguageModel().Deserialize(tzl).ToString());
        }
    }
}