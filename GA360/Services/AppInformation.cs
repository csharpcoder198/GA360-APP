using System;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace GA360.Services
{
    public class AppInformation : IAppInfo
    {
        private readonly IAppInfo _appInfo;

        public AppInformation()
        {
            _appInfo = Startup.ServiceProvider.GetService<IAppInfo>();
        }

        public void ShowSettingsUI()
        {
            _appInfo.ShowSettingsUI();
        }

        public string PackageName => _appInfo.PackageName;

        public string Name => _appInfo.Name;

        public Version Version => _appInfo.Version;

        public string VersionString => _appInfo.VersionString;

        public string BuildString => _appInfo.BuildString;

        public AppTheme RequestedTheme => _appInfo.RequestedTheme;
    }
}