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
        public uint CellId { get; set; }
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
        public uint NCI1 { get; set; }
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
        public byte NRSSI23 { get; set; }
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
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X28 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            throw new System.NotImplementedException();
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