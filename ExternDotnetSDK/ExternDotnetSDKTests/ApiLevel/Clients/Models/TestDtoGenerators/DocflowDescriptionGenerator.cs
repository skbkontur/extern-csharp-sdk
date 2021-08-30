using System;
using AutoBogus;
using AutoBogus.Conventions;
using Bogus;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Descriptions;
using Kontur.Extern.Client.Model.Docflows;
using Kontur.Extern.Client.Tests.TestHelpers.BogusExtensions;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.TestDtoGenerators
{
    internal class DocflowDescriptionGenerator
    {
        private const string PatternFor10Digits = "##########";
        private const string PatternFor7Digits = "#######";
        private const string PatternFor9Digits = "##########";
        private readonly IAutoFaker faker;

        public DocflowDescriptionGenerator()
        {
            Randomizer.Seed = new Random(1234);
            faker = AutoFaker.Create(builder =>
            {
                builder.WithConventions(x => x.Semver.Aliases("Version"));
                builder.WithConventions(x => x.FirstName.Aliases("Name", "FirstName", "Firstname"));
                builder.WithConventions(x => x.LastName.Aliases("LastName", "SurName", "Surname"));
                builder.WithConventions(x => x.FirstName.Aliases("Patronymic"));
                
                builder.WithSkip<Urn>();
                
                builder.RuleForPropNameOf("SvdRegCode", x => x.Random.ReplaceNumbers(PatternFor10Digits));
                builder.RuleForPropNameOf("okpo", x => x.Random.ReplaceNumbers(PatternFor10Digits));
                builder.RuleForPropNameOf("okud", x => x.Random.ReplaceNumbers(PatternFor7Digits));
                builder.RuleForPropNameOf("Knd", x => x.Random.ReplaceNumbers(PatternFor9Digits));
                builder.RuleForPropNameOf("SenderInn", x => x.Random.ReplaceNumbers(PatternFor10Digits));
                builder.RuleForPropNameOf("Inn", x => x.Random.ReplaceNumbers(PatternFor10Digits));
            });
        }

        public Docflow GenerateDocflowWithDescription(Type descriptionType, DocflowType docflowType)
        {
            var docflow = faker.Generate<Docflow>(c => c.WithSkip<Docflow>(nameof(Docflow.Description)));
            docflow.Description = (DocflowDescription) faker.Generate(descriptionType);
            docflow.Type = docflowType.ToUrn();
            return docflow;
        }
        
        public Docflow GenerateDocflowWithDescriptionOf<TDescription>(DocflowType docflowType)
            where TDescription : DocflowDescription
        {
            var docflow = faker.Generate<Docflow>(c => c.WithSkip<Docflow>(nameof(Docflow.Description)));
            docflow.Description = faker.Generate<TDescription>();
            docflow.Type = docflowType.ToUrn();
            return docflow;
        }

        public Docflow GenerateDocflowWithoutDescription(DocflowType docflowType)
        {
            var docflow = faker.Generate<Docflow>(c => c.WithSkip<Docflow>(nameof(Docflow.Description)));
            docflow.Description = null;
            docflow.Type = docflowType.ToUrn();
            return docflow;
        }
    }
}