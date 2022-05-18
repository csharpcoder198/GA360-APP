using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Shiny;

//[assembly: Dependency(typeof(GA360.Droid.Environment))]
namespace GA360.Droid
{
    [Activity(Label = "GA360", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            this.ShinyOnNewIntent(intent);
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            this.ShinyOnActivityResult(requestCode, resultCode, data);
        }
    }
    
    
    /*
     * 
    TODO Findout Why this is here. It wont compile but the sample MyCoffeeApp has it included.
    The actual Dark/Light seems to work. Dont know Why.
    public class Environment : IEnvironment
    {
        public async void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
        {
            if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                return;

            var activity = Platform.CurrentActivity;
            var window = activity.Window;

            //this may not be necessary(but may be fore older than M)
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {   
                await Task.Delay(50);
                WindowCompat.GetInsetsController(window, window.DecorView).AppearanceLightStatusBars = darkStatusBarTint;
            }

            window.SetStatusBarColor(color.ToPlatformColor());
        }
        
    }
    */

    
}
