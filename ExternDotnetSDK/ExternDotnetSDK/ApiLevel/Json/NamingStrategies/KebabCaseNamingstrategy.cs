using Newtonsoft.Json.Serialization;

namespace Kontur.Extern.Client.ApiLevel.Json.NamingStrategies
{
    public class KebabCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name) => ToKebabCase(name);

        private static string ToKebabCase(string str) => str.ToKebabCase();
    }
}