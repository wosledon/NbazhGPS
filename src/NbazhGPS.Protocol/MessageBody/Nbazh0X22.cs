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
    /// GPS定位包
    /// </summary>
    public class Nbazh0X22 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X22>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 
        /// </summary>
        public override byte MsgId => 0x22;
        /// <summary>
        /// 
        /// </summary>
        public override string Description => "GPS定位包";

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gps卫星数
        /// </summary>
        public byte GpsSatelliteCount { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public float Lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public float Lat { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public byte Speed { get; set; }
        /// <summary>
        /// 航向, 状态
        /// </summary>
        public HeadingAndStatus HeadingAndStatus { get; set; }
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
        /// Acc状态
        /// </summary>
        public AccState AccState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X22 value)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X22 Deserialize(ref NbazhGpsMessagePackReader reader)
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