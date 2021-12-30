using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;
using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 心跳包
    /// </summary>
    public class Nbazh0X13 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X01>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 
        /// </summary>
        public override byte MsgId => 0x13;
        /// <summary>
        /// 
        /// </summary>
        public override string Description => "心跳包";

        /// <summary>
        /// 终端信息内容
        /// </summary>
        public TerminalInfo TerminalInfo { get; set; }

        /// <summary>
        /// 电量等级
        /// </summary>
        public VoltageLevel VoltageLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GsmSignalStrength GsmSignalStrength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X01 value)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X01 Deserialize(ref NbazhGpsMessagePackReader reader)
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