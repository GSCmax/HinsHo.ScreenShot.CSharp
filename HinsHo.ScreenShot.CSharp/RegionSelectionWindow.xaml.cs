using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HinsHo.ScreenShot.CSharp
{
    /// <summary>
    /// RegionSelectionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RegionSelectionWindow : Window
    {
        private Point? _selectionStartPos;

        public Rect? SelectedRegion { get; set; }

        public RegionSelectionWindow()
        {
            InitializeComponent();
            Loaded += (s, e) => Activate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            InnerBorder.BorderThickness = new Thickness(1);
            _selectionStartPos = e.GetPosition(this);
            Mouse.Capture(this);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (!Equals(Mouse.Captured, this) || _selectionStartPos == null)
                return;

            SelectedRegion = new Rect(_selectionStartPos.Value, e.GetPosition(this));
            _selectionStartPos = default(Point?);
            Mouse.Capture(null);
            Close();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!Equals(Mouse.Captured, this) || _selectionStartPos == null)
                return;

            var position = e.GetPosition(this);
            var left = Math.Min(_selectionStartPos.Value.X, position.X);
            var top = Math.Min(_selectionStartPos.Value.Y, position.Y);
            Canvas.SetLeft(SelectionImage, -left);
            Canvas.SetTop(SelectionImage, -top);
            Canvas.SetLeft(SelectionBorder, left);
            Canvas.SetTop(SelectionBorder, top);
            SelectionBorder.Width = Math.Abs(position.X - _selectionStartPos.Value.X);
            SelectionBorder.Height = Math.Abs(position.Y - _selectionStartPos.Value.Y);
        }
    }
}
