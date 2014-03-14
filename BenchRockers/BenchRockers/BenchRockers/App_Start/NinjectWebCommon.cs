[assembly: WebActivator.PreApplicationStartMethod(typeof(BenchRockers.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(BenchRockers.App_Start.NinjectWebCommon), "Stop")]

namespace BenchRockers.App_Start
{
    using System;
    using System.Web;
    using BenchRockers.BusinessLayer.Interfaces;
    using BenchRockers.BusinessLayer.Services;
    using BenchRockers.Common.Interfaces;
    using BenchRockers.DataAccessLayer;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDataContext>().To<BenchRockersDbContext>().WithConstructorArgument("connectionString", "name=BenchRockersDbContext");
            kernel.Bind<IServiceFacade>()
              .To<ServiceFacade>()
              .WithConstructorArgument("dbContext", context => context.Kernel.Get<IDataContext>())
              .WithConstructorArgument("DbContextFactory", context => context.Kernel.Get<DbContextFactory>());
        }        
    }
}
