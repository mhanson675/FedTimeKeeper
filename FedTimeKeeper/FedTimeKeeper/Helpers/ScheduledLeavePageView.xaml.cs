using FedTimeKeeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper.Helpers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduledLeavePageView : ContentPage
    {
        private List<ScheduledLeave> scheduledLeaves;

        public ScheduledLeavePageView()
        {
            InitializeComponent();

            LoadScheduledLeaves();
        }

        private void LoadScheduledLeaves()
        {
            scheduledLeaves = new List<ScheduledLeave>
            {
                new ScheduledLeave
                {
                    StartDate = new DateTime(2021, 01, 01),
                    EndDate = new DateTime(2021, 01, 02),
                    HoursTaken = 8.0,
                    Type = LeaveType.Annual
                },
                new ScheduledLeave
                {
                    StartDate = new DateTime(2021, 02, 01),
                    EndDate = new DateTime(2021, 02, 02),
                    HoursTaken = 8.0,
                    Type = LeaveType.Annual
                },
                new ScheduledLeave
                {
                    StartDate = new DateTime(2021, 03, 01),
                    EndDate = new DateTime(2021, 03, 02),
                    HoursTaken = 8.0,
                    Type = LeaveType.Timeoff
                },
                new ScheduledLeave
                {
                    StartDate = new DateTime(2021, 04, 01),
                    EndDate = new DateTime(2021, 04, 02),
                    HoursTaken = 8.0,
                    Type = LeaveType.Sick
                },
            };

            ScheduledLeaveListView.ItemsSource = scheduledLeaves;
        }
    }
}