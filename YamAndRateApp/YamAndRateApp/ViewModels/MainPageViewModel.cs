using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YamAndRateApp.Helpers;
using YamAndRateApp.Utils;

namespace YamAndRateApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool displayLogIn;
        private RelayCommand logOut;
        private RelayCommand executeRestaurantSearch;

        public MainPageViewModel()
        {
            this.ValidateUser();
        }

        private void ValidateUser()
        {
            var currentUser = ParseUser.CurrentUser;

            if (currentUser == null)
            {
                this.DisplayLogIn = true;
            }
        }

        public bool DisplayLogIn
        {
            get
            {
                return this.displayLogIn;
            }
            set
            {
                if (this.displayLogIn != value)
                {
                    this.displayLogIn = value;
                    this.NotifyPropertyChanged("DisplayLogIn");
                }
            }
        }

        public ICommand LogOut
        {
            get
            {
                if (this.logOut == null)
                {
                    this.logOut = new RelayCommand(this.OnSearchExecute);
                }

                return this.logOut;
            }
        }

        public ICommand ExecuteRestaurantSearch
        {
            get
            {
                if (this.executeRestaurantSearch == null)
                {
                    this.executeRestaurantSearch = new RelayCommand(this.OnSearchExecute);
                }

                return this.executeRestaurantSearch;
            }
        }

        private void OnSearchExecute(object obj)
        {
            var searchPattern = obj.ToString();

            if (String.IsNullOrEmpty(searchPattern))
            {

            }
        }

        private void OnLogOutExecute(object obj)
        {
            ParseUser.LogOut();

            ToastManager toastManager = new ToastManager();
            var heading = "Successfully logged out!";
            var image = "/Assets/LockScreenLogo.scale-200.png";
            toastManager.CreateToast(heading, image);

            this.DisplayLogIn = true;
        }
    }
}
