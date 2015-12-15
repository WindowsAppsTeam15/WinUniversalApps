﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using YamAndRateApp.Models;

namespace YamAndRateApp.Helpers
{
    public class CategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var category = (Category)value;

            switch ((int)category)
            {
                case 1: return new SolidColorBrush(Colors.Beige);
                case 2: return new SolidColorBrush(Colors.Red);
                case 3: return new SolidColorBrush(Colors.Plum);
                case 4: return new SolidColorBrush(Colors.Pink);
                case 5: return new SolidColorBrush(Colors.HotPink);
                case 6: return new SolidColorBrush(Colors.Purple);
                case 7: return new SolidColorBrush(Colors.Violet);
                case 8: return new SolidColorBrush(Colors.Turquoise);
                case 9: return new SolidColorBrush(Colors.SeaGreen);
                case 10: return new SolidColorBrush(Colors.PowderBlue);
                case 11: return new SolidColorBrush(Colors.PapayaWhip);
                default: return new SolidColorBrush(Colors.Gray);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
