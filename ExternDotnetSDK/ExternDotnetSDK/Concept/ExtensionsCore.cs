using System;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Concept
{
    public interface IExtendableContext
    {
        Task<T> MixinAsync<T>(IExtension<EmptyContextPath, T> extension);
    }
    
    public interface IExtendableContext<out TContextPath>
    {
        Task<T> MixinClassStyleAsync<T>(IExtension<TContextPath, T> extension);
        Task<T> MixinDelegateStyleAsync<T>(Func<IKeApiClient, TContextPath, Options, Task<T>> extensionBody);
    }

    public interface IExtension<in TContextPath, TResult>
    {
        Task<TResult> Execute(IKeApiClient client, TContextPath contextPath, Options options);
    }

    struct Unit
    {
    }
}