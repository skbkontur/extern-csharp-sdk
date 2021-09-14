using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.Http.Options;
using Vostok.Clusterclient.Core.Model;
using Vostok.Commons.Time;
using Xunit;

namespace Kontur.Extern.Api.Client.Http.UnitTests
{
    public class TimeoutSpecification_Tests
    {
        [Fact]
        public void GetTimeout_should_return_default_timeout_for_long_operations()
        {
            var longOperationTimeout = 30.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), 20.Seconds(), longOperationTimeout);

            var timeout = TimeoutSpecification.LongOperationTimeout.GetTimeout(CreateWriteRequest(), requestTimeouts);

            timeout.Should().Be(longOperationTimeout);
        }

        [Fact]
        public void GetTimeout_should_return_default_timeout_for_write_operation()
        {
            Should_return_default_timeout_for_write_operation(TimeoutSpecification.RegularOperationTimeout);
        }

        [Fact]
        public void GetTimeout_should_return_default_timeout_for_read_operation()
        {
            Should_return_default_timeout_for_read_operation(TimeoutSpecification.RegularOperationTimeout);
        }

        [Fact]
        public void Default_instance_should_be_regular_timeout_specification()
        {
            Should_return_default_timeout_for_read_operation(default);
            Should_return_default_timeout_for_write_operation(default);
        }

        private static void Should_return_default_timeout_for_write_operation(TimeoutSpecification regularOperationTimeout)
        {
            var writeTimeout = 20.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), writeTimeout, 30.Seconds());

            var timeout = regularOperationTimeout.GetTimeout(CreateWriteRequest(), requestTimeouts);

            timeout.Should().Be(writeTimeout);
        }

        private static void Should_return_default_timeout_for_read_operation(TimeoutSpecification regularOperationTimeout)
        {
            var readTimeout = 10.Seconds();
            var requestTimeouts = new RequestTimeouts(readTimeout, 20.Seconds(), 30.Seconds());

            var timeout = regularOperationTimeout.GetTimeout(CreateReadRequest(), requestTimeouts);

            timeout.Should().Be(readTimeout);
        }

        [Fact]
        public void GetTimeout_should_return_specific_timeout()
        {
            var specificTimeout = 25.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), 20.Seconds(), 30.Seconds());

            var timeout = TimeoutSpecification.SpecificTimeout(specificTimeout).GetTimeout(CreateReadRequest(), requestTimeouts);

            timeout.Should().Be(specificTimeout);
        }

        [Fact]
        public void GetTimeout_should_fail_when_given_too_small_specific_timeout()
        {
            var requestTimeouts = new RequestTimeouts();
            var timeoutSpecification = TimeoutSpecification.SpecificTimeout(RequestTimeouts.MinTimeout - 1.Seconds());
            
            Action action = () => timeoutSpecification.GetTimeout(CreateReadRequest(), requestTimeouts);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void GetTimeout_should_fail_when_given_too_big_specific_timeout()
        {
            var requestTimeouts = new RequestTimeouts();
            var timeoutSpecification = TimeoutSpecification.SpecificTimeout(RequestTimeouts.MaxTimeout + 1.Seconds());
            
            Action action = () => timeoutSpecification.GetTimeout(CreateReadRequest(), requestTimeouts);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void SpecificOrLongOperationTimeout_should_create_long_op_spec_if_the_given_timeout_is_null()
        {
            var longOperationTimeout = 30.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), 20.Seconds(), longOperationTimeout);

            var timeout = TimeoutSpecification.SpecificOrLongOperationTimeout(null)
                .GetTimeout(CreateReadRequest(), requestTimeouts);

            timeout.Should().Be(longOperationTimeout);
        }

        [Fact]
        public void SpecificOrLongOperationTimeout_should_create_specific_op_spec_if_the_given_timeout_is_not_null()
        {
            var specificTimeout = 25.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), 20.Seconds(), 30.Seconds());

            var timeout = TimeoutSpecification.SpecificOrLongOperationTimeout(specificTimeout)
                .GetTimeout(CreateReadRequest(), requestTimeouts);

            timeout.Should().Be(specificTimeout);
        }

        [Fact]
        public void SpecificOrRegularOperationTimeout_should_create_regular_op_spec_if_the_given_timeout_is_null()
        {
            var writeTimeout = 20.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), writeTimeout, 30.Seconds());

            var timeout = TimeoutSpecification.SpecificOrRegularOperationTimeout(null)
                .GetTimeout(CreateWriteRequest(), requestTimeouts);

            timeout.Should().Be(writeTimeout);
        }

        [Fact]
        public void SpecificOrRegularOperationTimeout_should_create_specific_op_spec_if_the_given_timeout_is_not_null()
        {
            var specificTimeout = 25.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), 20.Seconds(), 30.Seconds());

            var timeout = TimeoutSpecification.SpecificOrRegularOperationTimeout(specificTimeout)
                .GetTimeout(CreateWriteRequest(), requestTimeouts);

            timeout.Should().Be(specificTimeout);
        }

        [Fact]
        public void Should_implicitly_return_regular_op_spec_from_null_timeout()
        {
            var writeTimeout = 20.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), writeTimeout, 30.Seconds());

            TimeoutSpecification specification = null;
            var timeout = specification.GetTimeout(CreateWriteRequest(), requestTimeouts);

            timeout.Should().Be(writeTimeout);
        }

        [Fact]
        public void Should_implicitly_return_specific_timeout_from_non_null_timeout()
        {
            var specificTimeout = 25.Seconds();
            var requestTimeouts = new RequestTimeouts(10.Seconds(), 20.Seconds(), 30.Seconds());

            TimeoutSpecification specification = specificTimeout;
            var timeout = specification.GetTimeout(CreateWriteRequest(), requestTimeouts);

            timeout.Should().Be(specificTimeout);
        }

        private static Request CreateWriteRequest() => Request.Post("/some");
        private static Request CreateReadRequest() => Request.Get("/some");
    }
}