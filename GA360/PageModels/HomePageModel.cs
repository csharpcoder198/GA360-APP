
using System;
using System.Threading.Tasks;
using System.Timers;
using GA360.Services;
using MvvmHelpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace GA360.PageModels
{
    public class HomePageModel : BaseViewModel
    {
        private readonly IPermissions _permissions;
        private readonly IDeviceInfo _deviceInfo;
        private readonly IPreferences _preferences;

        private Xamarin.Essentials.Location _location;

        PermissionStatus _permissionStatus;
        public PermissionStatus PermissionStatus
        {
            get => _permissionStatus;
            set => SetProperty(ref _permissionStatus, value);
        }

        public bool HasNetPermission { get; }
        public IAsyncCommand CheckGpsCommand  { get; }
        public IAsyncCommand RequestGrantPermissionAsyncCommand { get; }

        public IAsyncCommand SimpleCheckGpsCommand { get; }
        public HomePageModel(IPermissions permissions, IDeviceInfo deviceInfo, IPreferences preferences)
        {

            _permissions = permissions;
            _deviceInfo = deviceInfo;
            _preferences = preferences;
           
        }

        private async Task SimpleCheckGps()
        {
            var status = await _permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        }
    }
}