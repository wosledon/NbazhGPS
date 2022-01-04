#nullable enable
using System;
using System.Collections.Generic;
using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Models
{
    public class DataContent0X94Models
    {
        public class DataContent00
        {
            public double Voltage { get; set; }
        }

        public class DataContent04
        {
            public string? SourceData { get; set; }

            public Dictionary<string, string>? Data => GetData();

            private Dictionary<string, string>? GetData()
            {
                if (SourceData is "" or null)
                {
                    return null;
                }

                var res = new Dictionary<string, string>();

                var data = SourceData.Split(';');
                foreach (var item in data)
                {
                    var child = item.Split('=');
                    res.Add(child[0], child[1]);
                }

                return res;
            }
        }

        public class DataContent05
        {
            public DataContent0X94Enums.DataContent05Enums.IOState IOState { get; set; }
            public DataContent0X94Enums.DataContent05Enums.TriggerState TriggerState { get; set; }

            public DataContent0X94Enums.DataContent05Enums.DoorState DoorState { get; set; }
        }

        public class DataContent09
        {
            public SatelliteState Gps { get; set; } = null!;
            public SatelliteState BeiDou { get; set; } = null!;

            public byte ExtensionLength { get; set; } = 0x00;

            public byte[]? ExtensionBytes
            {
                get => (ExtensionLength == 0) ? null : ExtensionBytes;
                set => ExtensionBytes = value;
            }
        }

        public class SatelliteState
        {
            public DataContent0X94Enums.DataContent09Enums.GpsModuleState ModuleState { get; set; }
            public byte LocatedStars { get; set; }
            public byte[]? Strengths { get; set; }
            public byte VisibleNotParticipateStars { get; set; }
            public byte[]? VisibleStrengths { get; set; }

            public SatelliteState ToObject(ReadOnlySpan<byte> buffer)
            {
                ModuleState = (DataContent0X94Enums.DataContent09Enums.GpsModuleState)buffer[0];
                LocatedStars = buffer[1];
                Strengths = buffer.Slice(2, buffer[1]).ToArray();
                VisibleNotParticipateStars = buffer[2 + (buffer[1])];
                VisibleStrengths = buffer.Slice(2 + buffer[1], buffer[2 + (buffer[1])]).ToArray();

                return this;
            }
        }

        public class DataContent0A
        {
            /// <summary>
            /// BCD 16
            /// </summary>
            public string IMEI { get; set; } = null!;
            /// <summary>
            /// BCD 16
            /// </summary>
            public string IMSI { get; set; } = null!;
            /// <summary>
            /// BCD 20
            /// </summary>
            public string ICCID { get; set; } = null!;
        }

        public class DataContent10
        {
            public string? BrazilianMeter { get; set; }
        }
    }
}