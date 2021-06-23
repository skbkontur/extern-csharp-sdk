using Newtonsoft.Json;
using static Kontur.Extern.Client.Clients.Authentication.Client.Models.ClientConstants;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Models
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

        public static ErrorResponse Create(string error, string errorStatus, string description = null)
        {
            return new ErrorResponse
            {
                ErrorDescription = description, 
                Error = error,
                ErrorStatus = errorStatus
            };
        }
    }
}
