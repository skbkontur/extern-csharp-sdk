using System;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct ReportsTablePath
{
    public ReportsTablePath(Guid accountId, Guid organizationId, IExternClientServices services)
    {
        AccountId = accountId;
        OrganizationId = organizationId;
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public FormListPath Forms => new(AccountId, OrganizationId, services);
    public ReportsTableDocflowsPath ReportDocflows => new(AccountId, OrganizationId, services);
}