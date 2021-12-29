using System.Text.Json;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 登录包
    /// </summary>
    public class Nbazh0X01 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X01>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public override byte MsgId => 0x01;
        /// <summary>
        /// 终端Id
        /// </summary>
        public string TerminalId { get; set; }

        /// <summary>
        /// 终端类型标识码
        /// </summary>
        public ushort TerminalType { get; set; }

        /// <summary>
        /// 时区语言
        /// </summary>
        public TimeZoneLanguageModel TimeZoneLanguage { get; set; }
        

        /// <summary>
        /// 描述
        /// </summary>
        public override string Description => "登录包";

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X01 value)
        {
            writer.WriteBcd(value.TerminalId, 16);
            writer.WriteUInt16(value.TerminalType);
            writer.WriteUInt16(value.TimeZoneLanguage.Serialize());
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X01 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            Nbazh0X01 nb0X01 = new Nbazh0X01
            {
                TerminalId = reader.ReadBcd(16),
                TerminalType = reader.ReadUInt16(),
                TimeZoneLanguage = reader.ReadTimeZoneLanguage()
            };

            return nb0X01;
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <param name="writer"> </param>
        public void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer)
        {
            var replayTerminalId = reader.ReadBcd(16);
            var replayTerminalType = reader.ReadUInt16();
            var timeZoneLanguage = reader.ReadTimeZoneLanguage().ToString();

            writer.WriteString($"[{replayTerminalId}]终端ID", replayTerminalId);
            writer.WriteNumber($"[{replayTerminalType.ReadNumber()}]类型识别码", replayTerminalType);
            writer.WriteString($"[{timeZoneLanguage}]时区语言", timeZoneLanguage);
        }
    }
}