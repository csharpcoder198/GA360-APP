using GA360.PageModels;
using Shiny;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GA360.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = ShinyHost.ServiceProvider.GetService<AboutPageModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}