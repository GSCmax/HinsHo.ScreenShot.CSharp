using System.Windows.Media;

namespace HinsHo.ScreenShot.CSharp
{
    public class ScreenshotOptions
    {
        public ScreenshotOptions()
        {
            BackgroundOpacity = 0.5;
            SelectionRectangleBorderBrush = Brushes.Red;
        }

        public double BackgroundOpacity { get; set; }
        public Brush SelectionRectangleBorderBrush { get; set; }
    }
}
