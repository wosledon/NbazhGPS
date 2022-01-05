using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.Models;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X2ATest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        public Nbazh0X2ATest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var hex =
                "78 78 2E 2A 0F 0C 1D 07 11 39 CA 02 7A C8 00 0C 46 58 00 00 14 D8 31 32 35 32 30 31 33 35 33 32 31 37 37 30 37 39 00 00 00 00 00 00 01 00 2A 6E CE 0D 0A"
                    .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var packet = NbazhGpsMessageIds.Gps地址请求包.Create(new Nbazh0X2A()
            {
                DateTime = DateTime.Now,
                GpsSatelliteInfo = new GpsSatelliteInfo0X22()
                {
                    GpsInfoLength = 12,
                    GpsSatelliteCount = 10
                },
                Lon = 111.0001M,
                Lat = 43.0001M,
                Speed = 100,
                HeadingAndStatus = new HeadingAndStatus()
                {
                    EorWLon = PackageEnums0X22.EorWLon.东经,
                    GpsLocatedFunc = PackageEnums0X22.GpsLocatedFunc.Gps实时定位,
                    Heading = 300,
                    IsGpsLocated = PackageEnums0X22.IsGpsLocated.已定位,
                    SorNLat = PackageEnums0X22.SorNLat.北纬
                },
                TelephoneNumber = "12345678910",
                Alarm = Alarm0X26.ACC开,
                LanguageExtensionPortStatus = LanguageExtensionPortStatus.中文
            });

            var hex = NbazhGpsSerializer.Serialize(packet).ToHexString();
            _testOutputHelper.WriteLine(hex);
        }
    }
}