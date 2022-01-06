using System;
using System.Text.Json;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.MessageBody
{
    /// <summary>
    /// 校时包
    /// </summary>
    public class Nbazh0X8A : NbazhGpsBodies, INbazhGpsMessagePackageFormatter<Nbazh0X8A>, INbazhGpsAnalyze
    {
        /// <summary>
        /// </summary>
        public override byte MsgId => 0x8A;

        /// <summary>
        /// </summary>
        public override string Description => "校时包";

        /// <summary>
        /// 时间日期, 终端请求时没有该字段
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="writer"> </param>
        /// <param name="value">  </param>
        public void Serialize(ref NbazhGpsMessagePackWriter writer, Nbazh0X8A value)
        {
            if (DateTime is not null)
            {
                writer.WriteDateTime6(DateTime);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="reader"> </param>
        /// <returns> </returns>
        public Nbazh0X8A Deserialize(ref NbazhGpsMessagePackReader reader, bool isNeedStartEnd = true)
        {
            Nbazh0X8A nb0X8A = new Nbazh0X8A()
            {
                DateTime = reader.SrcBuffer.Length <= 10 ? null : reader.ReadDateTime6()
            };

            return nb0X8A;
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