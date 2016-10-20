using System;
using System.Reflection;
using System.Linq;
using Autofac.IoC.CoreServices;
using Autofac.IoC.BusinessServices;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using Autofac.IoC.Controllers.Controllers;
using Autofac.IoC.Controllers.Controllers.Api;

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

            var controllersAssembly = Assembly.GetAssembly(typeof(HomeController));
            var apiControllersAssembly = Assembly.GetAssembly(typeof(AccountController));

            builder.RegisterControllers(controllersAssembly);
            builder.RegisterApiControllers(apiControllersAssembly);
        }
    }
}
