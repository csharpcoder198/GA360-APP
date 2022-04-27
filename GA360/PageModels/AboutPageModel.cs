using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace GA360.PageModels
{
    public class AboutPageModel : BaseViewModel, IAppInfo, IDeviceInfo, IDeviceDisplay
    {
        private readonly IDeviceInfo _deviceInfo;
        private readonly IDeviceDisplay _displayInfo;

        private readonly IAppInfo _info;

        public AboutPageModel(IAppInfo info, IDeviceInfo deviceInfo, IDeviceDisplay displayInfo)
        {
            _info = info;
            _deviceInfo = deviceInfo;
            _displayInfo = displayInfo;
            var appDict = AppInfoToDictionary();
            var devDict = DevInfoToDictionary();
            AboutApp = DictionaryToStringTable(appDict) + DictionaryToStringTable(devDict);

            /*var scrDict = ScrInfoToDictionary();
            AboutDevice = DictionaryToStringTable(scrDict)*/
            ;
        }

        public string AboutApp { get; }
        public string AboutDevice { get; }

        public ICommand ClickCommand => new Command<string>(url => { Device.OpenUri(new Uri(url)); });

        public ICommand ShowSettingsCommand => new Command(url => { ShowSettingsUI(); });

        public void ShowSettingsUI()
        {
            _info.ShowSettingsUI();
        }

        public string PackageName => _info.PackageName;

        public string Name => _info.Name;
        public string VersionString => _info.VersionString;

        public Version Version => _info.Version;

        public string BuildString => _info.BuildString;

        public AppTheme RequestedTheme => _info.RequestedTheme;

        public bool KeepScreenOn { get; set; }
        public DisplayInfo MainDisplayInfo => _displayInfo.MainDisplayInfo;
        public event EventHandler<DisplayInfoChangedEventArgs> MainDisplayInfoChanged;

        public string Model => _deviceInfo.Model;

        public string Manufacturer => _deviceInfo.Manufacturer;

        public DevicePlatform Platform => _deviceInfo.Platform;

        public DeviceIdiom Idiom => _deviceInfo.Idiom;
        public DeviceType DeviceType => _deviceInfo.DeviceType;

        private string DictionaryToStringTable(Dictionary<string, string> dict)
        {
            var sb = new StringBuilder();
            foreach (var kvp in dict) sb.AppendLine($"{kvp.Key}: {kvp.Value}");

            return sb.ToString();
        }


        // AppInfo

        private Dictionary<string, string> AppInfoToDictionary()
        {
            var dict = new Dictionary<string, string>();
            //dict.Add(nameof(PackageName), PackageName);
            dict.Add(nameof(Name), Name);
            dict.Add(nameof(Version), $"{Version}");
            dict.Add("Build", BuildString);


            return dict;
        }

        // DeviceInfo
        private Dictionary<string, string> DevInfoToDictionary()
        {
            var dict = new Dictionary<string, string>();
            dict.Add(nameof(Manufacturer), $"{Manufacturer}");
            dict.Add(nameof(Model), $"{Model}");
            dict.Add(nameof(Platform), $"{Platform} {_deviceInfo.VersionString}");
            dict.Add("Screen Size (h x w) ", $"{MainDisplayInfo.Height} x {MainDisplayInfo.Width}");

            return dict;
        }

        // DisplayInfo
        private Dictionary<string, string> ScrInfoToDictionary()
        {
            var dict = new Dictionary<string, string>();
            //dict.Add(nameof(PackageName), PackageName);
            dict.Add("Screen Size (h x w): ", $"{MainDisplayInfo.Height} x {MainDisplayInfo.Width}");

            return dict;
        }
    }
}