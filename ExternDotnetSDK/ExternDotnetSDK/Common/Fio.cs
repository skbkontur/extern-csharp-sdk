using ExternDotnetSDK.JsonConverters;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Common
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
    public class Fio
    {
        public string Surname { get; set; } = "Фамилия";
        public string Name { get; set; } = "Имя";
        public string Patronymic { get; set; } = "Отчество";
    }
}