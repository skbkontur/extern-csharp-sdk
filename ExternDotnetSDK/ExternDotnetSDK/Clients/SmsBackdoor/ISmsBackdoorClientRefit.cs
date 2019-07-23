using System.Threading.Tasks;
using Refit;

namespace ExternDotnetSDK.Clients.SmsBackdoor
{
    internal interface ISmsBackdoorClientRefit
    {
        // ReSharper disable once StringLiteralTypo
        [Get("/v1/get-confirmationcode?requestId={requestId}")]
        Task<string> GetConfirmationCode(string requestId);
    }
}