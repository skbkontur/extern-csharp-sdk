using System.ComponentModel.DataAnnotations;

namespace ExternDotnetSDK.Organizations
{
    class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}