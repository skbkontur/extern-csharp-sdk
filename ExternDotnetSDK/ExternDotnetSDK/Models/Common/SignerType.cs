using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Common
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum SignerType
    {
        Unknown = 0,
        OrganizationRepresentative,
        ThirdPartyRepresentative,
        ProviderRepresentative,
        CuRepresentative,
        CuLocalRepresentative
    }
}