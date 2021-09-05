#nullable enable
using System;
using System.Net;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.ApiErrors;
using Kontur.Extern.Client.Models.ApiTasks;
using Kontur.Extern.Client.Models.Common;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.Api
{
    [TestFixture]
    internal class ApiTaskResultOfTwoResults_Tests
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

            var result = ApiTaskResult<SuccessResult, FailureResult>.Running(id, taskType);

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

            var result = ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(AnApiError, id, taskType);

            result.Should().BeEquivalentTo(expectedDto);
        }

        [Test]
        public void TaskFailure_should_fail_if_given_null_error()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(null!, id, taskType);

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

            var result = ApiTaskResult<SuccessResult, FailureResult>.Success(new SuccessResult(), id, taskType);

            result.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void Success_should_fail_if_given_null_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult, FailureResult>.Success(null!, id, taskType);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void Success_should_fail_if_given_empty_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult, FailureResult>.Success(SuccessResult.Empty, id, taskType);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void FailureResult_should_create_result_in_the_failed_state()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Failed
            };

            var result = ApiTaskResult<SuccessResult, FailureResult>.FailureResult(new FailureResult(), id, taskType);

            result.Should().BeEquivalentTo(expectedDto);
        }
        
        [Test]
        public void FailureResult_should_fail_if_given_null_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult, FailureResult>.FailureResult(null!, id, taskType);

            action.Should().Throw<ArgumentException>();
        }
        
        [Test]
        public void FailureResult_should_fail_if_given_empty_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            
            Action action = () => ApiTaskResult<SuccessResult, FailureResult>.FailureResult(FailureResult.Empty, id, taskType);

            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void TryGetSuccessResult_should_return_success_result_if_api_task_result_is_successful()
        {
            var successResult = new SuccessResult();
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.Success(successResult, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetSuccessResult(out var result);

            success.Should().BeTrue();
            result.Should().Be(successResult);
        }

        [Test]
        public void TryGetSuccessResult_should_return_error_if_api_task_result_is_failure()
        {
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.FailureResult(new FailureResult(), Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetSuccessResult(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }

        [Test]
        public void TryGetSuccessResult_should_return_error_if_api_task_result_is_api_error()
        {
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(AnApiError, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetSuccessResult(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }
        
        [Test]
        public void TryGetFailureResult_should_return_failure_result_if_api_task_result_is_failure()
        {
            var failureResult = new FailureResult();
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.FailureResult(failureResult, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetFailureResult(out var result);

            success.Should().BeTrue();
            result.Should().Be(failureResult);
        }

        [Test]
        public void TryGetFailureResult_should_return_error_if_api_task_result_is_successful()
        {
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.Success(new SuccessResult(), Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetFailureResult(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }

        [Test]
        public void TryGetFailureResult_should_return_error_if_api_task_result_is_api_error()
        {
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(AnApiError, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetFailureResult(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }
        
        [Test]
        public void TryGetTaskError_should_return_task_error_if_api_task_result_is_api_error()
        {
            var apiError = AnApiError;
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(apiError, Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetTaskError(out var result);

            success.Should().BeTrue();
            result.Should().Be(apiError);
        }

        [Test]
        public void TryGetTaskError_should_return_error_if_api_task_result_is_successful()
        {
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.Success(new SuccessResult(), Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetTaskError(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }
        
        [Test]
        public void TryGetTaskError_should_return_error_if_api_task_result_is_failure()
        {
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.FailureResult(new FailureResult(), Guid.NewGuid(), new Urn("nid", "nss"));

            var success = taskResult.TryGetTaskError(out var result);

            success.Should().BeFalse();
            result.Should().BeNull();
        }

        [Test]
        public void ConvertToSingleApiResult_should_convert_failure_result_to_api_error()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Failed
            };
            var expectedApiError = new ApiError(HttpStatusCode.Forbidden, "10");
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.FailureResult(new FailureResult(value: 10), id, taskType);

            var oneResultApiResult = taskResult.ConvertToSingleApiResult(
                x => x.Value,
                x => new ApiError(HttpStatusCode.Forbidden, x.Value.ToString())
            );
            
            oneResultApiResult.Should().BeEquivalentTo(expectedDto);
            oneResultApiResult.TryGetTaskError(out var apiError).Should().BeTrue();
            apiError.Should().BeEquivalentTo(expectedApiError);
        }
        
        [Test]
        public void ConvertToSingleApiResult_should_convert_success_result_to_success_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Succeed
            };
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.Success(new SuccessResult(value: 10), id, taskType);

            var oneResultApiResult = taskResult.ConvertToSingleApiResult(
                x => x.Value,
                x => new ApiError(HttpStatusCode.Forbidden, x.Value.ToString())
            );
            
            oneResultApiResult.Should().BeEquivalentTo(expectedDto);
            oneResultApiResult.TryGetSuccessResult(out var result).Should().BeTrue();
            result.Should().Be(10);
        }
        
        [Test]
        public void ConvertToSingleApiResult_should_convert_running_result_to_running_result()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Running
            };
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.Running(id, taskType);

            var oneResultApiResult = taskResult.ConvertToSingleApiResult(
                x => x.Value,
                x => new ApiError(HttpStatusCode.Forbidden, x.Value.ToString())
            );
            
            oneResultApiResult.Should().BeEquivalentTo(expectedDto);
            oneResultApiResult.TryGetSuccessResult(out _).Should().BeFalse();
        }
        
        [Test]
        public void ConvertToSingleApiResult_should_convert_task_error_result_to_api_error()
        {
            var id = Guid.NewGuid();
            var taskType = new Urn("nid", "nss");
            var expectedDto = new ExpectedResult
            {
                Id = id,
                TaskType = taskType,
                TaskState = ApiTaskState.Failed
            };
            var expectedApiError = new ApiError(HttpStatusCode.Forbidden, "an error");
            var taskResult = ApiTaskResult<SuccessResult, FailureResult>.TaskFailure(expectedApiError, id, taskType);

            var oneResultApiResult = taskResult.ConvertToSingleApiResult(
                x => x.Value,
                x => new ApiError(HttpStatusCode.Forbidden, x.Value.ToString())
            );
            
            oneResultApiResult.Should().BeEquivalentTo(expectedDto);
            oneResultApiResult.TryGetTaskError(out var apiError).Should().BeTrue();
            apiError.Should().BeEquivalentTo(expectedApiError);
        }
        
        private static ApiError AnApiError => new(HttpStatusCode.BadRequest, "really bad");

        private class SuccessResult : IApiTaskResult
        {
            public static SuccessResult Empty => new(true);
            
            public SuccessResult(bool isEmpty = false, int value = 0)
            {
                IsEmpty = isEmpty;
                Value = value;
            }

            public bool IsEmpty { get; }
            
            public int Value { get; set; }
        }

        private class FailureResult : IApiTaskResult
        {
            public static FailureResult Empty => new(true);
            
            public FailureResult(bool isEmpty = false, int value = 0)
            {
                IsEmpty = isEmpty;
                Value = value;
            }

            public bool IsEmpty { get; }
            public int Value { get; }
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