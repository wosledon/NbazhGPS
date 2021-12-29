using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test
{
    public class NbazhGpsHeaderTest
    {
        private readonly ITestOutputHelper _testOutputHelper;
        NbazhGpsSerializer NbazhSerializer = new NbazhGpsSerializer();

        public NbazhGpsHeaderTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            NbazhGpsHeader header = new NbazhGpsHeader()
            {
                MsgId = 0x01,
                MsgNum = 135,
            };

            var hex = NbazhSerializer.Serialize(header).ToHexString();
            Assert.Equal("05 01 0087 6CA7".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test2()
        {
            var headerBytes = "05 01 00 87 00 00".ToHexBytes();
            NbazhGpsHeader header = NbazhSerializer.Deserialize<NbazhGpsHeader>(headerBytes);
            Assert.Equal(135, header.MsgNum);
            Assert.Equal(NbazhGpsMessageIds.登陆包.ToValue(), header.MsgId);
            Assert.Equal(header.Length, headerBytes.Length-header.PackageType.ToValue());
        }

        [Fact]
        public void Test3()
        {
            NbazhGpsHeader header = new NbazhGpsHeader(PackageType.Type2)
            {
                MsgId = 0x01,
                MsgNum = 135,
            };

            var hex = NbazhSerializer.Serialize(header).ToHexString();
            Assert.Equal("00 05 01 0087 56B5".Replace(" ", ""), hex);
        }

        [Fact]
        public void Test4()
        {
            var headerBytes = "00 05 01 00 87 00 00".ToHexBytes();
            NbazhGpsHeader header = NbazhSerializer.Deserialize<NbazhGpsHeader>(headerBytes, PackageType.Type2);
            Assert.Equal(135, header.MsgNum);
            Assert.Equal(NbazhGpsMessageIds.登陆包.ToValue(), header.MsgId);
            Assert.Equal(header.Length, headerBytes.Length - header.PackageType.ToValue());
        }
    }
}