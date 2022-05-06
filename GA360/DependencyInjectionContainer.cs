using GA360.PageModels;
using GA360.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
namespace GA360
{
    public static class DependencyInjectionContainer
    {
        /// <summary>
        /// To Get one of these services the most likely pattern will be to
        /// use Constructor Injection.
        /// Alternatively you may call
        /// <IServivice> = Startup.ServiceProvider.GetService<Service>();
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<IAppInfo, AppInfoImplementation>();
            services.AddSingleton<IDeviceDisplay, DeviceDisplayImplementation>();
            services.AddSingleton<IPermissions, PermissionsImplementation>();
            services.AddSingleton<IPreferences, PreferencesImplementation>();
            services.AddSingleton<IPreferencesService, PreferencesService>();
            services.AddSingleton<IClipboard, ClipboardImplementation>();
            services.AddSingleton<IDeviceInfo, DeviceInfoImplementation>();
            services.AddSingleton<IGeolocation, GeolocationImplementation>();
            services.AddSingleton<IMessagingCenter, MessagingCenter>();
            
            return services;
        }

        /// <summary>
        /// This is called from Startup.Init. 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<HomePageModel>();
            services.AddTransient<ConfigPageModel>();
            services.AddTransient<AboutPageModel>();
            

            return services;
        }
    }
}