using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FedTimeKeeper.ViewModels
{
    public class ScheduledLeaveViewModel : BaseViewModel
    {
        private readonly INavigationService navigation;
        private readonly IScheduledLeaveService leaveService;

        public ICommand DeleteLeaveCommand => new Command<ScheduledLeave>(OnDeleteLeave);

        private ObservableCollection<ScheduledLeave> scheduledLeaves;
        public ObservableCollection<ScheduledLeave> ScheduledLeaves
        {
            get => scheduledLeaves;
            set
            {
                scheduledLeaves = value;
                OnPropertyChanged(nameof(ScheduledLeaves));
            }
        }


        public ScheduledLeaveViewModel(INavigationService navigation, IScheduledLeaveService leaveService)
        {
            this.navigation = navigation;
            this.leaveService = leaveService;
            LoadScheduledLeaves();
        }

        private void OnDeleteLeave(ScheduledLeave leave)
        {
            leaveService.DeleteLeave(leave);
            LoadScheduledLeaves();
        }

        private void LoadScheduledLeaves()
        {
            ScheduledLeaves = new ObservableCollection<ScheduledLeave>(leaveService.GetAllScheduled());
        }
    }
}
