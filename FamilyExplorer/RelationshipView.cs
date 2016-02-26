using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class RelationshipView : Relationship, INotifyPropertyChanged

    {
        
        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                if (value != path)
                {
                    path = value;
                    NotifyPropertyChanged();                    
                }
            }
        }

        private double pathThickness = 6;
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

        private string pathColor;
        public string PathColor
        {
            get { return pathColor; }
            set
            {
                if (value != pathColor)
                {
                    pathColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (value != selected)
                {
                    selected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelationshipView()
        {
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            Initialize();
        }

        public void Initialize()
        {
            
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
           
        }
    }
}
