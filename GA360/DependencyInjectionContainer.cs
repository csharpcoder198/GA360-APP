using GA360.PageModels;
using GA360.Services;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace GA360
{
    public static class DependencyInjectionContainer
    {
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