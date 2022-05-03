using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials.Interfaces;

namespace GA360.Services
{
    public class PreferencesService : IPreferencesService
    {
       
        private readonly IPreferences _preferences;

        public PreferencesService(IPreferences preferences)
        {
            // _preferences = Startup.ServiceProvider.GetService<IPreferences>();
            _preferences = preferences;
        }


        private const int _radius = 3;
        public int Radius
        {
            // get => Settings.Radius;
            // set => Settings.Radius = value;
            get => _preferences.Get(nameof(Radius), _radius);
            set => _preferences.Set(nameof(Radius), value);
        }

        private const int _theme = 0;
        public int Theme
        {
            // get => Settings.Theme;
            // set => Settings.Theme = value;
            get => _preferences.Get(nameof(Theme), _theme);
            set => _preferences.Set(nameof(Theme), value);
        }


        private const string _language = "EN";
        public string LanguageCode
        {
            // get => Settings.LanguageCode;
            // set => Settings.LanguageCode = value;
            get => _preferences.Get(nameof(LanguageCode), _language);
            set => _preferences.Set(nameof(LanguageCode), value);
        }

        // Denied = 1
        // Disabled = 2
        // Granted = 3
        // Restricted = 4
        // Unknown = 4
        private const int _gpsPermissionStatus = 4;
        public int GPSPermissionStatus
        {
            // get => Settings.Radius;
            // set => Settings.Radius = value;
            get => _preferences.Get(nameof(GPSPermissionStatus), _gpsPermissionStatus);
            set => _preferences.Set(nameof(GPSPermissionStatus), value);
        }
    }
}