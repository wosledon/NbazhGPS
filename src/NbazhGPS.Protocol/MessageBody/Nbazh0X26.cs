using System;
using System.Text.Json;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;
using NbazhGPS.Protocol.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 报警包
    /// </summary>
    public class Nbazh0X26 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X26>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 
        /// </summary>
        public override byte MsgId => 0x26;
        /// <summary>
        /// 
        /// </summary>
        public override string Description => "报警包";
        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Gps卫星数
        /// </summary>
        public byte GpsSatelliteCount { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal Lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal Lat { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public byte Speed { get; set; }
        /// <summary>
        /// 航向, 状态
        /// </summary>
        public HeadingAndStatus HeadingAndStatus { get; set; }

        /// <summary>
        /// LBS长度  自身+MCC+MNC+CellID
        /// </summary>
        public byte LBSLength { get; set; } = 9;
        /// <summary>
        /// 国家代号
        /// </summary>
        public ushort MCC { get; set; }
        /// <summary>
        /// 移动网号码
        /// </summary>
        public byte MNC { get; set; }
        /// <summary>
        /// 位置区码
        /// </summary>
        public ushort LAC { get; set; }
        /// <summary>
        /// 移动基站
        /// </summary>
        public UInt24 CellId { get; set; }
        /// <summary>
        /// 终端信息
        /// </summary>
        public TerminalInfo0X26 TerminalInfo { get; set; }
        /// <summary>
        /// 电压等级
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public VoltageLevel VoltageLevel { get; set; }
        /// <summary>
        /// GSM信号等级
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GsmSignalStrength GsmSignalStrength { get; set; }
        /// <summary>
        /// 报警
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Alarm0X26 Alarm { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; }
        /// <summary>
        /// 里程
        /// </summary>
        public uint? Mileage { get; set; } = null;
        /// <summary>
        /// 是否支持里程
        /// </summary>
        public bool IsSupportMileage = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X26 value)
        {
            writer.WriteDateTime6(value.DateTime);
            writer.WriteByte(value.GpsSatelliteCount);
            writer.WriteUInt32((uint)(Lon * 1800000));
            writer.WriteUInt32((uint)(value.Lat * 1800000));
            writer.WriteByte(value.Speed);
            writer.WriteUInt16(value.HeadingAndStatus.HeadingAndStatusToUInt16());
            writer.WriteByte(value.LBSLength);
            writer.WriteUInt16(value.MCC);
            writer.WriteByte(value.MNC);
            writer.WriteUInt16(value.LAC);
            writer.WriteUInt24(value.CellId);
            writer.WriteByte(value.TerminalInfo.ToByte());
            writer.WriteByte(VoltageLevel.ToByteValue());
            writer.WriteByte(value.GsmSignalStrength.ToByteValue());
            writer.WriteByte(value.Alarm.ToByteValue());
            writer.WriteByte(value.LanguageExtensionPortStatus.ToByteValue());
            if (value.Mileage.HasValue)
            {
                writer.WriteUInt32(value.Mileage.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X26 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            Nbazh0X26 nb0X26 = new Nbazh0X26()
            {
                IsSupportMileage = reader.SrcBuffer.Length > 30,
                DateTime = reader.ReadDateTime6(),
                GpsSatelliteCount = reader.ReadByte(),
                Lon = (decimal)reader.ReadUInt32() / 1800000,
                Lat = (decimal)reader.ReadUInt32() / 1800000,
                Speed = reader.ReadByte(),
                HeadingAndStatus = reader.ReadUInt16().ToHeadingAndStatus(),
                LBSLength = reader.ReadByte(),
                MCC = reader.ReadUInt16(),
                MNC = reader.ReadByte(),
                LAC = reader.ReadUInt16(),
                CellId = reader.ReadUInt24(),
                TerminalInfo = reader.ReadByte().ToTerminalInfo0X26(),
                VoltageLevel = (VoltageLevel)reader.ReadByte(),
                GsmSignalStrength = (GsmSignalStrength)reader.ReadByte(),
                Alarm = (Alarm0X26)reader.ReadByte(),
                LanguageExtensionPortStatus = (LanguageExtensionPortStatus)reader.ReadByte(),
                Mileage = IsSupportMileage ? reader.ReadUInt32() : 0
            };

            return nb0X26;
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