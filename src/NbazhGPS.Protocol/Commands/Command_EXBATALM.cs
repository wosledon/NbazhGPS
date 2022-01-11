using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Commands
{
    public class Command_EXBATALM : CommandBase
    {
        public override EV26Commands Command => EV26Commands.EXBATALM;

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
        }

        public override string ToCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}