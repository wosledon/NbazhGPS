using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Formatters;

namespace NbazhGPS.Protocol.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface INbazhGpsPackage :INbazhGpsMessagePackageFormatter<NbazhGpsPackage>, INbazhGpsAnalyze
    {
        /// <summary>
        /// 起始位
        /// </summary>
        public abstract ushort Begin { get; set; }
        /// <summary>
        /// 头数据
        /// </summary>
        public abstract NbazhGpsHeader Header { get; set; }
        /// <summary>
        /// 数据体
        /// </summary>
        public abstract NbazhGpsBodies Bodies { get; set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        public abstract PackageType PackageType { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public ushort End { get; set; }
    }
}