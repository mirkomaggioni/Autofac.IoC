using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using Autofac.IoC.BusinessServices;
using Autofac.IoC.Repositories;
using Autofac.IoC.CoreServices;
using FluentAssertions;
using Autofac.IoC.Controllers.Controllers;
using Autofac.IoC.Controllers.Controllers.Api;

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

        [Test]
        public void should_be_able_to_resolve_mvc_controller()
        {
            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                var controller = scope.Resolve<HomeController>();
                controller.Should().NotBeNull();
            }
        }

        [Test]
        public void should_be_able_to_resolve_api_controller()
        {
            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                var controller = scope.Resolve<AccountController>();
                controller.Should().NotBeNull();
            }
        }
    }
}
