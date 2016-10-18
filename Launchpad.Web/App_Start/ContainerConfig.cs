﻿
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using Launchpad.Services;
using Launchpad.Data;
using System.Configuration;

namespace Launchpad.Web.App_Start
{
    public class ContainerConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            //Register the types in the container
            builder.RegisterModule(new DataModule(ConfigurationManager.ConnectionStrings["LaunchpadDataContext"].ConnectionString));
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<WebModule>();

            // Get your HttpConfiguration.
            var config = System.Web.Http.GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();


            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}