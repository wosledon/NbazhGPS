using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.Extensions
{
    /// <summary>
    /// 终端信息内容扩展
    /// </summary>
    public static class TerminalInfoExtensions
    {
        /// <summary>
        /// 将 byte 转为终端信息内容 0x13
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerminalInfo0X13 ToTerminalInfo0X13(this byte data)
        {
            return new TerminalInfo0X13().ToObject(data);
        }
        /// <summary>
        /// 将 byte 转为终端信息内容 0x26
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerminalInfo0X26 ToTerminalInfo0X26(this byte data)
        {
            return new TerminalInfo0X26().ToObject(data);
        }
    }
}