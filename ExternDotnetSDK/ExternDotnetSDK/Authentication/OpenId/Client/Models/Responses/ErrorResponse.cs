using Newtonsoft.Json;
using static Kontur.Extern.Client.Authentication.OpenId.Client.Models.ContractConstants;

namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses
{
    public class ErrorResponse
    {
        [JsonProperty(ErrorModel.Error)]
        public string Error { get; set; }

        [JsonProperty(ErrorModel.ErrorDescription)]
        public string ErrorDescription { get; set; }

        [JsonProperty(ErrorModel.ErrorStatus)]
        public string ErrorStatus { get; set; }

        [JsonProperty(Multifactor.MultifactorInfo.PartialFactorToken)]
        public string PartialFactorToken { get; set; }

        [JsonProperty(Multifactor.MultifactorInfo.RequiredFactors)]
        public RequiredFactor[] RequiredFactors { get; set; }
    }
}
