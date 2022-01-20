using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.ApiLevel.Clients.Docflows;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Http;
using Kontur.Extern.Api.Client.Model.DocflowFiltering;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Models.Numbers;
using Kontur.Extern.Api.Client.Paths;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.AutoFaker;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.Docflows;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftsBuilders;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Xunit;
using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.UnitTests.Client.ApiLevel.Models.Requests.Docflow.PFR
{
    internal class GetTests
    {
    [Test]
            public void Get_should_fail_when_try_to_read_non_existent_docflow()
            {
;
            var sub = Substitute.For<IHttpResponse>();
            sub.GetMessageAsync<IHttpResponse>().ReturnsNull();

            var apiClient = new DocflowPath();
            
            var apiException = Assert.ThrowsAsync<ApiException>(async () => 
                await apiClient.Services.Api.Docflows.GetDocflowAsync(Guid.Empty, Guid.Empty).ConfigureAwait(false));


        }
        

        }
    }
