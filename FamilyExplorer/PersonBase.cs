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
    public abstract class PersonBase : INotifyPropertyChanged
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
        private string firstName;
        public string FirstName
        { 
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    NotifyPropertyChanged();
                    NotifyBasePropertyChanged();
                }
            } 
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    NotifyPropertyChanged();
                    NotifyBasePropertyChanged();
                }
            }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                if (value != gender)
                {
                    gender = value;                    
                    NotifyPropertyChanged();
                    NotifyBasePropertyChanged();
                }
            }
        }
        private DateTime dob;
        public DateTime DOB
        {
            get { return dob; }
            set
            {
                if (value != dob)
                {
                    dob = value;
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

        private int generationIndex;
        public int GenerationIndex
        {
            get { return generationIndex; }
            set
            {
                if (value != generationIndex)
                {
                    generationIndex = value;                   
                    NotifyPropertyChanged();
                    NotifyBasePropertyChanged();
                }
            }
        }
        private double siblingIndex;
        public double SiblingIndex
        {
            get { return siblingIndex; }
            set
            {
                if (value != siblingIndex)
                {
                    siblingIndex = value;                  
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
                if (property.GetValue(this) != property.GetValue(compareObject))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
