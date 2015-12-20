using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using YamAndRateApp.Models;
using YamAndRateApp.ViewModels;

namespace YamAndRateApp.Views
{
    public sealed partial class AllRestaurantsView : Page
    {
        public AllRestaurantsView()
        {
            this.InitializeComponent();
        }

        private void LoadDetailsView(object sender, RoutedEventArgs e)
        {
            var initiator = e.OriginalSource as Button;
            RestaurantLimitedViewModel currentRestaurant;

            if (initiator != null)
            {
                currentRestaurant = initiator.DataContext as RestaurantLimitedViewModel;

                if (currentRestaurant == null)
                {
                    return;
                }
                string selectedRestaurantId = currentRestaurant.Id;


                this.Frame.Navigate(typeof(RestaurantDetailsView),
                                new RestaurantNavigationArguments(selectedRestaurantId, EdgeTransitionLocation.Left));
            }
        }

        private async void GridView_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            this.ProgressRingControl.Visibility = Visibility.Collapsed;
            this.ProgressRingControl.IsActive = false;
        }
    }
}
