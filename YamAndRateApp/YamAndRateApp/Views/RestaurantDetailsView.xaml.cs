using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Parse;
using YamAndRateApp.Models;
using YamAndRateApp.ViewModels;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YamAndRateApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                    // Will be difficult to use???
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

            this.DataContext = new RestaurantViewModel(selectedRestaurantId);
        }
    }
}
