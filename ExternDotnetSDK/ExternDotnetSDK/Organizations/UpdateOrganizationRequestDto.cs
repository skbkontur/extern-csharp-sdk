using System.ComponentModel.DataAnnotations;

namespace ExternDotnetSDK.Organizations
{
    internal class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}