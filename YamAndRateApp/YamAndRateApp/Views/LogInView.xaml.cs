namespace YamAndRateApp.Views
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class LogInView : Page
    {
        public LogInView()
        {
            this.InitializeComponent();
        }

        private void OnRegisterButtonClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterView));
        }
    }
}