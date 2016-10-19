using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Tests.Modules
{
    public class PerRequestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PerRequestTokenService>()
                .AsSelf()
                .InstancePerRequest();

            builder.RegisterType<AccountService>()
                .AsSelf()
                .InstancePerRequest();
        }
    }
}
