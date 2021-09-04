#nullable enable
using System;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Api;
using Kontur.Extern.Client.ApiLevel.Models.Api.Enums;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Errors;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.Api
{
    [TestFixture]
    internal class ApiTaskResultOfOneResult_Tests
    {
        [Test]
        public void Running_should_create_result_in_the_running_state()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Running
            };

            var result = ApiTaskResult<SuccessResult>.Running(id, taskType);

            result.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void TaskFailure_should_create_result_in_the_failure_state()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Failed
            };

            var result = ApiTaskResult<SuccessResult>.TaskFailure(new ApiError(), id, taskType);

            result.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void TaskFailure_should_fail_if_given_null_error()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult>.TaskFailure(null!, id, taskType);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Success_should_create_result_in_the_success_state()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Succeed
            };

            var result = ApiTaskResult<SuccessResult>.Success(new SuccessResult(), id, taskType);

            result.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void Success_should_fail_if_given_null_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult>.Success(null!, id, taskType);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TryGetSuccessResult_should_return_success_result_if_api_task_result_is_successful()
        {
            var successResult = new SuccessResult();
            var taskResult = ApiTaskResult<SuccessResult>.Success(successResult, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetSuccessResult(out var result);

            success.Should().BeTrue();
            result.Should().Be(successResult);
        }

        [Test]
        public void TryGetSuccessResult_should_return_error_if_api_task_result_is_api_error()
        {
            var taskResult = ApiTaskResult<SuccessResult>.TaskFailure(new ApiError(), Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetSuccessResult(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }
        
        [Test]
        public void TryGetTaskError_should_return_task_error_if_api_task_result_is_api_error()
        {
            var apiError = new ApiError();
            var taskResult = ApiTaskResult<SuccessResult>.TaskFailure(apiError, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetTaskError(out var result);

            success.Should().BeTrue();
            result.Should().Be(apiError);
        }

        [Test]
        public void TryGetTaskError_should_return_error_if_api_task_result_is_successful()
        {
            var taskResult = ApiTaskResult<SuccessResult>.Success(new SuccessResult(), Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetTaskError(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }

        private class SuccessResult
        {
            public SuccessResult(bool isEmpty = false) => IsEmpty = isEmpty;
            public bool IsEmpty { get; }
        }

        [PublicAPI]
        private class ExpectedResult
        {
            public Guid Id { get; set; }
            public ApiTaskState TaskState { get; set; }
            public Urn? TaskType { get; set; }
        }
    }
}