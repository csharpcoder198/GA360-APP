﻿using Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GA360
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Startup.Init();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            OnResume();
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