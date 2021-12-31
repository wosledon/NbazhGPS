using System.Text.Json;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 服务器报警包中文地址回复
    /// </summary>
    public class Nbazh0X17 : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X17>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x17;

        /// <summary>
        /// </summary>
        public override string Description => "服务器报警包中文地址回复";

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X17 value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X17 Deserialize(ref NbazhGpsMessagePackReader reader)
        {
            throw new System.NotImplementedException();
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