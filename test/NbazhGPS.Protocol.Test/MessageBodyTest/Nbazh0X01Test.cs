using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X01Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public Nbazh0X01Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();

            //78 78 11 01 07 52 53 36 78 90 02 42 70 00 32 01 00 05 12 79 0D 0A

            var hex = "7878 11 01 07 52 53 36 78 90 02 42 7000 3201 0005 1279 0D0A".ToHexBytes();

            var packet = nbazhGpsSerializer.Deserialize(hex);
            Nbazh0X01 body = (Nbazh0X01)packet.Bodies;

            Assert.Equal(0x11, packet.Header.Length);
            Assert.Equal(0x01, packet.Header.MsgId);

            Assert.Equal("7 52 53 36 78 90 02 42".Replace(" ", ""), body.TerminalId);
            Assert.Equal(0x7000, body.TerminalType);
            //Assert.Equal(0x3201, body.TimeZoneLanguage.Serialize());

            Assert.Equal(0x0005, packet.Header.MsgNum);
            Assert.Equal(0x1279, packet.Header.Crc);

            // 时区 0011 001000000001
        }

        [Fact]
        public void Test2()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();

            NbazhGpsPackage packet = new NbazhGpsPackage();
            packet.Header = new NbazhGpsHeader()
            {
                MsgId = 0x01,
                MsgNum = 0x0005
            };
            packet.Bodies = new Nbazh0X01()
            {
                TerminalId = "7 52 53 36 78 90 02 42".Replace(" ", ""),
                TerminalType = 0x7000,
                TimeZoneLanguage = new TimeZoneLanguageModel()
                {
                    TimeZoneTime = 5.13f,
                    TimeZone = TimeZones.西,
                    ReservedBits = true,
                    LanguageChoose2 = false,
                    LanguageChoose1 = false,
                }
            };

            var hex = nbazhGpsSerializer.Serialize(packet).ToHexString();
            _testOutputHelper.WriteLine(hex);
            Assert.Equal(hex, "7878 11 01 07 52 53 36 78 90 02 42 7000 3201 0005 1279 0D0A".Replace(" ", ""));
            //Expected: 78780D01 75253367700 0320 10005 30B7 0D0A
            //Actual:   787811010752533678900242700 0320 10005 1279 0D0A
        }

        [Fact]
        public void Test3()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();

            NbazhGpsPackage packet = new NbazhGpsPackage(PackageType.Type2);
            packet.Header = new NbazhGpsHeader()
            {
                MsgId = 0x01,
                MsgNum = 0x0005
            };
            packet.Bodies = new Nbazh0X01()
            {
                TerminalId = "7 52 53 36 78 90 02 42".Replace(" ", ""),
                TerminalType = 0x7000,
                TimeZoneLanguage = new TimeZoneLanguageModel()
                {
                    TimeZoneTime = 5.13f,
                    TimeZone = TimeZones.西,
                    ReservedBits = true,
                    LanguageChoose2 = false,
                    LanguageChoose1 = false,
                }
            };

            var hex = nbazhGpsSerializer.Serialize(packet).ToHexString();
            _testOutputHelper.WriteLine(hex);
            Assert.Equal(hex, "7979001101075253367890024270003201000575CC0D0A".Replace(" ", ""));
            //Expected: 78780D01 75253367700 0320 10005 30B7 0D0A
            //Actual:   787811010752533678900242700 0320 10005 1279 0D0A
        }

        [Fact]
        public void Test4()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();

            //7979 0011 01 0752533678900242 7000 3201 0005 75CC 0D0A

            var hex = "7979001101075253367890024270003201000575CC0D0A".ToHexBytes();

            var packet = nbazhGpsSerializer.Deserialize(hex);
            Nbazh0X01 body = (Nbazh0X01)packet.Bodies;

            Assert.Equal(0x11, packet.Header.Length);
            Assert.Equal(0x01, packet.Header.MsgId);

            Assert.Equal("7 52 53 36 78 90 02 42".Replace(" ", ""), body.TerminalId);
            Assert.Equal(0x7000, body.TerminalType);
            //Assert.Equal(0x3201, body.TimeZoneLanguage.Serialize());

            Assert.Equal(0x0005, packet.Header.MsgNum);
            Assert.Equal(0x75CC, packet.Header.Crc);

            // 时区 0011 001000000001
        }
    }
}