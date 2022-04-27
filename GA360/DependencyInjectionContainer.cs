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
            services.AddSingleton<IPreferences, PreferencesImplementation>();
            services.AddSingleton<IPreferencesService, PreferencesService>();
            services.AddSingleton<IClipboard, ClipboardImplementation>();
            services.AddSingleton<IDeviceInfo, DeviceInfoImplementation>();
            services.AddSingleton<IDeviceDisplay, DeviceDisplayImplementation>();
            services.AddSingleton<IAppInfo, AppInfoImplementation>();

            return services;
        }

        public static IServiceCollection ConfigureViewModels(this IServiceCollection services)
        {
            services.AddTransient<ConfigPageModel>();
            services.AddTransient<AboutPageModel>();

            return services;
        }
    }
}