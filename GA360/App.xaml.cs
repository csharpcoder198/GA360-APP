using System.Threading.Tasks;
using Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GA360
{
    public partial class App : Application
    {
        // private reaIPreferencesService _preferencesService;
        public App()
        {
            InitializeComponent();
           // Startup.Init();

            MainPage = new AppShell();

        }

        protected override async void OnStart()
        {
            OnResume();
            await GpsPermissions();

        }

        private async Task GpsPermissions()
        {
            await Xamarin.Essentials.Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            /*
            object v = Startup.ServiceProvider.GetService(typeof(IPreferencesService));
            IPreferencesService ps = (IPreferencesService)v;
            ps.GPSPermissionStatus = (int)await Xamarin.Essentials.Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            */
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
            
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() => { TheTheme.SetTheme(); });
        }
    }
}