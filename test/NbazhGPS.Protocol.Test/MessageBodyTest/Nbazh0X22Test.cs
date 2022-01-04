using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.Models;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X22Test
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        public Nbazh0X22Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var hex = "7878 22 22 0F0C1D023305 C9 02 7A C8 18 0C 46 58 60 00 14 00 01 CC 00 28 7D 00 1F 71 00 00 01 00 08 20 86 0D 0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var package = NbazhGpsMessageIds.Gps定位包.Create(new Nbazh0X22()
            {
                DateTime = DateTime.Now,
                GpsSatelliteInfo = new GpsSatelliteInfo0X22()
                {
                    GpsInfoLength = 5,
                    GpsSatelliteCount = 5
                },
                Lon = 110.0000001M,
                Lat = 30.0000001M,
                Speed = 100,
                HeadingAndStatus = new HeadingAndStatus()
                {
                    EorWLon = PackageEnums0X22.EorWLon.东经,
                    GpsLocatedFunc = PackageEnums0X22.GpsLocatedFunc.Gps实时定位,
                    Heading = 10,
                    IsGpsLocated = PackageEnums0X22.IsGpsLocated.已定位,
                    SorNLat = PackageEnums0X22.SorNLat.北纬
                },
                MCC = 460,
                MNC = 0,
                LAC = 0,
                CellId = 4680,
                AccState = AccState.高,
                DataReportingMode = DataReportingMode.定时上报,
            });

            var hex = NbazhGpsSerializer.Serialize(package).ToHexString();

            _testOutputHelper.WriteLine(hex);
        }
        [Fact]
        public void Test3()
        {
            var hex = "78782222160104113B00550BCD3D800337F98064140A01CC00000000124801000000000BA00D0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}