using System;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test
{
    public class NbazhGpsPackageTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public NbazhGpsPackageTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();
            //7878 05 01 0001 D9DC 0D0A
            NbazhGpsPackage packet = new NbazhGpsPackage()
            {
                Header = new NbazhGpsHeader()
                {
                    MsgId = NbazhGpsMessageIds.登陆包.ToByteValue(),
                    MsgNum = 1,
                },
            };

            var hex = nbazhGpsSerializer.SerializeReadOnlySpan(packet);
            _testOutputHelper.WriteLine(hex.ToArray().ToHexString());
            Assert.Equal(0x78, hex[0]);
        }

        [Fact]
        public void Test2()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();
            var hex = "7878 05 01 0001 D9DC 0D0A".ToHexBytes();
            var packet = nbazhGpsSerializer.Deserialize(hex);

            Assert.Equal(0x05, packet.Header.Length);
            Assert.Equal(0x01, packet.Header.MsgId);
            Assert.Equal(0x0001, packet.Header.MsgNum);
            Assert.Equal(0xD9DC, packet.Header.Crc);
        }

        [Fact]
        public void Test3()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();
            //7979 0005 01 0001 EA75 0D0A
            NbazhGpsPackage packet = new NbazhGpsPackage(PackageType.Type2)
            {
                Header = new NbazhGpsHeader()
                {
                    MsgId = NbazhGpsMessageIds.登陆包.ToByteValue(),
                    MsgNum = 1,
                },
            };

            var hex = nbazhGpsSerializer.SerializeReadOnlySpan(packet);
            _testOutputHelper.WriteLine(hex.ToArray().ToHexString());
            Assert.Equal(0x79, hex[0]);
        }

        [Fact]
        public void Test4()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();
            var hex = "7979 0005 01 0001 EA75 0D0A".ToHexBytes();
            var packet = nbazhGpsSerializer.Deserialize(hex);

            Assert.Equal(0x0005, packet.Header.Length);
            Assert.Equal(0x01, packet.Header.MsgId);
            Assert.Equal(0x0001, packet.Header.MsgNum);
            Assert.Equal(0xEA75, packet.Header.Crc);
        }

        [Fact]
        public void Test5()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer(false);
            var hex = "05 01 0001 D9DC".ToHexBytes();
            var packet = nbazhGpsSerializer.Deserialize(hex);

            Assert.Equal(0x0005, packet.Header.Length);
            Assert.Equal(0x01, packet.Header.MsgId);
            Assert.Equal(0x0001, packet.Header.MsgNum);
            Assert.Equal(0xD9DC, packet.Header.Crc);
        }

        [Fact]
        public void Test6()
        {
            NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer(false);
            var hex = "0005 01 0001 EA75".ToHexBytes();
            var packet = nbazhGpsSerializer.Deserialize(hex, PackageType.Type2);

            Assert.Equal(0x0005, packet.Header.Length);
            Assert.Equal(0x01, packet.Header.MsgId);
            Assert.Equal(0x0001, packet.Header.MsgNum);
            Assert.Equal(0xEA75, packet.Header.Crc);
        }
    }
}