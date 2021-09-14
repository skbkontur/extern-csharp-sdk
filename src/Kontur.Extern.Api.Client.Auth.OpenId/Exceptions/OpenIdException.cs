using System;
using System.Runtime.Serialization;
using System.Text;
using Kontur.Extern.Api.Client.Auth.OpenId.Client.Models.Responses;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Exceptions
{
    [Serializable]
    public class OpenIdException : Exception
    {
        public OpenIdException(string message)
            : base(message)
        {
        }

        public OpenIdException(ErrorResponse errorResponse)
            : this(GetMessage(errorResponse))
        {
            
        }

        public OpenIdException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OpenIdException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        private static string GetMessage(ErrorResponse errorResponse)
        {
            if (string.IsNullOrWhiteSpace(errorResponse.Error))
                return "Unknown error";

            var errorText = new StringBuilder();
            errorText.Append(errorResponse.Error);
            if (!string.IsNullOrWhiteSpace(errorResponse.ErrorStatus))
            {
                errorText.Append(". ").Append(errorResponse.ErrorStatus);
            }
            if (!string.IsNullOrWhiteSpace(errorResponse.ErrorDescription))
            {
                errorText.Append(". ").Append(errorResponse.ErrorDescription);
            }

            if (!string.IsNullOrWhiteSpace(errorResponse.PartialFactorToken))
            {
                errorText.Append(". ").Append(errorResponse.PartialFactorToken);
            }

            if (errorResponse.RequiredFactors is {Length: > 0} factors)
            {
                foreach (var requiredFactor in factors)
                {
                    errorText.AppendLine().Append(requiredFactor.GrantType).Append(": ").Append(requiredFactor.Identity);
                }
            }

            errorText.Append(".");

            return errorText.ToString();
        }
    }
}