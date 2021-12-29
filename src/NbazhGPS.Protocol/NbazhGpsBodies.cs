using NbazhGPS.Protocol.Interfaces;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// 抽象数据体
    /// </summary>
    public abstract class NbazhGpsBodies:INbazhGpsDescription
    {
        /// <summary>
        /// 跳过数据体序列化
        /// 默认不跳过
        /// </summary>
        public virtual bool SkipSerialization { get; set; }
        /// <summary>
        /// 消息Id
        /// </summary>
        public abstract byte MsgId { get; }
        /// <summary>
        /// 描述
        /// </summary>
        public abstract string Description { get;}
    }
}