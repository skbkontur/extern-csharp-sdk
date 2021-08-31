using System;
using AutoBogus;
using AutoBogus.Conventions;
using Bogus;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.Common.Time;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal static class AutoFakerFactory
    {
        private const string PatternFor10Digits = "##########";
        private const string PatternFor7Digits = "#######";
        private const string PatternFor9Digits = "##########";

        public static IAutoFaker Create()
        {
            Randomizer.Seed = new Random(1234);
            return AutoFaker.Create(builder =>
            {
                builder.WithConventions(x => x.Semver.Aliases("Version"));
                builder.WithConventions(x => x.FirstName.Aliases("Name", "FirstName", "Firstname"));
                builder.WithConventions(x => x.LastName.Aliases("LastName", "SurName", "Surname"));
                builder.WithConventions(x => x.FirstName.Aliases("Patronymic"));

                builder.RuleForPropNameOf("SvdRegCode", x => x.Random.ReplaceNumbers(PatternFor10Digits));
                builder.RuleForPropNameOf("okpo", x => x.Random.ReplaceNumbers(PatternFor10Digits));
                builder.RuleForPropNameOf("okud", x => x.Random.ReplaceNumbers(PatternFor7Digits));
                builder.RuleForPropNameOf("Knd", x => x.Random.ReplaceNumbers(PatternFor9Digits));
                builder.RuleForPropNameOf("SenderInn", x => x.Random.ReplaceNumbers(PatternFor10Digits));
                builder.RuleForPropNameOf("Inn", x => x.Random.ReplaceNumbers(PatternFor10Digits));

                builder.WithSkip<Urn>();
            });
        }
    }
}