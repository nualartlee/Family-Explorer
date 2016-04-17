using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FamilyExplorer
{

    public class ZoomBorder : Border
    {
        private UIElement child = null;
        private Point origin;
        private Point start;

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        public override UIElement Child
        {
            get { return base.Child; }
            set
            {
                if (value != null && value != this.Child)
                    this.Initialize(value);
                base.Child = value;
            }
        }

        public void Initialize(UIElement element)
        {
            this.child = element;
            if (child != null)
            {
                TransformGroup group = new TransformGroup();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                TranslateTransform tt = new TranslateTransform();
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                this.PreviewMouseWheel += child_PreviewMouseWheel;
                this.PreviewMouseLeftButtonDown += child_PreviewMouseLeftButtonDown;
                this.PreviewMouseLeftButtonUp += child_PreviewMouseLeftButtonUp;
                this.PreviewMouseMove += child_PreviewMouseMove;
                this.PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  child_PreviewMouseRightButtonDown);
            }
        }

        public void ResetView()
        {
            ResetZoom();
            ResetPan();
        }

        public void ResetZoom()
        {
            if (child != null)
            {
                // reset zoom
                var st = GetScaleTransform(child);
                st.ScaleX = 1.0;
                st.ScaleY = 1.0;
                // Refresh size
                RefreshSize();
            }
        }

        public void ResetPan()
        {
            if (child != null)
            {
                // reset pan
                var tt = GetTranslateTransform(child);
                var st = GetScaleTransform(child);
                tt.X = 0.0 + ((FrameworkElement)child).ActualWidth * (1 - st.ScaleX);
                tt.Y = 0.0 + ((FrameworkElement)child).ActualHeight * (1 - st.ScaleY);
            }
        }

        public void ZoomIn()
        {
            Zoom(0.1, Center());
        }

        public void ZoomOut()
        {
            Zoom(-0.1, Center());
        }

        public void MoveUp()
        {
            var st = GetScaleTransform(child);
            var tt = GetTranslateTransform(child);
            Vector v = new Vector(0, -10 * st.ScaleY);
            tt.X += v.X;
            tt.Y += v.Y;
            // Keep child in bounds          
            BoundToParent();
        }

        public void MoveDown()
        {
            var st = GetScaleTransform(child);
            var tt = GetTranslateTransform(child);
            Vector v = new Vector(0, 10 * st.ScaleY);
            tt.X += v.X;
            tt.Y += v.Y;
            // Keep child in bounds          
            BoundToParent();
        }

        public void MoveLeft()
        {
            var st = GetScaleTransform(child);
            var tt = GetTranslateTransform(child);
            Vector v = new Vector(-10 * st.ScaleX, 0);
            tt.X += v.X;
            tt.Y += v.Y;
            // Keep child in bounds           
            BoundToParent();
        }

        public void MoveRight()
        {
            var st = GetScaleTransform(child);
            var tt = GetTranslateTransform(child);
            Vector v = new Vector(10 * st.ScaleX, 0);
            tt.X += v.X;
            tt.Y += v.Y;
            // Keep child in bounds          
            BoundToParent();
        }

        private void child_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null)
            {
                // Set increasing/decreasing zoom value according to mouse wheel
                double zoomValue = e.Delta > 0 ? 0.1 : -0.1;
                // Set relative point according to mouse location       
                Point relativePoint = e.GetPosition(child);
                Zoom(zoomValue, relativePoint);
            }
        }

        private void child_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                this.Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void child_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                this.Cursor = Cursors.Arrow;
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.Reset();
        }

        private void child_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured)
                {
                    Vector v = start - e.GetPosition(this);
                    var tt = GetTranslateTransform(child);
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                    BoundToParent();
                }
            }
        }

        private void Zoom(double zoomValue, Point relativePoint)
        {
            if (child != null)
            {
                // Get current transform settings
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                // Check max/min limits
                if (zoomValue > 0 && (st.ScaleX > 20 || st.ScaleY > 20)) { return; }
                if (zoomValue < 0 && (st.ScaleX < 0.2 || st.ScaleY < 0.2)) { return; }


                // Get absolute location of reference point
                double abosuluteX;
                double abosuluteY;
                abosuluteX = relativePoint.X * st.ScaleX + tt.X;
                abosuluteY = relativePoint.Y * st.ScaleY + tt.Y;

                // Calculate new zoom value
                st.ScaleX += st.ScaleX * zoomValue;
                st.ScaleY += st.ScaleY * zoomValue;

                // Apply new location of reference point
                tt.X = abosuluteX - relativePoint.X * st.ScaleX;
                tt.Y = abosuluteY - relativePoint.Y * st.ScaleY;

                // Refresh size
                RefreshSize();

                // Keep child in bounds           
                BoundToParent();
            }
        }

        private Point Center()
        {
            var tt = GetTranslateTransform(child);
            FrameworkElement childFE = (FrameworkElement)child;
            return new Point(childFE.ActualWidth / 2 - tt.X, childFE.ActualHeight / 2 - tt.Y);
        }

        private void BoundToParent()
        {
            // Get current transform settings   
            var tt = GetTranslateTransform(child);


            //var st = GetScaleTransform(child);
            //child.TransformToAncestor

            // Keep child in bounds of parent
            if (tt.X < -this.ActualWidth / 2) { tt.X = -this.ActualWidth / 2 + 20; }
            if (tt.Y < -this.ActualHeight / 2) { tt.Y = -this.ActualHeight / 2 + 20; }
            if (tt.X > this.ActualWidth / 2) { tt.X = this.ActualWidth / 2 - 20; }
            if (tt.Y > this.ActualHeight / 2) { tt.Y = this.ActualHeight / 2 - 20; }
        }

        private void RefreshSize()
        {
            // Get current transform settings
            var st = GetScaleTransform(child);

            // Reset zoomborder size
            this.Width = ((FrameworkElement)child).ActualWidth * st.ScaleX;
            if (this.Width < ((FrameworkElement)this.Parent).ActualWidth - 20) { this.Width = ((FrameworkElement)this.Parent).ActualWidth - 20; }
            this.Height = ((FrameworkElement)child).ActualHeight * st.ScaleY;
            if (this.Height < ((FrameworkElement)this.Parent).ActualHeight - 20) { this.Height = ((FrameworkElement)this.Parent).ActualHeight - 20; }
        }

    }
}
