using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.Extensions
{
    public static class GpsSatelliteInfo0X22Extension
    {
        public static byte ToByte(this GpsSatelliteInfo0X22 data)
        {
            return (byte)((data.GpsInfoLength << 4) | data.GpsSatelliteCount);
        }

        public static GpsSatelliteInfo0X22 ToGpsSatelliteInfoObject(this byte data)
        {
            return new GpsSatelliteInfo0X22()
            {
                GpsInfoLength = (byte)(data >> 4),
                GpsSatelliteCount = (byte)((byte)(data << 4) >> 4)
            };
        }
    }
}