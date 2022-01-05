using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X17Test
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        public Nbazh0X17Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var hex="7878 9F 17 99 00000001 414C41524D534D53 2626 970752A862A58B66003A00470054003000360044002D00310032003800330036002D005A004A004D002C5E7F4E1C7701002E60E05DDE5E02002E60E057CE533A002E4E915C71897F8DEF002E79BB60E05DDE5E025B665927655980B27EA6003200377C73002E002C00310030003A00340033 2626 000000000000000000000000000000000000000000 2323 001C EA97 0D0A".ToHexBytes();


            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var packet = NbazhGpsMessageIds.中文地址回复包.Create(new Nbazh0X17_1()
            {
                ServerFlagBits = 1,
                ALARMSMS = "ALARMSMS",
                AddressContent = "Test",
            });

            var hex = NbazhGpsSerializer.Serialize(packet).ToHexString();

            _testOutputHelper.WriteLine(hex);
        }

        [Fact]
        public void Test3()
        {
            var hex =
                "787835172F00000001414C41524D534D532626005400650073007426260000000000000000000000000000000000000000002323000099330D0A"
                    .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test4()
        {
            var hex =
                "78 78 24 17 01 CC 00 28 7D 00 1F 71 31 32 35 32 30 31 33 35 33 32 31 37 37 30 37 39 00 00 00 00 00 00 01 00 2A 7D D6 0D 0A"
                    .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test99()
        {
            Nbazh0X17 p = new Nbazh0X17();

            _testOutputHelper.WriteLine(p.GetType().Name);
        }
    }
}