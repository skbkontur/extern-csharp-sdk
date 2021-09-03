using System.Text.Json;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies
{
    internal class AsIsNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name;
    }
}