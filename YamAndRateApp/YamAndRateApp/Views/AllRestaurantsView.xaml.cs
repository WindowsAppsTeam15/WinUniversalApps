using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            string selectedRestaurantName = string.Empty;

            if (initiator != null)
            {
                var currentRestaurant = initiator.DataContext as RestaurantLimitedViewModel;

                if (currentRestaurant != null)
                {
                    selectedRestaurantName = currentRestaurant.Name;
                    // Or we can same and get the ID
                }
            }

            this.Frame.Navigate(typeof(RestaurantDetailsView), selectedRestaurantName);
        }
    }
}
