using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;

namespace NbazhGPS.Protocol.Models
{
    /// <summary>
    /// 终端信息
    /// </summary>
    public class TerminalInfo0X26
    {
        /// <summary>
        /// 
        /// </summary>
        public TerminalInfoEnums0X26.OilAndElectricity OilAndElectricity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TerminalInfoEnums0X26.GpsConnected GpsConnected { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TerminalInfoEnums0X26.Alarm Alarm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TerminalInfoEnums0X26.PowerCharging PowerCharging { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TerminalInfoEnums0X26.AccState Acc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TerminalInfoEnums0X26.Fortification Fortification { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte ToByte()
        {
            byte res = 0;
            res |= OilAndElectricity.ToByteValue();
            res |= GpsConnected.ToByteValue();
            res |= Alarm.ToByteValue();
            res |= PowerCharging.ToByteValue();
            res |= Acc.ToByteValue();
            res |= Fortification.ToByteValue();

            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TerminalInfo0X26 ToObject(byte data)
        {
            OilAndElectricity = (TerminalInfoEnums0X26.OilAndElectricity)(data & (1 << 7));
            GpsConnected = (TerminalInfoEnums0X26.GpsConnected)(data & (1 << 6));
            Alarm = (TerminalInfoEnums0X26.Alarm)((byte)((byte)(data << 2) >> 5) << 3);
            PowerCharging = (TerminalInfoEnums0X26.PowerCharging)(data & 1 << 2);
            Acc = (TerminalInfoEnums0X26.AccState)(data & (1 << 1));
            Fortification = (TerminalInfoEnums0X26.Fortification)(data & 1);

            return this;
        }
    }
}