using System;
using System.Web;
using AutoMapper;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using ProEvoCanary;
using ProEvoCanary.Authentication;
using ProEvoCanary.Domain.Helpers;
using ProEvoCanary.Domain.Helpers.Interfaces;
using ProEvoCanary.Domain.Repositories;
using ProEvoCanary.Domain.Repositories.Interfaces;
using IConfiguration = ProEvoCanary.Domain.Helpers.Interfaces.IConfiguration;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace ProEvoCanary
{
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
                kernel.Bind<HttpContext>().ToMethod(ctx => HttpContext.Current).InTransientScope();
                kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
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
            // Helper interfaces

            kernel.Bind<ICacheManager>().To<CachingManager>();
            kernel.Bind<ICacheRssLoader>().To<RssCacheLoader>();
            kernel.Bind<IConfiguration>().To<Configuration>();
            kernel.Bind<IDBHelper>().To<DBHelper>();
            kernel.Bind<IRssLoader>().To<RssLoader>();
            kernel.Bind<IAuthenticationHandler>().To<AuthenticationHandler>();
            kernel.Bind<IPasswordHash>().To<PasswordHash>();
            kernel.Bind<IAppUser>().To<CurrentAppUser>();
            kernel.Bind<IXmlGenerator>().To<XmlGenerator>();


            // Repository interfaces

            kernel.Bind<IAdminEventRepository>().To<AdminEventRepository>();
            kernel.Bind<ICacheEventRepository>().To<CacheEventRepository>();
            kernel.Bind<ICachePlayerRepository>().To<CachePlayerRepository>();
            kernel.Bind<ICacheResultsRepository>().To<ResultsCacheRepository>();
            kernel.Bind<IEventRepository>().To<AdminEventRepository>();
            kernel.Bind<IPlayerRepository>().To<PlayerRepository>();
            kernel.Bind<IResultRepository>().To<ResultsRepository>();
            kernel.Bind<IRssFeedRepository>().To<RssFeedRepositoryDecorator>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            //Auto mapper
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Domain.Models.RecordsModel, Models.RecordsModel>();
                cfg.CreateMap<Domain.Models.PlayerModel, Models.PlayerModel>();
                cfg.CreateMap<Domain.Models.EventModel, Models.EventModel>();
                cfg.CreateMap<Domain.Models.ResultsModel, Models.ResultsModel>();
                cfg.CreateMap<Domain.Models.RssFeedModel, Models.RssFeedModel>();
            });

            kernel.Bind<IMapper>().ToConstant(mapperConfiguration.CreateMapper());
        }
    }
}
