using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Authentication.OpenId.Client;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Requests;
using Kontur.Extern.Client.Authentication.OpenId.Client.Models.Responses;
using Kontur.Extern.Client.Authentication.OpenId.Provider.Models;
using Kontur.Extern.Client.Cryptography;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.AuthStrategies
{
    internal class CertificateOpenIdAuthenticationStrategy : IOpenIdAuthenticationStrategy
    {
        private readonly CertificateCredentials credentials;
        private readonly ICrypt cryptoProvider;

        public CertificateOpenIdAuthenticationStrategy(CertificateCredentials credentials, ICrypt cryptoProvider)
        {
            this.credentials = credentials;
            this.cryptoProvider = cryptoProvider;
        }

        public async Task<TokenResponse> AuthenticateAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout = null)
        {
            var authResponse = await AuthorizeByPublicKeyAsync(openId, options, timeout).ConfigureAwait(false);
            return await AuthorizeByHandshakeSecretAsync(authResponse, openId, options, timeout).ConfigureAwait(false);
        }

        private Task<TokenResponse> AuthorizeByHandshakeSecretAsync(CertificateAuthenticationResponse authResponse, IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout)
        {
            var request = new CertificateTokenRequest(
                cryptoProvider.Decrypt(authResponse.EncryptedKey),
                credentials.PublicKey.Thumbprint,
                options.Scope,
                options.ClientId,
                options.ApiKey);
            return openId.RequestTokenAsync(request, timeout);
        }

        private Task<CertificateAuthenticationResponse> AuthorizeByPublicKeyAsync(IOpenIdClient openId, OpenIdAuthenticationOptions options, TimeSpan? timeout)
        {
            var request = new CertificateAuthenticationRequest(
                credentials.PublicKey,
                credentials.Free,
                options.Scope,
                options.ClientId,
                options.ApiKey);
            return openId.CertificateAuthenticationAsync(request, timeout);
        }
    }
}