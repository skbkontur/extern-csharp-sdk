using System;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Attributes;

[UsedImplicitly]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Property)]
public class ClientDocumentationSectionAttribute : Attribute
{
}