using System.Text.Json;
using NbazhGPS.Protocol.BasicTypes;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// LBS地址请求包
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class Nbazh0X17_2 : Nbazh0X17, INbazhGpsMessagePackageFormatter<Nbazh0X17_2>
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x17;

        /// <summary>
        /// </summary>
        public override string Description => "LBS地址请求包";

        /// <summary>
        /// </summary>
        public new PackageType0X17 PackageType0X17 { get; set; } = PackageType0X17.终端地址请求包;

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
        public UInt24 CellId { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelephoneNumber { get; set; }

        /// <summary>
        /// 报警
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Alarm0X26 Alarm { get; set; } = Alarm0X26.正常;

        /// <summary>
        /// 语言
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LanguageExtensionPortStatus LanguageExtensionPortStatus { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X17_2 value)
        {
            writer.WriteUInt16(value.MCC);
            writer.WriteByte(value.MNC);
            writer.WriteUInt16(value.LAC);
            writer.WriteUInt24(value.CellId);
            writer.WriteAscii(value.TelephoneNumber, 21);
            writer.WriteByte(value.Alarm.ToByteValue());
            writer.WriteByte(value.LanguageExtensionPortStatus.ToByteValue());
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X17_2 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            Nbazh0X17_2 nb0X17 = new Nbazh0X17_2()
            {
                MCC = reader.ReadUInt16(),
                MNC = reader.ReadByte(),
                LAC = reader.ReadUInt16(),
                CellId = reader.ReadUInt24(),
                TelephoneNumber = reader.ReadAscii(21),
                Alarm = (Alarm0X26)reader.ReadByte(),
                LanguageExtensionPortStatus = (LanguageExtensionPortStatus)reader.ReadByte()
            };

            return nb0X17;
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