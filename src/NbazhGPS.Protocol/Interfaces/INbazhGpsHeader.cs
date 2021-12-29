using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Formatters;

namespace NbazhGPS.Protocol.Interfaces
{
    /// <summary>
    /// 头部
    /// </summary>
    public interface INbazhGpsHeader : INbazhGpsMessagePackageFormatter<NbazhGpsHeader>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public abstract byte MsgId { get; set; }
        /// <summary>
        /// 包长度
        /// </summary>
        public abstract ushort Length { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public abstract NbazhGpsBodies Bodies { get; set; }
        /// <summary>
        /// 消息流水号
        /// </summary>
        public abstract ushort MsgNum { get; set; }
        /// <summary>
        /// 错误校验
        /// </summary>
        public abstract ushort Crc { get; set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        public abstract PackageType PackageType { get; set; }

    }
}