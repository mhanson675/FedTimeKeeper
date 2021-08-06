using FedTimeKeeper.Models;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduledLeaveView : ContentPage
    {
        public ScheduledLeaveView()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<ScheduledLeaveViewModel>();
        }
    }
}