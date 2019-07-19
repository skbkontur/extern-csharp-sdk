using System.ComponentModel.DataAnnotations;

namespace ExternDotnetSDK.Organizations
{
    public class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}