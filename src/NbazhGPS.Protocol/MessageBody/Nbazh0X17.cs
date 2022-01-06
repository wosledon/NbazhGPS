using System;
using System.Text.Json;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessageBody;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    // TAG: 这是个SB协议，0X17这个协议号重用了好几次
    /// <summary>
    /// </summary>
    public class Nbazh0X17 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X17>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x17;

        /// <summary>
        /// </summary>
        public override string Description { get; }

        // 31
        /// <summary>
        /// </summary>
        public PackageType0X17 PackageType0X17 { get; set; } = PackageType0X17.服务器地址请求包中文回复;

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        /// <exception cref="System.NotImplementedException"> </exception>

        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X17 value)
        {
            if (value.PackageType0X17.Equals(PackageType0X17.服务器地址请求包中文回复))
            {
                (value as Nbazh0X17_1)!.Serialize(ref writer, value as Nbazh0X17_1);
            }
            else
            {
                (value as Nbazh0X17_2)!.Serialize(ref writer, value as Nbazh0X17_2);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        /// <exception cref="System.NotImplementedException"> </exception>
        public Nbazh0X17 Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            if (reader.SrcBuffer.Length != PackageType0X17.终端地址请求包.ToUInt16Value())
            {
                return new Nbazh0X17_1().Deserialize(ref reader);
            }
            else
            {
                return new Nbazh0X17_2().Deserialize(ref reader);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <param name="writer"> </param>
        /// <exception cref="System.NotImplementedException"> </exception>
        public void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer)
        {
            throw new System.NotImplementedException();
        }
    }
}