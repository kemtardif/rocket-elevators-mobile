# rocket-elevators-mobile
NEW API ROUTES : https://rocket-elevators-foundation-restapi.azurewebsites.net/changeStatus/
                  https://rocket-elevators-foundation-restapi.azurewebsites.net/api/Employees
                  
This mobile app was made using Xamarin, so to run it locally, you must open it using visual studio.

-For the login page, we first instantiate a LoginView model, used for the login, and add set the method "DisplayInvalidLoginPrompt" to the error message :

```
            var vm = new LoginViewModel();
            this.BindingContext = vm;


            vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
```

-The Email.completeted line change the focus to the submit command after entering the email.

-The Loginfview model's OnSubmit function is called when clicking the login button. It calls the API, which return the list of employees email. WHen then check
if the input email is found, then either go to main page or display the error message : 

```
         var _RESTHelper = new RESTHelper();
            var url = "https://rocket-elevators-foundation-restapi.azurewebsites.net/api/Employees";

            var t = Task.Run(() => _RESTHelper.get(url));
            t.Wait();

            var result = t.Result;

            if (result.Contains(email))
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayInvalidLoginPrompt();
            }
```

-On the main page, we have a list of elevators, which we clear first, then call the api for all elevators not in operation. It gives us back a JArray of (JSON) object, which we then cast into ELevators and then display the list :

```
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
```

- In the Elevator Status page, the OnStatusChange method is called when the status button is pressed. It changes the Elevator TextColor and status attributes, which is used in the html to change the...text color and status. A call is made to the api to change the information in the database. We then refresh the page with the updated elevator :

```            var elevator = (Elevator)BindingContext;

            elevator.TextColor = "green";
            elevator.status = "Online";

            var url = "https://rocket-elevators-foundation-restapi.azurewebsites.net/changeStatus/" + Convert.ToString(elevator.id);

            var t = Task.Run(() => _RESTHelper.put(url));
            t.Wait();

            await Navigation.PushAsync(new ElevatorStatus
            {
                BindingContext = elevator
            });
```

-I think that's about it, the rest is self-explanatory. The app works for ANdroid, and shouls work for IOs, since Xamarin is cross-platform (yay!)
