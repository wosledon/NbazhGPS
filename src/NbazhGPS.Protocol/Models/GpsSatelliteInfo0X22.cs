namespace NbazhGPS.Protocol.Models
{
    public class GpsSatelliteInfo0X22
    {
        /// <summary>
        /// GPS信息长度
        /// </summary>
        public byte GpsInfoLength { get; set; }
        /// <summary>
        /// 参与卫星定位数
        /// </summary>
        public byte GpsSatelliteCount { get; set; }
    }
}