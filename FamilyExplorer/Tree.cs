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

        private PersonView selectedPerson;
        public PersonView SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (value != selectedPerson)
                {
                    selectedPerson = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelationshipView selectedRelationship;
        public RelationshipView SelectedRelationship
        {
            get { return selectedRelationship; }
            set
            {
                if (value != selectedRelationship)
                {
                    selectedRelationship = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string undoDescription;
        public string UndoDescription
        {
            get { return undoDescription; }
            set
            {
                if (value != undoDescription)
                {
                    undoDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string redoDescription;
        public string RedoDescription
        {
            get { return redoDescription; }
            set
            {
                if (value != redoDescription)
                {
                    redoDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
