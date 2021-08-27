namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Info
{
    public class UlInfo
    {
        /// <summary>
        /// ОГРН
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Ogrn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}