using System;
using NbazhGPS.Protocol.Helpers;

namespace NbazhGPS.Protocol.Extensions
{
    /// <summary>
    /// Crc扩展
    /// </summary>
    public static class CrcExtensions
    {
        /// <summary>
        /// 校验Crc
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="crc"></param>
        /// <returns></returns>
        public static bool AuthCrc(this byte[] buffer, ushort crc)
        {
            return CrcHelper.GetCrc16(buffer).Equals(crc);
        }

        /// <summary>
        /// 校验Crc
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="crc"></param>
        /// <returns></returns>
        public static bool AuthCrc(this Span<byte> buffer, ushort crc)
        {
            return CrcHelper.GetCrc16(buffer).Equals(crc);
        }

        /// <summary>
        /// 校验Crc
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="crc"></param>
        /// <returns></returns>
        public static bool AuthCrc(this ReadOnlySpan<byte> buffer, ushort crc, bool isNeedStartEnd = true)
        {
            return CrcHelper.GetCrc16(buffer, isNeedStartEnd).Equals(crc);
        }
        /// <summary>
        /// 转化Crc
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static ushort ToCrc(this ReadOnlySpan<byte> buffer)
        {
            return (ushort)((buffer[0] << 8) + buffer[1]);
        }
    }
}