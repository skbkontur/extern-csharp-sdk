using System.Threading.Tasks;
using ExternDotnetSDK.Events;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.APIs
{
    internal interface IEventsApi
    {
        [Get("/v1/events?take={take}&fromId={fromId}")]
        Task<EventsPage> GetEvents(int take, string fromId = "0_0");
    }
}