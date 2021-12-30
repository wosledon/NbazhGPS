using System;
using System.IO;
using System.Text;
using System.Text.Json;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Extensions;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;
using NbazhGPS.Protocol.Internals;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// 序列化器
    /// </summary>
    public class NbazhGpsSerializer
    {
        private static readonly NbazhGpsPackage packet = new NbazhGpsPackage();
        private readonly NbazhGpsMsgIdFactory _nbazhGpsMsgIdFactory;
        private readonly NbazhGpsFormatterFactory _nbazhGpsFormatterFactory;
        /// <summary>
        /// 
        /// </summary>
        public NbazhGpsSerializer()
        {
            _nbazhGpsMsgIdFactory = new NbazhGpsMsgIdFactory();
            _nbazhGpsFormatterFactory = new NbazhGpsFormatterFactory();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nbazhGpsMsgIdFactory"></param>
        /// <param name="nbazhGpsFormatterFactory"></param>
        public NbazhGpsSerializer(NbazhGpsMsgIdFactory nbazhGpsMsgIdFactory, NbazhGpsFormatterFactory nbazhGpsFormatterFactory)
        {
            _nbazhGpsMsgIdFactory = nbazhGpsMsgIdFactory;
            _nbazhGpsFormatterFactory = nbazhGpsFormatterFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize(NbazhGpsPackage package, PackageType packageType = PackageType.Type1,
            int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackWriter writer = new NbazhGpsMessagePackWriter(buffer, package.PackageType);
                packet.Serialize(ref writer, package);
                return writer.FlushAndGetEncodingArray();
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="package"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan(NbazhGpsPackage package,
            PackageType packageType = PackageType.Type1, int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackWriter writer = new NbazhGpsMessagePackWriter(buffer, package.PackageType);
                packet.Serialize(ref writer, package);
                return writer.FlushAndGetEncodingReadOnlySpan();
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public NbazhGpsPackage Deserialize(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1,
            int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader reader = new NbazhGpsMessagePackReader(bytes, packageType);
                reader.Decode(buffer);
                return (NbazhGpsPackage)packet.Deserialize(ref reader);
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] Serialize<T>(T obj, PackageType packageType = PackageType.Type1, int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                _nbazhGpsFormatterFactory.FormatterDict.TryGetValue(typeof(T).GUID, out var formatter);
                NbazhGpsMessagePackWriter NbazhGpsMessagePackWriter = new NbazhGpsMessagePackWriter(buffer, packageType);
                ((INbazhGpsMessagePackageFormatter<T>)formatter)!.Serialize(ref NbazhGpsMessagePackWriter, obj);
                return NbazhGpsMessagePackWriter.FlushAndGetEncodingArray();
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public ReadOnlySpan<byte> SerializeReadOnlySpan<T>(T obj, PackageType packageType = PackageType.Type1, int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                _nbazhGpsFormatterFactory.FormatterDict.TryGetValue(typeof(T).GUID, out var formatter);
                NbazhGpsMessagePackWriter NbazhGpsMessagePackWriter = new NbazhGpsMessagePackWriter(buffer, packageType);
                ((INbazhGpsMessagePackageFormatter<T>)formatter)!.Serialize(ref NbazhGpsMessagePackWriter, obj);
                return NbazhGpsMessagePackWriter.FlushAndGetEncodingReadOnlySpan();
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public T Deserialize<T>(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                if (CheckPackageType(typeof(T)))
                    NbazhGpsMessagePackReader.Decode(buffer);
                INbazhGpsFormatterFactory factory = new NbazhGpsFormatterFactory();
                factory.FormatterDict.TryGetValue(typeof(T).GUID, out var formatter);
                return ((INbazhGpsMessagePackageFormatter<T>)formatter)!.Deserialize(ref NbazhGpsMessagePackReader);
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool CheckPackageType(Type type)
        {
            return type == typeof(NbazhGpsPackage) || type == typeof(NbazhGpsHeaderPackage);
        }

        /// <summary>
        /// 用于负载或者分布式的时候，在网关只需要解到头部。
        /// 根据头部的消息Id进行分发处理，可以防止小部分性能损耗。
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public NbazhGpsHeaderPackage HeaderDeserialize(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                NbazhGpsMessagePackReader.Decode(buffer);
                return new NbazhGpsHeaderPackage(ref NbazhGpsMessagePackReader);
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <param name="packageType"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public dynamic Deserialize(ReadOnlySpan<byte> bytes, Type type, PackageType packageType = PackageType.Type1, int minBufferSize = 4096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                _nbazhGpsFormatterFactory.FormatterDict.TryGetValue(type.GUID, out var formatter);
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                if (CheckPackageType(type))
                    NbazhGpsMessagePackReader.Decode(buffer);
                return NbazhGpsMessagePackFormatterResolverExtensions.NbazhDynamicDeserialize(formatter, ref NbazhGpsMessagePackReader);
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public string Analyze(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                NbazhGpsMessagePackReader.Decode(buffer);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                packet.Analyze(ref NbazhGpsMessagePackReader, utf8JsonWriter);
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public string Analyze<T>(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                if (CheckPackageType(typeof(T)))
                    NbazhGpsMessagePackReader.Decode(buffer);
                _nbazhGpsFormatterFactory.FormatterDict.TryGetValue(typeof(T).GUID, out var analyze);
                //var analyze = jT808Config.GetAnalyze<T>();
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteStartObject();
                ((INbazhGpsAnalyze)analyze)!.Analyze(ref NbazhGpsMessagePackReader, utf8JsonWriter);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                return value;
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 用于分包组合
        /// </summary>
        /// <param name="msgid">对应消息id</param>
        /// <param name="bytes">组合的数据体</param>
        /// <param name="packageType">对应版本号</param>
        /// <param name="options">序列化选项</param>
        /// <param name="minBufferSize">默认65535</param>
        /// <returns></returns>
        public string Analyze(byte msgid, ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, JsonWriterOptions options = default, int minBufferSize = 65535)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            
            try
            {
                if (_nbazhGpsMsgIdFactory.TryGetValue(msgid, out object msgHandle))
                {
                    if (_nbazhGpsFormatterFactory.FormatterDict.TryGetValue(msgHandle.GetType().GUID, out object instance))
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                        NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                        utf8JsonWriter.WriteStartObject();
                        ((INbazhGpsAnalyze)instance).Analyze(ref NbazhGpsMessagePackReader, utf8JsonWriter);
                        utf8JsonWriter.WriteEndObject();
                        utf8JsonWriter.Flush();
                        string value = Encoding.UTF8.GetString(memoryStream.ToArray());
                        return value;
                    }
                    return $"未找到对应的0x{msgid:X2}消息数据体类型";
                }
                return $"未找到对应的0x{msgid:X2}消息数据体类型";
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }

        /// <summary>
        /// 用于分包组合
        /// </summary>
        /// <param name="msgid">对应消息id</param>
        /// <param name="bytes">组合的数据体</param>
        /// <param name="packageType">对应版本号</param>
        /// <param name="options">序列化选项</param>
        /// <param name="minBufferSize">默认65535</param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer(byte msgid, ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, JsonWriterOptions options = default, int minBufferSize = 65535)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                if (_nbazhGpsMsgIdFactory.TryGetValue(msgid, out object msgHandle))
                {
                    if (_nbazhGpsFormatterFactory.FormatterDict.TryGetValue(msgHandle.GetType().GUID, out object instance))
                    {
                        using MemoryStream memoryStream = new MemoryStream();
                        using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                        NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                        utf8JsonWriter.WriteStartObject();
                        ((INbazhGpsAnalyze)instance)!.Analyze(ref NbazhGpsMessagePackReader, utf8JsonWriter);
                        utf8JsonWriter.WriteEndObject();
                        utf8JsonWriter.Flush();
                        return memoryStream.ToArray();
                    }
                    return Encoding.UTF8.GetBytes($"未找到对应的0x{msgid:X2}消息数据体类型");
                }
                return Encoding.UTF8.GetBytes($"未找到对应的0x{msgid:X2}消息数据体类型");
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                NbazhGpsMessagePackReader.Decode(buffer);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                packet.Analyze(ref NbazhGpsMessagePackReader, utf8JsonWriter);
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="packageType"></param>
        /// <param name="options"></param>
        /// <param name="minBufferSize"></param>
        /// <returns></returns>
        public byte[] AnalyzeJsonBuffer<T>(ReadOnlySpan<byte> bytes, PackageType packageType = PackageType.Type1, JsonWriterOptions options = default, int minBufferSize = 8096)
        {
            byte[] buffer = NbazhGpsArrayPool.Rent(minBufferSize);
            try
            {
                NbazhGpsMessagePackReader NbazhGpsMessagePackReader = new NbazhGpsMessagePackReader(bytes, packageType);
                if (CheckPackageType(typeof(T)))
                    NbazhGpsMessagePackReader.Decode(buffer);
                _nbazhGpsFormatterFactory.FormatterDict.TryGetValue(typeof(T).GUID, out var analyze);
                using MemoryStream memoryStream = new MemoryStream();
                using Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(memoryStream, options);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteStartObject();
                ((INbazhGpsAnalyze)analyze)!.Analyze(ref NbazhGpsMessagePackReader, utf8JsonWriter);
                if (!CheckPackageType(typeof(T))) utf8JsonWriter.WriteEndObject();
                utf8JsonWriter.Flush();
                return memoryStream.ToArray();
            }
            finally
            {
                NbazhGpsArrayPool.Return(buffer);
            }
        }
    }
}