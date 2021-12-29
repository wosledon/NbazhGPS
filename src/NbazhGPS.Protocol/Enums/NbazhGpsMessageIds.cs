namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 消息Id
    /// </summary>
    public enum NbazhGpsMessageIds : byte
    {
        /// <summary>
        /// </summary>
        登陆包 = 0x01,

        /// <summary>
        /// </summary>
        Gps定位包 = 0x22,

        /// <summary>
        /// </summary>
        心跳包 = 0x23,

        /// <summary>
        /// </summary>
        终端在线指令回复 = 0x21,

        /// <summary>
        /// </summary>
        报警数据 = 0x26,

        /// <summary>
        /// </summary>
        Gps地址请求包 = 0x2A,

        /// <summary>
        /// </summary>
        Lbs地址请求包 = 0x17,

        /// <summary>
        /// </summary>
        在线指令 = 0x80,

        /// <summary>
        /// </summary>
        校时包 = 0x8A,

        /// <summary>
        /// </summary>
        信息传输通用包 = 0x94,

        /// <summary>
        /// </summary>
        中文地址回复包 = 0x17,

        /// <summary>
        /// </summary>
        英文地址回复包 = 0x97,

        /// <summary>
        /// </summary>
        串口透传包 = 0x30,

        /// <summary>
        /// </summary>
        串口透传服务器下发 = 0x31
    }
}