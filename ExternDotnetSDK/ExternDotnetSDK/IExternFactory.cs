using JetBrains.Annotations;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public interface IExternFactory
    {
        IExtern Create();
    }
}