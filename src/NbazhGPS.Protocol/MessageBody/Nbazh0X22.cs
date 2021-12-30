using System;
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
        /// 数据上报模式
        /// </summary>
        public DataReportingMode DataReportingMode { get; set; }
        /// <summary>
        /// GPS实时补传
        /// </summary>
        public byte GpsRealTimeHeadIn { get; set; }

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
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X22 value)
        {
            writer.WriteDateTime6(value.DateTime);
            writer.WriteByte(value.GpsSatelliteCount);
            writer.WriteUInt32((uint)(value.Lon * 1800000));
            writer.WriteUInt32((uint)(value.Lat * 1800000));
            writer.WriteByte(value.Speed);
            writer.WriteUInt16(value.HeadingAndStatus.HeadingAndStatusToUInt16());
            writer.WriteUInt16(value.MCC);
            writer.WriteByte(value.MNC);
            writer.WriteUInt16(value.LAC);
            writer.WriteByte3(CellId);
            writer.WriteByte(value.AccState.ToByteValue());
            writer.WriteByte(value.DataReportingMode.ToByteValue());
            writer.WriteByte(GpsRealTimeHeadIn);
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
        public Nbazh0X22 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            Nbazh0X22 nb0X022 = new Nbazh0X22()
            {
                IsSupportMileage = reader.SrcBuffer.Length > 30,
                DateTime = reader.ReadDateTime6(),
                GpsSatelliteCount = reader.ReadByte(),
                Lon = (double)reader.ReadUInt32() / 1800000,
                Lat = (double)reader.ReadUInt32() / 1800000,
                Speed = reader.ReadByte(),
                HeadingAndStatus = reader.ReadUInt16().ToHeadingAndStatus(),
                MCC = reader.ReadUInt16(),
                MNC = reader.ReadByte(),
                LAC = reader.ReadUInt16(),
                CellId = reader.ReadByte3(),
                AccState = (AccState)reader.ReadByte(),
                DataReportingMode = (DataReportingMode)reader.ReadByte(),
                GpsRealTimeHeadIn = reader.ReadByte(),
                // 如果包长度不包含里程则不解析
                Mileage = IsSupportMileage ? reader.ReadUInt32() : 0
            };
            return nb0X022;
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