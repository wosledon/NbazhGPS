using System.Collections.Generic;

namespace NbazhGPS.Protocol.Interfaces
{
    /// <summary>
    /// 消息工厂接口
    /// </summary>
    public interface INbazhGpsMsgIdFactory: INbazhGpsExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<ushort, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryGetValue(ushort msgId, out object instance);
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNbazhGpsBodies"></typeparam>
        /// <returns></returns>
        INbazhGpsMsgIdFactory SetMap<TNbazhGpsBodies>() where TNbazhGpsBodies : NbazhGpsBodies;
    }
}