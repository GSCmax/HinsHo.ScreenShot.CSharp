using System.Windows.Media;

namespace HinsHo.ScreenShot.CSharp
{
    public class ScreenshotOptions
    {
        public double BackgroundOpacity { get; set; } = 0.5;
        public Brush SelectionRectangleBorderBrush { get; set; } = Brushes.Red;
    }
}
