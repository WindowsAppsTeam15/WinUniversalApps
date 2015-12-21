using Windows.Devices.Geolocation;

namespace YamAndRateApp.ViewModels.RestaurantViewModels
{
    public class BaseRestaurantViewModel : ViewModelBase
    {
        private string name;
        private string category;
        
        private string photoUrl;

        public string Id { get; set; }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
                this.NotifyPropertyChanged("Category");
            }
        }

        public string PhotoUrl
        {
            get
            {
                return this.photoUrl;
            }
            set
            {
                this.photoUrl = value;
                this.NotifyPropertyChanged("PhotoUrl");
            }
        }

        public Geopoint Coordinates { get; set; }
    }
}
