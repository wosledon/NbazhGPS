using System;
using System.Diagnostics.Tracing;
using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Commands
{
    public class Command_SOS : CommandBase
    {
        public enum Command_SOS_Func
        {
            /// <summary>
            /// 添加
            /// </summary>
            A,

            /// <summary>
            /// 删除
            /// </summary>
            D,

            /// <summary>
            /// 全匹配删除
            /// </summary>
            DF,
        }

        public Command_SOS_Func CommandSosFunc { get; set; }

        public string PhoneNumber1 { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="commandSosFunc"> </param>
        /// <param name="phoneNumber1">   </param>
        /// <param name="phoneNumber2">   </param>
        /// <param name="phoneNumber3">   </param>
        public Command_SOS(Command_SOS_Func commandSosFunc, string phoneNumber1, string phoneNumber2, string phoneNumber3)
        {
            CommandSosFunc = commandSosFunc;
            PhoneNumber1 = phoneNumber1;
            PhoneNumber2 = phoneNumber2;
            PhoneNumber3 = phoneNumber3;
        }

        public Command_SOS(Command_SOS_Func commandSosFunc, string phoneNumber1)
        {
            CommandSosFunc = commandSosFunc;
            PhoneNumber1 = phoneNumber1;
        }

        public override EV26Commands Command => EV26Commands.SOS;

        public override string ToCommand()
        {
            return CommandSosFunc switch
            {
                Command_SOS_Func.A => $"SOS,A,{PhoneNumber1},{PhoneNumber2},{PhoneNumber3}#",
                Command_SOS_Func.D => $"SOS,D,{PhoneNumber1},{PhoneNumber2},{PhoneNumber3}#",
                Command_SOS_Func.DF => $"SOS,D,{PhoneNumber1}#",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}