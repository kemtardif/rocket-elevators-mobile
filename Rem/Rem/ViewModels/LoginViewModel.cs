using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using Rem.Models;

namespace Rem.ViewModels
{
    /// <summary>
    /// Biew Model used for the login
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        public Action DisplayInvalidLoginPrompt;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        public ICommand SubmitCommand { protected set; get; }
        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }

        /// <summary>
        /// 
        ///Call when login In, this call the api, which return list of employee emails, and we check if 
        ///the entered email 
        /// </summary>
        async public void OnSubmit()
        {
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
        }
    }
}