using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Models;
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

        [Fact]
        public void Test3()
        {
            ushort tzl = 0x3201;
            var t = new TimeZoneLanguageModel().Deserialize(tzl);

            _testOutputHelper.WriteLine(t.ToString());
        }

        [Fact]
        public void Test4()
        {
            var tzl = new TimeZoneLanguageModel()
            {
                TimeZoneTime = 5.13f,
                TimeZone = TimeZones.西,
                ReservedBits = true,
                LanguageChoose2 = false,
                LanguageChoose1 = false,
            };

            _testOutputHelper.WriteLine(Convert.ToString(tzl.Serialize(), 2));
            _testOutputHelper.WriteLine(tzl.Serialize().ToString("X8"));
        }
    }
}