using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.Models
{
    /// <summary>
    /// 终端信息内容
    /// </summary>
    public partial class TerminalInfo0X13
    {
        /// <summary>
        /// 油电断开
        /// </summary>
        [Description("油电断开/接通")]
        public bool OilAndElectricityDisConnected { get; set; }

        /// <summary>
        /// GPS已定位
        /// </summary>
        [Description("GPS已定位")]
        public bool GpsConnected { get; set; }
        /// <summary>
        /// 扩展位 bit5
        /// </summary>
        [Description("扩展位 bit5")]
        public bool ExtensionBits5 { get; set; }
        /// <summary>
        /// 扩展位 bit4
        /// </summary>
        [Description("扩展位 bit4")]
        public bool ExtensionBits4 { get; set; }
        /// <summary>
        /// 扩展位 bit3
        /// </summary>
        [Description("扩展位 bit3")]
        public bool ExtensionBits3 { get; set; }

        /// <summary>
        /// 已接电源充电
        /// </summary>
        [Description("已接电源充电")]
        public bool PowerChargingConnected { get; set; }
        /// <summary>
        /// true: ACC High false: ACC Low
        /// </summary>
        [Description("ACC High")]
        public bool AccHigh { get; set; }
        /// <summary>
        /// 设防
        /// </summary>
        [Description("设防")]
        public bool Fortification { get; set; }

        private const byte Oaed = 1 << 7;
        private const byte Gps = 1 << 6;
        private const byte Eb5 = 1 << 5;
        private const byte Eb4 = 1 << 4;
        private const byte Eb3 = 1 << 3;
        private const byte Pcc = 1 << 2;
        private const byte Ah = 1 << 1;
        private const byte Ff = 1;

        /// <summary>
        /// 转为字节
        /// </summary>
        /// <returns></returns>
        public byte ToByte()
        {
            byte res = 0;

            if (OilAndElectricityDisConnected) res |= Oaed;
            if (GpsConnected) res |= Gps;
            if (ExtensionBits5) res |= Eb5;
            if (ExtensionBits4) res |= Eb4;
            if (ExtensionBits3) res |= Eb3;
            if (PowerChargingConnected) res |= Pcc;
            if (AccHigh) res |= Ah;
            if (Fortification) res |= Ff;

            return res;
        }

        /// <summary>
        /// 转为对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TerminalInfo0X13 ToObject(byte data)
        {
            OilAndElectricityDisConnected = (data & Oaed) > 0;
            GpsConnected = (data & Gps) > 0;
            ExtensionBits5 = (data & Eb5) > 0;
            ExtensionBits4 = (data & Eb4) > 0;
            ExtensionBits3 = (data & Eb3) > 0;
            PowerChargingConnected = (data & Pcc) > 0;
            AccHigh = (data & Ah) > 0;
            Fortification = (data & Ff) > 0;

            return this;
        }
    }
}