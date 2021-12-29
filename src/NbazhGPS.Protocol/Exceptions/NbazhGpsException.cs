using System;
using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Exceptions
{
    /// <summary>
    /// 异常处理
    /// </summary>
    [Serializable]
    public class NbazhGpsException: Exception
    {
        /// <summary>
        /// 统一错误码
        /// </summary>
        public NbazhGpsErrorCode ErrorCode { get; }
        public NbazhGpsException(NbazhGpsErrorCode errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }

        public NbazhGpsException(NbazhGpsErrorCode errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public NbazhGpsException(NbazhGpsErrorCode errorCode, Exception ex) : base(ex.Message, ex)
        {
            ErrorCode = errorCode;
        }

        public NbazhGpsException(NbazhGpsErrorCode errorCode, string message, Exception ex) : base(message, ex)
        {
            ErrorCode = errorCode;
        }
    }
}