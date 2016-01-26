using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FamilyExplorer
{
    public class Family : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<Person> members;
        public ObservableCollection<Person> Members
        {
            get{return members;}           
            set
            {
                if (value != members)
                {
                    members = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public Family()
        {                     
            CreateNewFamily();                
        }

        public void CreateNewFamily()
        {
            members = new ObservableCollection<Person> { };
            Person person = new Person(GetNextID());                                              
            AddPersonToFamily(person);            
        }

        public void AddMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            if (person.MotherId == 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void AddMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddMotherToPerson(person);
        }
       
        public void AddMotherToPerson(Person child)
        {
            Person mom = new Person(GetNextID());
            mom.FirstName = "Mother Of " + child.FirstName;
            mom.LastName = "";
            mom.Gender = "Female";
            mom.GenerationIndex = child.GenerationIndex - 1;
            mom.ChildrenIds.Add(child.Id);

            AddPersonToFamily(mom);

            child.MotherId = mom.Id;
        }

        public void AddFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            if (person.FatherId == 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void AddFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddFatherToPerson(person);
        }

        public void AddFatherToPerson(Person child)
        {
            Person dad = new Person(GetNextID());
            dad.FirstName = "Father Of " + child.FirstName;
            dad.LastName = "";
            dad.Gender = "Male";
            dad.GenerationIndex = child.GenerationIndex - 1;
            dad.ChildrenIds.Add(child.Id);

            AddPersonToFamily(dad);

            child.FatherId = dad.Id;
        }

        public void AddSibling_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {           
                e.CanExecute = true;          
        }

        public void AddSibling_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddSiblingToPerson(person);
        }

        public void AddSiblingToPerson(Person person)
        {
            Person sibling = new Person(GetNextID());
            sibling.FirstName = "Sibling Of " + person.FirstName;
            sibling.LastName = "";
            sibling.Gender = "Not Specified";
            sibling.GenerationIndex = person.GenerationIndex;
            sibling.SiblingIds.Add(person.Id);
            foreach (int siblingid in person.SiblingIds)
            {
                sibling.SiblingIds.Add(siblingid);
            }
            foreach (int siblingid in sibling.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(sibling.Id);
                }
            }

            AddPersonToFamily(sibling);            

        }

        private void AddToAllSiblings(Person person)
        {

        }

        private Person getPerson(int ID)
        {
            return (Person)members.Where(m => m.Id == ID);
        }

        private int GetNextID()
        {
            if (members.Count == 0) { return 1; }
            int? maxId = members.Max(m => m.Id) + 1;
            return maxId ?? 1;                       
        }

        private void AddPersonToFamily(Person person)
        {
            if (Members == null)
            {
                Members = new ObservableCollection<Person> { };
            }
            Members.Add(person);
            OrderGeneration(person.GenerationIndex);
        }

        private void OrderGeneration(int generation)
        {
            if (members.Count < 2) { return; }
            List<Person> generationMembers = new List<Person> { };
            generationMembers =(List<Person>)members.Where(m => m.GenerationIndex == generation).OrderBy(m => m.DOB).ToList<Person>();
            for (int i = 0; i< generationMembers.Count();i++)
            {
                generationMembers[i].SiblingIndex = i;
            }
        }
    }
}
