using System.Net.Http;
using System.Threading.Tasks;
using Refit;

namespace ExternDotnetSDK.Clients.SmsBackdoor
{
    public class SmsBackdoorClient
    {
        private readonly ISmsBackdoorClientRefit clientRefit;

        public SmsBackdoorClient(
            HttpClient client) =>
            clientRefit = RestService.For<ISmsBackdoorClientRefit>(client);

        public async Task<string> GetConfirmationCodeAsync(string requestId)
            => await clientRefit.GetConfirmationCode(requestId);
    }
}