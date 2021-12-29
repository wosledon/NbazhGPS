using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Exceptions;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// 
    /// </summary>
    public record NbazhGpsHeaderPackage
    {
        /// <summary>
        /// 起始符
        /// </summary>
        public ushort Begin { get; set; }
        /// <summary>
        /// 头数据
        /// </summary>
        
        public NbazhGpsHeader Header { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] Bodies { get; set; }
        /// <summary>
        /// Crc
        /// </summary>
        public ushort Crc { get; set; }
        /// <summary>
        /// 结束符
        /// </summary>
        public ushort End { get; set; }
        /// <summary>
        /// 包类型
        /// </summary>
        public PackageType PackageType { get; set; }
        /// <summary>
        /// 原数据
        /// </summary>
        public byte[] OriginalData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public NbazhGpsHeaderPackage(ref NbazhGpsMessagePackReader reader)
        {
            if (!reader.CheckCrcVerify)
            {
                throw new NbazhGpsException(NbazhGpsErrorCode.CrcVerifyFail, @"Crc校验失败");
            }

            

            Begin = reader.ReadStart();
            if (unchecked((ushort)((0x79 << 8) + 0x79)) == Begin)
            {
                reader.Type = PackageType.Type2;
            }
            PackageType = reader.Type;
            Header = new NbazhGpsHeader(PackageType);
            Header.PackageType = PackageType;
            Header.Length = PackageType switch
            {
                PackageType.Type1 => reader.ReadByte(),
                PackageType.Type2 => reader.ReadUInt16(),
                _ => throw new ArgumentOutOfRangeException()
            };
            Header.MsgId = reader.ReadByte();
            if (Header.Length > 0)
            {
                Bodies = reader.ReadContent(reader.SrcBuffer.Length - 6).ToArray();
            }
            else
            {
                Bodies = default;
            }
            reader.Skip(reader.SrcBuffer.Length - 6 - reader.ReaderCount);
            Header.MsgNum = reader.ReadUInt16();
            Header.Crc = reader.ReadUInt16();
            Crc = Header.Crc;
            End = reader.ReadEnd();

            OriginalData = reader.SrcBuffer.ToArray();
        }
    }
}