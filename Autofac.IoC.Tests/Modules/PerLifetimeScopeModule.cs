using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac.IoC.BusinessServices;

namespace Autofac.IoC.Tests.Modules
{
    public class PerLifetimeScopeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrdersService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            // PER MATCHING LIFETIMESCOPE
            builder.RegisterType<CustomerService>()
                .AsSelf()
                .InstancePerMatchingLifetimeScope("scope1");
        }

    }
}
