using System;
using System.Text.Json;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 服务器报警包英文地址回复
    /// </summary>
    [Obsolete("0x97协议中文档有错误", false)]
    public class Nbazh0X97 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X97>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x97;

        /// <summary>
        /// </summary>
        public override string Description => "服务器报警包英文地址回复";

        /// <summary>
        /// 指令长度
        /// </summary>
        public byte CommandLength { get; set; }

        /// <summary>
        /// 服务器标志位
        /// </summary>
        /// <remarks> 服务器标志位用于标志是哪个报警的标志 </remarks>
        public uint ServerFlagBits { get; set; }

        /// <summary>
        /// 报警编码标志
        /// </summary>
        public string ALARMSMS { get; set; }

        /// <summary>
        /// 分隔符1
        /// </summary>
        public string Flag1 { get; set; } = "&&";

        /// <summary>
        /// 地址内容
        /// </summary>
        public string AddressContent { get; set; }

        /// <summary>
        /// 分隔符2
        /// </summary>
        public string Flag2 { get; set; } = "&&";

        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelephoneNumber { get; set; } = "";
        /// <summary>
        /// 分隔符3
        /// </summary>
        public string Flag3 { get; set; } = "##";

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X97 value)
        {
            writer.Skip(1, out var lengthPos);
            var skipLength = writer.GetCurrentPosition();
            writer.WriteUInt32(value.ServerFlagBits);
            writer.WriteAscii(value.ALARMSMS, 8);
            writer.WriteAscii(value.Flag1, 2);
            writer.WriteUniCode(value.AddressContent);
            writer.WriteAscii(value.Flag2, 2);
            writer.WriteAscii(value.TelephoneNumber, 21);
            writer.WriteAscii(value.Flag3, 2);
            var length = writer.GetCurrentPosition() - skipLength;
            writer.WriteByteReturn((byte)length, lengthPos);
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X97 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            var commandLen = reader.ReadByte();
            CommandLength = commandLen;
            ServerFlagBits = reader.ReadUInt32();
            ALARMSMS = reader.ReadAscii(8);
            Flag1 = reader.ReadAscii(2);
            AddressContent = reader.ReadUnicode(commandLen - (4 + 8 + 2 + 2 + 2 + 21));
            Flag2 = reader.ReadAscii(2);
            TelephoneNumber = reader.ReadAscii(21);
            Flag3 = reader.ReadAscii(2);
            Nbazh0X97 nb0X97 = new Nbazh0X97()
            {
                //CommandLength = commandLen,
                //ServerFlagBits = reader.ReadUInt32(),
                //ALARMSMS = reader.ReadAscii(8),
                //Flag1 = reader.ReadAscii(2),
                //AddressContent = reader.ReadUnicode(commandLen - (4 + 8 + 2 + 2 + 2 + 21)),
                //Flag2 = reader.ReadAscii(2),
                //TelephoneNumber = reader.ReadAscii(21),
                //Flag3 = reader.ReadAscii(2)
            };
            return nb0X97;
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