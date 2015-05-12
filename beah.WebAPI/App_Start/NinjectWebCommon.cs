using System.Collections.Generic;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using beah.Library.Web.REST;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(beah.WebAPI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(beah.WebAPI.App_Start.NinjectWebCommon), "Stop")]

namespace beah.WebAPI.App_Start
{
    using System;
    using System.Web;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                DependencyResolver.SetResolver(new NinjectResolver(kernel));

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRestHelper>().To<RestHelper>();

        }        
    }

    public class NinjectResolver :  IDependencyResolver
    {
        IKernel _kernal;

        public NinjectResolver(IKernel kernel)
  {
    _kernal = kernel;
    
  }
 
  public object GetService(Type serviceType)
  {
    return _kernal.TryGet(serviceType);
  }
 
  public IEnumerable<object> GetServices(Type serviceType)
  {
    return _kernal.GetAll(serviceType);
  }
    }


}
