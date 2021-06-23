using System;

namespace Kontur.Extern.Client.Engine
{
    internal class AccountIdProvider : IAccountIdProvider
    {
        public Guid AccountId { get; set; }
    }

    public interface IAccountIdProvider
    {
        Guid AccountId { get; }
    }
}