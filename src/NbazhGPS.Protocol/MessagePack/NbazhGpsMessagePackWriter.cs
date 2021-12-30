using System;
using System.Buffers.Binary;
using System.Text;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Buffers;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Helpers;

namespace NbazhGPS.Protocol.MessagePack
{
    /// <summary>
    /// 消息写入器
    /// </summary>
    public ref struct NbazhGpsMessagePackWriter
    {
        private NbazhGpsBufferWriter _writer;
        /// <summary>
        /// 数据包类型
        /// </summary>
        public PackageType Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer">内存块</param>
        /// <param name="type">数据包类型, 默认类型1</param>
        public NbazhGpsMessagePackWriter(Span<byte> buffer, PackageType type = PackageType.Type1)
        {
            _writer = new NbazhGpsBufferWriter(buffer);
            Type = type;
        }
        /// <summary>
        /// 设置包类型
        /// </summary>
        /// <param name="type"></param>
        public void SetPackageType(PackageType type)
        {
            Type = type;
        }

        /// <summary>
        /// 编码后的数组
        /// </summary>
        /// <returns></returns>
        public byte[] FlushAndGetEncodingArray()
        {
            return _writer.Written.Slice(_writer.BeforeCodingWrittenPosition).ToArray();
        }
        /// <summary>
        /// 编码后的内存块
        /// </summary>
        /// <returns></returns>
        public ReadOnlySpan<byte> FlushAndGetEncodingReadOnlySpan()
        {
            return _writer.Written.Slice(_writer.BeforeCodingWrittenPosition);
        }
        /// <summary>
        /// 获取实际写入的内存块
        /// </summary>
        /// <returns></returns>
        public ReadOnlySpan<byte> FlushAndGetRealReadOnlySpan()
        {
            return _writer.Written;
        }
        /// <summary>
        /// 获取实际写入的数组
        /// </summary>
        /// <returns></returns>
        public byte[] FlushAndGetRealArray()
        {
            return _writer.Written.ToArray();
        }
        /// <summary>
        /// 写入头标识
        /// </summary>
        public void WriteStart() => WriteUInt16(NbazhGpsPackage.GetBeginFlag(Type));
        /// <summary>
        /// 写入头标识
        /// </summary>
        public void WriteStart(PackageType type) => WriteUInt16(NbazhGpsPackage.GetBeginFlag(type));
        /// <summary>
        /// 写入尾标识
        /// </summary>
        public void WriteEnd() => WriteUInt16(NbazhGpsPackage.EndFlag);
        /// <summary>
        /// 写入空标识,0x00
        /// </summary>
        /// <param name="position"></param>
        public void Nil(out int position)
        {
            position = _writer.WrittenCount;
            var span = _writer.Free;
            span[0] = 0x00;
            _writer.Advance(1);
        }
        /// <summary>
        /// 跳过多少字节数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="position">跳过前的内存位置</param>
        public void Skip(in int count, out int position)
        {
            position = _writer.WrittenCount;
            var span = _writer.Free;
            for (var i = 0; i < count; i++)
            {
                span[i] = 0x00;
            }
            _writer.Advance(count);
        }
        /// <summary>
        /// 跳过多少字节数
        /// </summary>
        /// <param name="count"></param>
        /// <param name="position">跳过前的内存位置</param>
        /// <param name="fullValue">用什么数值填充跳过的内存块</param>
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public void Skip(in int count, out int position, in byte fullValue = 0x00)
        {
            position = _writer.WrittenCount;
            var span = _writer.Free;
            for (var i = 0; i < count; i++)
            {
                span[i] = fullValue;
            }
            _writer.Advance(count);
        }
        /// <summary>
        /// 写入一个字符
        /// </summary>
        /// <param name="value"></param>
        public void WriteChar(in char value)
        {
            var span = _writer.Free;
            span[0] = (byte)value;
            _writer.Advance(1);
        }
        /// <summary>
        /// 写入一个字节
        /// </summary>
        /// <param name="value"></param>
        public void WriteByte(in byte value)
        {
            var span = _writer.Free;
            span[0] = value;
            _writer.Advance(1);
        }
        /// <summary>
        /// 写入两个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt16(in short value)
        {
            BinaryPrimitives.WriteInt16BigEndian(_writer.Free, value);
            _writer.Advance(2);
        }
        /// <summary>
        /// 写入两个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt16(in ushort value)
        {
            BinaryPrimitives.WriteUInt16BigEndian(_writer.Free, value);
            _writer.Advance(2);
        }
        /// <summary>
        /// 写入四个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt32(in int value)
        {
            BinaryPrimitives.WriteInt32BigEndian(_writer.Free, value);
            _writer.Advance(4);
        }
        /// <summary>
        /// 写入四个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt32(in uint value)
        {
            BinaryPrimitives.WriteUInt32BigEndian(_writer.Free, value);
            _writer.Advance(4);
        }
        /// <summary>
        /// 写入八个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt64(in ulong value)
        {
            BinaryPrimitives.WriteUInt64BigEndian(_writer.Free, value);
            _writer.Advance(8);
        }
        /// <summary>
        /// 写入八个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt64(in long value)
        {
            BinaryPrimitives.WriteInt64BigEndian(_writer.Free, value);
            _writer.Advance(8);
        }
        /// <summary>
        /// 写入三个字节
        /// </summary>
        /// <param name="value"></param>
        public void WriteByte3(in int value)
        {
            var bytes = new byte[] { (byte)(value >> 16), (byte)(value >> 8), (byte)(value) };
            WriteArray(bytes);
        }
        /// <summary>
        /// 写入UInt24
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt24(in UInt24 value)
        {
            WriteArray(value.ToBytes);
        }
        /// <summary>
        /// 写入数组
        /// </summary>
        /// <param name="src"></param>
        public void WriteArray(in ReadOnlySpan<byte> src)
        {
            src.CopyTo(_writer.Free);
            _writer.Advance(src.Length);
        }
        /// <summary>
        /// 根据内存定位,反写两个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteUInt16Return(in ushort value, in int position)
        {
            BinaryPrimitives.WriteUInt16BigEndian(_writer.Written.Slice(position, 2), value);
        }
        /// <summary>
        /// 根据内存定位,反写两个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteInt16Return(in short value, in int position)
        {
            BinaryPrimitives.WriteInt16BigEndian(_writer.Written.Slice(position, 2), value);
        }
        /// <summary>
        /// 根据内存定位,反写四个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteInt32Return(in int value, in int position)
        {
            BinaryPrimitives.WriteInt32BigEndian(_writer.Written.Slice(position, 4), value);
        }
        /// <summary>
        /// 根据内存定位,反写四个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteUInt32Return(in uint value, in int position)
        {
            BinaryPrimitives.WriteUInt32BigEndian(_writer.Written.Slice(position, 4), value);
        }

        /// <summary>
        /// 根据内存定位,反写八个字节的有符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteInt64Return(in long value, in int position)
        {
            BinaryPrimitives.WriteInt64BigEndian(_writer.Written.Slice(position, 8), value);
        }
        /// <summary>
        /// 根据内存定位,反写八个字节的无符号数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteUInt64Return(in ulong value, in int position)
        {
            BinaryPrimitives.WriteUInt64BigEndian(_writer.Written.Slice(position, 8), value);
        }
        /// <summary>
        /// 根据内存定位,反写1个字节的数值类型
        /// </summary>
        /// <param name="value"></param>
        /// <param name="position"></param>
        public void WriteByteReturn(in byte value, in int position)
        {
            _writer.Written[position] = value;
        }
        /// <summary>
        /// 根据内存定位,反写BCD编码数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        /// <param name="position"></param>
        public void WriteBcdReturn(in string value, in int len, in int position)
        {
            string bcdText = value ?? "";
            int startIndex = 0;
            int noOfZero = len - bcdText.Length;
            if (noOfZero > 0)
            {
                bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            int count = len / 2;
            var bcdSpan = bcdText.AsSpan();
            while (startIndex < bcdText.Length && byteIndex < count)
            {
                _writer.Written[position + (byteIndex++)] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
        }

        /// <summary>
        /// 根据内存定位,反写一组数组数据
        /// </summary>
        /// <param name="src"></param>
        /// <param name="position"></param>
        public void WriteArrayReturn(in ReadOnlySpan<byte> src, in int position)
        {
            src.CopyTo(_writer.Written.Slice(position));
        }

        /// <summary>
        /// 整型数据转为BCD BYTE
        /// 为了兼容int类型，不使用byte做参数
        /// 支持0xFF一个字节的转换
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public byte IntToBcd(int value)
        {
            byte result = 0;
            if (value <= 0xFF)
            {
                //19 00010011
                //0001 1001
                int high = value / 10;
                int low = value % 10;
                result = (byte)(high << 4 | low);
            }
            return result;
        }

        /// <summary>
        /// 整型数据转为BCD BYTE[]
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="list">bytes</param>
        /// <param name="count">字节数>=整数值</param>
        public void IntToBcd(int value, Span<byte> list, int count)
        {
            int level = count - 1;
            var high = value / 100;
            var low = value % 100;
            if (high > 0)
            {
                IntToBcd(high, list, --count);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
        }

        /// <summary>
        /// 整型数据转为BCD BYTE[]
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="list">bytes</param>
        /// <param name="byteCount">字节数>=整数值</param>
        public void IntToBcd(long value, Span<byte> list, int byteCount)
        {
            int level = byteCount - 1;
            if (level < 0) return;
            var high = value / 100;
            var low = value % 100;
            if (high > 0)
            {
                IntToBcd(high, list, --byteCount);
            }
            byte res = (byte)(((low / 10) << 4) + (low % 10));
            list[level] = res;
        }

        /// <summary>
        /// 写入BCD编码数据
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="byteCount">字节数>=整数值</param>
        public void WriteBcd(in long value, in int byteCount)
        {
            var span = _writer.Free;
            IntToBcd(value, _writer.Free, byteCount);
            _writer.Advance(byteCount);
        }

        /// <summary>
        /// 写入BCD编码数据
        /// </summary>
        /// <param name="value">整数值</param>
        /// <param name="count">字节数>=整数值</param>
        public void WriteBcd(in int value, in int count)
        {
            var span = _writer.Free;
            IntToBcd(value, _writer.Free, count);
            _writer.Advance(count);
        }
        /// <summary>
        /// 将指定内存块进行或运算并写入一个字节
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void WriteXor(in int start, in int end)
        {
            if (start > end)
            {
                throw new ArgumentOutOfRangeException($"start>end:{start}>{end}");
            }
            var xorSpan = _writer.Written.Slice(start, end);
            byte result = xorSpan[0];
            for (int i = start + 1; i < end; i++)
            {
                result = (byte)(result ^ xorSpan[i]);
            }
            var span = _writer.Free;
            span[0] = result;
            _writer.Advance(1);
        }
        /// <summary>
        /// 将指定内存块进行或运算并写入一个字节
        /// </summary>
        /// <param name="start"></param>
        public void WriteXor(in int start)
        {
            if (_writer.WrittenCount < start)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{_writer.WrittenCount}>{start}");
            }
            var xorSpan = _writer.Written.Slice(start);
            byte result = xorSpan[0];
            for (int i = start + 1; i < xorSpan.Length; i++)
            {
                result = (byte)(result ^ xorSpan[i]);
            }
            var span = _writer.Free;
            span[0] = result;
            _writer.Advance(1);
        }
        /// <summary>
        /// 将内存块进行或运算并写入一个字节
        /// </summary>
        public void WriteXor()
        {
            if (_writer.WrittenCount < 1)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{_writer.WrittenCount}>{1}");
            }
            //从第1位开始
            var xorSpan = _writer.Written.Slice(1);
            byte result = xorSpan[0];
            for (int i = 1; i < xorSpan.Length; i++)
            {
                result = (byte)(result ^ xorSpan[i]);
            }
            var span = _writer.Free;
            span[0] = result;
            _writer.Advance(1);
        }
        /// <summary>
        /// 写入Crc
        /// </summary>
        public void WriteCrcForHeader()
        {
            if (_writer.WrittenCount < 1)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{_writer.WrittenCount}>{1}");
            }

            var xorSpan = _writer.Written.Slice(1);
            var crc = CrcHelper.GetCrc16ForHeader(xorSpan);
            var span = _writer.Free;
            span[0] = (byte)(crc >> 8);
            span[1] = (byte)crc;
            _writer.Advance(2);
        }
        /// <summary>
        /// 写入Crc
        /// </summary>
        public void WriteCrcForPackage()
        {
            if (_writer.WrittenCount < 1)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{_writer.WrittenCount}>{1}");
            }

            var xorSpan = _writer.Written.Slice(0);
            var crc = CrcHelper.GetCrc16ForPackage(xorSpan);
            var span = _writer.Free;
            span[0] = (byte)(crc >> 8);
            span[1] = (byte)crc;
            _writer.Advance(2);
        }
        /// <summary>
        /// 写入Crc
        /// </summary>
        public void WriteCrc()
        {
            if (_writer.WrittenCount < 1)
            {
                throw new ArgumentOutOfRangeException($"Written<start:{_writer.WrittenCount}>{1}");
            }

            var xorSpan = _writer.Written.Slice(1);
            var crc = CrcHelper.GetCrc16(xorSpan);
            _writer.Advance(2);

        }
        /// <summary>
        /// 写入BCD编码数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteBcd(in string value, in int len)
        {
            string bcdText = value ?? "";
            int startIndex = 0;
            int noOfZero = len - bcdText.Length;
            if (noOfZero > 0)
            {
                bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            int count = len / 2;
            var bcdSpan = bcdText.AsSpan();
            var spanFree = _writer.Free;
            while (startIndex < bcdText.Length && byteIndex < count)
            {
                spanFree[byteIndex++] = Convert.ToByte(bcdSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
            _writer.Advance(byteIndex);
        }
        /// <summary>
        /// 写入Hex编码数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteHex(string value, in int len)
        {
            value ??= "";
            value = value.Replace(" ", "");
            int startIndex = 0;
            if (value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                startIndex = 2;
            }
            int length = len;
            if (length == -1)
            {
                length = (value.Length - startIndex) / 2;
            }
            int noOfZero = length * 2 + startIndex - value.Length;
            if (noOfZero > 0)
            {
                value = value.Insert(startIndex, new string('0', noOfZero));
            }
            int byteIndex = 0;
            var hexSpan = value.AsSpan();
            var spanFree = _writer.Free;
            while (startIndex < value.Length && byteIndex < length)
            {
                spanFree[byteIndex++] = Convert.ToByte(hexSpan.Slice(startIndex, 2).ToString(), 16);
                startIndex += 2;
            }
            _writer.Advance(byteIndex);
        }
        /// <summary>
        /// 写入ASCII编码数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteAscii(in string value)
        {
            var spanFree = _writer.Free;
            var bytes = Encoding.ASCII.GetBytes(value).AsSpan();
            bytes.CopyTo(spanFree);
            _writer.Advance(bytes.Length);
        }
        /// <summary>
        /// 写入数值类型，数字编码 大端模式、高位在前
        /// </summary>
        /// <param name="value"></param>
        /// <param name="len"></param>
        public void WriteBigNumber(in string value, int len)
        {
            var spanFree = _writer.Free;
            ulong number = string.IsNullOrEmpty(value) ? 0 : (ulong)double.Parse(value);
            for (int i = len - 1; i >= 0; i--)
            {
                spanFree[i] = (byte)(number & 0xFF);  //取低8位
                number = number >> 8;
            }
            _writer.Advance(len);
        }
        /// <summary>
        /// 写入六个字节的可空日期类型,yyMMddHHmmss
        /// </summary>
        /// <param name="value"></param>
        public void WriteDateTime6(in DateTime? value)
        {
            var span = _writer.Free;
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
            _writer.Advance(6);
        }
        /// <summary>
        /// 获取当前内存块写入的位置
        /// </summary>
        /// <returns></returns>
        public int GetCurrentPosition()
        {
            return _writer.WrittenCount;
        }
        /// <summary>
        /// 编码
        /// </summary>
        internal void WriteEncode()
        {
            var tmpSpan = _writer.Written;
            _writer.BeforeCodingWrittenPosition = _writer.WrittenCount;
            var spanFree = _writer.Free;
            int tempOffset = 0;
            spanFree[tempOffset++] = tmpSpan[0];
            for (int i = 1; i < tmpSpan.Length - 1; i++)
            {
                spanFree[tempOffset++] = tmpSpan[i];
            }
            spanFree[tempOffset++] = tmpSpan[tmpSpan.Length - 1];
            _writer.Advance(tempOffset);
        }
    }
}