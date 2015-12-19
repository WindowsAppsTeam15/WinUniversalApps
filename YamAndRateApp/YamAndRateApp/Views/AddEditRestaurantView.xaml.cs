using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YamAndRateApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YamAndRateApp.Views
{
    public sealed partial class AddEditRestaurantView : Page
    {
        private int currentVisibleSpecialtyField;
        private Geolocator geolocator;

        public AddEditRestaurantView()
        {
            this.InitializeComponent();
            currentVisibleSpecialtyField = 0;
            GetUserLocation();
        }

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
            this.LongitudeField.Text = position.Coordinate.Longitude.ToString();
            this.LattitudeField.Text = position.Coordinate.Latitude.ToString();
        }

        private void DisplayNewSpecialty(object sender, RoutedEventArgs e)
        {
            currentVisibleSpecialtyField += 1;
            switch (currentVisibleSpecialtyField)
            {
                case 1:
                    this.FirstHidden.Visibility = Visibility.Visible;
                    break;
                case 2:
                    this.SecondHidden.Visibility = Visibility.Visible;
                    break;
                case 3:
                    this.ThirdHidden.Visibility = Visibility.Visible;
                    break;
                case 4:
                    this.ForthHidden.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter != null)
            {
                string selectedRestaurantName = e.Parameter.ToString();

                this.DataContext = new RestaurantViewModel(selectedRestaurantName);
            }
            else
            {
                this.DataContext = new RestaurantViewModel();
            }

        }
    }
}
