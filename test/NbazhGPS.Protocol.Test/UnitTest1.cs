using System;
using NbazhGPS.Protocol.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace NbazhGPS.Protocol.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var data = WriteDateTime6(DateTime.UtcNow);
            _testOutputHelper.WriteLine($"{data[0]}:{data[1]}:{data[2]}:{data[3]}:{data[4]}:{data[5]}");
        }

        public byte[] WriteDateTime6(in DateTime? value)
        {
            var span = new byte[6];
            if (value == null)
            {
                span[0] = 0;
                span[1] = 0;
                span[2] = 0;
                span[3] = 0;
                span[4] = 0;
                span[5] = 0;
            }
            else
            {
                byte yy = Convert.ToByte(value.Value.Year.ToString().Substring(2, 2));
                span[0] = yy;
                span[1] = value.Value.Month.ToByteValue();
                span[2] = value.Value.Day.ToByteValue();
                span[3] = value.Value.Hour.ToByteValue();
                span[4] = value.Value.Minute.ToByteValue();
                span[5] = value.Value.Second.ToByteValue();
            }

            return span;
        }
    }
}