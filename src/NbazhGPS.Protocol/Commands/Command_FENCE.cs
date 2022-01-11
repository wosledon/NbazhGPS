using System;
using System.Globalization;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;

namespace NbazhGPS.Protocol.Commands
{
    public class Command_FENCE : CommandBase
    {
        /// <summary>
        /// 围栏区域类型
        /// </summary>
        public enum Command_FENCE_Shape
        {
            圆形区域 = 0,
            方形区域 = 1
        }

        /// <summary>
        /// 开启/关闭围栏报警
        /// </summary>
        public enum ONOFF
        {
            ON,
            OFF
        }

        /// <summary>
        /// 报警方式
        /// </summary>
        public enum AlarmFunc
        {
            /// <summary>
            /// 入围栏报警
            /// </summary>
            IN,

            /// <summary>
            /// 出围栏报警
            /// </summary>
            OUT
        }

        /// <summary>
        /// 警报上传方式
        /// </summary>
        public enum AlarmUploadFunc
        {
            仅GPRS = 0,
            SMS_GPRS = 1,
        }

        public Command_FENCE_Shape CommandFenceShape { get; set; }
        public ONOFF B { get; set; }

        public decimal D { get; set; }

        public decimal E { get; set; }

        public decimal F { get; set; }

        public decimal G { get; set; }

        public AlarmFunc? X { get; set; }
        public AlarmUploadFunc M { get; }

        /// <summary>
        /// 圆形区域
        /// </summary>
        /// <param name="b"> 开启/关闭围栏报警 </param>
        /// <param name="d"> </param>
        /// <param name="e"> </param>
        /// <param name="f"> </param>
        /// <param name="x"> 报警方式 </param>
        /// <param name="m"> 报警上报方式 </param>
        public Command_FENCE(ONOFF b, decimal d, decimal e, decimal f, AlarmFunc? x = null, AlarmUploadFunc m = AlarmUploadFunc.仅GPRS)
        {
            CommandFenceShape = Command_FENCE_Shape.圆形区域;
            D = d;
            E = e;
            F = f;
            X = x;
            M = m;
            B = b;
        }

        /// <summary>
        /// 方形区域
        /// </summary>
        /// <param name="b"> 开启/关闭围栏报警 </param>
        /// <param name="d"> </param>
        /// <param name="e"> </param>
        /// <param name="f"> </param>
        /// <param name="x"> 报警方式 </param>
        /// <param name="m"> 报警上报方式 </param>
        public Command_FENCE(ONOFF b, decimal d, decimal e, decimal f, decimal g, AlarmFunc? x = null, AlarmUploadFunc m = AlarmUploadFunc.仅GPRS)
        {
            CommandFenceShape = Command_FENCE_Shape.方形区域;
            D = d;
            E = e;
            F = f;
            G = g;
            X = x;
            M = m;
            B = b;
        }

        public override EV26Commands Command => EV26Commands.FENCE;

        public override string ToCommand()
        {
            return CommandFenceShape switch
            {
                Command_FENCE_Shape.圆形区域 =>
                    $"FENCE,{B.ToString()},{D.ToString(CultureInfo.InvariantCulture)},{E.ToString(CultureInfo.InvariantCulture)},{F.ToString(CultureInfo.InvariantCulture)},{X.ToString()},{M.ToValue()}",
                Command_FENCE_Shape.方形区域 =>
                    $"FENCE,{B.ToString()},{D.ToString(CultureInfo.InvariantCulture)},{E.ToString(CultureInfo.InvariantCulture)},{F.ToString(CultureInfo.InvariantCulture)},{G.ToString(CultureInfo.InvariantCulture)},{X.ToString()},{M.ToValue()}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}