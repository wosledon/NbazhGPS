using System;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.Models;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X27Test
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        public Nbazh0X27Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var hex = "78 78 26 27 10 04 19 09 2D 07 C5 02 7A C9 1C 0C 46 58 00 00 05 37 09 00 00 00 00 00 00 00 00 80 02 00 0C 01 FF 00 00 4D F6 0D 0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var packet = NbazhGpsMessageIds.报警包_多围栏.Create(new Nbazh0X27()
            {
                DateTime = DateTime.Now,
                GpsSatelliteInfo = new GpsSatelliteInfo0X22()
                {
                    GpsInfoLength = 5,
                    GpsSatelliteCount = 5,
                },
                Lon = 111.0001M,
                Lat = 32.0001M,
                Speed = 100,
                HeadingAndStatus = new HeadingAndStatus()
                {
                    EorWLon = PackageEnums0X22.EorWLon.东经,
                    GpsLocatedFunc = PackageEnums0X22.GpsLocatedFunc.Gps实时定位,
                    Heading = 330,
                    IsGpsLocated = PackageEnums0X22.IsGpsLocated.已定位,
                    SorNLat = PackageEnums0X22.SorNLat.北纬
                },
                LBSLength = 7,
                MCC = 1,
                MNC = 2,
                LAC = 3,
                CellId = new UInt24(4),
                TerminalInfo = new TerminalInfo0X26()
                {
                    Acc = TerminalInfoEnums0X26.AccState.高,
                    Alarm = TerminalInfoEnums0X26.Alarm.低电报警,
                    Fortification = TerminalInfoEnums0X26.Fortification.撤防,
                    GpsConnected = TerminalInfoEnums0X26.GpsConnected.Gps已定位,
                    OilAndElectricity = TerminalInfoEnums0X26.OilAndElectricity.油电接通,
                    PowerCharging = TerminalInfoEnums0X26.PowerCharging.已接电源充电
                },
                VoltageLevel = VoltageLevel.电量很低,
                Alarm = Alarm0X26.ACC关,
                LanguageExtensionPortStatus = LanguageExtensionPortStatus.中文,
                FenceNumber = 5
            });

            var hex = NbazhGpsSerializer.Serialize(packet);

            _testOutputHelper.WriteLine(hex.ToHexString());
        }

        [Fact]
        public void Test3()
        {
            var hex = "787826271601050A2720550BE8B574036EE8B464154A0700010200030400005E0200FF010500001B0E0D0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test4()
        {
            var packet1 = NbazhGpsMessageIds.报警包_多围栏.Create(new Nbazh0X27()
            {
                DateTime = Convert.ToDateTime("2022-01-05 10:41:00"),
                GpsSatelliteInfo = new GpsSatelliteInfo0X22()
                {
                    GpsInfoLength = 5,
                    GpsSatelliteCount = 5,
                },
                Lon = 111.0001M,
                Lat = 32.0001M,
                Speed = 100,
                HeadingAndStatus = new HeadingAndStatus()
                {
                    EorWLon = PackageEnums0X22.EorWLon.东经,
                    GpsLocatedFunc = PackageEnums0X22.GpsLocatedFunc.Gps实时定位,
                    Heading = 330,
                    IsGpsLocated = PackageEnums0X22.IsGpsLocated.已定位,
                    SorNLat = PackageEnums0X22.SorNLat.北纬
                },
                LBSLength = 7,
                MCC = 1,
                MNC = 2,
                LAC = 3,
                CellId = new UInt24(4),
                TerminalInfo = new TerminalInfo0X26()
                {
                    Acc = TerminalInfoEnums0X26.AccState.高,
                    Alarm = TerminalInfoEnums0X26.Alarm.低电报警,
                    Fortification = TerminalInfoEnums0X26.Fortification.撤防,
                    GpsConnected = TerminalInfoEnums0X26.GpsConnected.Gps已定位,
                    OilAndElectricity = TerminalInfoEnums0X26.OilAndElectricity.油电接通,
                    PowerCharging = TerminalInfoEnums0X26.PowerCharging.已接电源充电
                },
                VoltageLevel = VoltageLevel.电量很低,
                Alarm = Alarm0X26.ACC关,
                LanguageExtensionPortStatus = LanguageExtensionPortStatus.中文,
                FenceNumber = 5
            });

            var packet2 = NbazhGpsMessageIds.报警包_多围栏.Create(new Nbazh0X27()
            {
                DateTime = Convert.ToDateTime("2022-01-05 10:41:00"),
                GpsSatelliteInfo = new GpsSatelliteInfo0X22()
                {
                    GpsInfoLength = 5,
                    GpsSatelliteCount = 5,
                },
                Lon = 111.0001M,
                Lat = 32.0001M,
                Speed = 100,
                HeadingAndStatus = new HeadingAndStatus()
                {
                    EorWLon = PackageEnums0X22.EorWLon.东经,
                    GpsLocatedFunc = PackageEnums0X22.GpsLocatedFunc.Gps实时定位,
                    Heading = 330,
                    IsGpsLocated = PackageEnums0X22.IsGpsLocated.已定位,
                    SorNLat = PackageEnums0X22.SorNLat.北纬
                },
                LBSLength = 7,
                MCC = 1,
                MNC = 2,
                LAC = 3,
                CellId = new UInt24(4),
                TerminalInfo = new TerminalInfo0X26()
                {
                    Acc = TerminalInfoEnums0X26.AccState.高,
                    Alarm = TerminalInfoEnums0X26.Alarm.低电报警,
                    Fortification = TerminalInfoEnums0X26.Fortification.撤防,
                    GpsConnected = TerminalInfoEnums0X26.GpsConnected.Gps已定位,
                    OilAndElectricity = TerminalInfoEnums0X26.OilAndElectricity.油电接通,
                    PowerCharging = TerminalInfoEnums0X26.PowerCharging.已接电源充电
                },
                VoltageLevel = VoltageLevel.电量很低,
                Alarm = Alarm0X26.ACC关,
                LanguageExtensionPortStatus = LanguageExtensionPortStatus.中文,
                FenceNumber = 5
            });

            var hex1 = NbazhGpsSerializer.Serialize(packet1).ToHexString();
            var hex2 = NbazhGpsSerializer.Serialize(packet2).ToHexString();

            _testOutputHelper.WriteLine(hex1);
            _testOutputHelper.WriteLine(hex2);

            Assert.NotEqual(hex1, hex2);
        }
    }
}