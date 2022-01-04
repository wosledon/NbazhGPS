using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X28Test
    {
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

        [Fact]
        public void Test1()
        {
            var hex = @"78 78 3B 28 10 01 0D 02 02 02 01 CC 00 28 7D 00 1F 71 3E 28 7D 00 1F 72 31 28 7D 001E 23 2D 28 7D 00 1F 40 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 00 02 0005 B1 4B 0D 0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}