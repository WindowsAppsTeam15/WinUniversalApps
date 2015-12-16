using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using YamAndRateApp.Models;
using YamAndRateApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YamAndRateApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NearbyRestaurantsView : Page
    {
        private Geolocator geolocator;


        public NearbyRestaurantsView()
        {
            this.InitializeComponent();

            this.MapControl1.Center =
                new Geopoint(new BasicGeoposition()
                {
                    Latitude = 42.672475,
                    Longitude = 23.361026
                });
            MapControl1.ZoomLevel = 15;

            GetUserLocation();

            this.Restaurants = new ObservableCollection<RestaurantLimitedViewModel>();

            this.Restaurants.Add(new RestaurantLimitedViewModel("Mrysnoto", 4.5, "https://farm4.staticflickr.com/3795/13818125963_5a67445be7_b.jpg", Category.Bulgarian, new Coordinates(42.670384, 23.362506)));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Chehov 52", 2.5, "http://i2.cdn.turner.com/cnnnext/dam/assets/140911111141-photo-realism-sam-silva-ocelot-horizontal-large-gallery.jpg", Category.French, new Coordinates(42.670025, 23.363450)));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Chistoto", 6.5, "http://i2.cdn.turner.com/cnnnext/dam/assets/150409184903-12-week-in-photos-0410-restricted-super-169.jpg", Category.Chinise, new Coordinates(42.671299, 23.364003)));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Gotiniq zaek", 9.8, "http://static.ddmcdn.com/gif/easter-bunny-photos-150405-480702997.jpg", Category.Bulgarian, new Coordinates(42.671536, 23.365290)));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Karuchkata", 1.5, "http://also.kottke.org/misc/images/2014-photos-drought.jpg", Category.French, new Coordinates(42.673437, 23.363305)));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Batman", 4.5, "http://img.actucine.com/wp-content/uploads/2015/07/Batman-Superman-150713.jpg", Category.Italian, new Coordinates(42.669363, 23.360682)));
            this.Restaurants.Add(new RestaurantLimitedViewModel("Tiger-tiger", 4.5, "http://img2.holidayiq.com/photos/ra/Ranthambore-Photos-Tigers-shareiq-1371017873-33553-JPG-destreviewimages-510x340-1371017873.JPG", Category.OtherAsian, new Coordinates(42.669225, 23.358005)));

            foreach (var restaurant in this.Restaurants)
            {
                var restaurantLocation = new Geopoint(new BasicGeoposition()
                {
                    Latitude = restaurant.Coordinates.Latitude,
                    Longitude = restaurant.Coordinates.Longitude
                });

                MapIcon restaurantPositionIcon = new MapIcon();
                restaurantPositionIcon.Location = restaurantLocation;
                restaurantPositionIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                restaurantPositionIcon.Title = restaurant.Name;
                restaurantPositionIcon.ZIndex = 0;

                this.MapControl1.MapElements.Add(restaurantPositionIcon);
                // userPositionIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customicon.png"));
            }
        }

        public ICollection<RestaurantLimitedViewModel> Restaurants { get; set; }

        private async void GetUserLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    // _rootPage.NotifyUser("Waiting for update...", NotifyType.StatusMessage);

                    // If DesiredAccuracy or DesiredAccuracyInMeters are not set (or value is 0), DesiredAccuracy.Default is used.
                    this.geolocator = new Geolocator { ReportInterval = 5000 };
                    this.geolocator.MovementThreshold = 10;

                    // Subscribe to the StatusChanged event to get updates of location status changes.
                    this.geolocator.PositionChanged += OnPositionChanged;

                    // Carry out the operation.
                    Geoposition pos = await geolocator.GetGeopositionAsync();

                    UpdateLocationData(pos);
                    //    _rootPage.NotifyUser("Location updated.", NotifyType.StatusMessage);
                    break;

                    //case GeolocationAccessStatus.Denied:
                    //    _rootPage.NotifyUser("Access to location is denied.", NotifyType.ErrorMessage);
                    //    LocationDisabledMessage.Visibility = Visibility.Visible;
                    //    UpdateLocationData(null);
                    //    break;

                    //case GeolocationAccessStatus.Unspecified:
                    //    _rootPage.NotifyUser("Unspecified error.", NotifyType.ErrorMessage);
                    //    UpdateLocationData(null);
                    //    break;
            }
        }

        async private void OnPositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                UpdateLocationData(e.Position);
            });
        }

        private void UpdateLocationData(Geoposition position)
        {
            var mapCenter = new Geopoint(new BasicGeoposition()
            {
                Latitude = position.Coordinate.Latitude,
                Longitude = position.Coordinate.Longitude
            });

            this.MapControl1.Center = mapCenter;

            MapIcon userPositionIcon = new MapIcon();
            userPositionIcon.Location = mapCenter;
            userPositionIcon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            userPositionIcon.Title = "YourPosition";
            userPositionIcon.ZIndex = 0;
            userPositionIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
            // userPositionIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/customicon.png"));

            this.MapControl1.MapElements.Add(userPositionIcon);
            this.MapControl1.Center = mapCenter;
            this.MapControl1.ZoomLevel = 15;
        }
    }
}
