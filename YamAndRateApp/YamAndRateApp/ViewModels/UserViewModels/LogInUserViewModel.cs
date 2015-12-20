namespace YamAndRateApp.ViewModels.UserViewModels
{
    using System.Windows.Input;

    using Parse;

    using YamAndRateApp.Helpers;
    using YamAndRateApp.Utils;
    using System.Net;

    public class LogInUserViewModel : BaseUserViewModel
    {
        private ICommand loginUser;

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

            this.ErrorMessage = string.Empty;
            ToastManager toastManager = new ToastManager();
            var heading = "Successfully logged in!";
            var content = string.Format("User: {0}", this.Username);
            var image = "/Assets/LockScreenLogo.scale-200.png";
            var navigateTo = "main";
            toastManager.CreateToast(heading, content, image, navigateTo);
        }
    }
}
