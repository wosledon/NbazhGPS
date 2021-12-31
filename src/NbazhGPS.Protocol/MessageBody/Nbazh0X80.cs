using System.Text.Json;
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
        /// 
        /// </summary>
        public override byte MsgId => 0x80;

        public override string Description => "在线指令";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X80 value)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public Nbazh0X80 Deserialize(ref NbazhGpsMessagePackReader reader)
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