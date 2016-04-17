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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class RelationshipSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double pathThickness = 4;
        public double PathThickness
        {
            get { return pathThickness; }
            set
            {
                if (value != pathThickness)
                {
                    pathThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double selectedPathThickness = 7;
        public double SelectedPathThickness
        {
            get { return selectedPathThickness; }
            set
            {
                if (value != selectedPathThickness)
                {
                    selectedPathThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double pathCornerRadius = 10;
        public double PathCornerRadius
        {
            get { return pathCornerRadius; }
            set
            {
                if (value != pathCornerRadius)
                {
                    pathCornerRadius = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double pathOffSelectMother = 0;
        public double PathOffSelectMother
        {
            get { return pathOffSelectMother; }
            set
            {
                if (value != pathOffSelectMother)
                {
                    pathOffSelectMother = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffSelectFather = 8;
        public double PathOffSelectFather
        {
            get { return pathOffSelectFather; }
            set
            {
                if (value != pathOffSelectFather)
                {
                    pathOffSelectFather = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffsetSibling = -8;
        public double PathOffsetSibling
        {
            get { return pathOffsetSibling; }
            set
            {
                if (value != pathOffsetSibling)
                {
                    pathOffsetSibling = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffSelectFriend = -16;
        public double PathOffSelectFriend
        {
            get { return pathOffSelectFriend; }
            set
            {
                if (value != pathOffSelectFriend)
                {
                    pathOffSelectFriend = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffSelectPartner = 16;
        public double PathOffSelectPartner
        {
            get { return pathOffSelectPartner; }
            set
            {
                if (value != pathOffSelectPartner)
                {
                    pathOffSelectPartner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffsetAbuse = 24;
        public double PathOffsetAbuse
        {
            get { return pathOffsetAbuse; }
            set
            {
                if (value != pathOffsetAbuse)
                {
                    pathOffsetAbuse = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double PathOffset(int type)
        {
            switch (type)
            {
                case 0:
                    return 0;
                case 1:
                    return PathOffSelectMother;
                case 2:
                    return PathOffSelectFather;
                case 3:
                    return PathOffsetSibling;
                case 4:
                    return PathOffSelectFriend;
                case 5:
                    return PathOffSelectPartner;
                case 6:
                    return PathOffsetAbuse;
                default:
                    return 0;
            }
        }

        private string pathColorMother = "Black";
        public string PathColorMother
        {
            get { return pathColorMother; }
            set
            {
                if (value != pathColorMother)
                {
                    pathColorMother = value;
                    NotifyPropertyChanged();                    
                }
            }
        }
        private string pathColorFather = "DarkGray";
        public string PathColorFather
        {
            get { return pathColorFather; }
            set
            {
                if (value != pathColorFather)
                {
                    pathColorFather = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string pathColorSibling = "LightGray";
        public string PathColorSibling
        {
            get { return pathColorSibling; }
            set
            {
                if (value != pathColorSibling)
                {
                    pathColorSibling = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string pathColorFriend = "LightBlue";
        public string PathColorFriend
        {
            get { return pathColorFriend; }
            set
            {
                if (value != pathColorFriend)
                {
                    pathColorFriend = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string pathColorPartner = "Pink";
        public string PathColorPartner
        {
            get { return pathColorPartner; }
            set
            {
                if (value != pathColorPartner)
                {
                    pathColorPartner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string pathColorAbuse = "Red";
        public string PathColorAbuse
        {
            get { return pathColorAbuse; }
            set
            {
                if (value != pathColorAbuse)
                {
                    pathColorAbuse = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PathColor(int type)
        {
            switch (type)
            {
                case 0:
                    return "";
                case 1:
                    return PathColorMother;
                case 2:
                    return PathColorFather;
                case 3:
                    return PathColorSibling;
                case 4:
                    return PathColorFriend;
                case 5:
                    return PathColorPartner;
                case 6:
                    return PathColorAbuse;
                default:
                    return "";
            }
        }

        private string selectedPathColor = "LightGreen";
        public string SelectedPathColor
        {
            get { return selectedPathColor; }
            set
            {
                if (value != selectedPathColor)
                {
                    selectedPathColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string highlightedPathColor = "Orange";
        public string HighlightedPathColor
        {
            get { return highlightedPathColor; }
            set
            {
                if (value != highlightedPathColor)
                {
                    highlightedPathColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void CopyProperties(Object copyObject)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                property.SetValue(this, property.GetValue(copyObject));
            }
        }

        public bool IsEqual(Object compareObject)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                var thisProperty = property.GetValue(this);
                var comparedProperty = property.GetValue(compareObject);
                if (!thisProperty.Equals(comparedProperty))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
