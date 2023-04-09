using System.Windows;
using System.Windows.Media;

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

        private void Screen_Shot_Button_Click(object sender, RoutedEventArgs e)
        {
            var options = new HinsHo.ScreenShot.CSharp.ScreenshotOptions()
            {
                BackgroundOpacity = 0.5,
                SelectionRectangleBorderBrush = Brushes.Blue
            };
            var bitmapSource = HinsHo.ScreenShot.CSharp.Screenshot.CaptureRegionToBitmapSource(options);
            imgScreenShot.Source = bitmapSource;
        }

        private void Full_Screen_Shot_Button_Click(object sender, RoutedEventArgs e)
        {
            var bitmapSource = HinsHo.ScreenShot.CSharp.Screenshot.CaptureAllScreens();
            imgScreenShot.Source = bitmapSource;
        }
    }
}
