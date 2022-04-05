using System;
using System.Collections.Generic;
using System.Net;
using AutoBogus;
using AutoBogus.Conventions;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Common.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Documents.Enums;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.Models.Numbers.BusinessRegistration;
using Kontur.Extern.Api.Client.Testing.Generators;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers;
using Kontur.Extern.Api.Client.UnitTests.TestHelpers.BogusExtensions;
using Randomizer = Bogus.Randomizer;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker
{
    internal class AutoFakerFactory
    {
        private const string PatternFor13Digits = "#############";
        private const string PatternFor15Digits = "###############";
        private const string PatternFor4Digits = "####";

        public static IAutoFaker CreateFaker() => new AutoFakerFactory().Create();

        private readonly AuthoritiesCodesGenerator codesGenerator;
        private readonly List<Action<IAutoGenerateConfigBuilder, AuthoritiesCodesGenerator>> additionalConfigurations = new();

        public AutoFakerFactory()
        {
            var random = new Random(1234);
            Randomizer.Seed = random;
            codesGenerator = new AuthoritiesCodesGenerator(random);
        }

        public AutoFakerFactory AddConfiguration(Action<IAutoGenerateConfigBuilder, AuthoritiesCodesGenerator> additionalConfiguration)
        {
            additionalConfigurations.Add(additionalConfiguration);
            return this;
        }

        public IAutoFaker Create() => AutoBogus.AutoFaker.Create(builder =>
        {
            DefaultConfiguration(builder);
            foreach (var configuration in additionalConfigurations)
            {
                configuration(builder, codesGenerator);
            }
        });

        private void DefaultConfiguration(IAutoGenerateConfigBuilder builder)
        {
            builder.WithConventions(x => x.Semver.Aliases("Version"));
            builder.WithConventions(x => x.FirstName.Aliases("Name", "FirstName", "Firstname"));
            builder.WithConventions(x => x.LastName.Aliases("LastName", "SurName", "Surname"));
            builder.WithConventions(x => x.FirstName.Aliases("Patronymic"));

            builder.RuleForPropNameOf("okpo", _ => codesGenerator.LegalEntityOkpo().ToString());
            builder.RuleForPropNameOf("okud", _ => codesGenerator.Okud().ToString());
            builder.RuleForPropNameOf("ogrn", x => x.Random.ReplaceNumbers(PatternFor13Digits));
            builder.RuleForPropNameOf("ogrnIp", x => x.Random.ReplaceNumbers(PatternFor15Digits));
            builder.RuleForPropNameOf("Knd", _ => codesGenerator.Knd().ToString());
            builder.RuleForPropNameOf("SenderInn", _ => codesGenerator.LegalEntityInn().ToString());
            builder.RuleForPropNameOf("Inn", _ => codesGenerator.LegalEntityInn().ToString());
            builder.RuleForPropNameOf("FssCode", _ => codesGenerator.FssCode().ToString());
            builder.RuleForPropNameOf("upfrCode", _ => codesGenerator.UpfrCode().ToString());
            builder.RuleForPropNameOf("togsCode", _ => codesGenerator.TogsCode().ToString());
            builder.RuleForPropNameOf("mriCode", _ => codesGenerator.MriCode().ToString());
            builder.RuleForPropNameOf("ifnsCode", _ => codesGenerator.IfnsCode().ToString());
            builder.RuleForPropNameOf("registrationNumberFss", _ => codesGenerator.FssRegNumber().ToString());
            builder.RuleForPropNameOf("registrationNumberPfr", _ => codesGenerator.PfrRegNumber().ToString());
            builder.RuleForPropNameOf("registrationIfnsCode", x => x.Random.ReplaceNumbers(PatternFor4Digits));
            builder.RuleForPropNameOf("kpp", _ => codesGenerator.Kpp().ToString());
            builder.RuleForPropNameOf("ipaddress", x => x.Internet.Ip());

            builder.RuleForType<DateOnly>(x => x.Date.Recent(30));

            builder.RuleForType(_ => codesGenerator.LegalEntityOkpo());
            builder.RuleForType(_ => codesGenerator.Okud());
            builder.RuleForType(_ => codesGenerator.Knd());
            builder.RuleForType(_ => codesGenerator.LegalEntityInn());
            builder.RuleForType(_ => codesGenerator.PersonInn());
            builder.RuleForType(_ => codesGenerator.FssCode());
            builder.RuleForType(_ => codesGenerator.UpfrCode());
            builder.RuleForType(_ => codesGenerator.TogsCode());
            builder.RuleForType(_ => codesGenerator.MriCode());
            builder.RuleForType(_ => codesGenerator.IfnsCode());
            builder.RuleForType(_ => codesGenerator.FssRegNumber());
            builder.RuleForType(_ => codesGenerator.PfrRegNumber());
            builder.RuleForType(_ => codesGenerator.Kpp());
            builder.RuleForType(x => IPAddress.Parse(x.Internet.Ip()));
            builder.RuleForType(x => x.Person.PersonFullName());

            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocflowType>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocumentType>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DraftBuilderType>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<SvdregCode>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromOfStruct<DocflowStatus>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromOfStruct<DocflowState>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromOfStruct<PfrLetterType>()));
            builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromOfStruct<DocflowExtendedStatus>()));

            builder.WithSkip<Urn>();
        }
    }
}