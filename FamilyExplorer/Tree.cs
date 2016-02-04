using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyExplorer
{
    public class Tree : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private double xposition;
        public double XPosition
        {
            get { return xposition; }
            set
            {
                if (value != xposition)
                {
                    xposition = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double yposition;
        public double YPosition
        {
            get { return yposition; }
            set
            {
                if (value != yposition)
                {
                    yposition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double width;
        public double Width
        {
            get { return width; }
            set
            {
                if (value != width)
                {
                    width = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double height;
        public double Height
        {
            get { return height; }
            set
            {
                if (value != height)
                {
                    height = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double widthScaled;
        public double WidthScaled
        {
            get { return widthScaled; }
            set
            {
                if (value != widthScaled)
                {
                    widthScaled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double heightScaled;
        public double HeightScaled
        {
            get { return heightScaled; }
            set
            {
                if (value != heightScaled)
                {
                    heightScaled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double windowWidth;
        public double WindowWidth
        {
            get { return windowWidth; }
            set
            {
                if (value != windowWidth)
                {
                    windowWidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double windowHeight;
        public double WindowHeight
        {
            get { return windowHeight; }
            set
            {
                if (value != windowHeight)
                {
                    windowHeight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double scale;
        public double Scale
        {
            get { return scale; }
            set
            {
                if (value != scale)
                {
                    scale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double scaleCenterX;
        public double ScaleCenterX
        {
            get { return scaleCenterX; }
            set
            {
                if (value != scaleCenterX)
                {
                    scaleCenterX = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double scaleCenterY;
        public double ScaleCenterY
        {
            get { return scaleCenterY; }
            set
            {
                if (value != scaleCenterY)
                {
                    scaleCenterY = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Point scaleOrigin;
        public Point ScaleOrigin
        {
            get { return scaleOrigin; }
            set
            {
                if (value != scaleOrigin)
                {
                    scaleOrigin = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
