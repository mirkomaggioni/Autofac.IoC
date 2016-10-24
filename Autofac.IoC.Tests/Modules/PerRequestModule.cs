using System;
using System.Reflection;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.BusinessServices;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using Autofac.IoC.Controllers.Controllers;
using Autofac.IoC.Controllers.Controllers.Api;
using Autofac.Core;

namespace Autofac.IoC.Tests.Modules
{
    public class PerRequestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountService>()
                .AsSelf()
                .InstancePerRequest()
                .WithParameter(new ResolvedParameter(
                    (pi, ctx) => pi.ParameterType == typeof(ITokenService),
                    (pi, ctx) => ctx.ResolveKeyed<ITokenService>("singletonTokenService")
                ));

            var controllersAssembly = Assembly.GetAssembly(typeof(HomeController));
            var apiControllersAssembly = Assembly.GetAssembly(typeof(AccountController));

            builder.RegisterControllers(controllersAssembly);
            builder.RegisterApiControllers(apiControllersAssembly);
        }
    }
}
