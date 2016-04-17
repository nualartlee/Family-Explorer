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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public sealed class Settings : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static Settings instance = null;
        private static readonly object padlock = new object();

        private PersonSettings personSettings;
        public PersonSettings Person
        {
            get { return personSettings; }
            set
            {
                if (value != personSettings)
                {
                    personSettings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelationshipSettings relationshipSettings;
        public RelationshipSettings Relationship
        {
            get { return relationshipSettings; }
            set
            {
                if (value != relationshipSettings)
                {
                    relationshipSettings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Settings()
        {
            Person = new PersonSettings();
            Relationship = new RelationshipSettings();
        }

        public static Settings Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Settings();
                    }
                    return instance;
                }
            }
        }
    }
}
