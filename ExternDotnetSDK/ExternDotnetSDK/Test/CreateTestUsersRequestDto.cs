using System.ComponentModel.DataAnnotations;

namespace ExternDotnetSDK.Test
{
    public class CreateTestUsersRequestDto
    {
        [Required]
        public string Phone { get; set; }

        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string OrganizationName { get; set; }
    }
}