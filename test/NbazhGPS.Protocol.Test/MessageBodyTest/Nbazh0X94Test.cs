using NbazhGPS.Protocol.Extensions;
using Xunit;

namespace NbazhGPS.Protocol.Test.MessageBodyTest
{
    public class Nbazh0X94Test
    {
        private NbazhGpsSerializer NbazhGpsSerializer = new NbazhGpsSerializer();
        [Fact]
        public void Test1()
        {
            var hex = "79 79 00 7F 94 04 41 4C 4D 31 3D 43 34 3B 41 4C 4D 32 3D 43 43 3B 41 4C 4D 33 3D 3443 3B 53 54 41 31 3D 43 30 3B 44 59 44 3D 30 31 3B 53 4F 53 3D 2C 2C 3B 43 45 4E 54 45 52 3D 3B 46 45 4E 43 45 3D 46 65 6E 63 65 2C 4F 4E 2C 30 2C 32 33 2E 31 31 31 38 30 39 2C 31 31 34 2E 34 30 39 32 36 34 2C 34 30 30 2C 49 4E 20 6F 72 20 4F 55 54 2C 30 3B 4D 49 46 49 3D 4D 49 46 49 2C 4F 46 46 00 0A 06 1E 0D 0A"
                .ToHexBytes();

            var packet = NbazhGpsSerializer.Deserialize(hex);
        }
    }
}