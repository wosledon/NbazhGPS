using System;
using System.Reflection.PortableExecutable;
using NbazhGPS.Protocol.BasicTypes;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test.BaseTypes
{
    public class UInt24Test
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UInt24Test(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            UInt24 value = new UInt24(new byte[]{1, 2, 3});


            _testOutputHelper.WriteLine(value.Value.ToString());
        }
    }
}