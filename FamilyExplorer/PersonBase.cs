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
                }
            }
        }

        //private int motherId;
        //public int MotherId
        //{
        //    get { return motherId; }
        //    set
        //    {
        //        if (value != motherId)
        //        {
        //            motherId = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //private int fatherId;
        //public int FatherId
        //{
        //    get { return fatherId; }
        //    set
        //    {
        //        if (value != fatherId)
        //        {
        //            fatherId = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}       
        //private List<int> siblingIds;
        //public List<int> SiblingIds
        //{
        //    get { return siblingIds; }
        //    set
        //    {
        //        if (value != siblingIds)
        //        {
        //            siblingIds = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //private List<int> friendIds;
        //public List<int> FriendIds
        //{
        //    get { return friendIds; }
        //    set
        //    {
        //        if (value != friendIds)
        //        {
        //            friendIds = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //private List<int> partnerIds;
        //public List<int> PartnerIds
        //{
        //    get { return partnerIds; }
        //    set
        //    {
        //        if (value != partnerIds)
        //        {
        //            partnerIds = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}        
        //private List<int> childrenIds;
        //public List<int> ChildrenIds
        //{
        //    get { return childrenIds; }
        //    set
        //    {
        //        if (value != childrenIds)
        //        {
        //            childrenIds = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //private List<int> abuserIds;
        //public List<int> AbuserIds
        //{
        //    get { return abuserIds; }
        //    set
        //    {
        //        if (value != abuserIds)
        //        {
        //            abuserIds = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}
        //private List<int> victimIds;
        //public List<int> VictimIds
        //{
        //    get { return victimIds; }
        //    set
        //    {
        //        if (value != victimIds)
        //        {
        //            victimIds = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

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

        private int generationIndex;
        public int GenerationIndex
        {
            get { return generationIndex; }
            set
            {
                if (value != generationIndex)
                {
                    generationIndex = value;
                    //setPosition();
                    NotifyPropertyChanged();
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
                    //setPosition();
                    NotifyPropertyChanged();
                }
            }
        }

        public void CopyBaseProperties(Object personObject)
        {
           foreach (PropertyInfo property in this.GetType().BaseType.GetProperties())
            {
                property.SetValue(this, property.GetValue(personObject));
            }            
        }

        
    }
}
