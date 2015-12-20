using Parse;

namespace YamAndRateApp.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private string currentUsername;

        public HeaderViewModel()
        {
            var user = ParseUser.CurrentUser;
            if (user != null )
            {
                this.CurrentUsername = user.Username;
            }
        }

        public string CurrentUsername {
            get
            {
                return this.currentUsername;
            }
            set
            {
                if (this.currentUsername != value)
                {
                    this.currentUsername = value;
                    this.NotifyPropertyChanged("CurrentUsername");
                }
            }
        }
    }
}
