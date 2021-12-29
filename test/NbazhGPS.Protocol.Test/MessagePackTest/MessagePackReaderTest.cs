using System;
using System.Linq;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.MessagePack;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessagePackTest
{
    public class MessagePackReaderTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MessagePackReaderTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var hex = "0320";
            NbazhGpsMessagePackReader reader = new NbazhGpsMessagePackReader(hex.ToHexBytes(), PackageType.Type1);

            _testOutputHelper.WriteLine(reader.ReadTimeZoneLanguage().ToString());
        }

        [Fact]
        public void Test2()
        {
            ushort time = (8 * 100) << 4;

            _testOutputHelper.WriteLine(time.ToString("x8"));
        }

        [Fact]
        public void Test3()
        {
            ushort time = 8;
            _testOutputHelper.WriteLine(time.FormatTimeZoneLanguageTime());

            time = 1245;
            _testOutputHelper.WriteLine(time.FormatTimeZoneLanguageTime());

            time = 845;
            _testOutputHelper.WriteLine(time.FormatTimeZoneLanguageTime());
        }
    }
}