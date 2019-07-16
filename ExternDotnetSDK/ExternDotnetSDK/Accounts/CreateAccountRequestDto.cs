using System.ComponentModel.DataAnnotations;

namespace ExternDotnetSDK.Accounts
{
    public class CreateAccountRequestDto
    {
        [Required]
        public string Inn { get; set; }

        public string Kpp { get; set; }

        [Required]
        public string OrganizationName { get; set; }
    }
}