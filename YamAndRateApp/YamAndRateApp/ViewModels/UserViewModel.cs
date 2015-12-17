namespace YamAndRateApp.ViewModels
{
    using System;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;

    public class UserViewModel : BaseViewModel
    {
        private string email;
        private string password;
        private string confirmedPassword;
        private bool isInRegisterMode;
        private ICommand registerUser;
        private ICommand loginUser;

        public UserViewModel()
        {
            this.isInRegisterMode = false;
        }

        public ICommand RegisterUser
        {
            get
            {
                if (this.registerUser == null)
                {
                    this.registerUser = new RelayCommand(this.OnRegisterUserExecute);
                }

                return this.registerUser;
            }
        }

        public ICommand LogInUser
        {
            get
            {
                if (this.loginUser == null)
                {
                    this.loginUser = new RelayCommand(this.OnLogInUserExecute);
                }

                return this.loginUser;
            }
        }

        public string Email
        {
            get { return this.email; }

            set
            {
                if (value != this.email)
                {
                    this.email = value;
                    base.NotifyPropertyChanged("Email");
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

        public string ConfirmedPassword
        {
            get { return this.confirmedPassword; }

            set
            {
                if (value != this.confirmedPassword)
                {
                    this.confirmedPassword = value;
                    this.NotifyPropertyChanged("ConfirmedPassword");
                }
            }
        }

        public bool IsInRegisterMode
        {
            get { return this.isInRegisterMode; }

            set
            {
                if (value != this.isInRegisterMode)
                {
                    this.isInRegisterMode = value;
                    base.NotifyPropertyChanged("IsInRegisterMode");
                }
            }
        }

        private async void OnRegisterUserExecute()
        {
            if (!isInRegisterMode)
            {
                this.IsInRegisterMode = true;
                return;
            }

            /*
            // We should implement vaidations for email and pass length
            if (string.IsNullOrEmpty(this.Pasword)
                || string.IsNullOrEmpty(this.RepeatedPassword)
                || string.IsNullOrEmpty(this.Email))
            {
                // Retern error
                return;
            }

            // Register user
            return;
            */

            var user = new ParseUser()
            {
                Username = this.Email,
                Password = this.Password,
                Email = this.Email
            };

            await user.SignUpAsync();
        }

        private async void OnLogInUserExecute()
        {
            // We should implement vaidations for email and pass length
            /*
            if (string.IsNullOrEmpty(this.Pasword)
               || string.IsNullOrEmpty(this.Email))
            {
                // Return error message
                return;
            }

            // Login user
            return;
            */

            try
            {
                await ParseUser.LogInAsync(this.Email, this.Password);
                // Login was successful.
                // TODO: Redirect to all restaurants view?
            }
            catch (Exception e)
            {
                // The login failed. Check the error to see why.
            }
        }
    }
}
