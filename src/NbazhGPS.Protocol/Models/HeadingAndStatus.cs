using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Models
{
    /// <summary>
    /// 航向以及状态
    /// </summary>
    public class HeadingAndStatus
    {
        //78 78 22 22 0F 0C 1D 02 33 05
        //C9 02 7A C8 18 0C 46 58 60 00
        //14 00 01 CC 00 28 7D 00 1F 71
        //00 00 01 00 08 20 86 0D 0A
        /// <summary>
        /// 实时GPS
        /// </summary>
        public HeadingAndStatusEnums0X22.GpsLocatedFunc GpsLocatedFunc { get; set; }
        /// <summary>
        /// GPS定位已否
        /// </summary>
        public HeadingAndStatusEnums0X22.IsGpsLocated IsGpsLocated { get; set; }
        /// <summary>
        /// 东经或者西经
        /// </summary>
        /// <remarks>true: 西经 false: 东经</remarks>
        public HeadingAndStatusEnums0X22.EorWLon EorWLon { get; set; }
        /// <summary>
        /// 南纬或者北纬
        /// </summary>
        public HeadingAndStatusEnums0X22.SorNLat SorNLat { get; set; }

        /// <summary>
        /// 航向
        /// </summary>
        public ushort Heading { get; set; }
    }
}