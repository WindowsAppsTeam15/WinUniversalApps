namespace YamAndRateApp
{
    using System;

    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Parse;

    using YamAndRateApp.ViewModels;
    using YamAndRateApp.Views;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += (sender, args) =>
            {
                if (Frame.CanGoBack)
                {
                    Frame.GoBack();
                    args.Handled = true;
                }
            };

            this.DataContext = new MainPageViewModel();
        }

        public void GoToLogInBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LogInView));
        }

        public void GoToNearbyRestaurantsBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NearbyRestaurantsView));
        }

        public void GoToAddNewRestaurantBtn(object sender, RoutedEventArgs e)
        {
            if (ParseUser.CurrentUser != null)
            {
                this.Frame.Navigate(typeof(AddEditRestaurantView));
            }
            else
            {
                this.tbMessage.Text = "You must be logged in to add new restaurant!";
                this.tbMessage.Visibility = Visibility.Visible;
                return;
            }            
        }

        public void GoToAllRestaurantsBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllRestaurantsView), String.Empty);
        }

        public void SearchForRestaurantsBtn(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AllRestaurantsView), this.SearchInput.Text);
        }
    }
}
