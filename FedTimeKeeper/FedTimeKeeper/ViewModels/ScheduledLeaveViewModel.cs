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

        private ObservableCollection<ScheduledLeave> leaves;

        public ICommand DeleteLeaveCommand => new Command<ScheduledLeave>(OnDeleteLeave);
        //public ICommand LeaveSelectedCommand => new Command<ScheduledLeave>(OnLeaveSelected);
        public ObservableCollection<ScheduledLeave> ScheduledLeaves
        {
            get => leaves;
            set
            {
                leaves = value;
                OnPropertyChanged(nameof(ScheduledLeaves));
            }
        }


        public ScheduledLeaveViewModel(INavigationService navigation, IScheduledLeaveService leaveService)
        {
            this.navigation = navigation;
            this.leaveService = leaveService;
            ScheduledLeaves = new ObservableCollection<ScheduledLeave>();
            leaves = new ObservableCollection<ScheduledLeave>(this.leaveService.GetAllScheduled());
        }

        private void OnDeleteLeave(ScheduledLeave leave)
        {
            leaveService.DeleteLeave(leave);
        }
        //private void OnLeaveSelected(ScheduledLeave leave)
        //{
        //    navigation.NavigateTo(ViewNames.)
        //}
    }
}
