namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// GSM信号强度
    /// </summary>
    public enum GsmSignalStrength: byte
    {
        /// <summary>
        /// 
        /// </summary>
        无信号 = 0x00,
        /// <summary>
        /// 
        /// </summary>
        信号极弱 = 0x01,
        /// <summary>
        /// 
        /// </summary>
        信号较弱 = 0x02,
        /// <summary>
        /// 
        /// </summary>
        信号良好 = 0x03,
        /// <summary>
        /// 
        /// </summary>
        信号强 = 0x04
    }
}