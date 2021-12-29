using System.Text.Json;
using NbazhGPS.Protocol.MessagePack;

namespace NbazhGPS.Protocol.Interfaces
{
    /// <summary>
    /// 分析器
    /// </summary>
    public interface INbazhGpsAnalyze
    {
        void Analyze(ref NbazhGpsMessagePackReader reader, Utf8JsonWriter writer);
    }
}