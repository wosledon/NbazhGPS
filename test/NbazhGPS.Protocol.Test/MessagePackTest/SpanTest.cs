using System;
using System.Text;
using NbazhGPS.Protocol.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.MessagePackTest
{
    public class SpanTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public SpanTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var bytes = Encoding.ASCII.GetBytes("ASCII").AsSpan();

            var res = new byte[21];
            bytes.CopyTo(res);

            _testOutputHelper.WriteLine(res.ToHexString());
        }
    }
}