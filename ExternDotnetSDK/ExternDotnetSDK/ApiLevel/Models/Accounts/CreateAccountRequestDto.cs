using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Accounts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CreateAccountRequestDto
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
//        [Required]
        public string Inn { get; set; }
        /// <summary>
        /// КПП организации
        /// </summary>
        public string Kpp { get; set; }
        /// <summary>
        /// Название организации
        /// </summary>
  //      [Required]
        public string OrganizationName { get; set; }
    }
}