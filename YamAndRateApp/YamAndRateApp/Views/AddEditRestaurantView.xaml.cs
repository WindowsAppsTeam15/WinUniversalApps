using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace YamAndRateApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddEditRestaurantView : Page
    {
        private int currentVisibleSpecialtyField;

        public AddEditRestaurantView()
        {
            this.InitializeComponent();
            currentVisibleSpecialtyField = 0;
        }

        private void DisplayNewSpecialty(object sender, RoutedEventArgs e)
        {
            currentVisibleSpecialtyField += 1;
            switch (currentVisibleSpecialtyField)
            {
                case 1:
                    this.FirstHidden.Visibility = Visibility.Visible;
                    break;
                case 2:
                    this.SecondHidden.Visibility = Visibility.Visible;
                    break;
                case 3:
                    this.ThirdHidden.Visibility = Visibility.Visible;
                    break;
                case 4:
                    this.ForthHidden.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }
    }
}
