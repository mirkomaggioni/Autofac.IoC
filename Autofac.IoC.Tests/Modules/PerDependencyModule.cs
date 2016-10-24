using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Autofac.Core;
using System.Reflection;
using Autofac.IoC.Repositories;
using Autofac.IoC.CoreServices;

namespace Autofac.IoC.Tests.Modules
{
    public class PerDependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var repositoriesAssembly = Assembly.GetAssembly(typeof(ProductsRepository));
            builder.RegisterAssemblyTypes(repositoriesAssembly)
                    .Where(t => t.Name.EndsWith("Repository"))
                    .AsImplementedInterfaces()
                    .WithParameter(
                        new TypedParameter(typeof(string), ConfigurationManager.ConnectionStrings["Context"].ToString())
                    );

            builder.RegisterType<PerDependencyTokenService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .Keyed<ITokenService>("perDependencyTokenService");
        }
    }
}
