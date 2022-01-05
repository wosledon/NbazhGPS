using System.Diagnostics;
using NbazhGPS.Protocol.Enums;

namespace NbazhGPS.Protocol.Extensions
{
    public static class NbazhGpsMessageIdExtensions
    {
        public static ushort MsgNum = 0;

        public static NbazhGpsPackage Create<T>(this NbazhGpsMessageIds msgId, T body, PackageType type = PackageType.Type1)
            where T : NbazhGpsBodies
        {
            NbazhGpsPackage package = new NbazhGpsPackage(type)
            {
                Header = new NbazhGpsHeader(type)
                {
                    MsgId = (byte)msgId,
                    MsgNum = (MsgNum ^ 0xFFFF) == 0 ? MsgNum = 0 : MsgNum++,
                },
                Bodies = body
            };

            return package;
        }

        public static NbazhGpsPackage Create(this NbazhGpsMessageIds msgId, PackageType type = PackageType.Type1)
        {
            NbazhGpsPackage package = new NbazhGpsPackage(type)
            {
                Header = new NbazhGpsHeader(type)
                {
                    MsgId = (byte)msgId,
                    MsgNum = (MsgNum ^ 0xFFFF) == 0 ? MsgNum = 0 : MsgNum++,
                },
            };

            return package;
        }

        public static NbazhGpsPackage Create<T>(this byte msgId, T body, PackageType type = PackageType.Type1)
            where T : NbazhGpsBodies
        {
            NbazhGpsPackage package = new NbazhGpsPackage(type)
            {
                Header = new NbazhGpsHeader(type)
                {
                    MsgId = (byte)msgId,
                    MsgNum = (MsgNum ^ 0xFFFF) == 0 ? MsgNum = 0 : MsgNum++,
                },
                Bodies = body
            };

            return package;
        }

        public static NbazhGpsPackage Create<T>(this NbazhGpsMessageIds msgId,ushort msgNum, T body, PackageType type = PackageType.Type1)
            where T : NbazhGpsBodies
        {
            NbazhGpsPackage package = new NbazhGpsPackage(type)
            {
                Header = new NbazhGpsHeader(type)
                {
                    MsgId = (byte)msgId,
                    MsgNum = msgNum,
                },
                Bodies = body
            };

            return package;
        }

        public static NbazhGpsPackage Create(this NbazhGpsMessageIds msgId,ushort msgNum, PackageType type = PackageType.Type1)
        {
            NbazhGpsPackage package = new NbazhGpsPackage(type)
            {
                Header = new NbazhGpsHeader(type)
                {
                    MsgId = (byte)msgId,
                    MsgNum = msgNum
                },
            };

            return package;
        }

        public static NbazhGpsPackage Create<T>(this byte msgId, ushort msgNum, T body, PackageType type = PackageType.Type1)
            where T : NbazhGpsBodies
        {
            NbazhGpsPackage package = new NbazhGpsPackage(type)
            {
                Header = new NbazhGpsHeader(type)
                {
                    MsgId = (byte)msgId,
                    MsgNum = msgNum
                },
                Bodies = body
            };

            return package;
        }
    }
}