using System.Windows;

namespace HinsHo.ScreenShot.CSharp.WPF_Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var bitmapSource = HinsHo.ScreenShot.CSharp.Screenshot.CaptureRegionToBitmapSource();
            this.imgScreenShot.Source = bitmapSource;
        }
    }
}
