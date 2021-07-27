using JetBrains.Annotations;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public interface IExternBuilder
    {
        IExtern Create();
    }
}