using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rem.Models;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElevatorStatus : ContentPage
    {
        private readonly RESTHelper _RESTHelper = new RESTHelper();
        public ElevatorStatus()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnStatusChange(object sender, EventArgs e)
        {

            var elevator = (Elevator)BindingContext;

            elevator.TextColor = "green";
            elevator.status = "Online";

            var url = "https://rocket-elevators-foundation-restapi.azurewebsites.net/changeStatus/" + Convert.ToString(elevator.id);

            var t = Task.Run(() => _RESTHelper.put(url));
            t.Wait();

            await Navigation.PushAsync(new ElevatorStatus
            {
                BindingContext = elevator
            });
        }

        async void Logout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        async void MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}