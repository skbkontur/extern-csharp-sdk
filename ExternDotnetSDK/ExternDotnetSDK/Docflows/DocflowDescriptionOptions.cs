using System;
using System.Collections.Generic;
using ExternDotnetSDK.Docflows.Descriptions;
using JetBrains.Annotations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ExternDotnetSDK.Docflows
{
    [UsedImplicitly]
    internal class DocflowDescriptionOptions : ISchemaFilter
    {
        public void Apply(Schema schema, ISchemaRegistry schemaRegistry, Type type)
        {
            if (type == typeof(DocflowDescriptionOptions))
            {
                schema.Properties = new Dictionary<string, Schema>()
                {
                    {"ReportDescription", schemaRegistry.GetOrRegister(typeof (ReportDescription))},
                    {"DemandDescription", schemaRegistry.GetOrRegister(typeof (DemandDescription))},
                    {"SubmissionDescription", schemaRegistry.GetOrRegister(typeof (SubmissionDescription))},
                    {"StatReportDescription", schemaRegistry.GetOrRegister(typeof (StatReportDescription))},
                    {"IonDescription", schemaRegistry.GetOrRegister(typeof (IonDescription))},
                    {"StatLetterDescription", schemaRegistry.GetOrRegister(typeof (StatLetterDescription))},
                    {"StatCuLetterDescription", schemaRegistry.GetOrRegister(typeof (StatCuLetterDescription))},
                    {"LetterDescription", schemaRegistry.GetOrRegister(typeof (LetterDescription))},
                    {"CuLetterDescription", schemaRegistry.GetOrRegister(typeof (CuLetterDescription))},
                    {"ApplicationDescription", schemaRegistry.GetOrRegister(typeof (ApplicationDescription))},
                    {"PfrIosDescription", schemaRegistry.GetOrRegister(typeof (PfrIosDescription))},
                    {"PfrLetterDescription", schemaRegistry.GetOrRegister(typeof (PfrLetterDescription))},
                    {"PfrCuLetterDescription", schemaRegistry.GetOrRegister(typeof (PfrCuLetterDescription))},
                    {"PfrReportDescription", schemaRegistry.GetOrRegister(typeof (PfrReportDescription))},
                    {"FssReportDescription", schemaRegistry.GetOrRegister(typeof (FssReportDescription))}
                };
            }
        }

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            //ISchemaFilter interface differs from the one i had to copy initially
            //Initial interface used a method above, so i had to improvise
            Apply(schema, context.SchemaRegistry, context.SystemType);
        }
    }
}