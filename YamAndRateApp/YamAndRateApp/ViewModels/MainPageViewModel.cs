using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YamAndRateApp.Helpers;

namespace YamAndRateApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool displayLogIn;
        private RelayCommand logOut;

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
                    this.logOut = new RelayCommand(this.OnLogOutExecute);
                }

                return this.logOut;
            }
        }

        private void OnLogOutExecute(object obj)
        {
            ParseUser.LogOut();
            this.DisplayLogIn = true;
        }
    }
}
