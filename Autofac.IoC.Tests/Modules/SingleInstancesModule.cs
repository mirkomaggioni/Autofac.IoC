using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.BusinessServices;
using Autofac.Core;

namespace Autofac.IoC.Tests.Modules
{
    public class SingleInstancesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SingletonTokenService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance()
                .Keyed<ITokenService>("singletonTokenService");

            builder.RegisterType<ProductsService>()
                .AsSelf()
                .SingleInstance()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(ITokenService),
                    (pi, ctx) => ctx.ResolveKeyed<ITokenService>("singletonTokenService")
                ));

            builder.RegisterType<LoggerService>()
                .AsSelf()
                .SingleInstance()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(ITokenService),
                    (pi, ctx) => ctx.ResolveKeyed<ITokenService>("singletonTokenService")
                ));
        }
    }
}
