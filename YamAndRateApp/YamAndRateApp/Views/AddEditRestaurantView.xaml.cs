using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
                int SelectedRestaurantId = int.Parse(e.Parameter.ToString());

                this.DataContext = new RestaurantViewModel(SelectedRestaurantId);
            }
            else
            {
                this.DataContext = new RestaurantViewModel();
            }

        }

        private void ShowUploadOptions(object sender, RoutedEventArgs e)
        {
            this.uploadOptionsGrid.Visibility = Visibility.Visible;
        }

        private async void OnLibraryButtonClick(object sender, RoutedEventArgs e)
        {
            int decodePixelHeight = 100;
            int decodePixelWidth = 100;

            FileOpenPicker open = new FileOpenPicker();
            open.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            open.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include the file type
            open.FileTypeFilter.Clear();
            open.FileTypeFilter.Add(".jpg");

            // Open a stream for the selected file
            StorageFile file = await open.PickSingleFileAsync();

            // Ensure a file was selected
            if (file != null)
            {
                // Ensure the stream is disposed once the image is loaded
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    // Convert IRandomAccessStream to byte array
                    var reader = new DataReader(fileStream.GetInputStreamAt(0));
                    var bytes = new byte[fileStream.Size];
                    await reader.LoadAsync((uint)fileStream.Size);
                    reader.ReadBytes(bytes);

                    (this.DataContext as RestaurantViewModel).PhotoData = bytes;

                    // Set the image source to the selected bitmap
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.DecodePixelHeight = decodePixelHeight;
                    bitmapImage.DecodePixelWidth = decodePixelWidth;

                    await bitmapImage.SetSourceAsync(fileStream);
                    uploadedPhoto.Source = bitmapImage;
                }
            }
        }
    }
}
