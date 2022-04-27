using GA360.PageModels;
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
            BindingContext = Startup.ServiceProvider.GetService<AboutPageModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}