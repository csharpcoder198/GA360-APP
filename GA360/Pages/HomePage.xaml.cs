using System;
using System.Threading.Tasks;
using GA360.PageModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GA360.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private bool isGettingLocation;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<HomePageModel>();
        }

        
        void Button1_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = false;
        }


        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            isGettingLocation = true;
            var btn = sender as Button;
            resultLocation.Text = string.Empty;
            btn.IsEnabled = false;
            var perm = await Xamarin.Essentials.Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            var isGranted = perm == PermissionStatus.Granted;
            if (!isGranted)
                resultLocation.Text = "You will need to Go to settings | Privacy | GA360 and allow locations to use the app";
            else
            {
                while (isGettingLocation)
                {
                    try
                    {
                        stopButton.IsEnabled = true;
                        var result = await Xamarin.Essentials.Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMinutes(1)));
                        resultLocation.Text += $"lat: {result.Latitude}, lng: {result.Longitude}{Environment.NewLine}";

                        await Task.Delay(1000);
                    }catch (Exception ex)
                    {
                        resultLocation.Text += $"{Environment.NewLine}{ ex.Message}";
                        isGettingLocation = false;
                    }
                }
            }
            stopButton.IsEnabled = false;
            btn.IsEnabled = true;
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();
           
        }
    }
}