using System.ComponentModel.DataAnnotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    public class CreateOrganizationRequestDto
    {
        [Required]
        public string Inn { get; set; }

        public string Kpp { get; set; }

        [Required]
        public string Name { get; set; }
    }
}