using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Autofac.IoC.CoreServices;
using FluentAssertions;
using Autofac.IoC.Repositories;

namespace Autofac.IoC.Tests
{
    public class PerDependencyTests : BaseTests
    {
        [Test]
        public void should_exists_different_token_per_dependency()
        {
            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                var tokenService1 = scope.Resolve<PerDependencyTokenService>();
                var tokenService2 = scope.Resolve<PerDependencyTokenService>();

                Guid token1 = tokenService1.GetToken();
                Guid token2 = tokenService2.GetToken();

                token1.Should().NotBeEmpty();
                token2.Should().NotBeEmpty();
                token1.Should().NotBe(token2);
            }
        }

        [Test]
        public void should_be_able_to_resolve_instance_per_dependency()
        {
            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                var result = scope.Resolve<IProductsRepository>();
                result.Should().NotBeNull();
            }
        }

        [Test]
        public void should_not_be_able_to_resolve_instance_per_dependency()
        {
            ProductsRepository repository = null;

            try
            {
                using (var scope = base.containerBuilder.BeginLifetimeScope())
                {
                    repository = scope.Resolve<ProductsRepository>();
                    repository.ShouldBeEquivalentTo(null);
                }
            }
            catch (Exception)
            {
                    repository.ShouldBeEquivalentTo(null);
            }
        }
    }
}
