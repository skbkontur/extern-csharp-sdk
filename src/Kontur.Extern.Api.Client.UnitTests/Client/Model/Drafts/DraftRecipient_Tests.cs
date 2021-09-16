using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts;
using Kontur.Extern.Api.Client.Model.Drafts;
using Kontur.Extern.Api.Client.Models.Numbers;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.Client.Model.Drafts
{
    [TestFixture]
    internal class DraftRecipient_Tests
    {
        [TestFixture]
        internal class Ifns
        {
            [Test]
            public void Should_fail_when_given_null_ifns_code()
            {
                Action action = () => DraftRecipient.Ifns(null!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_create_ifns_recipient()
            {
                var expectedRequest = new RecipientInfoRequest
                {
                    IfnsCode = IfnsCode.Parse("1234"),
                    MriCode = MriCode.Parse("5678")
                };
                    
                var request = DraftRecipient
                    .Ifns(IfnsCode.Parse("1234"), MriCode.Parse("5678"))
                    .ToRequest();
                
                request.Should().BeEquivalentTo(expectedRequest);
            }
        }

        [TestFixture]
        internal class Upfr
        {
            [Test]
            public void Should_fail_when_given_null_code()
            {
                Action action = () => DraftRecipient.Upfr(null!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_create_upfr_recipient()
            {
                var expectedRequest = new RecipientInfoRequest
                {
                    UpfrCode = UpfrCode.Parse("123-456")
                };
                    
                var request = DraftRecipient
                    .Upfr(UpfrCode.Parse("123-456"))
                    .ToRequest();
                
                request.Should().BeEquivalentTo(expectedRequest);
            }
        }

        [TestFixture]
        internal class Togs
        {
            [Test]
            public void Should_fail_when_given_null_code()
            {
                Action action = () => DraftRecipient.Togs(null!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_create_upfr_recipient()
            {
                var expectedRequest = new RecipientInfoRequest
                {
                    TogsCode = TogsCode.Parse("12-45")
                };
                    
                var request = DraftRecipient
                    .Togs(TogsCode.Parse("12-45"))
                    .ToRequest();
                
                request.Should().BeEquivalentTo(expectedRequest);
            }
        }

        [TestFixture]
        internal class Fss
        {
            [Test]
            public void Should_fail_when_given_null_code()
            {
                Action action = () => DraftRecipient.Fss(null!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_create_upfr_recipient()
            {
                var expectedRequest = new RecipientInfoRequest
                {
                    FssCode = FssCode.Parse("12341")
                };
                    
                var request = DraftRecipient
                    .Fss(FssCode.Parse("12341"))
                    .ToRequest();
                
                request.Should().BeEquivalentTo(expectedRequest);
            }
        }
        
        [TestFixture]
        internal class RegistrationIfns
        {
            [Test]
            public void Should_fail_when_given_null_code()
            {
                Action action = () => DraftRecipient.RegistrationIfns(null!);

                action.Should().Throw<ArgumentException>();
            }

            [Test]
            public void Should_create_upfr_recipient()
            {
                var expectedRequest = new RecipientInfoRequest
                {
                    RegistrationIfnsCode = IfnsCode.Parse("1234")
                };
                    
                var request = DraftRecipient
                    .RegistrationIfns(IfnsCode.Parse("1234"))
                    .ToRequest();
                
                request.Should().BeEquivalentTo(expectedRequest);
            }
        }
    }
}