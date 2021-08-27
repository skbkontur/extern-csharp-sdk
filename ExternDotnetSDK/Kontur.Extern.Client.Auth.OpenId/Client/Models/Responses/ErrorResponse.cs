using JetBrains.Annotations;

namespace Kontur.Extern.Client.Auth.OpenId.Client.Models.Responses
{
    [PublicAPI]
    public class ErrorResponse
    {
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
        public string ErrorStatus { get; set; }
        public string PartialFactorToken { get; set; }
        public RequiredFactor[] RequiredFactors { get; set; }
    }
}
