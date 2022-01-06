using System;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// 头部
    /// </summary>
    public class NbazhGpsHeader: INbazhGpsHeader
    {
        /// <summary>
        /// 默认 type1
        /// </summary>
        public NbazhGpsHeader()
        {
            PackageType = PackageType.Type1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public NbazhGpsHeader(PackageType type)
        {
            PackageType = type;
        }

        /// <summary>
        /// 包长度
        /// </summary>
        public ushort Length
        {
            get;
            set;
        }
        /// <summary>
        /// 消息Id
        /// </summary>
        public byte MsgId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public NbazhGpsBodies Bodies { get; set; }
        /// <summary>
        /// 消息流水号
        /// </summary>
        public ushort MsgNum { get; set; }
        /// <summary>
        /// 错误校验
        /// </summary>
        public ushort Crc { get; set; }
        

        /// <summary>
        /// 协议类型
        /// </summary>
        public PackageType PackageType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, NbazhGpsHeader value)
        {
            //05 01 0087 0000
            var writeReturnPosition = 0;
            // 写入header
            // 跳过Length
            if (value.PackageType == PackageType.Type1)
            {
                writer.Skip(1, out var pos);
                writeReturnPosition = pos;
            }
            else
            {
                writer.Skip(2, out var pos);
                writeReturnPosition = pos;
            }
            // 获取当前数据块的位置
            int headerLength = writer.GetCurrentPosition();
            // 写入消息Id
            writer.WriteByte(value.MsgId);
            // 写入消息体
            if (value.Bodies != null)
            {
                if (!value.Bodies.SkipSerialization)
                {
                    NbazhGpsMessagePackFormatterResolverExtensions.NbazhDynamicSerialize(value.Bodies,
                        ref writer, value.Bodies);
                }
            }
            writer.WriteUInt16(value.MsgNum);
            writer.WriteCrcForHeader();
            // 处理数据体长度, 回写Length
            value.Length = (ushort)(writer.GetCurrentPosition() - headerLength);
            if (value.PackageType == PackageType.Type1)
            {
                writer.WriteByteReturn((byte)value.Length, writeReturnPosition);
            }
            else
            {
                writer.WriteUInt16Return(value.Length, writeReturnPosition);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public NbazhGpsHeader Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            NbazhGpsHeader header = new NbazhGpsHeader(reader.Type);
            header.Length = header.PackageType switch
            {
                PackageType.Type1 => reader.ReadByte(),
                PackageType.Type2 => reader.ReadUInt16(),
                _ => reader.ReadByte()
            };
            header.MsgId = reader.ReadByte();
            // 跳过信息内容
            reader.Skip(header.Length - 5);
            header.MsgNum = reader.ReadUInt16();
            header.Crc = reader.ReadUInt16();
            return header;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        public void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}