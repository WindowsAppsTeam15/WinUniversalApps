using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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

namespace YamAndRateApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                 Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                 Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            this.InitializeParse();
        }

        private void InitializeParse()
        {
            ParseObject.RegisterSubclass<Specialty>();
            ParseObject.RegisterSubclass<Vote>();
            ParseObject.RegisterSubclass<Restaurant>();

            ParseClient.Initialize("PSONNOvcFcWay4nWraPDC6mqmQnbbYWXdvqVsu2u", "JEX9HSMJKrPmJxFsXKLygK6ZuDWv2xa9NNFrbCzD");

            // this.CreateRestaurants();
        }

        private async void CreateRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
           {
               new Restaurant()
               {
                   Name = "Mrysnoto UI",
                   Description = "Very cool and delicate cuisine",
                   Category = "Bulgarian",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 3,
                   Location = new ParseGeoPoint(42.670384, 23.362506),
                   Id = 1
               },
               new Restaurant()
               {
                   Name = "Chehov 52",
                   Description = "Very good",
                   Category = "French",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 2.5,
                   Location = new ParseGeoPoint(42.670025, 23.363450),
                   Id = 2
               },
               new Restaurant()
               {
                   Name = "Chistoto",
                   Description = "Delicious cooking",
                   Category = "Chinese",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 4.5,
                   Location = new ParseGeoPoint(42.671299, 23.364003),
                   Id = 3
               },
               new Restaurant()
               {
                   Name = "Gotiniq zaek",
                   Description = "Very good",
                   Category = "Bulgarian",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 8.6,
                   Location = new ParseGeoPoint(42.670384, 23.362506),
                   Id = 4
               },
               new Restaurant()
               {
                   Name = "Batman",
                   Description = "Very cool and delicate cuisine",
                   Category = "French",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 6.7,
                   Location = new ParseGeoPoint(42.671536, 23.365290),
                   Id = 5
               },
               new Restaurant()
               {
                   Name = "Tiger-tiger",
                   Description = "Very cool and delicate cuisine",
                   Category = "Other Asian",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 9.5,
                   Location = new ParseGeoPoint(42.673437, 23.363305),
                   Id = 6
               },
               new Restaurant()
               {
                   Name = "Karuchkata",
                   Description = "Good",
                   Category = "Bulgarian",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 5.3,
                   Location = new ParseGeoPoint(42.669363, 23.360682 ),
                   Id = 7
               },
               new Restaurant()
               {
                   Name = "Test Restaurant",
                   Description = "Test",
                   Category = "Italian",
                   Specialties = new List<string>() { "Shkembe chorba", "Kiselo zae s praz", "Chibapchita", "Bob", "Lik s oriz", "Krokodil na plocha" },
                   Rating = 7,
                   Location = new ParseGeoPoint(42.669225, 23.358005),
                   Id = 8
               }
           };

            await restaurants.SaveAllAsync();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
