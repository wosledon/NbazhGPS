using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X8ATest
    {
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();
        [Fact]
        public void Test1()
        {
            var hex = "78 78 0B 8A 0F 0C 1D 00 00 15 00 06 F0 86 0D 0A"
                .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }

        [Fact]
        public void Test2()
        {
            var hex = "78 78 05 8A 00 06 88 29 0D 0A"
                .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}