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


        public RoutedCommand AddMotherRoutedCommand = new RoutedCommand();        
  
   //   CanExecuteDelegate = x => !String.IsNullOrEmpty(SearchText),
   //   ExecuteDelegate = x => myDataView.Filter = stateObj => ((Person)stateObj).Name.Contains(SearchText)

        //BindingCommand AddMother = new BindingCommand
        //{
        //    CanExecuteDelegate = x => AddMother_CanExecute(),
        //    ExecuteDelegate = x => AddMother_Executed
        //};

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

        }
    }
}
