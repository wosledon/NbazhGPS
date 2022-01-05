using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X97Test
    {
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();
        [Fact]
        public void Test1()
        {
            //var hex = "79 79 00 BC 97 00 B5 00 00 00 01 41 4C 41 52 4D 53 4D 53 26 26 00 4A 00 4D 00 30 00 31 00 2D 00 38 00 39 00 37 00 33 00 31 00 3A 00 53 00 4F 00 53 00 20 00 61 00 6C 00 61 00 72 00 6D 00 2E 00 68 00 74 00 74 00 70 00 3A 00 2F 00 2F 00 6D 00 61 00 70 00 73 00 2E 00 67 00 6F 00 6F 00 67 00 6C 00 65 00 2E 00 63 00 6F 00 6D 00 2F 00 6D 00 61 00 70 00 73 00 3F 00 7100 3D 00 4E 00 32 00 32 00 2E 00 35 00 37 00 33 00 35 00 36 00 2C 00 45 00 31 00 31 00 33 00 2E 00 39 00 32 00 31 00 37 00 31 26 26 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 23 23 00 69 15 9B 0D 0A"
            //    .ToHexBytes();

            //var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}