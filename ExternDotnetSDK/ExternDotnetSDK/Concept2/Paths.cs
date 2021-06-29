using System;

namespace Kontur.Extern.Client.Concept2
{
    internal readonly struct AccountListContext
    {
        public AccountListContext(IExternClientServices services) => Services = services;
        
        public IExternClientServices Services { get; }

        public AccountContext WithId(Guid accountId) => new AccountContext(accountId, Services);
    }

    internal readonly struct AccountContext
    {
        public AccountContext(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        

        public OrganizationListContext Organizations => new OrganizationListContext(AccountId, Services);
        public DocflowListContext Docflows => new DocflowListContext(AccountId, Services);
    }

    internal readonly struct OrganizationListContext
    {
        public OrganizationListContext(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public OrganizationContext WithId(Guid organizationId) => new OrganizationContext(AccountId, organizationId, Services);
    }

    internal readonly struct OrganizationContext
    {
        public OrganizationContext(Guid accountId, Guid organizationId, IExternClientServices services)
        {
            AccountId = accountId;
            OrganizationId = organizationId;
            Services = services;
        }
        
        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
        public IExternClientServices Services { get; }
    }

    internal readonly struct DocflowListContext
    {
        public DocflowListContext(Guid accountId, IExternClientServices services)
        {
            AccountId = accountId;
            Services = services;
        }

        public Guid AccountId { get; }
        public IExternClientServices Services { get; }

        public DocflowContext WithId(Guid docflowId) => new DocflowContext(AccountId, docflowId, Services);
    }

    internal readonly struct DocflowContext
    {
        public DocflowContext(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentListContext Documents => new DocumentListContext(AccountId, DocflowId, Services);
    }

    internal readonly struct DocumentListContext
    {
        public DocumentListContext(Guid accountId, Guid docflowId, IExternClientServices services)
        {
            AccountId = accountId;
            DocflowId = docflowId;
            Services = services;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
        public IExternClientServices Services { get; }

        public DocumentContext WithId(Guid documentId) => new DocumentContext(AccountId, DocflowId, documentId, Services);
    }

    internal readonly struct DocumentContext
    {
        public DocumentContext(Guid accountId, Guid docflowId, Guid documentId, IExternClientServices services)
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