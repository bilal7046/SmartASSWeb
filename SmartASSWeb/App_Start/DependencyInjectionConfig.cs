using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Core;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb
{
    public class DependencyInjectionConfig
    {
        public static void  Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<FirebaseAdminService>().As<IFirebaseAdminService>();
            builder.RegisterType<FirebaseService>().As<IFirebaseService>();
            //builder.RegisterType<MockFirebaseService>().As<IFirebaseService>();
            builder.RegisterType<KeyFileResolver>().As<IKeyFileResolver>();
            builder.RegisterType<PackageResolverService>().As<IPackageResolverService>();
            builder.RegisterType<AddressService>().As<IAddressService>();
            builder.RegisterType<NotificationService>().As<INotificationService>();
            builder.RegisterType<ImageReSizerService>().As<IImageReSizerService>();
            builder.RegisterType<LookupDictionary>().As<ILookupDictionary>();
            builder.RegisterType<ActivityService>().As<IActivityService>();
            builder.RegisterType<UserManagementService>().As<IUserManagementService>();
            builder.RegisterType<UniqueCodeGenerator>().As<IUniqueCodeGenerator>();
            builder.RegisterType<QRCodeGeneratorService>().As<IQRCodeGeneratorService>();
            builder.RegisterType<WeatherService>().As<IWeatherService>();
            builder.RegisterType<BusinessCardLinkService>().As<IBusinessCardLinkService>();
            
            builder.RegisterType<ChatService>().As<IChatService>();
            builder.RegisterType<BusinessCardChartDataService>().As<IBusinessCardChartDataService>();
            builder.RegisterType<ManagerChartDataService>().As<IManagerChartDataService>();
            builder.RegisterType<TeamMemberChartDataService>().As< ITeamMemberChartDataService>();

            builder.RegisterType<BusinessCardMapper>().As<IBusinessCardMapper>();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}