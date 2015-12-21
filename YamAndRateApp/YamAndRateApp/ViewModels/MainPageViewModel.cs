namespace YamAndRateApp.ViewModels
{
    using Parse;
    
    using System.Windows.Input;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Utils;

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

            ToastManager toastManager = new ToastManager();
            var heading = "Successfully logged out!";
            var image = "/Assets/LockScreenLogo.scale-200.png";
            toastManager.CreateToast(heading, image);

            this.DisplayLogIn = true;
        }
    }
}
