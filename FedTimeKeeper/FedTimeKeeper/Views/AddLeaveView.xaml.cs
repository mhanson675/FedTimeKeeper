using FedTimeKeeper.Models;
using FedTimeKeeper.Services.Interfaces;
using FedTimeKeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FedTimeKeeper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLeaveView : ContentPage
    {
        public AddLeaveView()
        {
            InitializeComponent();

            BindingContext = App.GetViewModel<AddLeaveViewModel>();
        }
    }
}