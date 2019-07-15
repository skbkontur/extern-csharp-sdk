using System.ComponentModel.DataAnnotations;

namespace ExternDotnetSDK.Organizations
{
    class CreateOrganizationRequestDto
    {
        [Required]
        public string Inn { get; set; }
        public string Kpp { get; set; }
        [Required]
        public string Name { get; set; }
    }
}