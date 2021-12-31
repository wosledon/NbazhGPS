using System;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;
using NbazhGPS.Protocol.Models;

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
        public double Lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
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
        public int CellId { get; set; }
        /// <summary>
        /// 终端信息
        /// </summary>
        public TerminalInfo0X26 TerminalInfo { get; set; }
        /// <summary>
        /// 电压等级
        /// </summary>
        public VoltageLevel VoltageLevel { get; set; }
        /// <summary>
        /// GSM信号等级
        /// </summary>
        public GsmSignalStrength GsmSignalStrength { get; set; }
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
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X26 Deserialize(ref NbazhGpsMessagePackReader reader)
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