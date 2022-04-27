using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials.Interfaces;

namespace GA360.Services
{
    public class PreferencesService : IPreferencesService
    {
        private const int _theme = 0;

        private const int _radius = 3;

        private const string _language = "EN";

        private readonly IPreferences _preferences;

        public PreferencesService()
        {
            _preferences = Startup.ServiceProvider.GetService<IPreferences>();
        }

        public int Radius
        {
            // get => Settings.Radius;
            // set => Settings.Radius = value;
            get => _preferences.Get(nameof(Radius), _radius);
            set => _preferences.Set(nameof(Radius), value);
        }

        public int Theme
        {
            // get => Settings.Theme;
            // set => Settings.Theme = value;
            get => _preferences.Get(nameof(Theme), _theme);
            set => _preferences.Set(nameof(Theme), value);
        }

        public string LanguageCode
        {
            // get => Settings.LanguageCode;
            // set => Settings.LanguageCode = value;
            get => _preferences.Get(nameof(LanguageCode), _language);
            set => _preferences.Set(nameof(LanguageCode), value);
        }
    }
}