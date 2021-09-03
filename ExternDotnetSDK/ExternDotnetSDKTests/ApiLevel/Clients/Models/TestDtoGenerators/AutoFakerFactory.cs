using System;
using AutoBogus;
using AutoBogus.Conventions;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Model.Numbers;
using Kontur.Extern.Client.Testing.Generators;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;
using Randomizer = Bogus.Randomizer;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal static class AutoFakerFactory
    {
        private const string PatternFor10Digits = "##########";
        private const string PatternFor13Digits = "#############";
        private const string PatternFor15Digits = "###############";
        private const string PatternFor7Digits = "#######";
        private const string PatternFor9Digits = "##########";
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
                builder.RuleForPropNameOf("okpo", x => codesGenerator.LegalEntityOkpo().ToString());
                builder.RuleForPropNameOf("okud", x => codesGenerator.Okud().ToString());
                builder.RuleForPropNameOf("ogrn", x => x.Random.ReplaceNumbers(PatternFor13Digits));
                builder.RuleForPropNameOf("ogrnIp", x => x.Random.ReplaceNumbers(PatternFor15Digits));
                builder.RuleForPropNameOf("Knd", x => codesGenerator.Knd().ToString());
                builder.RuleForPropNameOf("SenderInn", x => codesGenerator.LegalEntityInn().ToString());
                builder.RuleForPropNameOf("Inn", x => codesGenerator.LegalEntityInn().ToString());
                builder.RuleForPropNameOf("FssCode", x => codesGenerator.FssCode().ToString());
                builder.RuleForPropNameOf("upfrCode", x => codesGenerator.UpfrCode().ToString());
                builder.RuleForPropNameOf("togsCode", x => codesGenerator.TogsCode().ToString());
                builder.RuleForPropNameOf("mriCode", x => codesGenerator.MriCode().ToString());
                builder.RuleForPropNameOf("ifnsCode", x => codesGenerator.IfnsCode().ToString());
                builder.RuleForPropNameOf("registrationNumberFss", x => codesGenerator.FssRegNumber().ToString());
                builder.RuleForPropNameOf("registrationNumberPfr", x => codesGenerator.PfrRegNumber().ToString());
                builder.RuleForPropNameOf("registrationIfnsCode", x => x.Random.ReplaceNumbers(PatternFor4Digits));
                builder.RuleForPropNameOf("kpp", x => codesGenerator.Kpp().ToString());
                builder.RuleForPropNameOf("ipaddress", x => x.Internet.Ip());

                builder.RuleForType<DateOnly>(x => x.Date.Recent(30));
                
                builder.WithSkip<Urn>();
            });
        }
    }
}