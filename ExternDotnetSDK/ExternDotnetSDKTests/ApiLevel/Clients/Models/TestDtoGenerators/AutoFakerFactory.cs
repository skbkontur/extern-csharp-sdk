using System;
using AutoBogus;
using AutoBogus.Conventions;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Model.Documents;
using Kontur.Extern.Client.Models.Common;
using Kontur.Extern.Client.Testing.Generators;
using Kontur.Extern.Client.Tests.TestHelpers;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;
using Randomizer = Bogus.Randomizer;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal static class AutoFakerFactory
    {
        private const string PatternFor10Digits = "##########";
        private const string PatternFor13Digits = "#############";
        private const string PatternFor15Digits = "###############";
        private const string PatternFor4Digits = "####";

        public static IAutoFaker Create()
        {
            var random = new Random(1234);
            Randomizer.Seed = random;
            var codesGenerator = new AuthoritiesCodesGenerator(random);
            return AutoFaker.Create(builder =>
            {
                builder.WithConventions(x => x.Semver.Aliases("Version"));
                builder.WithConventions(x => x.FirstName.Aliases("Name", "FirstName", "Firstname"));
                builder.WithConventions(x => x.LastName.Aliases("LastName", "SurName", "Surname"));
                builder.WithConventions(x => x.FirstName.Aliases("Patronymic"));

                builder.RuleForPropNameOf("SvdRegCode", x => x.Random.ReplaceNumbers(PatternFor10Digits));
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
                
                builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocflowType>()));
                builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromNestedTypesOfStruct<DocumentType>()));
                builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromOfStruct<DocflowStatus>()));
                builder.RuleForType(x => x.PickRandom(EnumLikeType.AllEnumValuesFromOfStruct<DocflowState>()));
                
                builder.WithSkip<Urn>();
            });
        }
    }
}