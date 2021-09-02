using System.Text.Json;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies
{
    internal class AsIsNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name;
    }
}