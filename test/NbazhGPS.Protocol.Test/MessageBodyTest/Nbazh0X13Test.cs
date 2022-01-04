using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.Models;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X13Test
    {
        NbazhGpsSerializer nbazhGpsSerializer = new NbazhGpsSerializer();
        [Fact]
        public void Test1()
        {
            var hex = "78 78 0A 13 40 04 04 00 01 00 0F DC EE 0D 0A".ToHexBytes();

            var packet = nbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var packet = NbazhGpsMessageIds.心跳包.Create(new Nbazh0X13()
            {
                TerminalInfo = new TerminalInfo0X13()
                {
                    GpsConnected = true,
                },
                VoltageLevel = VoltageLevel.电量中,
                GsmSignalStrength = GsmSignalStrength.信号强,
                LanguageExtensionPortStatus = LanguageExtensionPortStatus.中文
            });

            var hex = "78 78 0A 13 40 04 04 00 01 00 0F DC EE 0D 0A";

            var res = nbazhGpsSerializer.Serialize(packet);

            //Assert.Equal(hex.Replace(" ", ""), res.ToHexString());
        }
    }
}