namespace YamAndRateApp.ViewModels.RestaurantViewModels
{
    using System.Collections.ObjectModel;

    public abstract class DetailsBaseRestaurantViewModel : BaseRestaurantViewModel
    {
        private string description;
        private ObservableCollection<string> specialties; // Check about this

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                this.NotifyPropertyChanged("Description");
            }
        }

        // Think about this
        public ObservableCollection<string> Specialties
        {
            get
            {
                return this.specialties;
            }
            set
            {
                this.specialties = value;
                this.NotifyPropertyChanged("Specialties");
            }
        }
    }
}
