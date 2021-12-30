using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Globalization;
using System.Linq;
using System.Text;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.MessagePack
{
    /// <summary>
    /// 消息读取器
    /// </summary>
    public ref struct NbazhGpsMessagePackReader
    {
        /// <summary>
        /// 读取Buffer
        /// </summary>
        public ReadOnlySpan<byte> Reader { get; private set; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public ReadOnlySpan<byte> SrcBuffer { get; }

        /// <summary>
        /// 读取到的数量
        /// </summary>
        public int ReaderCount { get; private set; }

        /// <summary>
        /// 包类型
        /// </summary>
        public PackageType Type { get; set; }

        private bool _checkCrcVerify;

        //private bool _decoded { get; set; }
        private static readonly byte[] decode7878 = { 0x78, 0x78 };

        private static readonly byte[] decode7979 = { 0x79, 0x79 };

        /// <summary>
        /// </summary>
        /// <param name="srcBuffer"> </param>
        /// <param name="type">      </param>
        public NbazhGpsMessagePackReader(ReadOnlySpan<byte> srcBuffer, PackageType type)
        {
            SrcBuffer = srcBuffer;
            ReaderCount = 0;
            Type = type;
            Reader = srcBuffer;
            _checkCrcVerify = false;
        }

        /// <summary>
        /// 解码时算出校验和
        /// </summary>
        /// <param name="allocateBuffer"> </param>
        public void Decode(Span<byte> allocateBuffer)
        {
            ReadOnlySpan<byte> crcCode = SrcBuffer.Slice(SrcBuffer.Length - 4, 2);
            _checkCrcVerify = SrcBuffer.AuthCrc(crcCode.ToCrc());
        }

        /// <summary>
        /// </summary>
        public bool CheckCrcVerify => _checkCrcVerify;

        /// <summary>
        /// 读取标识头
        /// </summary>
        /// <returns> </returns>
        public ushort ReadStart() => ReadUInt16();

        /// <summary>
        /// 读取标识尾
        /// </summary>
        /// <returns> </returns>
        public ushort ReadEnd() => ReadUInt16();

        /// <summary>
        /// 读取有符号位的两字节数值类型
        /// </summary>
        /// <returns> </returns>
        private short ReadInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetReadOnlySpan(2));
        }

        /// <summary>
        /// 读取无符号位的两字节数值类型
        /// </summary>
        /// <returns> </returns>
        public ushort ReadUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetReadOnlySpan(2));
        }

        /// <summary>
        /// 读取有符号位的四字节数值类型
        /// </summary>
        /// <returns> </returns>
        public int ReadInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetReadOnlySpan(4));
        }

        /// <summary>
        /// 读取无符号位的四字节数值类型
        /// </summary>
        /// <returns> </returns>
        public uint ReadUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetReadOnlySpan(4));
        }

        /// <summary>
        /// 读取有符号位的八字节数值类型
        /// </summary>
        /// <returns> </returns>
        public long ReadInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetReadOnlySpan(8));
        }

        /// <summary>
        /// 读取无符号位的八字节数值类型
        /// </summary>
        /// <returns> </returns>
        public ulong ReadUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetReadOnlySpan(8));
        }

        /// <summary>
        /// 读取一个字节
        /// </summary>
        /// <returns> </returns>
        public byte ReadByte()
        {
            return GetReadOnlySpan(1)[0];
        }

        /// <summary>
        /// 读取一个字符
        /// </summary>
        /// <returns> </returns>
        public char ReadChar()
        {
            return (char)GetReadOnlySpan(1)[0];
        }

        /// <summary>
        /// 虚拟读取一个字节，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public byte ReadVirtualByte()
        {
            return GetVirtualReadOnlySpan(1)[0];
        }

        /// <summary>
        /// 虚拟读取一个数组，不计入内存偏移量
        /// </summary>
        /// <param name="count"> </param>
        /// <returns> </returns>
        public ReadOnlySpan<byte> ReadVirtualArray(int count)
        {
            return GetVirtualReadOnlySpan(count);
        }

        /// <summary>
        /// 虚拟读取无符号位的两字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public ushort ReadVirtualUInt16()
        {
            return BinaryPrimitives.ReadUInt16BigEndian(GetVirtualReadOnlySpan(2));
        }

        /// <summary>
        /// 虚拟读取有符号位的两字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public short ReadVirtualInt16()
        {
            return BinaryPrimitives.ReadInt16BigEndian(GetVirtualReadOnlySpan(2));
        }

        /// <summary>
        /// 虚拟读取无符号位的四字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public uint ReadVirtualUInt32()
        {
            return BinaryPrimitives.ReadUInt32BigEndian(GetVirtualReadOnlySpan(4));
        }

        /// <summary>
        /// 虚拟读取有符号位的四字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public int ReadVirtualInt32()
        {
            return BinaryPrimitives.ReadInt32BigEndian(GetVirtualReadOnlySpan(4));
        }

        /// <summary>
        /// 虚拟读取无符号位的八字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public ulong ReadVirtualUInt64()
        {
            return BinaryPrimitives.ReadUInt64BigEndian(GetVirtualReadOnlySpan(8));
        }

        /// <summary>
        /// 虚拟读取有符号位的八字节数值类型，不计入内存偏移量
        /// </summary>
        /// <returns> </returns>
        public long ReadVirtualInt64()
        {
            return BinaryPrimitives.ReadInt64BigEndian(GetVirtualReadOnlySpan(8));
        }

        /// <summary>
        /// 读取三字节
        /// </summary>
        /// <returns></returns>
        public int ReadByte3()
        {
            var spans = GetReadOnlySpan(3);
            return (spans[0] << 16) + (spans[1] << 8) + spans[2];
        }

        /// <summary>
        /// 读取三字节
        /// </summary>
        /// <returns></returns>
        public UInt24 ReadUInt24()
        {
            return new UInt24(GetReadOnlySpan(3));
        }

        /// <summary>
        /// 虚拟的获取三字节
        /// </summary>
        /// <returns></returns>
        public UInt24 GetVirtualUInt24()
        {
            return new UInt24(GetVirtualReadOnlySpan(3));
        }
        /// <summary>
        /// 虚拟的获取三字节
        /// </summary>
        /// <returns></returns>
        public int GetVirtualByte3()
        {
            var spans = GetVirtualReadOnlySpan(3);
            return (spans[0] << 16) + (spans[1] << 8) + spans[2];
        }

        /// <summary>
        /// 读取数量大小的内存块
        /// </summary>
        /// <param name="count"> </param>
        /// <returns> </returns>
        private ReadOnlySpan<byte> GetReadOnlySpan(int count)
        {
            ReaderCount += count;
            return Reader.Slice(ReaderCount - count);
        }

        /// <summary>
        /// 虚拟读取数量大小的内存块，不计入内存偏移量
        /// </summary>
        /// <param name="count"> </param>
        /// <returns> </returns>
        public ReadOnlySpan<byte> GetVirtualReadOnlySpan(int count)
        {
            return Reader.Slice(ReaderCount, count);
        }

        /// <summary>
        /// 读取数字编码 大端模式、高位在前
        /// </summary>
        /// <param name="len"> </param>
        public string ReadBigNumber(int len)
        {
            ulong result = 0;
            var readOnlySpan = GetReadOnlySpan(len);
            for (int i = 0; i < len; i++)
            {
                ulong currentData = (ulong)readOnlySpan[i] << (8 * (len - i - 1));
                result += currentData;
            }
            return result.ToString();
        }

        /// <summary>
        /// 读取固定大小的内存块
        /// </summary>
        /// <param name="len"> </param>
        /// <returns> </returns>
        public ReadOnlySpan<byte> ReadArray(int len)
        {
            return GetReadOnlySpan(len).Slice(0, len);
        }

        /// <summary>
        /// 读取固定大小的内存块
        /// </summary>
        /// <param name="start"> </param>
        /// <param name="end">   </param>
        /// <returns> </returns>
        public ReadOnlySpan<byte> ReadArray(int start, int end)
        {
            return Reader.Slice(start, end);
        }

        /// <summary>
        /// 读取ASCII编码
        /// </summary>
        /// <param name="len"> </param>
        /// <returns> </returns>
        public string ReadAscii(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string value = Encoding.ASCII.GetString(readOnlySpan.Slice(0, len).ToArray());
            return value;
        }

        /// <summary>
        /// 读取16进制编码
        /// </summary>
        /// <param name="len"> </param>
        /// <returns> </returns>
        public string ReadHex(int len)
        {
            var readOnlySpan = GetReadOnlySpan(len);
            string hex = HexUtil.DoHexDump(readOnlySpan, 0, len);
            return hex;
        }

        /// <summary>
        /// 读取BCD编码
        /// </summary>
        /// <param name="len">  </param>
        /// <param name="trim"> </param>
        /// <returns> </returns>
        public string ReadBcd(int len, bool trim = true)
        {
            int count = len / 2;
            var readOnlySpan = GetReadOnlySpan(count);
            StringBuilder bcdSb = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                bcdSb.Append(readOnlySpan[i].ToString("X2"));
            }
            if (trim)
            {
                return bcdSb.ToString().TrimStart('0');
            }
            else
            {
                return bcdSb.ToString();
            }
        }

        /// <summary>
        /// 读取六字节日期,yyMMddHHmmss
        /// </summary>
        public DateTime? ReadDateTime6()
        {
            //DateTime d;
            try
            {
                var readOnlySpan = GetReadOnlySpan(6);
                var formatter = new DateTimeFormatInfo();
                formatter.ShortDatePattern = "yy/MM/dd HH:mm:ss";
                return Convert.ToDateTime(
                    $"{readOnlySpan[0]}-{readOnlySpan[1]}-{readOnlySpan[2]} {readOnlySpan[3]}:{readOnlySpan[4]}:{readOnlySpan[5]}"
                    , formatter);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 16进制的BCD BYTE转成整型
        /// </summary>
        /// <param name="value">16进制的BCD BYTE转成整型</param>
        /// <returns></returns>
        public int BcdToInt(byte value)
        {
            int high = value >> 4;
            int low = value & 0xF;
            int number = 10 * high + low;
            return number;
        }

        /// <summary>
        /// 读取时区语言
        /// </summary>
        /// <returns> </returns>
        //public string ReadTimeZoneLanguage()
        //{
        //    var timeZoneLanguage = GetReadOnlySpan(2);

        // ushort lineData = (ushort)((timeZoneLanguage[0] << 8) + timeZoneLanguage[1]);

        // ushort time = (ushort)((lineData << 4 >> 4) / 100); var timeZone = ((lineData >> 12) &
        // 00000001) == 0 ? true: false;

        //    return $"{(timeZone?"东":"西")}{time}区,GMT{(timeZone?"+":"-")}{time.FormatTimeZoneLanguageTime()}";
        //}

        public TimeZoneLanguageModel ReadTimeZoneLanguage()
        {
            var timeZoneLanguage = GetReadOnlySpan(2);
            return new TimeZoneLanguageModel().Deserialize(timeZoneLanguage);
        }

        /// <summary>
        /// 读取数据体内存块
        /// </summary>
        /// <param name="count"> </param>
        /// <returns> </returns>
        public ReadOnlySpan<byte> ReadContent()
        {
            return Reader.Slice(ReaderCount);
        }

        /// <summary>
        /// 读取数据体内存块
        /// </summary>
        /// <param name="count"> </param>
        /// <returns> </returns>
        public ReadOnlySpan<byte> ReadContent(int count)
        {
            return Reader.Slice(ReaderCount, count-ReaderCount);
        }

        /// <summary>
        /// 读取剩余数据体内容长度
        /// </summary>
        /// <returns> </returns>
        public int ReadCurrentRemainContentLength()
        {
            return Reader.Length - ReaderCount;
        }

        /// <summary>
        /// 跳过多少字节
        /// </summary>
        /// <param name="count"> </param>
        public void Skip(int count = 1)
        {
            ReaderCount += count;
        }
    }
}