﻿using System;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Events;
using Kontur.Extern.Api.Client.Http;
using Vostok.Clusterclient.Core.Model;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Api.Client.ApiLevel.Clients.Events
{
    //todo Сделать нормальные тесты для методов.
    public class EventsClient : IEventsClient
    {
        private readonly IHttpRequestsFactory http;

        public EventsClient(IHttpRequestsFactory http) => this.http = http;

        public Task<EventsPage> GetEventsAsync(int take, string fromId = "0_0", TimeSpan? timeout = null)
        {
            var url = new RequestUrlBuilder("/v1/events")
                .AppendToQuery("take", take)
                .AppendToQuery("fromId", fromId)
                .Build();
            return http.GetAsync<EventsPage>(url, timeout);
        }
    }
}