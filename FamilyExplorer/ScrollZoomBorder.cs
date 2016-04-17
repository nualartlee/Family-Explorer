/* 
Family Explorer - Record and View Family Relationships
Copyright(C) 2016  Javier Nualart Lee (nualartlee@yahoo.com)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License version 3 as
published by the Free Software Foundation.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.If not, see<http://www.gnu.org/licenses/> */
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
            return (TranslateTransform)((TransformGroup)this.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

        private ScaleTransform GetScaleTransform()
        {
            return (ScaleTransform)((TransformGroup)this.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }
        
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
            this.RenderTransform = group;
            this.RenderTransformOrigin = new Point(0.0, 0.0);
            parent.PreviewMouseWheel += child_PreviewMouseWheel;
            parent.MouseLeftButtonDown += child_PreviewMouseLeftButtonDown;
            parent.MouseLeftButtonUp += child_PreviewMouseLeftButtonUp;
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
            parent.CaptureMouse();
        }

        private void child_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            parent.ReleaseMouseCapture();                     
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
