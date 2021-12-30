namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 电压等级
    /// </summary>
    public enum VoltageLevel: byte
    {
        /// <summary>
        /// 关机
        /// </summary>
        无电 = 0x00,
        /// <summary>
        /// 不足以打电话发短信等
        /// </summary>
        电量极低 = 0x01,
        /// <summary>
        /// 低电警报
        /// </summary>
        电量很低 = 0x02,
        /// <summary>
        /// 可正常使用
        /// </summary>
        电量低 = 0x03,
        /// <summary>
        /// 
        /// </summary>
        电量中 = 0x04,
        /// <summary>
        /// 
        /// </summary>
        电量高 = 0x05,
        /// <summary>
        /// 
        /// </summary>
        电量极高 = 0x06
    }
}