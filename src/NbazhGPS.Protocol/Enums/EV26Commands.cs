namespace NbazhGPS.Protocol.Enums
{
    /// <summary>
    /// EV26功能指令表
    /// </summary>
    public enum EV26Commands
    {
        /// <summary>
        /// 版本查询
        /// </summary>
        VERSION = 0x1001,

        /// <summary>
        /// 查询参数设置
        /// </summary>
        PARAM = 0x1002,

        /// <summary>
        /// 精简参数查询
        /// </summary>
        SCXSZ = 0x1003,

        /// <summary>
        /// 查询GPRS参数
        /// </summary>
        GPRSSET = 0x1004,

        /// <summary>
        /// 查询状态
        /// </summary>
        STATUS = 0x1005,

        /// <summary>
        /// 地址查询
        /// </summary>
        DW = 0x1006,

        /// <summary>
        /// 围栏状态查询
        /// </summary>
        FENCE = 0x1007,

        /// <summary>
        /// 位移状态查询
        /// </summary>
        MOVING = 0x1008,

        //======================
        /// <summary>
        /// 恢复出厂设置
        /// </summary>
        FACTORY = 0x2001,

        /// <summary>
        /// SOS设置
        /// </summary>
        SOS = 0x2002,

        /// <summary>
        /// 北斗模块,数据定时发送间隔
        /// </summary>
        TIMER = 0x2003,

        /// <summary>
        /// 延时设防设置
        /// </summary>
        DEFEMSE = 0x2004,

        /// <summary>
        /// 撤防指令
        /// </summary>
        DSRESET = 0x2005,

        /// <summary>
        /// 设置围栏报警
        /// </summary>
        FENCE_SET = 0x2006,

        /// <summary>
        /// 震动报警设置指令
        /// </summary>
        SENALM = 0x2007,

        /// <summary>
        /// 外部电源低电报警
        /// </summary>
        EXBATALM = 0x2008,

        /// <summary>
        /// 外部低电保护电压提醒
        /// </summary>
        EXBATCUT = 0x2009,

        /// <summary>
        /// 位移报警设置
        /// </summary>
        MOVING_SET = 0x200A,

        /// <summary>
        /// 超速提醒设置
        /// </summary>
        SPEED = 0x200B,

        /// <summary>
        /// 手动设防
        /// </summary>
        C111 = 0x200C,

        /// <summary>
        /// 手动撤防
        /// </summary>
        C000 = 0x200D,

        /// <summary>
        /// 设防模式
        /// </summary>
        DEFMODE = 0x200E,

        /// <summary>
        /// 激活北斗模块指令
        /// </summary>
        GPSON = 0x200F,

        /// <summary>
        /// 点名上报LBS位置
        /// </summary>
        LBS = 0x2010,

        /// <summary>
        /// 低电保护触发飞行模式设置
        /// </summary>
        FLYCUT = 0x2011,

        /// <summary>
        /// 变速报警
        /// </summary>
        SPEEDCHECK = 0x2012,

        /// <summary>
        /// 急转弯报警
        /// </summary>
        SWERVE = 0x2013,

        /// <summary>
        /// 碰撞报警
        /// </summary>
        COLLIDE = 0x2014,

        /// <summary>
        /// 侧翻报警
        /// </summary>
        ROLLVER = 0x2015,

        /// <summary>
        /// LBS传点设置
        /// </summary>
        LBSON = 0x2016,

        /// <summary>
        /// ICCID号点名查询
        /// </summary>
        ICCID = 0x2017,
    }
}