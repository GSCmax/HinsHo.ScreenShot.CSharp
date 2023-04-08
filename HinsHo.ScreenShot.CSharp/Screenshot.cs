using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Matrix = System.Windows.Media.Matrix;
using Size = System.Drawing.Size;

namespace HinsHo.ScreenShot.CSharp
{
    public class Screenshot
    {
        static Screenshot()
        {
            using (var source = new HwndSource(new HwndSourceParameters()))
            {
                toDevice = source.CompositionTarget.TransformToDevice;
            }
        }

        static Matrix toDevice;

        private static BitmapSource CaptureAllScreens()
        {
            int screenWidth = Convert.ToInt32(SystemParameters.VirtualScreenWidth * toDevice.M11);
            int screenHeight = Convert.ToInt32(SystemParameters.PrimaryScreenHeight * toDevice.M22);

            using (var bitmap = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb))
            {
                var graphics = Graphics.FromImage(bitmap);
                graphics.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight), CopyPixelOperation.SourceCopy);

                return bitmap.ToBitmapSource();
            }
        }

        public static BitmapSource CaptureRegionToBitmapSource(ScreenshotOptions options = null)
        {
            if (options == null)
                options = new ScreenshotOptions();
            var bitmap = CaptureAllScreens();

            RegionSelectionWindow window = new RegionSelectionWindow();
            window.BackgroundImage.Source = bitmap;
            window.BackgroundImage.Opacity = options.BackgroundOpacity;
            window.InnerBorder.BorderBrush = options.SelectionRectangleBorderBrush;
            window.Left = SystemParameters.VirtualScreenLeft;
            window.Top = SystemParameters.VirtualScreenTop;
            window.Width = SystemParameters.VirtualScreenWidth;
            window.Height = SystemParameters.VirtualScreenHeight;
            window.ShowDialog();

            if (window.SelectedRegion == null)
                return null;

            return GetBitmapRegion(bitmap, window.SelectedRegion.Value);
        }

        private static BitmapSource GetBitmapRegion(BitmapSource bitmap, Rect rect)
        {
            if (rect.Width <= 0 | rect.Height <= 0)
                return null;

            return new CroppedBitmap(bitmap, new Int32Rect()
            {
                X = Convert.ToInt32(rect.X * toDevice.M11),
                Y = Convert.ToInt32(rect.Y * toDevice.M22),
                Width = Convert.ToInt32(rect.Width * toDevice.M11),
                Height = Convert.ToInt32(rect.Height * toDevice.M22)
            });
        }
    }
}
