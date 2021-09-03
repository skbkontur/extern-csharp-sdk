using System.Text.Json;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingPolicies
{
    public class KebabCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToKebabCase();
    }
}