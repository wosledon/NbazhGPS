using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X21Test
    {
        [Fact]
        public void Test1()
        {
            NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();

            var hex = "7979 000D 21 00000001 01 4F4B21 001A F281 0D0A".ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}