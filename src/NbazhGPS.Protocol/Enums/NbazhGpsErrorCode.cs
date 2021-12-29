namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 异常错误码
    /// </summary>
    public enum NbazhGpsErrorCode
    {
        /// <summary>
        /// Crc校验失败
        /// </summary>
        CrcVerifyFail = 1001,
        /// <summary>
        /// 消息头解析错误
        /// </summary>
        HeaderParseError = 2001,
        /// <summary>
        /// 消息体解析错误
        /// </summary>
        BodiesParseError = 2002,
        /// <summary>
        /// 经纬度错误
        /// </summary>
        LatOrLngError = 3001,
    }
}