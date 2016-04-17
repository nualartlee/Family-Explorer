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
