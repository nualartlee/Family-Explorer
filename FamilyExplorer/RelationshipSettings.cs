using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private double pathCornerRadius = 5;
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

        private double pathOffsetMother = 0;
        public double PathOffsetMother
        {
            get { return pathOffsetMother; }
            set
            {
                if (value != pathOffsetMother)
                {
                    pathOffsetMother = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffsetFather = 0;
        public double PathOffsetFather
        {
            get { return pathOffsetFather; }
            set
            {
                if (value != pathOffsetFather)
                {
                    pathOffsetFather = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffsetSibling = 0;
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
        private double pathOffsetFriend = 8;
        public double PathOffsetFriend
        {
            get { return pathOffsetFriend; }
            set
            {
                if (value != pathOffsetFriend)
                {
                    pathOffsetFriend = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffsetPartner = 4;
        public double PathOffsetPartner
        {
            get { return pathOffsetPartner; }
            set
            {
                if (value != pathOffsetPartner)
                {
                    pathOffsetPartner = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double pathOffsetAbuse = 12;
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
                    return PathOffsetMother;
                case 2:
                    return PathOffsetFather;
                case 3:
                    return PathOffsetSibling;
                case 4:
                    return PathOffsetFriend;
                case 5:
                    return PathOffsetPartner;
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
        private string pathColorFriend = "LightGreen";
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
        private string pathColorPartner = "Orange";
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



    }
}
