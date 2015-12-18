namespace YamAndRateApp.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    using Parse;

    using YamAndRateApp.Models;

    public class ListOFRestaurantsViewModel : BaseViewModel
    {
        private ObservableCollection<RestaurantLimitedViewModel> restaurants;

        public ListOFRestaurantsViewModel()
        {
            this.LoadRestaurants();

            /*
            this.Restaurants.Add(new RestaurantLimitedViewModel("Mrysnoto", 4.5,
                "https://farm4.staticflickr.com/3795/13818125963_5a67445be7_b.jpg",
                CategoryType.Bulgarian,
                new Geopoint(new BasicGeoposition() { Latitude = 42.670384, Longitude = 23.362506 })));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Chehov 52", 2.5,
                "http://i2.cdn.turner.com/cnnnext/dam/assets/140911111141-photo-realism-sam-silva-ocelot-horizontal-large-gallery.jpg",
                CategoryType.French,
                new Geopoint(new BasicGeoposition() { Latitude = 42.670025, Longitude = 23.363450 })));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Chistoto", 6.5,
                "http://i2.cdn.turner.com/cnnnext/dam/assets/150409184903-12-week-in-photos-0410-restricted-super-169.jpg",
                CategoryType.Chinise,
                new Geopoint(new BasicGeoposition() { Latitude = 42.671299, Longitude = 23.364003 })));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Gotiniq zaek", 9.8,
                "http://static.ddmcdn.com/gif/easter-bunny-photos-150405-480702997.jpg",
                CategoryType.Bulgarian,
                new Geopoint(new BasicGeoposition() { Latitude = 42.671536, Longitude = 23.365290 })));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Karuchkata", 1.5,
                "http://also.kottke.org/misc/images/2014-photos-drought.jpg",
                CategoryType.French,
                new Geopoint(new BasicGeoposition() { Latitude = 42.673437, Longitude = 23.363305 })));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Batman", 4.5,
                "http://img.actucine.com/wp-content/uploads/2015/07/Batman-Superman-150713.jpg",
                CategoryType.Italian,
                new Geopoint(new BasicGeoposition() { Latitude = 42.669363, Longitude = 23.360682 })));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Tiger-tiger", 4.5,
                "http://img2.holidayiq.com/photos/ra/Ranthambore-Photos-Tigers-shareiq-1371017873-33553-JPG-destreviewimages-510x340-1371017873.JPG",
                CategoryType.OtherAsian,
                new Geopoint(new BasicGeoposition() { Latitude = 42.669225, Longitude = 23.358005 })));
                */
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

        private async Task LoadRestaurants()
        {
            var restaurants = await new ParseQuery<Restaurant>().FindAsync();

            this.Restaurants = restaurants.AsQueryable().Select(model => new RestaurantLimitedViewModel
            {
                Name = model.Name,
                Rating = model.Rating,
                PhotoUrl = "https://farm4.staticflickr.com/3795/13818125963_5a67445be7_b.jpg",
                Category = model.Category.CategoryType
            });
        }
    }
}
