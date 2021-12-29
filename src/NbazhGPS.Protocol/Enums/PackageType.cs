using System.ComponentModel;

namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 包类型
    /// </summary>
    public enum PackageType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("包长度为1")]
        Type1 = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("包长度为2")]
        Type2 = 2
    }
}