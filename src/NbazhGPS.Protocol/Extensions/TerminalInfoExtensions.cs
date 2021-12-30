using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.Extensions
{
    /// <summary>
    /// 终端信息内容扩展
    /// </summary>
    public static class TerminalInfoExtensions
    {
        /// <summary>
        /// 将 byte 转为终端信息内容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TerminalInfo ToTerminalInfo(this byte data)
        {
            return new TerminalInfo().ToObject(data);
        }
    }
}