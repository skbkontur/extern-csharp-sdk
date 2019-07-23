using System;
using System.ComponentModel.DataAnnotations;
using ExternDotnetSDK.JsonConverters;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace ExternDotnetSDK.Test
{
    [JsonObject(NamingStrategyType = typeof(KebabCaseNamingStrategy))]
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