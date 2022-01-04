namespace NbazhGPS.Protocol.Enums
{
    public enum InfoType: byte
    {
        外电电压 = 0x00,
        终端状态同步 = 0x04,
        边门状态 = 0x05,
        自检参数 = 0x08,
        定位卫星信息 = 0x09,
        ICCID信息 = 0x0A,
        巴西计价器 = 0x10
    }
}