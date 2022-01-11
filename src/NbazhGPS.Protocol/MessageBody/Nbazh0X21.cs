using System;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NbazhGPS.Protocol.MessageBody
{
    //TODO : 忽略的包 0x21
    /// <summary>
    /// 终端在线指令回复
    /// </summary>
    public class Nbazh0X21 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X21>, INbazhGpsAnalyze
    {
        public override byte MsgId => 0x21;
        public override string Description => "终端回复(通用指令)";

        /// <summary>
        /// 服务器标记位
        /// </summary>
        public uint ServerFlagBits { get; set; }

        /// <summary>
        /// 内容编码
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ContentEncodeEnums0X21 ContentEncode { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X21 value)
        {
            writer.WriteUInt32(value.ServerFlagBits);
            writer.WriteByte(value.ContentEncode.ToByteValue());
            if (value.ContentEncode == ContentEncodeEnums0X21.ASCII)
            {
                writer.WriteAscii(value.Content);
            }
            else
            {
                writer.WriteUniCode(value.Content);
            }
        }

        public Nbazh0X21 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            Nbazh0X21 nb0X21 = new Nbazh0X21()
            {
                ServerFlagBits = reader.ReadUInt32(),
                ContentEncode = (ContentEncodeEnums0X21)reader.ReadByte()
            };

            var len = reader.SrcBuffer.Length - 5 - ((isNeedStartEnd ? 4 : 0) + 2 + 1 + 2 + 2);

            nb0X21.Content = nb0X21.ContentEncode switch
            {
                ContentEncodeEnums0X21.ASCII => reader.ReadAscii(len),
                ContentEncodeEnums0X21.UTF16_BE => reader.ReadUnicode(len),
                _ => throw new ArgumentOutOfRangeException()
            };

            return nb0X21;
        }

        public void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}