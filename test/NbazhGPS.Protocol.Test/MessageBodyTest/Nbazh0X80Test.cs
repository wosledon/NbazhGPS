using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X80Test
    {
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();
        [Fact]
        public void Test1()
        {
            var hex = "78 78 0E 80 08 00 00 00 00 73 6F 73 23 00 01 6D 6A 0D 0A"
                .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}