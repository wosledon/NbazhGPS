
namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HeadingAndStatusEnums0X22
    {
        /// <summary>
        /// GPS实时/差分定位
        /// </summary>
        public enum GpsLocatedFunc : ushort
        {
            /// <summary>
            /// 实时GPS定位
            /// </summary>
            Gps实时定位 = 0,
            /// <summary>
            /// 
            /// </summary>
            Gps差分定位 = 1 << 13
        }

        /// <summary>
        /// GPS定位已否
        /// </summary>
        public enum IsGpsLocated : ushort
        {
            /// <summary>
            /// 
            /// </summary>
            已定位 = 1 << 12,
            /// <summary>
            /// 
            /// </summary>
            未定位 = 0
        }
        /// <summary>
        /// 东经或者西经
        /// </summary>
        public enum EorWLon
        {
            /// <summary>
            /// 
            /// </summary>
            东经 = 0,
            /// <summary>
            /// 
            /// </summary>
            西经 = 1 << 11
        }
        /// <summary>
        /// 南纬或者北纬
        /// </summary>
        public enum SorNLat
        {
            /// <summary>
            /// 
            /// </summary>
            南纬 = 0,
            /// <summary>
            /// 
            /// </summary>
            北纬 = 1 << 10
        }
    }

}