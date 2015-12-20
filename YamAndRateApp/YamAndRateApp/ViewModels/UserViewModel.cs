namespace YamAndRateApp.ViewModels
{
    using System;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Utils;
    using System.Net;

    public class UserViewModel : BaseViewModel
    {
        private string username;
        private string email;
        private string password;
        private string confirmedPassword;
        private string errorMessage;
        private ICommand registerUser;
        private ICommand loginUser;

        public UserViewModel()
        {
            this.ErrorMessage = string.Empty;
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

        private async void OnRegisterUserExecute(object parameters)
        {
            if (Validator.ValidateUserRegistration(this.Username, this.Email, this.Password, this.ConfirmedPassword) != string.Empty)
            {
                this.ErrorMessage = Validator.ValidateUserRegistration(this.Username, this.Email, this.Password, this.ConfirmedPassword);
                return;
            }

            int usersWithCurrentUsername = 0;
            try
            {
                usersWithCurrentUsername = await ParseUser.Query.WhereEqualTo("username", this.Username).CountAsync();
            }
            catch (Exception)
            {

            }

            if (usersWithCurrentUsername != 0)
            {
                this.ErrorMessage = "Username is already taken!";
                return;
            }

            int usersWithCurrentEmail = 0;

            try
            {
               usersWithCurrentEmail  = await ParseUser.Query.WhereEqualTo("email", this.Email).CountAsync();
            }
            catch (Exception)
            {

            }

            if (usersWithCurrentEmail != 0)
            {
                this.ErrorMessage = "Email is already taken!";
                return;
            }

            var user = new ParseUser()
            {
                Username = this.Username,
                Password = this.Password,
                Email = this.Email
            };

            try
            {
                await user.SignUpAsync();                
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    this.ErrorMessage = "No internet connection!";
                    return;
                }    
                
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    this.ErrorMessage = "Invalid e-mail!";
                    return;
                }            
            }

            // TODO: Somehow redirect to login page  
            this.ErrorMessage = "";
            ToastManager toastManager = new ToastManager();
            var heading = "Successful registration!";
            var content = string.Empty;
            var image = "/Assets/LockScreenLogo.scale-200.png";
            toastManager.CreateToast(heading, content, image);
        }

        private async void OnLogInUserExecute(object parameters)
        {
            try
            {
                await ParseUser.LogInAsync(this.Username, this.Password);                
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    this.ErrorMessage = "No internet connection!";
                    return;
                }

                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    this.ErrorMessage = "Invalid username or password!";
                    return;
                }
            }

            // TODO: Somehow redirect to home page?
            this.ErrorMessage = string.Empty;
            ToastManager toastManager = new ToastManager();
            var heading = "Successfully logged in!";
            var content = string.Format("User: {0}", this.Username);
            var image = "/Assets/LockScreenLogo.scale-200.png";
            toastManager.CreateToast(heading, content, image);
        }
    }
}
