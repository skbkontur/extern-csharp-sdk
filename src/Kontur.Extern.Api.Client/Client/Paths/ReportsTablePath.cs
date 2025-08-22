using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ClientDocumentationSection]
public readonly struct ReportsTablePath
{
    public ReportsTablePath(Guid accountId, Guid organizationId, IExternClientServices services)
    {
        AccountId = accountId;
        OrganizationId = organizationId;
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    public IExternClientServices Services { get; }

    public FormListPath Forms => new(AccountId, OrganizationId, Services);
    public ReportsTableDocflowsPath ReportDocflows => new(AccountId, OrganizationId, Services);
}