using System.Buffers;

namespace NbazhGPS.Protocol
{
    /// <summary>
    /// 内存池
    /// </summary>
    public class NbazhGpsArrayPool
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ArrayPool<byte> ArrayPool;

        static NbazhGpsArrayPool()
        {
            ArrayPool = ArrayPool<byte>.Create();
        }
        /// <summary>
        /// 申请
        /// </summary>
        /// <param name="minimumLength"></param>
        /// <returns></returns>
        public static byte[] Rent(int minimumLength)
        {
            return ArrayPool.Rent(minimumLength);
        }

        public static void Return(byte[] array, bool clearArray = false)
        {
            ArrayPool.Return(array, clearArray);
        }
    }
}