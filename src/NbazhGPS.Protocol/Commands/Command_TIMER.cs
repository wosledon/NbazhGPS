using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Commands
{
    public class Command_TIMER : CommandBase
    {
        /// <summary>
        /// ACC ON状态下上传间隔
        /// </summary>
        public int T1 { get; set; }

        /// <summary>
        /// ACC OFF状态下上传间隔
        /// </summary>
        public int T2 { get; set; }

        public Command_TIMER(int t1, int t2)
        {
            T1 = t1;
            T2 = t2;
        }

        public override EV26Commands Command => EV26Commands.TIMER;

        public override string ToCommand()
        {
            return $"TIMER,{T1},{T2}#";
        }
    }
}