namespace YamAndRateApp.Views
{
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Controls.Primitives;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Navigation;

    using YamAndRateApp.Models;
    using Windows.UI.Xaml.Media.Animation;
    using YamAndRateApp.ViewModels.RestaurantViewModels;

    public sealed partial class RestaurantDetailsView : Page
    {
        private string selectedRestaurantId;
        private int x1;
        private int x2;

        public RestaurantDetailsView()
        {
            this.InitializeComponent();

            this.ManipulationMode = ManipulationModes.TranslateX;
            this.ManipulationStarted += (s, e) => this.x1 = (int)e.Position.X;
            this.ManipulationCompleted += (s, e) =>
            {
                this.x2 = (int)e.Position.X;
                if (this.x1 > this.x2)
                {
                    var nextRestaurantId = this.NextId.Text;

                    var entranceTransition = new PaneThemeTransition();
                    entranceTransition.Edge = EdgeTransitionLocation.Left;
                    this.Transitions.Clear();
                    this.Transitions.Add(entranceTransition);

                    this.Frame.Navigate(typeof(RestaurantDetailsView),
                        new RestaurantNavigationArguments(nextRestaurantId, EdgeTransitionLocation.Right));
                }

                if (this.x1 < this.x2)
                {
                    var prevRestaurantId = this.PrevId.Text;

                    var entranceTransition = new PaneThemeTransition();
                    entranceTransition.Edge = EdgeTransitionLocation.Right;
                    this.Transitions.Clear();
                    this.Transitions.Add(entranceTransition);

                    this.Frame.Navigate(typeof(RestaurantDetailsView),
                        new RestaurantNavigationArguments(prevRestaurantId, EdgeTransitionLocation.Left));
                }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var navigationArgs = (e.Parameter) as RestaurantNavigationArguments;
            selectedRestaurantId = navigationArgs.RestaurantId;

            var entranceTransition = new PaneThemeTransition();
            entranceTransition.Edge = navigationArgs.NavigationDirection;
            this.Transitions.Add(entranceTransition);

            this.DataContext = new DisplayRestaurantViewModel(selectedRestaurantId);
        }
    }
}
