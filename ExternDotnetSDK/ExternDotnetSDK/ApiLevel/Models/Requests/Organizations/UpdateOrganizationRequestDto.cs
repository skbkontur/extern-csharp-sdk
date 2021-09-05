using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Requests.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class UpdateOrganizationRequestDto
    {
        /// <summary>
        /// Название организации
        /// </summary>
        public string Name { get; set; }
    }
}