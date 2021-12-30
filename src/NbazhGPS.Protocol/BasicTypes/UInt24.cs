using System;
using System.Runtime.InteropServices;

namespace NbazhGPS.Protocol.BasicTypes
{
    /// <summary>
    /// 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct UInt24
    {
        private Byte _b0;
        private Byte _b1;
        private Byte _b2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public UInt24(UInt32 value)
        {
            _b0 = (byte)((value) & 0xFF);
            _b1 = (byte)((value >> 8) & 0xFF);
            _b2 = (byte)((value >> 16) & 0xFF);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public UInt24(byte[] bytes)
        {
            _b0 = bytes[0];
            _b1 = bytes[1];
            _b2 = bytes[2];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        public UInt24(ReadOnlySpan<byte> bytes)
        {
            _b0 = bytes[0];
            _b1 = bytes[1];
            _b2 = bytes[2];
        }
        /// <summary>
        /// 
        /// </summary>
        public UInt32 Value => (uint)(_b0 | (_b1 << 8) | (_b2 << 16));

        /// <summary>
        /// 
        /// </summary>
        public byte[] ToBytes => new[] { _b0, _b1, _b2 };
    }
}