using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Autofac.IoC.CoreServices;
using Autofac.IoC.Repositories;
using Autofac.IoC.BusinessServices;
using Autofac.Core;
using System.Configuration;
using System.Reflection;
using Autofac.IoC.Tests.Modules;
using System.Web.Http;
using Autofac.Integration.WebApi;

namespace Autofac.IoC.Tests
{
    [TestFixture]
    public class BaseTests
    {
        protected IContainer containerBuilder;
        protected HttpConfiguration httpConfiguration;

        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            // SINGLE INSTANCES
            builder.RegisterModule(new SingleInstancesModule());

            // PER DEPENDENCY
            builder.RegisterModule(new PerDependencyModule());

            // PER REQUEST
            builder.RegisterModule(new PerRequestModule());

            // PER LIFETIMESCOPE
            builder.RegisterModule(new PerLifetimeScopeModule());

            containerBuilder = builder.Build();

            httpConfiguration = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(containerBuilder)
            };
        }

    [TearDown]
    public void TearDown()
    {
        httpConfiguration.Dispose();
        containerBuilder.Dispose();
    }
}

}
