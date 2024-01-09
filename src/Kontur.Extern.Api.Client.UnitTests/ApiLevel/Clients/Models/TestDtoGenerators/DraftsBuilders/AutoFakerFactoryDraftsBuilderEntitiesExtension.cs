using System.Linq;
using Bogus;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using Kontur.Extern.Api.Client.Testing.Generators;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftsBuilders
{
    internal static class AutoFakerFactoryDraftsBuilderEntitiesExtension
    {
        public static AutoFakerFactory AddDraftsBuilderEntitiesGeneration(this AutoFakerFactory autoFakerFactory)
        {
            return autoFakerFactory.AddConfiguration((builder, codesGenerator) =>
            {
                builder.RuleForType(x => new RegistrationInfo(
                    x.Make(3, () => RandomApplicant(x, codesGenerator)).ToArray(),
                    null,
                    RandomLegalEntityInfo(x, codesGenerator),
                    ApplicationCode.Р11001
                ));
            });
            
            static UlInfo RandomLegalEntityInfo(Faker faker, AuthoritiesCodesGenerator codesGenerator) =>
                new(codesGenerator.Ogrn(), faker.Company.CompanyName());
            
            static ApplicantInfo RandomApplicant(Faker faker, AuthoritiesCodesGenerator codesGenerator) => 
                new(codesGenerator.PersonInn(), faker.Person.PersonFullName(), faker.Internet.Email());
        }
    }
}