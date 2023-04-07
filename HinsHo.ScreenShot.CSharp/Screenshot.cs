using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;
using Size = System.Drawing.Size;

namespace HinsHo.ScreenShot.CSharp
{
    public class Screenshot
    {
        public static BitmapSource CaptureAllScreens()
        {
            return CaptureRegionToBitmapSource(new Rect(SystemParameters.VirtualScreenLeft, SystemParameters.VirtualScreenTop, SystemParameters.VirtualScreenWidth, SystemParameters.VirtualScreenHeight));
        }

        public static BitmapSource CaptureRegionToBitmapSource(ScreenshotOptions options = null/* TODO Change to default(_) if this is not a reference type */)
        {
            if (options == null)
                options = new ScreenshotOptions();
            var bitmap = CaptureAllScreens();

            var left = SystemParameters.VirtualScreenLeft;
            var top = SystemParameters.VirtualScreenTop;
            var right = left + SystemParameters.VirtualScreenWidth;
            var bottom = right + SystemParameters.VirtualScreenHeight;

            RegionSelectionWindow window = new RegionSelectionWindow();
            window.WindowStyle = WindowStyle.None;
            window.ResizeMode = ResizeMode.NoResize;
            window.Topmost = true;
            window.ShowInTaskbar = false;
            window.BorderThickness = new Thickness(0);
            window.BackgroundImage.Source = bitmap;
            window.BackgroundImage.Opacity = options.BackgroundOpacity;
            window.InnerBorder.BorderBrush = options.SelectionRectangleBorderBrush;
            window.Left = left;
            window.Top = top;
            window.Width = right - left;
            window.Height = bottom - top;

            window.ShowDialog();

            if (window.SelectedRegion == null)
                return null/* TODO Change to default(_) if this is not a reference type */;

            return GetBitmapRegion(bitmap, window.SelectedRegion.Value);
        }

        public static Bitmap CaptureRegionToBitmap(ScreenshotOptions options = null/* TODO Change to default(_) if this is not a reference type */)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            BitmapSource bs = CaptureRegionToBitmapSource();
            if (bs == null)
                return null/* TODO Change to default(_) if this is not a reference type */;

            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)bs));
            encoder.Save(ms);
            Bitmap bp = new Bitmap(ms);
            ms.Close();

            return bp;
        }


        public static BitmapSource CaptureRegionToBitmapSource(Rect rect)
        {
            using (var bitmap = new Bitmap((int)rect.Width, (int)rect.Height, PixelFormat.Format32bppArgb))
            {
                var graphics = System.Drawing.Graphics.FromImage(bitmap);
                graphics.CopyFromScreen((int)rect.X, (int)rect.Y, 0, 0, new Size((int)rect.Size.Width, (int)rect.Size.Height), CopyPixelOperation.SourceCopy);

                return bitmap.ToBitmapSource();
            }
        }

        public static Bitmap CaptureRegionToBitmap(Rect rect)
        {
            using (var bitmap = new Bitmap((int)rect.Width, (int)rect.Height, PixelFormat.Format32bppArgb))
            {
                var graphics = System.Drawing.Graphics.FromImage(bitmap);
                graphics.CopyFromScreen((int)rect.X, (int)rect.Y, 0, 0, new Size((int)rect.Size.Width, (int)rect.Size.Height), CopyPixelOperation.SourceCopy);

                return bitmap;
            }
        }

        private static BitmapSource GetBitmapRegion(BitmapSource bitmap, Rect rect)
        {
            if (rect.Width <= 0 | rect.Height <= 0)
                return null/* TODO Change to default(_) if this is not a reference type */;

            return new CroppedBitmap(bitmap, new Int32Rect() { X = (int)rect.X, Y = (int)rect.Y, Width = (int)rect.Width, Height = (int)rect.Height });
        }
    }
}
