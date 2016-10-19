using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Autofac.IoC.BusinessServices;
using Autofac.IoC.Repositories;
using Autofac.IoC.CoreServices;
using FluentAssertions;
using System.Net.Http;

namespace Autofac.IoC.Tests
{
    public class PerRequestTests : BaseTests
    {
        [Test]
        public void should_is_not_the_same_instance_for_different_requests()
        {
            AccountService accountService1, accountService2;

            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.SetConfiguration(httpConfiguration);
                var dependencyScope = request.GetDependencyScope();
                accountService1 = dependencyScope.GetService(typeof(AccountService)) as AccountService;
            }

            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.SetConfiguration(httpConfiguration);
                var dependencyScope = request.GetDependencyScope();
                accountService2 = dependencyScope.GetService(typeof(AccountService)) as AccountService;
            }

            ReferenceEquals(accountService1, accountService2).ShouldBeEquivalentTo(false);
        }

        [Test]
        public void should_not_be_able_to_resolve_instance_per_request()
        {
            LoggerService service = null;

            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    request.SetConfiguration(httpConfiguration);
                    var dependencyScope = request.GetDependencyScope();
                    service = dependencyScope.GetService(typeof(LoggerService)) as LoggerService;

                    service.ShouldBeEquivalentTo(null);
                }
            }
            catch (Exception ex)
            {
                service.ShouldBeEquivalentTo(null);
            }
        }

        [Test]
        public void should_be_able_to_resolve_instance_per_request()
        {
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.SetConfiguration(httpConfiguration);
                var dependencyScope = request.GetDependencyScope();
                AccountService service = dependencyScope.GetService(typeof(AccountService)) as AccountService;

                service.Should().NotBeNull();
            }
        }
    }
}
