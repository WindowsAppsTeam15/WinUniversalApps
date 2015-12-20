using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace YamAndRateApp.Helpers
{
    public class DisplayLogInConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var displayLogIn = (bool)value;

            if (displayLogIn)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
