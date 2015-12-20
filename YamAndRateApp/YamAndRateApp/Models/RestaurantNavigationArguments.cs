
namespace YamAndRateApp.Models
{
    using Windows.UI.Xaml.Controls.Primitives;

    public class RestaurantNavigationArguments
    {
        public RestaurantNavigationArguments(int restaurantId, EdgeTransitionLocation navigationDirection)
        {
            this.RestaurantId = restaurantId;
            this.NavigationDirection = navigationDirection;
        }

        public EdgeTransitionLocation NavigationDirection { get; private set; }
        public int RestaurantId { get; private set; }
    }
}
