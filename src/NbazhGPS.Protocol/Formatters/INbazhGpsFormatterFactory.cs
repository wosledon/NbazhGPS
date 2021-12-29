using System;
using System.Collections;
using System.Collections.Generic;
using NbazhGPS.Protocol.Interfaces;

namespace NbazhGPS.Protocol.Formatters
{
    /// <summary>
    /// 
    /// </summary>
    public interface INbazhGpsFormatterFactory: INbazhGpsExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        IDictionary<Guid, object> FormatterDict { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TINbazhGpsFormatter"></typeparam>
        /// <returns></returns>
        INbazhGpsFormatterFactory SetMap<TINbazhGpsFormatter>()
            where TINbazhGpsFormatter : INbazhGpsFormatter;
    }
}