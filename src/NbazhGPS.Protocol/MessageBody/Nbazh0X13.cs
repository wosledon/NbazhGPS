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
    /// 心跳包
    /// </summary>
    public class Nbazh0X13 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X13>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x13;

        /// <summary>
        /// </summary>
        public override string Description => "心跳包";

        /// <summary>
        /// 终端信息内容
        /// </summary>
        public TerminalInfo0X13 TerminalInfo { get; set; }

        /// <summary>
        /// 电量等级
        /// </summary>
        public VoltageLevel VoltageLevel { get; set; }

        /// <summary>
        /// GSM信号强度
        /// </summary>
        public GsmSignalStrength GsmSignalStrength { get; set; }

        /// <summary>
        /// 语言/扩展口状态
        /// </summary>
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X13 value)
        {
            writer.WriteByte(value.TerminalInfo.ToByte());
            writer.WriteByte(value.VoltageLevel.ToByteValue());
            writer.WriteByte(value.GsmSignalStrength.ToByteValue());
            writer.WriteUInt16(value.LanguageExtensionPortStatus.ToUInt16Value());
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X13 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            Nbazh0X13 nb0X13 = new Nbazh0X13()
            {
                TerminalInfo = reader.ReadByte().ToTerminalInfo0X13(),
                VoltageLevel = (VoltageLevel)reader.ReadByte(),
                GsmSignalStrength = (GsmSignalStrength)reader.ReadByte(),
                LanguageExtensionPortStatus = (LanguageExtensionPortStatus)reader.ReadUInt16()
            };

            return nb0X13;
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