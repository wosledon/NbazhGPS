using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;

namespace NbazhGPS.Protocol.Handlers
{
    public static class EV26CommandHandler
    {
        public static string Query(this EV26Commands commands)
        {
            return commands.ToValue() > 0x2000 ? $"{commands.ToString()}#" : $"{commands.ToString()}";
        }

        public static string Command(this EV26Commands commands, string cmd)
        {
            return cmd;
        }
    }
}