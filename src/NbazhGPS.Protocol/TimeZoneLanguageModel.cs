using System;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// 时区语言
    /// </summary>
    public class TimeZoneLanguageModel
    {
        /// <summary>
        /// 数据
        /// </summary>
        private ushort Data { get; set; } = 0;

        /// <summary>
        /// 高位, 1.5字节
        /// </summary>
        private ushort HighParts { get; set; } = 0;

        /// <summary>
        /// 低位, 0.5字节
        /// </summary>
        private ushort LowParts { get; set; } = 0;

        /// <summary>
        /// 时区时间
        /// </summary>
        public ushort TimeZoneTime { get; set; } = 0;

        /// <summary>
        /// 时区东西
        /// </summary>
        public TimeZones TimeZone { get; set; } = TimeZones.东;

        /// <summary>
        /// 保留位
        /// </summary>
        public bool ReservedBits { get; set; } = false;

        /// <summary>
        /// 语言选择位 bit1
        /// </summary>
        public bool LanguageChoose2 { get; set; } = false;

        /// <summary>
        /// 语言选择位 bit0
        /// </summary>
        public bool LanguageChoose1 { get; set; } = false;

        private const byte TimeZoneFlag = 1;
        private const byte ReservedBitsFlag = 1 << 1;
        private const byte LanguageChoose2Flag = 1 << 2;
        private const byte LanguageChoose1Flag = 1 << 3;

        /// <summary>
        /// 序列化时区语言
        /// </summary>
        /// <returns> </returns>
        public ushort Serialize()
        {
            ushort high = (ushort)(TimeZoneTime * 100);
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            for (int i = 3; i > 0; i--)
            {
                if ((high >> (i * 4)) != 0)
                {
                    high = (ushort)(high << (4 * (3 - i)));
                    break;
                }
            }

            HighParts = (ushort)(high >> 4);
            if (TimeZone == TimeZones.西)
            {
                LowParts |= TimeZoneFlag;
            }

            if (LanguageChoose2)
            {
                LowParts |= LanguageChoose2Flag;
            }

            if (LanguageChoose1)
            {
                LowParts |= LanguageChoose1Flag;
            }
            LowParts = (ushort)(LowParts << 12);
            Data = (ushort)(HighParts | LowParts);
            return Data;
        }

        /// <summary>
        /// 反序列化时区语言
        /// </summary>
        /// <param name="timeZone"> </param>
        /// <returns> </returns>
        public TimeZoneLanguageModel Deserialize(ushort timeZone)
        {
            Data = timeZone;
            HighParts = (ushort)(Data << 4 >> 4);
            TimeZoneTime = (ushort)(HighParts / 100);

            LowParts = (byte)(Data >> 12);
            TimeZone = (TimeZones)(LowParts ^ (ushort)TimeZones.东);
            LanguageChoose2 = (LowParts ^ LanguageChoose2Flag) >= 1;
            LanguageChoose1 = (LowParts ^ LanguageChoose1Flag) >= 1;

            return this;
        }

        /// <summary>
        /// 反序列化时区语言
        /// </summary>
        /// <param name="timeZone"> </param>
        /// <returns> </returns>
        public TimeZoneLanguageModel Deserialize(ReadOnlySpan<byte> timeZone)
        {
            Data = (ushort)(timeZone[0] << 8 + timeZone[1]);
            HighParts = (ushort)(Data << 4 >> 4);
            TimeZoneTime = (ushort)(HighParts / 100);

            LowParts = (byte)(Data >> 12);
            TimeZone = (TimeZones)(LowParts ^ (ushort)TimeZones.东);
            LanguageChoose2 = (LowParts ^ LanguageChoose2Flag) >= 1;
            LanguageChoose1 = (LowParts ^ LanguageChoose1Flag) >= 1;

            return this;
        }

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public override string ToString()
        {
            return $"{(TimeZone.ToString())}{TimeZoneTime}区,GMT{(TimeZone == TimeZones.东 ? "+" : "-")}{TimeZoneTime.FormatTimeZoneLanguageTime()}";
        }
    }
}