using System;
using System.Threading.Tasks;
using GA360.PageModels;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Shiny;
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
            BindingContext = ShinyHost.ServiceProvider.GetService<HomePageModel>();
            HomePageModel hpm = BindingContext as HomePageModel;
            hpm.ConnectionFrameVisible = false;
            NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;

            NotificationCenter.Current.NotificationTapped += Current_NotificationTapped;
        }

        private void Current_NotificationTapped(NotificationEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //DisplayAlert("Notification tapped", "", "OK");
                if (await DisplayAlert("GA360", $"Do you want to be connected {Environment.NewLine}with a medical professional?", "Yes", "No"))
                {
                    HomePageModel hpm = BindingContext as HomePageModel;
                    hpm.ConnectionFrameVisible = true;
                }
            });
        }

        private  void Current_NotificationReceived(NotificationEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                if (await DisplayAlert("GA360", $"Do you want to be connected{Environment.NewLine}with a medical professional?", "Yes", "No"))
                {
                    HomePageModel hpm = BindingContext as HomePageModel;
                    hpm.ConnectionFrameVisible = true;
                }
            });
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
            var perm = await Xamarin.Essentials.Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var deniedOrUnknown = perm == PermissionStatus.Denied || perm == PermissionStatus.Unknown;
            if (deniedOrUnknown)
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

        public async void ButtonNotify_Clicked(object sender, System.EventArgs e)
        {
            await Task.Delay(100);
            HomePageModel hpm = BindingContext as HomePageModel;
            hpm.ConnectionFrameVisible = false;
            await Task.Delay(5000);
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Connect to Healthcare provider?",
                Title = "GA360 Proximity Alert",
                // ReturningData = "Dummy Data",
                NotificationId = 1337
                // NotifyTime = DateTime.Now.AddSeconds(5)
                
            };
            
            await NotificationCenter.Current.Show(notification);
        }

        public void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            HomePageModel hpm = BindingContext as HomePageModel;
            hpm.ConnectionFrameVisible = false;

        }
    }
}