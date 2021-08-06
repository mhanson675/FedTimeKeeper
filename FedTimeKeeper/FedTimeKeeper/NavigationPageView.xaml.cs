using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationPageView : ContentPage
    {
        private readonly IScheduledLeaveService leaveService;
        private readonly ILeaveSummaryService leaveSummaryService;

        public NavigationPageView(IScheduledLeaveService leaveService, ILeaveSummaryService leaveSummaryService)
        {
            InitializeComponent();
            this.leaveService = leaveService;
            this.leaveSummaryService = leaveSummaryService;
        }

        private async void HomePageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageView());
        }

        private async void AddLeaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddLeaveView());
        }

        private async void ViewLeaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LeaveInformationView(leaveSummaryService));
        }

        private async void ScheduledLeaveButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScheduledLeaveView());
        }

        private async void AppSettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsView());
        }
    }
}