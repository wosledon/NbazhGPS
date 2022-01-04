using System;
using System.Linq;
using NbazhGPS.Protocol.Enums;
using NbazhGPS.Protocol.Models;

namespace NbazhGPS.Protocol.Extensions
{
    public static class DataContent0X94Extensions
    {
        public static DataContent0X94Models.DataContent05 ToObject(this byte data)
        {
            return new DataContent0X94Models.DataContent05()
            {
                IOState = (DataContent0X94Enums.DataContent05Enums.IOState)(data & (1 << 2)),
                TriggerState = (DataContent0X94Enums.DataContent05Enums.TriggerState)(data & (1 << 1)),
                DoorState = (DataContent0X94Enums.DataContent05Enums.DoorState)(data & 1)
            };
        }

        public static byte ToByte(this DataContent0X94Models.DataContent05 data)
        {
            return (byte)(data.IOState.ToByteValue() | data.TriggerState.ToByteValue() | data.DoorState.ToByteValue());
        }

        public static Span<byte> ToSpan(this DataContent0X94Models.SatelliteState data)
        {
            var res = new byte[] { data.ModuleState.ToByteValue() }
                .Concat(new[] { data.LocatedStars.ToByteValue() });
            if (data.Strengths != null)
                res.Concat(data.Strengths);
            res.Concat(new[] { data.VisibleNotParticipateStars.ToByteValue() });
            if (data.VisibleStrengths != null)
                res.Concat(data.VisibleStrengths);

            return res.ToArray().AsSpan();
        }

        public static DataContent0X94Models.SatelliteState ToObject(this byte[] buffer)
        {
            return new DataContent0X94Models.SatelliteState()
            {
                ModuleState = (DataContent0X94Enums.DataContent09Enums.GpsModuleState)buffer[0],
                LocatedStars = buffer[1],
                Strengths = buffer.AsSpan().Slice(2, buffer[1]).ToArray(),
                VisibleNotParticipateStars = buffer[2 + (buffer[1])],
                VisibleStrengths = buffer.AsSpan().Slice(2 + buffer[1], buffer[2 + (buffer[1])]).ToArray()
            };
        }

        public static DataContent0X94Models.SatelliteState ToObject(this ReadOnlySpan<byte> buffer)
        {
            return new DataContent0X94Models.SatelliteState()
            {
                ModuleState = (DataContent0X94Enums.DataContent09Enums.GpsModuleState)buffer[0],
                LocatedStars = buffer[1],
                Strengths = buffer.Slice(2, buffer[1]).ToArray(),
                VisibleNotParticipateStars = buffer[2 + (buffer[1])],
                VisibleStrengths = buffer.Slice(2 + buffer[1], buffer[2 + (buffer[1])]).ToArray()
            };
        }

        public static DataContent0X94Models.DataContent09 ToDataContent09(this ReadOnlySpan<byte> buffer)
        {
            var gps = buffer.Slice(0, 1 + buffer[1]);
            var beidou = buffer.Slice(gps.Length, buffer[gps.Length + 1]);
            var extenLen = buffer[beidou.Length + gps.Length - 1];
            var extension = buffer.Slice(beidou.Length + gps.Length + 1);
            return new DataContent0X94Models.DataContent09()
            {
                Gps = new DataContent0X94Models.SatelliteState().ToObject(gps),
                BeiDou = new DataContent0X94Models.SatelliteState().ToObject(beidou),
                ExtensionLength = extenLen,
                ExtensionBytes = extension.ToArray()
            };
        }
    }
}