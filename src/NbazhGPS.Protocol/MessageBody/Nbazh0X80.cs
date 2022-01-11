using System;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 在线指令
    /// </summary>
    public class Nbazh0X80 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X80>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x80;

        public override string Description => "在线指令";

        /// <summary>
        /// 指令长度
        /// </summary>
        public byte CommandLength { get; set; }

        /// <summary>
        /// 服务器标记位
        /// </summary>
        public uint ServerFlagBits { get; set; }

        /// <summary>
        /// 指令内容
        /// </summary>
        public string CommandContext { get; set; }

        /// <summary>
        /// 报警
        /// </summary>
        public Alarm0X26 Alarm { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X80 value)
        {
            //7878 0E 80 | 08 00 00 00 00 73 6F 73 23 | 0001 6D6A 0D0A
            writer.Skip(1, out var pos);
            var localPos = writer.GetCurrentPosition();

            writer.WriteUInt32(value.ServerFlagBits);
            writer.WriteAscii(value.CommandContext);
            int len = writer.GetCurrentPosition() - localPos;
            writer.WriteByteReturn((byte)len, pos);
            writer.WriteByte(value.Alarm.ToByteValue());
            writer.WriteByte(value.LanguageExtensionPortStatus.ToByteValue());
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X80 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            var commandLen = reader.ReadByte();
            Nbazh0X80 nb0X80 = new Nbazh0X80()
            {
                CommandLength = commandLen,
                ServerFlagBits = reader.ReadUInt32(),
                CommandContext = reader.ReadAscii(commandLen - 6),
                Alarm = (Alarm0X26)reader.ReadByte(),
                LanguageExtensionPortStatus = (LanguageExtensionPortStatus)reader.ReadByte()
            };

            return nb0X80;
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