using System.Text.Json.Serialization;
using static Kontur.Extern.Client.Auth.OpenId.Client.Models.ContractConstants;

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses
{
    public class ErrorResponse
    {
        [JsonPropertyName(ErrorModel.Error)]
        public string Error { get; set; }

        [JsonPropertyName(ErrorModel.ErrorDescription)]
        public string ErrorDescription { get; set; }

        [JsonPropertyName(ErrorModel.ErrorStatus)]
        public string ErrorStatus { get; set; }

        [JsonPropertyName(Multifactor.MultifactorInfo.PartialFactorToken)]
        public string PartialFactorToken { get; set; }

        [JsonPropertyName(Multifactor.MultifactorInfo.RequiredFactors)]
        public RequiredFactor[] RequiredFactors { get; set; }
    }
}
