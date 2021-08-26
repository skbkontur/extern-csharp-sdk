using System.Text.Json;

namespace Kontur.Extern.Client.ApiLevel.Json.NamingStrategies
{
    public class _KebabCaseNamingStrategy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToKebabCase();
    }
}