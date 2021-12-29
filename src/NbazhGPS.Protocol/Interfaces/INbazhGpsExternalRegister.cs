using System.Reflection;

namespace NbazhGPS.Protocol.Interfaces
{
    /// <summary>
    /// 外部注册
    /// </summary>
    public interface INbazhGpsExternalRegister
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalAssembly"></param>
        void Register(Assembly externalAssembly);
    }
}