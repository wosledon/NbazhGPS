using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;

namespace NbazhGPS.Protocol.Commands
{
    public class Command_SENALM : CommandBase
    {
        public enum ONOFF
        {
            ON,
            OFF
        }

        /// <summary>
        /// 警报上传方式
        /// </summary>
        public enum AlarmUploadFunc
        {
            仅GPRS = 0,
            SMS_GPRS = 1,
            GPRS_SMS_CALL = 2,
            GPRS_CALL = 3
        }

        public ONOFF A { get; set; }

        public AlarmUploadFunc M { get; set; }

        public Command_SENALM(ONOFF a, AlarmUploadFunc m)
        {
            A = a;
            M = m;
        }

        public override EV26Commands Command => EV26Commands.SENALM;

        public override string ToCommand()
        {
            return $"SENALM,{A.ToString()},{M.ToValue()}#";
        }

        public string Close()
        {
            return $"SENALM,OFF#";
        }
    }
}