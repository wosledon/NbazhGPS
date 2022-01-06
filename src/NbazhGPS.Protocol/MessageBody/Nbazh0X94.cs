#nullable enable

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;
using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 信息传输通用包
    /// </summary>
    public class Nbazh0X94 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X94>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x94;

        /// <summary>
        /// </summary>
        public override string Description => "信息传输通用包";

        /// <summary>
        /// 信息类型
        /// </summary>
        public InfoType InfoType { get; set; }

        /// <summary>
        /// 类型为00时
        /// </summary>
        public DataContent0X94Models.DataContent00? DataContent00 { get; set; }

        /// <summary>
        /// 类型为04时
        /// </summary>
        public DataContent0X94Models.DataContent04? DataContent04 { get; set; }

        /// <summary>
        /// 类型为05时
        /// </summary>
        public DataContent0X94Models.DataContent05? DataContent05 { get; set; }

        /// <summary>
        /// </summary>
        public DataContent0X94Models.DataContent09? DataContent09 { get; set; }

        /// <summary>
        /// </summary>
        public DataContent0X94Models.DataContent0A? DataContent0A { get; set; }

        public DataContent0X94Models.DataContent10? DataContent10 { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X94 value)
        {
            writer.WriteByte(value.InfoType.ToByteValue());
            switch (value.InfoType)
            {
                case InfoType.外电电压:
                    if (value.DataContent00 != null) writer.WriteUInt16((ushort)(value.DataContent00.Voltage * 100));
                    break;

                case InfoType.终端状态同步:
                    writer.WriteAscii(value.DataContent04?.SourceData);
                    break;

                case InfoType.边门状态:
                    writer.WriteByte(value.DataContent05.ToByte());
                    break;

                case InfoType.自检参数:
                    break;

                case InfoType.定位卫星信息:
                    if (value.DataContent09 != null) writer.WriteArray(value.DataContent09.Gps.ToSpan());
                    if (value.DataContent09 != null) writer.WriteArray(value.DataContent09.BeiDou.ToSpan());
                    writer.WriteByte(value.DataContent09!.ExtensionLength);
                    if (value.DataContent09.ExtensionBytes != null)
                        writer.WriteArray(value.DataContent09.ExtensionBytes);
                    break;

                case InfoType.ICCID信息:
                    writer.WriteBcd(value.DataContent0A!.IMEI, 16);
                    writer.WriteBcd(value.DataContent0A.IMSI, 16);
                    writer.WriteBcd(value.DataContent0A.ICCID, 20);
                    break;

                case InfoType.巴西计价器:
                    if (value.DataContent10 != null) writer.WriteAscii(value.DataContent10.BrazilianMeter);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X94 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            Nbazh0X94 nb0X94 = new Nbazh0X94()
            {
                InfoType = (InfoType)reader.ReadByte()
            };

            switch (nb0X94.InfoType)
            {
                case InfoType.外电电压:
                    nb0X94.DataContent00 = new DataContent0X94Models.DataContent00()
                    {
                        Voltage = reader.ReadUInt16() / 100f
                    };
                    break;

                case InfoType.终端状态同步:
                    nb0X94.DataContent04 = new DataContent0X94Models.DataContent04()
                    {
                        SourceData = reader.ReadAscii(reader.SrcBuffer.Length - 12),
                    };
                    break;

                case InfoType.边门状态:
                    nb0X94.DataContent05 = reader.ReadByte().ToObject();
                    break;

                case InfoType.自检参数:
                    break;

                case InfoType.定位卫星信息:
                    nb0X94.DataContent09 = reader.ReadArray(reader.SrcBuffer.Length - 12).ToDataContent09();
                    break;

                case InfoType.ICCID信息:
                    nb0X94.DataContent0A = new DataContent0X94Models.DataContent0A()
                    {
                        IMEI = reader.ReadBcd(16),
                        IMSI = reader.ReadBcd(16),
                        ICCID = reader.ReadBcd(20)
                    };
                    break;

                case InfoType.巴西计价器:
                    nb0X94.DataContent10 = new DataContent0X94Models.DataContent10()
                    {
                        BrazilianMeter = reader.ReadAscii(reader.SrcBuffer.Length - 11)
                    };
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            return nb0X94;
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