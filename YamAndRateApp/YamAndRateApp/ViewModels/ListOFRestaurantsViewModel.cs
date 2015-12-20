namespace YamAndRateApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Parse;

    using YamAndRateApp.Models;
    using Windows.Devices.Geolocation;
    using Utils;
    public class ListOFRestaurantsViewModel : ViewModelBase
    {
        private ObservableCollection<RestaurantLimitedViewModel> restaurants;

        public ListOFRestaurantsViewModel()
            : this(String.Empty)
        {
        }

        public ListOFRestaurantsViewModel(string pattern)
        {
            this.LoadRestaurants(pattern);
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

        //private async void LoadRestaurants()
        //{
        //    try
        //    {
        //        var restaurants = await new ParseQuery<Restaurant>().FindAsync();

        //        var loadedRestaurants = restaurants.AsQueryable().Select(model => new RestaurantLimitedViewModel
        //        {
        //            Name = model.Name,
        //            Rating = model.Rating,
        //            PhotoUrl = model.Photo.Url.ToString(),
        //            Category = model.Category,
        //            Id = model.ObjectId,
        //            Coordinates = new Geopoint(new BasicGeoposition() { Longitude = model.Location.Longitude, Latitude = model.Location.Latitude })
        //        });

        //        this.Restaurants = loadedRestaurants.ToList();
        //    }
        //    catch (Exception)
        //    {
        //        ToastManager toastManager = new ToastManager();
        //        var heading = "There is no internet connection!";
        //        var image = "/Assets/LockScreenLogo.scale-200.png";
        //        var navigateTo = "main";
        //        toastManager.CreateToast(heading, String.Empty, image, navigateTo);
        //        return;
        //    } 
        //}

        private async void LoadRestaurants(string pattern)
        {
            try
            {
                IEnumerable<Restaurant> restaurants = await new ParseQuery<Restaurant>().FindAsync();

                if (String.IsNullOrEmpty(pattern))
                {
                    restaurants = (await new ParseQuery<Restaurant>().FindAsync()).AsQueryable();
                }
                else
                {
                    restaurants = (await new ParseQuery<Restaurant>().FindAsync()).AsQueryable().Where(r => r.Name.Contains(pattern));
                }

                var loadedRestaurants = restaurants.Select(model => new RestaurantLimitedViewModel
                {
                    Name = model.Name,
                    Rating = model.Rating,
                    PhotoUrl = model.Photo.Url.ToString(),
                    Category = model.Category,
                    Id = model.ObjectId,
                    Coordinates = new Geopoint(new BasicGeoposition() { Longitude = model.Location.Longitude, Latitude = model.Location.Latitude })
                });

                this.Restaurants = loadedRestaurants.ToList();
            }
            catch (Exception)
            {
                ToastManager toastManager = new ToastManager();
                var heading = "There is no internet connection!";
                var image = "/Assets/LockScreenLogo.scale-200.png";
                var navigateTo = "main";
                toastManager.CreateToast(heading, String.Empty, image, navigateTo);
                return;
            }
        }
    }
}
