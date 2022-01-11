using System.Diagnostics.Tracing;
using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Commands
{
    public abstract class CommandBase
    {
        public abstract EV26Commands Command { get; }

        public abstract string ToCommand();
    }
}