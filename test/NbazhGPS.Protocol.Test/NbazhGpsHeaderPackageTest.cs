using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test
{
    public class NbazhGpsHeaderPackageTest
    {
        NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        [Fact]
        public void Test1()
        {
            var data = NbazhGpsSerializer.HeaderDeserialize("78 78 11 01 07 52 53 36 78 90 02 42 70 00 32 01 00 05 12 79 0D 0A"
                .ToHexBytes());
            //78 78 11 01 07 52 53 36 78 90 02 42 70 00 32 01 00 05 12 79 0D 0A
            Assert.Equal(0x11, data.Header.Length);
            Assert.Equal(0x01, data.Header.MsgId);
            Assert.Equal(0x0005, data.Header.MsgNum);
            Assert.Equal(0x1279, data.Crc);
        }
    }
}