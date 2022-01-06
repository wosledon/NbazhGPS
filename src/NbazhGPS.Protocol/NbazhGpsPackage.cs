using System;
using System.Data;
using System.Dynamic;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Exceptions;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.Internals;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// Nbazh数据包
    /// </summary>
    public class NbazhGpsPackage : INbazhGpsPackage
    {
        /// <summary>
        /// </summary>
        public NbazhGpsPackage()
        {
            // ReSharper disable once ShiftExpressionRealShiftCountIsZero
            Begin = unchecked((ushort)((0x78 << 8) + 0x78));
        }

        /// <summary>
        /// </summary>
        /// <param name="type"> 协议类型 </param>
        public NbazhGpsPackage(PackageType type)
        {
            PackageType = type;
            Begin = type switch
            {
                // ReSharper disable once ShiftExpressionRealShiftCountIsZero
                PackageType.Type1 => unchecked((ushort)((0x78 << 8) + 0x78)),
                // ReSharper disable once ShiftExpressionRightOperandNotEqualRealCount
                PackageType.Type2 => unchecked((ushort)((0x79 << 8) + 0x79)),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        /// <summary>
        /// 结束标记
        /// </summary>
        public const ushort EndFlag = unchecked((ushort)((0x0D << 8) + 0x0A));

        /// <summary>
        /// 起始位
        /// </summary>
        public ushort Begin { get; set; }

        /// <summary>
        /// 头数据
        /// </summary>
        public NbazhGpsHeader Header { get; set; }

        /// <summary>
        /// 数据体
        /// </summary>
        public NbazhGpsBodies Bodies { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public ushort End { get; set; } = EndFlag;

        /// <summary>
        /// 获取起始位
        /// </summary>
        /// <param name="type"> </param>
        /// <returns> </returns>
        public static ushort GetBeginFlag(PackageType type)
        {
            return type switch
            {
                // ReSharper disable once ShiftExpressionRealShiftCountIsZero
                PackageType.Type1 => unchecked((ushort)((0x78 << 8) + 0x78)),
                // ReSharper disable once ShiftExpressionRightOperandNotEqualRealCount
                PackageType.Type2 => unchecked((ushort)((0x79 << 8) + 0x79)),
                // ReSharper disable once ShiftExpressionRealShiftCountIsZero
                _ => unchecked((ushort)((0x78 << 8) + 0x78)),
            };
        }

        /// <summary>
        /// 设置头尾
        /// </summary>
        /// <param name="type"> </param>
        public void SetBeginFlag(PackageType type)
        {
            Begin = type switch
            {
                // ReSharper disable once ShiftExpressionRealShiftCountIsZero
                PackageType.Type1 => unchecked((ushort)((0x78 << 8) + 0x78)),
                // ReSharper disable once ShiftExpressionRightOperandNotEqualRealCount
                PackageType.Type2 => unchecked((ushort)((0x79 << 8) + 0x79)),
                // ReSharper disable once ShiftExpressionRealShiftCountIsZero
                _ => unchecked((ushort)(0x78 << 8 + 0x78)),
            };
        }

        /// <summary>
        /// 协议类型
        /// </summary>
        public PackageType PackageType { get; set; } = PackageType.Type1;

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, NbazhGpsPackage value)
        {
            var writeReturnPosition = 0;
            writer.WriteStart(value.PackageType);
            // 写入header 跳过Length
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
            writer.WriteByte(value.Header.MsgId);
            // 写入消息体
            if (value.Bodies != null)
            {
                if (!value.Bodies.SkipSerialization)
                {
                    NbazhGpsMessagePackFormatterResolverExtensions.NbazhDynamicSerialize(value.Bodies,
                        ref writer, value.Bodies);
                }
            }
            writer.WriteUInt16(value.Header.MsgNum);
            //writer.WriteUInt16(value.Header.Crc);
            // 处理数据体长度, 回写Length, 加上Crc的长度
            value.Header.Length = (ushort)(writer.GetCurrentPosition() - headerLength + 2);
            if (value.PackageType == PackageType.Type1)
            {
                writer.WriteByteReturn((byte)value.Header.Length, writeReturnPosition);
            }
            else
            {
                writer.WriteUInt16Return(value.Header.Length, writeReturnPosition);
            }
            writer.WriteCrcForPackage();
            writer.WriteEnd();
            writer.WriteEncode();
        }

        /// <summary>
        /// </summary>
        /// <param name="reader">         </param>
        /// <param name="isNeedStartEnd"> 是否需要解码收尾标识 </param>
        /// <returns> </returns>
        public NbazhGpsPackage Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd)
        {
            INbazhGpsMsgIdFactory factory = new NbazhGpsMsgIdFactory();

            if (!reader.CheckCrcVerify)
            {
                throw new NbazhGpsException(NbazhGpsErrorCode.CrcVerifyFail, @"Crc校验失败.");
            }

            NbazhGpsPackage packet = new NbazhGpsPackage(reader.Type);
            if(isNeedStartEnd) packet.Begin = reader.ReadStart();
            if (unchecked((ushort)((0x79 << 8) + 0x79)) == packet.Begin)
            {
                packet.PackageType = PackageType.Type2;
            }
            packet.Header = new NbazhGpsHeader(reader.Type);
            packet.Header.Length = packet.PackageType switch
            {
                PackageType.Type1 => reader.ReadByte(),
                PackageType.Type2 => reader.ReadUInt16(),
                _ => throw new ArgumentOutOfRangeException()
            };
            packet.Header.MsgId = reader.ReadByte();
            packet.PackageType = reader.Type;
            if (packet.Header.Length - 5 > 0)
            {
                if (factory.TryGetValue(packet.Header.MsgId, out var instance))
                {
                    try
                    {
                        packet.Bodies = NbazhGpsMessagePackFormatterResolverExtensions.NbazhDynamicDeserialize(
                            instance, ref reader, isNeedStartEnd);
                    }
                    catch (Exception ex)
                    {
                        throw new NbazhGpsException(NbazhGpsErrorCode.BodiesParseError, ex);
                    }
                }
            }

            packet.Header.MsgNum = reader.ReadUInt16();
            packet.Header.Crc = reader.ReadUInt16();
            if (isNeedStartEnd)
            {
                packet.End = reader.ReadEnd();
            }

            return packet;
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <param name="writer"> </param>
        public void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}