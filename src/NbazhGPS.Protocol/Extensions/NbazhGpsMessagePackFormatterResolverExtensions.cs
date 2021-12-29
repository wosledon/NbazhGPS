using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using NbazhGPS.Protocol.Buffers;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.Extensions
{
    /// <summary>
    /// 动态序列化扩展
    /// </summary>
    public static class NbazhGpsMessagePackFormatterResolverExtensions
    {
        delegate void NbazhSerializeMethod(object dynamicFormatter, ref NbazhGpsMessagePackWriter writer, object value);

        delegate dynamic NbazhDeserializeMethod(object dynamicFormatter, ref NbazhGpsMessagePackReader reader);

        static readonly ConcurrentDictionary<Type, (object Value, NbazhSerializeMethod SerializeMethod)> NbazhSerializers =
            new();

        static readonly ConcurrentDictionary<Type, (object Value, NbazhDeserializeMethod DeserializeMethod)> NbazhDeserializes =
            new();

        /// <summary>
        /// 动态序列化
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void NbazhDynamicSerialize(object objFormatter, ref NbazhGpsMessagePackWriter writer,
            object value)
        {
            Type type = value.GetType();
            var ti = type.GetTypeInfo();

            if (!NbazhSerializers.TryGetValue(type, out var formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(INbazhGpsMessagePackageFormatter<>).MakeGenericType(t);
                    var param0 = Expression.Parameter(typeof(object), "formatter");
                    var param1 = Expression.Parameter(typeof(NbazhGpsMessagePackWriter).MakeByRefType(), "writer");
                    var param2 = Expression.Parameter(typeof(object), "value"); 
                    var serializeMethodInfo = formatterType.GetRuntimeMethod("Serialize", new[] { typeof(NbazhGpsMessagePackWriter).MakeByRefType(), t});
                    var body = Expression.Call(
                        Expression.Convert(param0, formatterType),
                        serializeMethodInfo,
                        param1,
                        ti.IsValueType ? Expression.Unbox(param2, t) : Expression.Convert(param2, t));
                    var lambda = Expression.Lambda<NbazhSerializeMethod>(body, param0, param1, param2).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                NbazhSerializers.TryAdd(t, formatterAndDelegate);
            }
            formatterAndDelegate.SerializeMethod(formatterAndDelegate.Value, ref writer, value);
        }
        /// <summary>
        /// 动态反序列化
        /// </summary>
        /// <param name="objFormatter"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static dynamic NbazhDynamicDeserialize(object objFormatter, ref NbazhGpsMessagePackReader reader)
        {
            var type = objFormatter.GetType();
            //   (object Value, JT808DeserializeMethod DeserializeMethod) formatterAndDelegate;
            if (!NbazhDeserializes.TryGetValue(type, out var formatterAndDelegate))
            {
                var t = type;
                {
                    var formatterType = typeof(INbazhGpsMessagePackageFormatter<>).MakeGenericType(t);
                    ParameterExpression param0 = Expression.Parameter(typeof(object), "formatter");
                    ParameterExpression param1 = Expression.Parameter(typeof(NbazhGpsMessagePackReader).MakeByRefType(), "reader");
                    var deserializeMethodInfo = type.GetRuntimeMethod("Deserialize", new[] { typeof(NbazhGpsMessagePackReader).MakeByRefType()});
                    var body = Expression.Call(
                        Expression.Convert(param0, type),
                        deserializeMethodInfo,
                        param1);
                    var lambda = Expression.Lambda<NbazhDeserializeMethod>(body, param0, param1).Compile();
                    formatterAndDelegate = (objFormatter, lambda);
                }
                NbazhDeserializes.TryAdd(t, formatterAndDelegate);
            }
            return formatterAndDelegate.DeserializeMethod(formatterAndDelegate.Value, ref reader);
        }
    }
}