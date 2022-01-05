using System;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X28Test
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        public Nbazh0X28Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var hex = @"78 78 3B 28 10 01 0D 02 02 02 01 CC 00 28 7D 00 1F 71 3E 28 7D 00 1F 72 31 28 7D 001E 23 2D 28 7D 00 1F 40 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 00 02 0005 B1 4B 0D 0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var packet = NbazhGpsMessageIds.Lbs多基站扩展包.Create(new Nbazh0X28()
            {
                DateTime = Convert.ToDateTime("2022-01-05 09:19:00"),
                MCC = 1,
                MNC = 2,
                LAC = 3,
                CellId = new UInt24(4),
                RSSI = 5,
                NLAC1 = 1,
                NLAC2 = 2,
                NLAC3 = 3,
                NLAC4 = 2,
                NLAC5 = 2,
                NLAC6 = 2,
                NCI1 = new UInt24(1),
                NCI2 = new UInt24(1),
                NCI3 = new UInt24(1),
                NCI4 = new UInt24(1),
                NCI5 = new UInt24(1),
                NCI6 = new UInt24(1),
                NRSSI1 = 2,
                NRSSI2 = 2,
                NRSSI3 = 2,
                NRSSI4 = 2,
                NRSSI5 = 2,
                NRSSI6 = 2,
            }, PackageType.Type1);

            var hex = NbazhGpsSerializer.Serialize(packet);

            _testOutputHelper.WriteLine(hex.ToHexString());
        }

        [Fact]
        public void Test3()
        {
            var hex =
                "7878382816010509130000010200030400000500010100000200020100000200030100000200020100000200020100000200020100000200002F700D0A"
                    .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}