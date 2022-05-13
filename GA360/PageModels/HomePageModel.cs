
using System.Threading.Tasks;
using MvvmHelpers;
using Shiny.Locations;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace GA360.PageModels
{
    public class HomePageModel : BaseViewModel
    {
        private readonly IPermissions _permissions;
        private readonly IDeviceInfo _deviceInfo;
        private readonly IPreferences _preferences;
        private readonly IMessagingCenter _messagingCenter;
        private readonly IGeofenceManager _geofenceManager;
        private readonly IGeofenceDelegate _geofenceDelegate;

        private Xamarin.Essentials.Location _location;

        private bool _connectionFrameVisible;
        public bool ConnectionFrameVisible
        {
            get => _connectionFrameVisible;
            set => SetProperty(ref _connectionFrameVisible, value);
        }

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
        public HomePageModel(IPermissions permissions, IDeviceInfo deviceInfo, IPreferences preferences, IMessagingCenter messagingCenter)
        {

            _permissions = permissions;
            _deviceInfo = deviceInfo;
            _preferences = preferences;
            _messagingCenter = messagingCenter;
            
            _messagingCenter.Subscribe<HomePageModel, object>(this, "position", async (sender, arg) =>
             {
                 await Task.Delay(100);


             });

            Startup.ServiceProvider.GetService(typeof(IGeofenceManager));


        }

        private async Task SimpleCheckGps()
        {
            var status = await _permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            
        }
    }
}