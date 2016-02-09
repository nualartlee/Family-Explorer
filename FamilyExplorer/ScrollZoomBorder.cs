using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FamilyExplorer
{

    public class ScrollZoomBorder : Border
    {
        private UIElement parent = null;
        private Point origin;
        private Point start;

        private TranslateTransform GetTranslateTransform()
        {
            return (TranslateTransform)((TransformGroup)this.LayoutTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform()
        {
            return (ScaleTransform)((TransformGroup)this.LayoutTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        //public override UIElement Child
        //{
        //    get { return base.Child; }
        //    set
        //    {
        //        if (value != null && value != this.Child)
        //            this.Initialize(value);
        //        base.Child = value;
        //    }
        //}                 


        public ScrollZoomBorder()
        {
            this.Loaded += ScrollZoomBorder_Loaded;                        
        }

        private void ScrollZoomBorder_Loaded(object sender, RoutedEventArgs e)
        {
            parent = (UIElement)this.Parent;
            TransformGroup group = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            group.Children.Add(st);
            TranslateTransform tt = new TranslateTransform();
            group.Children.Add(tt);
            this.LayoutTransform = group;
            this.RenderTransformOrigin = new Point(0.0, 0.0);
            parent.PreviewMouseWheel += child_PreviewMouseWheel;
            parent.PreviewMouseLeftButtonDown += child_PreviewMouseLeftButtonDown;
            parent.PreviewMouseLeftButtonUp += child_PreviewMouseLeftButtonUp;
            parent.PreviewMouseMove += child_PreviewMouseMove;
            parent.PreviewMouseRightButtonDown += new MouseButtonEventHandler(child_PreviewMouseRightButtonDown);
        }  

        public void Reset()
        {
            // reset zoom
            var st = GetScaleTransform();
            st.ScaleX = 1.0;
            st.ScaleY = 1.0;

            // reset pan
            var tt = GetTranslateTransform();
            tt.X = 0.0;
            tt.Y = 0.0;
        }

        #region Child Events

        private void child_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            var st = GetScaleTransform();
            var tt = GetTranslateTransform();

            double zoom = e.Delta > 0 ? .1 : -.1;
            if (!(e.Delta > 0) && (st.ScaleX < .4 || st.ScaleY < .4))
                return;

            Point relative = e.GetPosition(parent);
            double abosuluteX;
            double abosuluteY;

            abosuluteX = relative.X * st.ScaleX + tt.X;
            abosuluteY = relative.Y * st.ScaleY + tt.Y;

            st.ScaleX += zoom;
            st.ScaleY += zoom;

            tt.X = abosuluteX - relative.X * st.ScaleX;
            tt.Y = abosuluteY - relative.Y * st.ScaleY;

        }

        private void child_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tt = GetTranslateTransform();
            start = e.GetPosition(parent);
            origin = new Point(tt.X, tt.Y);
            //Parent().Cursor = Cursors.Hand;
            parent.CaptureMouse();
        }

        private void child_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            parent.ReleaseMouseCapture();
            //this.Cursor = Cursors.Arrow;            
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.Reset();
        }

        private void child_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (parent.IsMouseCaptured)
            {
                var tt = GetTranslateTransform();
                Vector v = start - e.GetPosition(parent);
                if (v.X > 0)
                {

                }
                tt.X = origin.X - v.X;


                tt.Y = origin.Y - v.Y;
            }
        }

        #endregion



    }
}
