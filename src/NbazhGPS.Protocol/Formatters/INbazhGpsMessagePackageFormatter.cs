using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.Formatters
{
    /// <summary>
    /// 序列化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INbazhGpsMessagePackageFormatter<T>: INbazhGpsFormatter
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        void Serialize(ref NbazhGpsMessagePackWriter writer, T value);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        T Deserialize(ref NbazhGpsMessagePackReader reader);
    }
}