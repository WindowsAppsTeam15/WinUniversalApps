namespace YamAndRateApp.ViewModels
{
    using Windows.Devices.Geolocation;

    public class RestaurantLimitedViewModel
    {
        public string Name { get; set; }

        public double Rating { get; set; }

        public string PhotoUrl { get; set; }

        public string Category { get; set; }

        public Geopoint Coordinates { get; set; }
    }
}
