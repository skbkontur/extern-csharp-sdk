using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Client.Http.Serialization.SysTextJson.NamingStrategies
{
    public class KebabCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name) => ToKebabCase(name);

        private static string ToKebabCase(string str) => str.ToKebabCase();
    }
}