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
    public class PersonSettings : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private double width = 110;
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
        private double height = 90;
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
        private double horizontalSpace = 70;
        public double HorizontalSpace
        {
            get { return horizontalSpace; }
            set
            {
                if (value != horizontalSpace)
                {
                    horizontalSpace = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double verticalSpace = 110;
        public double VerticalSpace
        {
            get { return verticalSpace; }
            set
            {
                if (value != verticalSpace)
                {
                    verticalSpace = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double margin = 5;
        public double Margin
        {
            get { return margin; }
            set
            {
                if (value != margin)
                {
                    margin = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double borderThickness = 4;
        public double BorderThickness
        {
            get { return borderThickness; }
            set
            {
                if (value != borderThickness)
                {
                    borderThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double highlightBorderThickness = 2;
        public double HighlightBorderThickness
        {
            get { return highlightBorderThickness; }
            set
            {
                if (value != highlightBorderThickness)
                {
                    highlightBorderThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public static string[] GenderList = new string[]
        {
            "Female", "Male", "Other", "Not Specified"
        };

        private string backgroundColorFemale = "LightPink";
        public string BackgroundColorFemale
        {
            get { return backgroundColorFemale; }
            set
            {
                if (value != backgroundColorFemale)
                {
                    backgroundColorFemale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string backgroundColorMale = "LightBlue";
        public string BackgroundColorMale
        {
            get { return backgroundColorMale; }
            set
            {
                if (value != backgroundColorMale)
                {
                    backgroundColorMale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string backgroundColorOther = "White";
        public string BackgroundColorOther
        {
            get { return backgroundColorOther; }
            set
            {
                if (value != backgroundColorOther)
                {
                    backgroundColorOther = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string backgroundColorNotSpecified = "White";
        public string BackgroundColorNotSpecified
        {
            get { return backgroundColorNotSpecified; }
            set
            {
                if (value != backgroundColorNotSpecified)
                {
                    backgroundColorNotSpecified = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string borderBrushColorFemale = "Red";
        public string BorderBrushColorFemale
        {
            get { return borderBrushColorFemale; }
            set
            {
                if (value != borderBrushColorFemale)
                {
                    borderBrushColorFemale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string borderBrushColorMale = "Gray";
        public string BorderBrushColorMale
        {
            get { return borderBrushColorMale; }
            set
            {
                if (value != borderBrushColorMale)
                {
                    borderBrushColorMale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string borderBrushColorOther = "Black";
        public string BorderBrushColorOther
        {
            get { return borderBrushColorOther; }
            set
            {
                if (value != borderBrushColorOther)
                {
                    borderBrushColorOther = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string borderBrushColorNotSpecified = "Black";
        public string BorderBrushColorNotSpecified
        {
            get { return borderBrushColorNotSpecified; }
            set
            {
                if (value != borderBrushColorNotSpecified)
                {
                    borderBrushColorNotSpecified = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string textColorFemale = "Black";
        public string TextColorFemale
        {
            get { return textColorFemale; }
            set
            {
                if (value != textColorFemale)
                {
                    textColorFemale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string textColorMale = "Black";
        public string TextColorMale
        {
            get { return textColorMale; }
            set
            {
                if (value != textColorMale)
                {
                    textColorMale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string textColorOther = "Black";
        public string TextColorOther
        {
            get { return textColorOther; }
            set
            {
                if (value != textColorOther)
                {
                    textColorOther = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string textColorNotSpecified = "Black";
        public string TextColorNotSpecified
        {
            get { return textColorNotSpecified; }
            set
            {
                if (value != textColorNotSpecified)
                {
                    textColorNotSpecified = value;
                    NotifyPropertyChanged();
                }
            }
        }

        

        public string BackgroundColor(string gender)
        {
            switch (gender)
            {
                case "Female":                    
                    return BackgroundColorFemale;
                case "Male":
                    return BackgroundColorMale;
                case "Other":
                    return BackgroundColorOther;
                case "Not Specified":
                    return BackgroundColorNotSpecified;               
                default:
                    return "";
            }
        }
        public string BorderBrushColor(string gender)
        {
            switch (gender)
            {
                case "Female":
                    return BorderBrushColorFemale;
                case "Male":
                    return BorderBrushColorMale;
                case "Other":
                    return BorderBrushColorOther;
                case "Not Specified":
                    return BorderBrushColorNotSpecified;
                default:
                    return "";
            }
        }
        public string TextColor(string gender)
        {
            switch (gender)
            {
                case "Female":
                    return TextColorFemale;
                case "Male":
                    return TextColorMale;
                case "Other":
                    return TextColorOther;
                case "Not Specified":
                    return TextColorNotSpecified;
                default:
                    return "";
            }
        }

        private string highlightedBorderBrushColor = "Orange";
        public string HighlightedBorderBrushColor
        {
            get { return highlightedBorderBrushColor; }
            set
            {
                if (value != highlightedBorderBrushColor)
                {
                    highlightedBorderBrushColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string selectedBorderBrushColor = "LightGreen";
        public string SelectedBorderBrushColor
        {
            get { return selectedBorderBrushColor; }
            set
            {
                if (value != selectedBorderBrushColor)
                {
                    selectedBorderBrushColor = value;
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
