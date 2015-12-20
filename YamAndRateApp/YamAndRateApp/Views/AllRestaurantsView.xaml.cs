using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using YamAndRateApp.Models;
using YamAndRateApp.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YamAndRateApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllRestaurantsView : Page
    {
        public AllRestaurantsView()
        {
            this.InitializeComponent();
        }

        private void LoadDetailsView(object sender, RoutedEventArgs e)
        {
            var initiator = e.OriginalSource as Button;
            int selectedRestaurantId = 1;

            if (initiator != null)
            {
                var currentRestaurant = initiator.DataContext as RestaurantLimitedViewModel;

                if (currentRestaurant != null)
                {
                    selectedRestaurantId = currentRestaurant.Id;
                }
            }

            this.Frame.Navigate(typeof(RestaurantDetailsView),
                            new RestaurantNavigationArguments(selectedRestaurantId, EdgeTransitionLocation.Left));
        }

        private async void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            this.ProgressRingControl.Visibility = Visibility.Collapsed;
            this.ProgressRingControl.IsActive = false;
        }
    }
}
