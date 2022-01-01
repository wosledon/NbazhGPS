using System.ComponentModel;

namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 包类型
    /// </summary>
    public enum PackageType
    {
        /// <summary>
        /// </summary>
        [Description("包长度为1")]
        Type1 = 1,

        /// <summary>
        /// </summary>
        [Description("包长度为2")]
        Type2 = 2
    }

    /// <summary>
    /// 0x17包类型
    /// </summary>
    public enum PackageType0X17
    {
        终端地址请求包 = 31,
        服务器地址请求包中文回复 = 39
    }
}