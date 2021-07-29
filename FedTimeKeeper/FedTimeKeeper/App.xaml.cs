using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper
{
    public partial class App : Application
    {
        public enum Theme { Light, Dark }
        public static Theme AppTheme { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new NavigationPageView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
