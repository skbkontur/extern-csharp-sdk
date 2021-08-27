using System.Text.Json;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies
{
    public class SnakeCaseNamingStrategy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToSnakeCase();
    }
}