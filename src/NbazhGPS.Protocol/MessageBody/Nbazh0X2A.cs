using System;
using System.Text.Json;
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
    /// GPS地址请求包
    /// </summary>
    public class Nbazh0X2A:NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X2A>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 
        /// </summary>
        public override byte MsgId => 0x2A;

        /// <summary>
        /// 
        /// </summary>
        public override string Description => "GPS地址请求包";
        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime? DateTime;

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
        /// 电话号码
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// 报警
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Alarm0X26 Alarm { get; set; } = Alarm0X26.正常;
        /// <summary>
        /// 语言
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X2A value)
        {
            writer.WriteDateTime6(value.DateTime);
            writer.WriteByte(value.GpsSatelliteCount);
            writer.WriteUInt32((uint)(value.Lon * 1800000));
            writer.WriteUInt32((uint)(Lat * 1800000));
            writer.WriteUInt16(value.HeadingAndStatus.ToUInt16());
            writer.WriteAscii(value.TelephoneNumber, 21);
            writer.WriteByte(value.Alarm.ToByteValue());
            writer.WriteByte(value.LanguageExtensionPortStatus.ToByteValue());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X2A Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            Nbazh0X2A nb0X2A = new Nbazh0X2A()
            {
                DateTime = reader.ReadDateTime6(),
                GpsSatelliteCount = reader.ReadByte(),
                Lon = (decimal)reader.ReadUInt32() / 1800000,
                Lat = (decimal)reader.ReadUInt32() / 1800000,
                Speed = reader.ReadByte(),
                HeadingAndStatus = reader.ReadUInt16().ToHeadingAndStatus(),
                TelephoneNumber = reader.ReadAscii(21),
                Alarm = (Alarm0X26)reader.ReadByte(),
                LanguageExtensionPortStatus = (LanguageExtensionPortStatus)reader.ReadByte()
            };

            return nb0X2A;
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