using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class UpdateOrganizationRequest
    {
        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; }
    }
}