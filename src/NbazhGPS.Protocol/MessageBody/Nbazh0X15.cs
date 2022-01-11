using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    //TODO : 忽略的包 0X15
    public class Nbazh0X15 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X15>, INbazhGpsAnalyze
    {
        public override byte MsgId => 0x15;
        public override string Description => "终端回复(旧版)";

        /// <summary>
        /// 指令长度
        /// </summary>
        public byte CommandLength { get; set; }

        /// <summary>
        /// 服务器标志位
        /// </summary>
        public uint ServerFlagBits { get; set; }

        /// <summary>
        /// 指令内容
        /// </summary>
        public string CommandContent { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; }

        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X15 value)
        {
            writer.Skip(1, out var pos);
            var loc = writer.GetCurrentPosition();
            writer.WriteUInt32(value.ServerFlagBits);
            writer.WriteAscii(value.CommandContent);
            var len = writer.GetCurrentPosition() - loc;
            writer.WriteByteReturn((byte)len, pos);
            writer.WriteUInt16(LanguageExtensionPortStatus.ToUInt16Value());
        }

        public Nbazh0X15 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            Nbazh0X15 nb0X15 = new Nbazh0X15()
            {
                CommandLength = reader.ReadByte(),
                ServerFlagBits = reader.ReadUInt32(),
            };

            nb0X15.CommandContent = reader.ReadAscii(nb0X15.CommandLength - 4);
            nb0X15.LanguageExtensionPortStatus = (LanguageExtensionPortStatus)reader.ReadUInt16();

            return nb0X15;
        }

        public void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}