using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss
{
    [PublicAPI]
    public class FssSedoInsuredPersonRegistrationResultDescription : FssSedoDescription
    {
        /// <summary>
        /// СНИЛС
        /// </summary>
        public string? Snils { get; set; }
    }
}