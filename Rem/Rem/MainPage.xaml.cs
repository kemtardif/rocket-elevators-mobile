using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rem.Models;
using Newtonsoft.Json.Linq;

namespace Rem
{
    public partial class MainPage : ContentPage
    {
        private readonly RESTHelper _RESTHelper;
        public IList<Elevator> Elevators { get; private set; }

        public MainPage()
        {


            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Elevators = new List<Elevator>();

            Elevators.Clear();
            _RESTHelper = new RESTHelper();

            /////Call api to display the elevators not in operations

            var url = "https://rocket-elevators-foundation-restapi.azurewebsites.net/api/Elevators/GetelevatorsStatus";

            var t = Task.Run(() => _RESTHelper.get(url));
            t.Wait();

            JArray elevatorJSON = JArray.Parse(t.Result);

            foreach (JObject item in elevatorJSON)
            {
                Elevators.Add(new Elevator
                {
                    id = (int)item["id"],
                    status = (string)item["status"],
                    serial_number = (string)item["serial_number"],
                    model = (string)item["model"],
                    type_building = (string)item["type_building "],
                    date_last_inspection = (DateTime)item["date_last_inspection"],
                    TextColor = "red"


                });


            }

            BindingContext = this;
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new ElevatorStatus
            {
                BindingContext = e.SelectedItem as Elevator
            });
        }

        async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new ElevatorStatus
            {
                BindingContext = e.Item as Elevator
            });
        }

         async void Logout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
