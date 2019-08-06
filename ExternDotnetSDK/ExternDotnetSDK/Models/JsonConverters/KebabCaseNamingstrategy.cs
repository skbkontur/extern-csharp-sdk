using Newtonsoft.Json.Serialization;

namespace ExternDotnetSDK.Models.JsonConverters
{
    public class KebabCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            return ToKebabCase(name);
        }

        private static string ToKebabCase(string str)
        {
            return str.ToKebabCase();
        }
    }
}