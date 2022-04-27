using GA360.PageModels;
using GA360.Services;
using Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GA360.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigPage : ContentPage
    {
        private readonly IPreferencesService _preferencesService;

        private bool loaded;

        public ConfigPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<ConfigPageModel>();

            _preferencesService = Startup.ServiceProvider.GetService<IPreferencesService>();
            switch (_preferencesService.Theme)
            {
                case 0:
                    RadioButtonSystem.IsChecked = true;
                    break;
                case 1:
                    RadioButtonLight.IsChecked = true;
                    break;
                case 2:
                    RadioButtonDark.IsChecked = true;
                    break;
            }

            switch (_preferencesService.LanguageCode)
            {
                case "EN":
                    RadioButtonEnglish.IsChecked = true;
                    break;
                case "ES":
                    RadioButtonSpanish.IsChecked = true;
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loaded = true;
            var vm = (ConfigPageModel)BindingContext;
            vm.IsVisibleToClipBoard = false;
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!loaded)
                return;

            if (!e.Value)
                return;

            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;

            switch (val)
            {
                case "System":
                    _preferencesService.Theme = 0;
                    break;
                case "Light":
                    _preferencesService.Theme = 1;
                    break;
                case "Dark":
                    _preferencesService.Theme = 2;
                    break;
            }

            TheTheme.SetTheme();
        }

        private void LanguageRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!loaded)
                return;

            if (!e.Value)
                return;

            var val = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(val))
                return;

            _preferencesService.LanguageCode = val;
        }
    }
}