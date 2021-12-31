using System;
using System.Globalization;
using System.Text.Json;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// LBS多基站扩展信息包
    /// </summary>
    public class Nbazh0X28 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X28>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 
        /// </summary>
        public override byte MsgId => 0x028;
        /// <summary>
        /// 
        /// </summary>
        public override string Description => "LBS多基站扩展信息包";
        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime? DateTime { get; set; }
        /// <summary>
        /// 国家代号
        /// </summary>
        public ushort MCC { get; set; }
        /// <summary>
        /// 移动网号码
        /// </summary>
        public byte MNC { get; set; }
        /// <summary>
        /// 位置区域码
        /// </summary>
        public ushort LAC { get; set; }
        /// <summary>
        /// 移动基站
        /// </summary>
        public UInt24 CellId { get; set; }
        /// <summary>
        /// 小区信号强度
        /// </summary>
        public byte RSSI { get; set; }
        /// <summary>
        /// LAC
        /// </summary>
        public ushort NLAC1 { get; set; }
        /// <summary>
        /// CellId
        /// </summary>
        public UInt24 NCI1 { get; set; }
        /// <summary>
        /// RSSI
        /// </summary>
        public byte NRSSI1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ushort NLAC2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt24 NCI2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte NRSSI2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ushort NLAC3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt24 NCI3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte NRSSI3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ushort NLAC4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt24 NCI4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte NRSSI4 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ushort NLAC5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt24 NCI5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte NRSSI5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ushort NLAC6 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UInt24 NCI6 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public byte NRSSI6 { get; set; }
        /// <summary>
        /// 时间提前量
        /// </summary>
        public byte TimeAdvanceAmount { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X28 value)
        {
            writer.WriteDateTime6(value.DateTime);
            writer.WriteUInt16(value.MCC);
            writer.WriteByte(value.MNC);
            writer.WriteUInt16(value.LAC);
            writer.WriteUInt24(value.CellId);
            writer.WriteByte(value.RSSI);
            writer.WriteUInt16(NLAC1);
            writer.WriteUInt24(value.NCI1);
            writer.WriteByte(value.NRSSI1);
            writer.WriteUInt16(NLAC2);
            writer.WriteUInt24(value.NCI2);
            writer.WriteByte(value.NRSSI2);
            writer.WriteUInt16(NLAC3);
            writer.WriteUInt24(value.NCI3);
            writer.WriteByte(value.NRSSI3);
            writer.WriteUInt16(NLAC4);
            writer.WriteUInt24(value.NCI4);
            writer.WriteByte(value.NRSSI4);
            writer.WriteUInt16(NLAC5);
            writer.WriteUInt24(value.NCI5);
            writer.WriteByte(value.NRSSI5);
            writer.WriteUInt16(NLAC6);
            writer.WriteUInt24(value.NCI6);
            writer.WriteByte(value.NRSSI6);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X28 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            Nbazh0X28 nb0X28 = new Nbazh0X28()
            {
                DateTime = reader.ReadDateTime6(),
                MCC = reader.ReadUInt16(),
                MNC = reader.ReadByte(),
                LAC = reader.ReadUInt16(),
                CellId = reader.ReadUInt24(),
                RSSI = reader.ReadByte(),
                NLAC1 = reader.ReadByte(),
                NCI1 = reader.ReadUInt24(),
                NRSSI1 = reader.ReadByte(),
                NLAC2 = reader.ReadByte(),
                NCI2 = reader.ReadUInt24(),
                NRSSI2 = reader.ReadByte(),
                NLAC3 = reader.ReadByte(),
                NCI3 = reader.ReadUInt24(),
                NRSSI3 = reader.ReadByte(),
                NLAC4 = reader.ReadByte(),
                NCI4 = reader.ReadUInt24(),
                NRSSI4 = reader.ReadByte(),
                NLAC5 = reader.ReadByte(),
                NCI5 = reader.ReadUInt24(),
                NRSSI5 = reader.ReadByte(),
                NLAC6 = reader.ReadByte(),
                NCI6 = reader.ReadUInt24(),
                NRSSI6 = reader.ReadByte()
            };

            return nb0X28;
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