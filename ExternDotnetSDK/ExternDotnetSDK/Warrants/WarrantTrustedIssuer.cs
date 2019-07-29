using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Warrants
{
    /// <summary>
    ///     Представитель, который выдал доверенность (передоверитель)
    /// </summary>
    [PublicAPI]
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public class WarrantTrustedIssuer
    {
        /// <summary>
        ///     Тип представителя
        /// </summary>
        public TrustedIssuerType Type { get; set; }

        /// <summary>
        ///     Информация о частном лице или индивидуальном предпринимателе - представителе, выдавшем доверенность
        /// </summary>
        [CanBeNull]
        public WarrantIndividual TrustedIssuerIndividual { get; set; }

        /// <summary>
        ///     Информация об организации - представителе, выдавшем доверенность
        /// </summary>
        [CanBeNull]
        public TrustedIssuerOrganization TrustedIssuerOrganization { get; set; }
    }
}