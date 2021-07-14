namespace Kontur.Extern.Client.Authentication.OpenId.Client.Models
{
    internal static class ContractConstants
    {
        public static class ErrorModel
        {
            public const string Error = "error";
            public const string ErrorStatus = "error_status";
            public const string ErrorDescription = "error_description";
        }

        public static class Multifactor
        {
            public static class MultifactorInfo
            {
                public const string PartialFactorToken = "partial_factor_token";
                public const string RequiredFactors = "required_factors";
            }

            public static class Factors
            {
                public const string GrantType = "grant_type";
                public const string Identity = "identity";
            }
        }

        public static class ClientAuthenticate
        {
            public const string ClientId = "client_id";
            public const string ClientSecret = "client_secret";
        }

        public static class CertificateAuthenticationRequest
        {
            public const string PublicKey = "public_key";
            public const string IgnoreUntrustedHeuristicError = "ignore_untrusted_heuristic_error";
            public const string IsFree = "free";
            public const string PartialFactorToken = Multifactor.MultifactorInfo.PartialFactorToken;
        }

        public static class CertificateAuthenticationResponse
        {
            public const string EncryptedKey = "encrypted_key";
            public const string TrustedThumbprints = "trusted_thumbprints";
        }

        public static class TokenRequest
        {
            public const string GrantType = "grant_type";
            public const string Scope = "scope";
        }

        public static class RefreshTokenRequest
        {
            public const string RefreshToken = "refresh_token";
        }

        public static class PasswordTokenRequest
        {
            public const string UserName = "username";
            public const string Password = "password";
            public const string PartialFactorToken = Multifactor.MultifactorInfo.PartialFactorToken;
        }

        public static class TrustedTokenRequest
        {
            public const string Token = "token";
        }

        public static class CertificateTokenRequest
        {
            public const string Thumbprint = "thumbprint";
            public const string DecryptedKey = "decrypted_key";
        }

        public static class TokenResponse
        {
            public const string AccessToken = "access_token";
            public const string ExpiresIn = "expires_in";
            public const string TokenType = "token_type";
            public const string RefreshToken = "refresh_token";
            public const string IdentityToken = "id_token";
        }

        public static class GrantTypes
        {
            public const string Password = "password";
            public const string RefreshToken = "refresh_token";
            public const string Certificate = "certificate";
            public const string Trusted = "trusted";
        }
    }
}