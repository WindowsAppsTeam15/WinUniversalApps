namespace YamAndRateApp.Models
{
    public struct Coordinates
    {
        public Coordinates(double lat, double longi)
        {
            this.Latitude = lat;
            this.Longitude = longi;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}