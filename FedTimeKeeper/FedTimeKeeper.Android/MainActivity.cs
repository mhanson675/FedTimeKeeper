using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;

namespace FedTimeKeeper.Droid
{
    [Activity(Label = "FedTimeKeeper", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());
            //LoadApplication(new App(AddServices));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        static void AddServices(IServiceCollection services)
        {
            //TODO: Add any platform services or remove
            //example: services.AddSingleton<ISomeAndroidService, SomeAndroidImplementedService>();
        }
    }
}