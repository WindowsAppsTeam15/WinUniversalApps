namespace YamAndRateApp.Helpers
{
    using System;

    public class RatingConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rating = (double)value;

            return rating.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
