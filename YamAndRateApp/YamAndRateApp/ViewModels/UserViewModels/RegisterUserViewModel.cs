namespace YamAndRateApp.ViewModels.UserViewModels
{
    using System;
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Utils;
    using System.Net;

    public class RegisterUserViewModel : BaseUserViewModel
    {
        private string email;
        private string confirmedPassword;
        private ICommand registerUser;

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
                usersWithCurrentEmail = await ParseUser.Query.WhereEqualTo("email", this.Email).CountAsync();
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

            this.ErrorMessage = "";
            ToastManager toastManager = new ToastManager();
            var heading = "Successful registration!";
            var content = string.Empty;
            var image = "/Assets/LockScreenLogo.scale-200.png";
            var navigateTo = "login";
            toastManager.CreateToast(heading, content, image, navigateTo);
        }
    }
}
