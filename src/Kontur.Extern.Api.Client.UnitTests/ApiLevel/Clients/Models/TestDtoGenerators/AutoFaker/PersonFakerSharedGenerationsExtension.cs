using Bogus;
using Kontur.Extern.Api.Client.Models.Common;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker
{
    internal static class PersonFakerSharedGenerationsExtension
    {
        public static PersonFullName PersonFullName(this Person personFaker) => 
            new(personFaker.LastName, personFaker.FirstName, personFaker.LastName);
    }
}