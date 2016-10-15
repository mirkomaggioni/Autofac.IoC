using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Autofac.IoC.BusinessServices;
using FluentAssertions;

namespace Autofac.IoC.Tests
{
    public class InstancePerLifetimeScopeTests : BaseTests
    {
        [Test]
        public void should_is_not_the_same_instance()
        {
            OrdersService ordersService1, ordersService2;

            using (var scope = this.containerBuilder.BeginLifetimeScope())
            {
                ordersService1 = scope.Resolve<OrdersService>();
            }

            using (var scope = this.containerBuilder.BeginLifetimeScope())
            {
                ordersService2 = scope.Resolve<OrdersService>();
            }

            object.ReferenceEquals(ordersService1, ordersService2).ShouldBeEquivalentTo(false);
        }

        [Test]
        public void should_not_be_able_to_resolve()
        {
            CustomerService customerService1 = null, customerService2 = null;

            try
            {
                using (var scope = this.containerBuilder.BeginLifetimeScope())
                {
                    customerService1 = scope.Resolve<CustomerService>();

                    using (var scope1 = this.containerBuilder.BeginLifetimeScope("scope1"))
                    {
                        customerService2 = scope.Resolve<CustomerService>();
                    }

                    customerService1.ShouldBeEquivalentTo(null);
                }
            }
            catch (Exception)
            {
                    customerService1.ShouldBeEquivalentTo(null);
            }
        }

        [Test]
        public void should_be_able_to_resolve()
        {
            CustomerService customerService1, customerService2;

            using (var scope = this.containerBuilder.BeginLifetimeScope("scope1"))
            {
                customerService1 = scope.Resolve<CustomerService>();

                using (var scope1 = this.containerBuilder.BeginLifetimeScope())
                {
                    customerService2 = scope.Resolve<CustomerService>();
                }
            }

            object.ReferenceEquals(customerService1, customerService2).ShouldBeEquivalentTo(true);
        }
    }
}
