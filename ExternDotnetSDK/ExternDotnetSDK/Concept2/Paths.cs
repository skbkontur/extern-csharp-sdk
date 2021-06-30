using System;

namespace Kontur.Extern.Client.Concept2
{
    internal readonly struct AccountListPath
    {
        public AccountListPath(IExternClientServices services) => Services = services;
        
        public IExternClientServices Services { get; }

        public AccountPath WithId(Guid accountId) => new AccountPath(accountId, Services);
    }

    internal readonly struct AccountPath
    {
        public AccountPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationListPath Organizations => new OrganizationListPath(AccountId, Services);
        public DocflowListPath Docflows => new DocflowListPath(AccountId, Services);
    }

    internal readonly struct OrganizationListPath
    {
        public OrganizationListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationPath WithId(Guid organizationId) => new OrganizationPath(AccountId, organizationId, Services);
    }

    internal readonly struct OrganizationPath
    {
        public OrganizationPath(Guid accountId, Guid organizationId, IExternClientServices services)
        {
            AccountId = accountId;
            OrganizationId = organizationId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
        public IExternClientServices Services { get; }
    }

    internal readonly struct DocflowListPath
    {
        public DocflowListPath(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public DocflowPath WithId(Guid docflowId) => new DocflowPath(AccountId, docflowId, Services);
    }

    internal readonly struct DocflowPath
    {
        public DocflowPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentListPath Documents => new DocumentListPath(AccountId, DocflowId, Services);
    }

    internal readonly struct DocumentListPath
    {
        public DocumentListPath(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentPath WithId(Guid documentId) => new DocumentPath(AccountId, DocflowId, documentId, Services);
    }

    internal readonly struct DocumentPath
    {
        public DocumentPath(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            DocumentId = documentId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public Guid DocumentId { get; }
        public IExternClientServices Services { get; }
    }
}