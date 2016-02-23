using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    class PersonData : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                ResetData();
            }
        }

        private Person person;
        public Person Person
        {
            get { return person; }
            set
            {
                if (value != person)
                {
                    person = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<Person> siblings;
        public ObservableCollection<Person> Siblings
        {
            get { return siblings; }
            set
            {
                if (value != siblings)
                {
                    siblings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PersonData(Person person)
        {
            Person = person;
            if (person != null)
            {
                person.PropertyChanged += new PropertyChangedEventHandler(DataChanged);                
                ResetData();
            }
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            ResetData();
        }

        public void ResetData()
        {
            
        }

        private void ResetSiblings()
        {

        }

    }
}
