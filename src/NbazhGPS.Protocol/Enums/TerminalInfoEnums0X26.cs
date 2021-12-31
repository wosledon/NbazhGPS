namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 终端信息
    /// </summary>
    public class TerminalInfoEnums0X26
    {
        /// <summary>
        /// 油电
        /// </summary>
        public enum OilAndElectricity : byte
        {
            /// <summary>
            /// 
            /// </summary>
            油电断开 = 1 << 7,

            /// <summary>
            /// 
            /// </summary>
            油电接通 = 0
        }

        /// <summary>
        /// Gps是否已定位
        /// </summary>
        public enum GpsConnected : byte
        {
            /// <summary>
            /// 
            /// </summary>
            Gps已定位 = 1 << 6,

            /// <summary>
            /// 
            /// </summary>
            Gps未定位 = 0
        }

        /// <summary>
        /// 报警
        /// </summary>
        public enum Alarm : byte
        {
            /// <summary>
            /// 
            /// </summary>
            SOS求救 = 4 << 3,

            /// <summary>
            /// 
            /// </summary>
            低电报警 = 3 << 3,

            /// <summary>
            /// 
            /// </summary>
            断电报警 = 2 << 3,

            /// <summary>
            /// 
            /// </summary>
            震动报警 = 1 << 3,

            /// <summary>
            /// 
            /// </summary>
            正常 = 0
        }
        /// <summary>
        /// 
        /// </summary>
        public enum PowerCharging
        {
            /// <summary>
            /// 
            /// </summary>
            已接电源充电 = 1 << 2,
            /// <summary>
            /// 
            /// </summary>
            未接电源充电 = 0
        }

        /// <summary>
        /// ACC状态
        /// </summary>
        public enum AccState : byte
        {
            /// <summary>
            /// 
            /// </summary>
            高 = 1 << 1,

            /// <summary>
            /// 
            /// </summary>
            低 = 0
        }

        /// <summary>
        /// 设防
        /// </summary>
        public enum Fortification : byte
        {
            /// <summary>
            /// 
            /// </summary>
            设防 = 1,
            /// <summary>
            /// 
            /// </summary>
            撤防 = 0
        }
    }
}