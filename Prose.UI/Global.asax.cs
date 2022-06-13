using SimpleInjector;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Prose.Core.Interfaces.Repository;
using Prose.Infrastructure.Persistency.Repository;
using System.Reflection;
using Prose.Application.Interfaces;
using Prose.Application.Services;
using Prose.Application.Mappers;

namespace Prose.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);    


            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<ITopicService, TopicService>(Lifestyle.Scoped);

            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<ITopicRepository, TopicRepository>(Lifestyle.Scoped);

            // Automapper
            container.RegisterSingleton<MapperProvider>();
            container.RegisterSingleton(() => GetMapper(container));


            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private AutoMapper.IMapper GetMapper(Container container)
        {
            var mp = container.GetInstance<MapperProvider>();
            return mp.GetMapper();
        }
    }
}
