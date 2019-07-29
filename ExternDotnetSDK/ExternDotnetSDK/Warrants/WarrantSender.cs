using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
{
    /// <summary>
    ///     Уполномоченный представитель, на которого выдана доверенность (отправитель)
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantSender
    {
        /// <summary>
        ///     Информация о частном лице или индивидуальном предпринимателе - уполномоченном представителе (отправителе)
        /// </summary>
        [CanBeNull]
        public WarrantIndividual SenderIndividual { get; set; }

        /// <summary>
        ///     Информация об организации - уполномоченном представителе (отправителе)
        /// </summary>
        [CanBeNull]
        public WarrantOrganization SenderOrganization { get; set; }
    }
}