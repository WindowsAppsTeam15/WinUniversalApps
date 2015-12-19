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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YamAndRateApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RestaurantDetailsView : Page
    {
        private int selectedRestaurantId;
        private int x1;
        private int x2;

        public RestaurantDetailsView()
        {
            this.InitializeComponent();            

            this.ManipulationMode = ManipulationModes.TranslateX;
            this.ManipulationStarted += (s, e) => this.x1 = (int)e.Position.X;
            this.ManipulationCompleted += async (s, e) =>
            {
                this.x2 = (int)e.Position.X;
                if (this.x1 > this.x2)
                {
                    var nextRestaurantId = ++this.selectedRestaurantId;
                    int restaurantsCount = await this.GetRestaurantsCount();
                    if (nextRestaurantId <= restaurantsCount)
                    {
                        this.DataContext = new RestaurantViewModel(nextRestaurantId);
                    }       
                    else
                    {
                        nextRestaurantId = --this.selectedRestaurantId;
                    }             
                }

                if (this.x1 < this.x2)
                {
                    var prevRestaurantId = --this.selectedRestaurantId;
                    if (prevRestaurantId > 0)
                    {
                        this.DataContext = new RestaurantViewModel(prevRestaurantId);
                    }
                    else
                    {
                        prevRestaurantId = ++this.selectedRestaurantId;
                    }
                }
            };
        }

        private async Task<int> GetRestaurantsCount()
        {
            int count = await new ParseQuery<Restaurant>().CountAsync();
            return count;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            selectedRestaurantId = (int)e.Parameter;

            this.DataContext = new RestaurantViewModel(selectedRestaurantId);
        }
    }
}
