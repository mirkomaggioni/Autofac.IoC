﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Autofac.IoC.CoreServices;
using Autofac.IoC.Repositories;
using Autofac.IoC.BusinessServices;
using Autofac.Core;
using System.Configuration;
using System.Reflection;

namespace Autofac.IoC.Tests
{
    [TestFixture]
    public class BaseTests
    {
        protected IContainer containerBuilder;
        protected List<Product> _products;

        [SetUp]
        public void Setup()
        {
            _products = new List<Product>()
            {
                new Product()
                {
                    Code = 1,
                    Description = "Product1"
                },
                new Product()
                {
                    Code = 2,
                    Description = "Product2"
                },
                new Product()
                {
                    Code = 3,
                    Description = "Product3"
                }
            };

            var builder = new ContainerBuilder();
            
            // SINGLE INSTANCES
            builder.RegisterType<SingletonTokenService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<ProductsService>()
                .AsSelf()
                .SingleInstance();

            // PER DEPENDENCY
            var repositoriesAssembly = Assembly.GetAssembly(typeof(ProductsRepository));
            builder.RegisterAssemblyTypes(repositoriesAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .WithParameter(
                    new TypedParameter(typeof(string), ConfigurationManager.ConnectionStrings["Context"].ToString())
                );

            // ADDING PRESERVEEXISTINGDEFAULT IN ORDER TO MAKE SINGLETONTOKENSERVICE THE DEFAULT
            builder.RegisterType<PerDependencyTokenService>()
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PreserveExistingDefaults();

            // PER LIFETIMESCOPE
            builder.RegisterType<OrdersService>()
                .AsSelf()
                .InstancePerLifetimeScope();

            // PER MATCHING LIFETIMESCOPE
            builder.RegisterType<CustomerService>()
                .AsSelf()
                .InstancePerMatchingLifetimeScope("scope1");

            containerBuilder = builder.Build();
        }

        [TearDown]
        public void TearDown()
        {
            containerBuilder.Dispose();
        }
    }
}