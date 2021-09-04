using System.Text.Json;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}