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
        public NbazhGpsMsgIdFactory()
        {
            Map = new Dictionary<ushort, object>();
            InitMap(Assembly.GetExecutingAssembly());
        }

        private void InitMap(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(w => w.BaseType == typeof(NbazhGpsBodies)).ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                ushort msgId = 0;
                try
                {
                    msgId = (ushort)type.GetProperty(nameof(NbazhGpsBodies.MsgId))!.GetValue(instance);
                }
                catch (Exception ex)
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
        public IDictionary<ushort, object> Map { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool TryGetValue(ushort msgId, out object instance)
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
            var msgId = (ushort)type.GetProperty(nameof(NbazhGpsBodies.MsgId))!.GetValue(instance);
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