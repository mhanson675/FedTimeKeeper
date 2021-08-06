using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FedTimeKeeper.Services;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.Utilities;
using FedTimeKeeper.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaveInformationView : ContentPage
    {
        public LeaveInformationView()
        {
            InitializeComponent();

            BindingContext = App.GetViewModel<LeaveInformationViewModel>();
        }
    }
}