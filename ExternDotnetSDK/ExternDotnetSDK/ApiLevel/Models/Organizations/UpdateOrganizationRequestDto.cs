using System.ComponentModel.DataAnnotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    public class UpdateOrganizationRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}