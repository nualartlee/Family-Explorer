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
    public abstract class RelationshipBase : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler BasePropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void NotifyBasePropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (BasePropertyChanged != null)
            {
                BasePropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
                    NotifyBasePropertyChanged();
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
                    NotifyBasePropertyChanged();
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
                    NotifyBasePropertyChanged();
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
                    NotifyBasePropertyChanged();
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
                    NotifyBasePropertyChanged();
                }
            }
        }        

        public void CopyBaseProperties(Object copyObject)
        {
            foreach (PropertyInfo property in this.GetType().BaseType.GetProperties())
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
                if (thisProperty != null)
                {
                    if (comparedProperty != null)
                    {
                        if (!thisProperty.Equals(comparedProperty)) { return false; }
                    }
                    else { return false; }
                }
                else
                {
                    if (comparedProperty != null) { return false; }
                }

            }
            return true;
        }

    }
}
