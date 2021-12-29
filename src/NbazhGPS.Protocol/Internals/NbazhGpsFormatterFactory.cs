using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NbazhGPS.Protocol.Formatters;
using NbazhGPS.Protocol.Interfaces;

namespace NbazhGPS.Protocol.Internals
{
    /// <summary>
    /// 
    /// </summary>
    public class NbazhGpsFormatterFactory: INbazhGpsFormatterFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<Guid, object> FormatterDict { get; }
        /// <summary>
        /// 
        /// </summary>
        public NbazhGpsFormatterFactory()
        {
            FormatterDict = new Dictionary<Guid, object>();
            Init(Assembly.GetExecutingAssembly());
        }

        private void Init(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(w => w.GetInterfaces().Contains(typeof(INbazhGpsFormatter))))
            {
                var implTypes = type.GetInterfaces();
                if (implTypes != null && implTypes.Length > 1)
                {
                    var firstType = implTypes.FirstOrDefault(f => f.Name == typeof(INbazhGpsMessagePackageFormatter<>).Name);
                    var genericImplType = firstType.GetGenericArguments().FirstOrDefault();
                    if (genericImplType != null)
                    {
                        if (!FormatterDict.ContainsKey(genericImplType.GUID))
                        {
                            FormatterDict.Add(genericImplType.GUID, Activator.CreateInstance(genericImplType));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TINbazhGpsFormatter"></typeparam>
        /// <returns></returns>
        public INbazhGpsFormatterFactory SetMap<TINbazhGpsFormatter>() where TINbazhGpsFormatter : INbazhGpsFormatter
        {
            Type type = typeof(INbazhGpsFormatter);
            if (!FormatterDict.ContainsKey(type.GUID))
            {
                FormatterDict.Add(type.GUID, Activator.CreateInstance(type));
            }
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalAssembly"></param>
        public void Register(Assembly externalAssembly)
        {
            Init(externalAssembly);
        }
    }
}