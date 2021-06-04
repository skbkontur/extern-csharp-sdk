using System.Net.Http;
using Kontur.Extern.Client.Clients.Authentication.Client.Models;
using Newtonsoft.Json;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Factories
{
    internal class ServiceErrorBuilder 
    {

        public TError BuildError<TError>(HttpContent content)
        {
            return typeof (TError) == typeof (EmptyError)
                ? default(TError)
                : JsonConvert.DeserializeObject<TError>(content.ToString());
        }
    }
}