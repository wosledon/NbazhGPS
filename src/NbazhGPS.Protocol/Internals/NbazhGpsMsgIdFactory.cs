using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NbazhGPS.Protocol.Interfaces;

namespace NbazhGPS.Protocol.Internals
{
    /// <summary>
    /// 消息工厂
    /// </summary>
    public class NbazhGpsMsgIdFactory: INbazhGpsMsgIdFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public NbazhGpsMsgIdFactory()
        {
            Map = new Dictionary<byte, object>();
            InitMap(Assembly.GetExecutingAssembly());
        }

        private void InitMap(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(w => w.BaseType == typeof(NbazhGpsBodies)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                byte msgId = 0;
                try
                {
                    msgId = (byte)type.GetProperty(nameof(NbazhGpsBodies.MsgId))!.GetValue(instance);
                }
                // catch (Exception ex)
                catch
                {
                    continue;
                }
                if (Map.ContainsKey(msgId))
                {
                    throw new ArgumentException($"{type.FullName} {msgId} An element with the same key already exists.");
                }
                else
                {
                    Map.Add(msgId, instance);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalAssembly"></param>
        public void Register(Assembly externalAssembly)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<byte, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool TryGetValue(byte msgId, out object instance)
        {
            return Map.TryGetValue(msgId, out instance);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNbazhGpsBodies"></typeparam>
        /// <returns></returns>
        public INbazhGpsMsgIdFactory SetMap<TNbazhGpsBodies>() where TNbazhGpsBodies : NbazhGpsBodies
        {
            Type type = typeof(TNbazhGpsBodies);
            var instance = Activator.CreateInstance(type);
            var msgId = (byte)type.GetProperty(nameof(NbazhGpsBodies.MsgId))!.GetValue(instance);
            if (Map.ContainsKey(msgId))
            {
                throw new ArgumentException($"{type.FullName} {msgId} An element with the same key already exists.");
            }
            else
            {
                Map.Add(msgId, instance);
            }
            return this;
        }
    }
}