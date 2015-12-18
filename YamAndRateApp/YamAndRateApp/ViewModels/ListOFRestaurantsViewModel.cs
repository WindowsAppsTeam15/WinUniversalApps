namespace YamAndRateApp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Parse;

    using YamAndRateApp.Models;
    using Windows.Devices.Geolocation;

    public class ListOFRestaurantsViewModel : BaseViewModel
    {
        private ObservableCollection<RestaurantLimitedViewModel> restaurants;

        public ListOFRestaurantsViewModel()
        {
            this.LoadRestaurants();
        }

        public IEnumerable<RestaurantLimitedViewModel> Restaurants
        {
            get
            {
                if (this.restaurants == null)
                {
                    this.restaurants = new ObservableCollection<RestaurantLimitedViewModel>();
                }

                return this.restaurants;
            }
            set
            {
                if (this.restaurants == null)
                {
                    this.restaurants = new ObservableCollection<RestaurantLimitedViewModel>();
                }

                this.restaurants.Clear();
                foreach (var item in value)
                {
                    this.restaurants.Add(item);
                }
            }
        }

        private async void LoadRestaurants()
        {
            var restaurants = await new ParseQuery<Restaurant>().FindAsync();

            this.Restaurants = restaurants.AsQueryable().Select(model => new RestaurantLimitedViewModel
            {
                Name = model.Name,
                Rating = model.Rating,
                PhotoUrl = model.Photo.Url.ToString(),
                Category = model.Category,
                Coordinates = new Geopoint(new BasicGeoposition() { Longitude = model.Location.Longitude, Latitude = model.Location.Latitude })             
            });
        }
    }
}
