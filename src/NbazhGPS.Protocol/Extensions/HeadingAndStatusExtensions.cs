using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class HeadingAndStatusExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HeadingAndStatus ToHeadingAndStatus(this ushort data)
        {
            var has = new HeadingAndStatus()
            {
                GpsLocatedFunc = (HeadingAndStatusEnums0X22.GpsLocatedFunc)(data & (1 << 13)),
                IsGpsLocated = (HeadingAndStatusEnums0X22.IsGpsLocated)(data & (1 << 12)),
                EorWLon = (HeadingAndStatusEnums0X22.EorWLon)(data & (1 << 11)),
                SorNLat = (HeadingAndStatusEnums0X22.SorNLat)(data & (1 << 10)),
                Heading = (ushort)((ushort)(data << 6) >> 6)
            };

            return has;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ushort HeadingAndStatusToUInt16(this HeadingAndStatus data)
        {
            ushort res = 0;
            res |= data.GpsLocatedFunc.ToUInt16Value();
            res |= data.IsGpsLocated.ToUInt16Value();
            res |= data.EorWLon.ToUInt16Value();
            res |= data.SorNLat.ToUInt16Value();
            res |= data.Heading;

            return res;
        }
    }
}