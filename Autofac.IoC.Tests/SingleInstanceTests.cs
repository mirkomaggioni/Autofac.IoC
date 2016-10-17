using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using Autofac.IoC.CoreServices;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Tests
{
    public class SingleInstanceTests: BaseTests
    {
        [Test]
        public void should_exists_only_one_token_single_instance()
        {
            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                var tokenService1 = scope.Resolve<SingletonTokenService>();
                var tokenService2 = scope.Resolve<SingletonTokenService>();

                Guid token1 = tokenService1.GetToken();
                Guid token2 = tokenService2.GetToken();

                token1.Should().NotBeEmpty();
                token2.Should().NotBeEmpty();
                token1.Should().Equals(token2);
            }
        }

        [Test]
        public void should_return_the_products_list()
        {
            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                var productsService = scope.Resolve<ProductsService>();
                var result = productsService.GetProducts();

                result.Should().NotBeNull();
                result.Should().NotBeEmpty();
            }
        }

        [Test]
        public void should_is_the_same_single_instance()
        {
            ProductsService productsService1, productsService2;

            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                productsService1 = scope.Resolve<ProductsService>();
            }

            using (var scope = containerBuilder.BeginLifetimeScope())
            {
                productsService2 = scope.Resolve<ProductsService>();
            }

            ReferenceEquals(productsService1, productsService2).ShouldBeEquivalentTo(true);
        }
    }
}
