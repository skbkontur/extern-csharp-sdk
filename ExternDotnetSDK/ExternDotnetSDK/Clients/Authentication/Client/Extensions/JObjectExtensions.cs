using System.Collections.Generic;
using System.Security.Claims;
using Newtonsoft.Json.Linq;

namespace Kontur.Extern.Client.Clients.Authentication.Client.Extensions
{
    internal static class JObjectExtensions
    {
        public static IEnumerable<Claim> ToClaims(this JObject json)
        {
            foreach (var x in json)
            {
                if (x.Value is JArray array)
                {
                    foreach (var item in array)
                    {
                        yield return new Claim(x.Key, item.ToString());
                    }
                }
                else
                {
                    yield return new Claim(x.Key, x.Value.ToString());
                }
            }
        }
    }
}
