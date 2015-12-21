namespace YamAndRateApp
{
    using System;
    using System.Collections.Generic;

    using Windows.UI.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    using Parse;

    using YamAndRateApp.LocalDb;
    using YamAndRateApp.ViewModels;
    using YamAndRateApp.Views;    

    public sealed partial class MainPage : Page
    {
        private LocalDbManager dbManager;

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

            this.dbManager = new LocalDbManager();
            this.dbManager.InitAsync();

            this.SearchInput.TextChanged += new TextChangedEventHandler(SearchInputChanged);
        }

        private async void SearchInputChanged(object sender, TextChangedEventArgs e)
        {
            string searchInput = this.SearchInput.Text;
            var searchEntries = await this.dbManager.GetAllSearchEntriesAsync();            
            var searchedPatterns = new List<string>();
            // searchedPatterns.Clear();

            foreach (var item in searchEntries)
            {
                if (!string.IsNullOrWhiteSpace(searchInput))
                {
                    if (item.Pattern.StartsWith(searchInput))
                    {
                        searchedPatterns.Add(item.Pattern);
                    }
                }
            }

            if (searchedPatterns.Count > 0)
            {
                this.SearchSuggestions.ItemsSource = searchedPatterns;
                this.SearchSuggestions.Visibility = Visibility.Visible;
            }
            else
            {
                this.SearchSuggestions.ItemsSource = null;
                this.SearchSuggestions.Visibility = Visibility.Collapsed;
            }
        }

        private void SearchSuggestionsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SearchSuggestions.ItemsSource != null)
            {
                this.SearchSuggestions.Visibility = Visibility.Collapsed;
                this.SearchInput.TextChanged -= new TextChangedEventHandler(SearchInputChanged);
                if (this.SearchSuggestions.SelectedIndex != -1)
                {
                    this.SearchInput.Text = this.SearchSuggestions.SelectedItem.ToString();
                }

                this.SearchInput.TextChanged += new TextChangedEventHandler(SearchInputChanged);
            }
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

        public async void SearchForRestaurantsBtn(object sender, RoutedEventArgs e)
        {
            var searchEntry = new SearchEntry()
            {
                Pattern = this.SearchInput.Text
            };
            await dbManager.InsertSearchEntryAsync(searchEntry);

            this.Frame.Navigate(typeof(AllRestaurantsView), this.SearchInput.Text);
        }        
    }
}
