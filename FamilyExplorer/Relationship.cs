using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class Relationship : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int type;
        public int Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int personSourceId;
        public int PersonSourceId
        {
            get { return personSourceId; }
            set
            {
                if (value != personSourceId)
                {
                    personSourceId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int personDestinationId;
        public int PersonDestinationId
        {
            get { return personDestinationId; }
            set
            {
                if (value != personDestinationId)
                {
                    personDestinationId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                if (value != notes)
                {
                    notes = value;
                    NotifyPropertyChanged();                    
                }
            }
        }

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
    }
}
