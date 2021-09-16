using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Accounts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class CreateAccountRequest
    {
        /// <summary>
        /// ИНН организации
        /// </summary>
//        [Required]
        public string Inn { get; set; } = null!;
        
        /// <summary>
        /// КПП организации
        /// </summary>
        public Kpp? Kpp { get; set; }
        
        /// <summary>
        /// Название организации
        /// </summary>
  //      [Required]
        public string OrganizationName { get; set; } = null!;
    }
}