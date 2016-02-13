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
