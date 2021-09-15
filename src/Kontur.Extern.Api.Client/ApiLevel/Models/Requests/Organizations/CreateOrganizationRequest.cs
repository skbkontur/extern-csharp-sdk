using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CreateOrganizationRequest
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
        //[Required]
        public string Inn { get; set; } = null!;

        /// <summary>
        /// КПП организации
        /// </summary>
        public string? Kpp { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        //[Required]
        public string Name { get; set; } = null!;
    }
}