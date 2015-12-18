namespace YamAndRateApp.Helpers
{
    using System;

    using Windows.UI;
    using Windows.UI.Xaml.Data;
    using Windows.UI.Xaml.Media;

    public class CategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var category = (string)value;

            switch (category)
            {
                case "Italian": return new SolidColorBrush(Colors.Coral);
                case "French": return new SolidColorBrush(Colors.PaleVioletRed);
                case "Chinese": return new SolidColorBrush(Colors.Plum);
                case "Other Asian": return new SolidColorBrush(Colors.Pink);
                case "Bulgarian": return new SolidColorBrush(Colors.HotPink);
                default: return new SolidColorBrush(Colors.Gray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
