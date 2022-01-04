namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public class DataContent0X94Enums
    {
        public class DataContent05Enums
        {
            public enum IOState : byte
            {
                高 = 1 << 2,
                低 = 0
            }

            public enum TriggerState : byte
            {
                高触发 = 1 << 1,
                低触发 = 0
            }

            public enum DoorState : byte
            {
                开启 = 1,
                关闭 = 0
            }
        }

        public class DataContent09Enums
        {
            public enum GpsModuleState: byte
            {
                没有此功能 = 0x00,
                搜星 = 0x01,
                _2D定位 = 0x02,
                _3D定位 = 0x03,
                休眠 = 0x04
            }
        }
    }
}