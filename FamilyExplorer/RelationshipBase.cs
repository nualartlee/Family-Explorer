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
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
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

        //private int personSourceId;
        //public int PersonSourceId
        //{
        //    get { return personSourceId; }
        //    set
        //    {
        //        if (value != personSourceId)
        //        {
        //            personSourceId = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        //private int personDestinationId;
        //public int PersonDestinationId
        //{
        //    get { return personDestinationId; }
        //    set
        //    {
        //        if (value != personDestinationId)
        //        {
        //            personDestinationId = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

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

        public void CopyBaseProperties(Object relationshipObject)
        {
            foreach (PropertyInfo property in this.GetType().BaseType.GetProperties())
            {
                property.SetValue(this, property.GetValue(relationshipObject));
            }
        }
    }
}
