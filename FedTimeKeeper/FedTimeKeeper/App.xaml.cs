using FedTimeKeeper.Database;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using FedTimeKeeper.ViewModels;
using FedTimeKeeper.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }
        public enum Theme { Light, Dark }
        public static Theme AppTheme { get; set; }

        public static BaseViewModel GetViewModel<TViewModel>()
            where TViewModel : BaseViewModel =>
            ServiceProvider.GetRequiredService<TViewModel>();

        public App(Action<IServiceCollection> addPlatformServices = null)
        {
            InitializeComponent();

            SetupServices(addPlatformServices);
            ConfigureNavigation();

            var localDatabase = ServiceProvider.GetRequiredService<ILocalDatabase>();
            localDatabase.Initialize();

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

        private void SetupServices(Action<IServiceCollection> addPlatformServices = null)
        {
            var services = new ServiceCollection();

            //TODO: Add Platform Specific Services
            addPlatformServices?.Invoke(services);

            //TODO: Add Viewmodels
            services.AddTransient<NavigationPageViewModel>();
            services.AddTransient<AddLeaveViewModel>();
            services.AddTransient<HomePageViewModel>();
            services.AddTransient<LeaveInformationViewModel>();
            services.AddTransient<ScheduledLeaveViewModel>();
            services.AddTransient<SettingsViewModel>();

            //TODO: Add Core Services here
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<ILocalDatabase, SqliteDatabase>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IScheduledLeaveService, ScheduledLeaveService>();
            services.AddScoped<ILeaveSummaryService, LeaveSummaryService>();
            services.AddScoped<FedAnnualLeaveCalculator>();
            services.AddScoped<FedSickLeaveCalculator>();
            services.AddScoped(sp => new FederalPayCalendar(
                    sp.GetRequiredService<ISettingsService>().GetFirstPayPeriodStart(),
                    sp.GetRequiredService<ISettingsService>().GetFirstPayPeriodStart()));
            
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureNavigation()
        {
            var navigationService = ServiceProvider.GetRequiredService<INavigationService>();

            navigationService.Configure(ViewNames.AddLeaveView, typeof(AddLeaveView));
            navigationService.Configure(ViewNames.HomePageView, typeof(HomePageView));
            navigationService.Configure(ViewNames.LeaveInformationView, typeof(LeaveInformationView));
            navigationService.Configure(ViewNames.ScheduledLeaveView, typeof(ScheduledLeaveView));
            navigationService.Configure(ViewNames.SettingsView, typeof(SettingsView));
        }
    }
}
