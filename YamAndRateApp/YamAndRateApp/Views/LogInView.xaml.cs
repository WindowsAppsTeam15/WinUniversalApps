﻿using System;
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
    public sealed partial class LogInView : Page
    {
        public LogInView()
        {
            this.InitializeComponent();
        }

        //public void RegisterBtnClick(object sender, RoutedEventArgs e)
        //{
        //    if (this.ReapeatPass.Visibility == Visibility.Collapsed)
        //    {
        //        this.ReapeatPass.Visibility = Visibility.Visible;
        //        return;
        //    }

        //    // Collect data from input fields
        //    // Send data and register user

        //    // Redirect user to initial view
        //    this.Frame.Navigate(typeof(MainPage));
        //}
    }
}
