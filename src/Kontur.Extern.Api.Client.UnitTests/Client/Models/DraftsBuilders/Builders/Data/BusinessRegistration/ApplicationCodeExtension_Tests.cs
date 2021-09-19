using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    public class ApplicationCodeExtension_Tests
    {
        [TestCaseSource(nameof(IndividualEntrepreneurCodes))]
        public void IsIndividualEntrepreneur_should_indicate_that_application_code_is_belong_to_individual_entrepreneur(ApplicationCode applicationCode)
        {
            var isIndividualEntrepreneurCode = applicationCode.IsIndividualEntrepreneur();
            
            isIndividualEntrepreneurCode.Should().BeTrue();
        }
        
        [TestCaseSource(nameof(LegalEntityCodes))]
        public void IsIndividualEntrepreneur_should_indicate_that_application_code_is_belong_to_legal_entity(ApplicationCode applicationCode)
        {
            var isIndividualEntrepreneurCode = applicationCode.IsIndividualEntrepreneur();
            
            isIndividualEntrepreneurCode.Should().BeFalse();
        }

        private static IEnumerable<ApplicationCode> IndividualEntrepreneurCodes =>
            Enum.GetValues<ApplicationCode>()
                .Where(x => x.ToString().Substring(1).StartsWith("2"));

        private static IEnumerable<ApplicationCode> LegalEntityCodes =>
            Enum.GetValues<ApplicationCode>()
                .Where(x => x.ToString().Substring(1).StartsWith("1"));
    }
}