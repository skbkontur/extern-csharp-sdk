namespace Kontur.Extern.Client.ApiLevel.Json.NamingStrategies
{
    public class KebabCaseNamingStrategy : Newtonsoft.Json.Serialization.NamingStrategy
    {
        protected override string ResolvePropertyName(string name) => name.ToKebabCase();
    }
}