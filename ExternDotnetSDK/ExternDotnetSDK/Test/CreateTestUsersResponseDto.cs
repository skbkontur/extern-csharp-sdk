using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Test
{
    public class CreateTestUsersResponseDto
    {
        public CreateTestUsersResponseDto(CreateTestUsersRequestDto testUserRequisites, Guid portalUserId)
        {
            Phone = testUserRequisites.Phone;
            Inn = testUserRequisites.Inn;
            Kpp = testUserRequisites.Kpp;
            FirstName = testUserRequisites.FirstName;
            Surname = testUserRequisites.Surname;
            Patronymic = testUserRequisites.Patronymic;
            OrganizationName = testUserRequisites.OrganizationName;
            PortalUserId = portalUserId;
        }

        [UsedImplicitly]
        public CreateTestUsersResponseDto()
        {
        }

        [Required]
        public string Phone { get; set; }

        public string Inn { get; set; }
        public string Kpp { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string OrganizationName { get; set; }
        public Guid PortalUserId { get; set; }
    }
}