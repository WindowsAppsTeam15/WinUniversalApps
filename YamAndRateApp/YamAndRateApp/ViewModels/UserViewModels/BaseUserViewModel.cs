namespace YamAndRateApp.ViewModels.UserViewModels
{
    public abstract class BaseUserViewModel : ViewModelBase
    {
        private string username;
        private string password;
        private string errorMessage;

        public BaseUserViewModel()
        {
            this.ErrorMessage = string.Empty;
        }

        public string Username
        {
            get { return this.username; }

            set
            {
                if (value != this.username)
                {
                    this.username = value;
                    base.NotifyPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return this.password; }

            set
            {
                if (value != this.password)
                {
                    this.password = value;
                    this.NotifyPropertyChanged("Password");
                }
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
                this.NotifyPropertyChanged("ErrorMessage");
            }
        }
    }
}
