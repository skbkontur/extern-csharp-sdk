using System;
using System.Text.Json.Serialization;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Converters;

namespace Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class YesNoUnknownNullableBoolJsonPropertyAttribute : JsonConverterAttribute
    {
        public YesNoUnknownNullableBoolJsonPropertyAttribute()
            : base(typeof (YesNoUnknownBooleanConverter))
        {
        }
    }
}