using System;

namespace Kontur.Extern.Client.Concept
{
    public struct EmptyContextPath
    {
    }

    public struct AccountPath
    {
        public AccountPath(Guid accountId) => AccountId = accountId;

        public Guid AccountId { get; }
    }

    public struct OrganizationPath
    {
        public OrganizationPath(Guid organizationId, Guid accountId)
        {
            OrganizationId = organizationId;
            AccountId = accountId;
        }

        public Guid AccountId { get; }
        public Guid OrganizationId { get; }
    }

    public struct DocflowPath
    {
        public DocflowPath(Guid docflowId, Guid accountId)
        {
            DocflowId = docflowId;
            AccountId = accountId;
        }

        public Guid AccountId { get; }
        public Guid DocflowId { get; }
    }
    
    public struct DocumentPath
    {
        public DocumentPath(Guid documentId, Guid docflowId, Guid accountId)
        {
            DocumentId = documentId;
            DocflowId = docflowId;
            AccountId = accountId;
        }

        public Guid AccountId { get; }
        public Guid DocumentId { get; }
        public Guid DocflowId { get; }
    }
}