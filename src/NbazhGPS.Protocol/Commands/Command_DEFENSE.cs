using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Commands
{
    public class Command_DEFENSE : CommandBase
    {
        public Command_DEFENSE()
        {
        }

        public override EV26Commands Command => EV26Commands.DEFEMSE;

        public override string ToCommand()
        {
            return $"DEFENSE,A#";
        }
    }
}