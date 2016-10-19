using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Tests.Modules
{
    public class SingleInstancesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SingletonTokenService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ProductsService>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<LoggerService>()
                .AsSelf()
                .SingleInstance();

        }
    }
}
