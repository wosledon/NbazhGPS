namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 数据上报模式
    /// </summary>
    public enum DataReportingMode: byte
    {
        /// <summary>
        /// 
        /// </summary>
        定时上报 = 0x00,
        /// <summary>
        /// 
        /// </summary>
        定距上报 = 0x01,
        /// <summary>
        /// 
        /// </summary>
        拐点上报 = 0x02,
        /// <summary>
        /// 
        /// </summary>
        Acc状态改变上传 = 0x03,
        /// <summary>
        /// 
        /// </summary>
        从运动变为静止状态后_补传最后一个定位点 = 0x04,
        /// <summary>
        /// 
        /// </summary>
        网络断开重连后_上报之前最后一个有效的上传点 = 0x05,
        /// <summary>
        /// 
        /// </summary>
        上报模式_星历更新强制上传Gps点 = 0x06,
        /// <summary>
        /// 
        /// </summary>
        上报模式_按键上传定位点 = 0x07,
        /// <summary>
        /// 
        /// </summary>
        上报模式_开机上报位置信息 = 0x08,
        /// <summary>
        /// 
        /// </summary>
        上报模式_未使用 = 0x09,
        /// <summary>
        /// 
        /// </summary>
        上报模式_设备静止后上报最后的经纬度_但时间更新 = 0x0A,
        /// <summary>
        /// 
        /// </summary>
        Wifi解析经纬度上传包 = 0x0B,
        /// <summary>
        /// 
        /// </summary>
        上报模式_Ljdw立即定位指令上报 = 0x0C,
        /// <summary>
        /// 
        /// </summary>
        上报模式_设备静止后上报最后的经纬度 = 0x0D,
        /// <summary>
        /// 
        /// </summary>
        上报模式_GPSDUP上传_静止状态定时上传 = 0x0E,
        /// <summary>
        /// 
        /// </summary>
        上报模式_退出追踪模式 = 0x0F
    }
}