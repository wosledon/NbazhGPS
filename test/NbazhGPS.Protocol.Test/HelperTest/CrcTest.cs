using System;
using System.Xml;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Helpers;
using Xunit;

namespace NbazhGPS.Protocol.Test.HelperTest
{
    public class CrcTest
    {
        [Fact]
        public void Test1()
        {
            ReadOnlySpan<byte> hex = "78 78 11 01 07 52 53 36 78 90 02 42 70 00 32 01 00 05 12 79 0D 0A".ToHexBytes();
            var sourceCrc = hex.Slice(hex.Length - 4, 2).ToCrc();
            var dsCrc = CrcHelper.GetCrc16(hex);
            Assert.True(hex.AuthCrc(sourceCrc));
        }
    }
}