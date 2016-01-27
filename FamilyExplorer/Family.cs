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


        private void CreateNewFamily()
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
       
        private void AddMotherToPerson(Person child)
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

        private void AddFatherToPerson(Person child)
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

        public void AddSiblingEqualParents_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {            
                e.CanExecute = true;          
        }

        public void AddSiblingEqualParents_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddSiblingEqualParentsToPerson(person);
        }

        private void AddSiblingEqualParentsToPerson(Person person)
        {
            // Create new sibling
            Person newSibling = new Person(GetNextID());
            newSibling.FirstName = "Sibling Of " + person.FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = person.GenerationIndex;
            // Add current siblings to new sibling's list
            newSibling.SiblingIds.Add(person.Id);
            foreach (int siblingid in person.SiblingIds)
            {
                newSibling.SiblingIds.Add(siblingid);
            }
            // Add parents to new sibling
            newSibling.MotherId = person.MotherId;
            newSibling.FatherId = person.FatherId;
            // Add new sibling to all other siblings' lists
            foreach (int siblingid in newSibling.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(newSibling.Id);
                }
            }
            // Add new sibling to mother's children
            Person mom = getPerson(person.MotherId);
            if (mom != null)
            {
                mom.ChildrenIds.Add(newSibling.Id);
            }            
            // Add new sibling to father's children
            Person dad = getPerson(person.FatherId);
            if (dad != null)
            {
                dad.ChildrenIds.Add(newSibling.Id);
            }

            AddPersonToFamily(newSibling);            

        }

        public void AddSiblingByMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddSiblingByMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddSiblingByMotherToPerson(person);
        }

        private void AddSiblingByMotherToPerson(Person person)
        {
            // Create new sibling
            Person newSibling = new Person(GetNextID());
            newSibling.FirstName = "Sibling Of " + person.FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = person.GenerationIndex;
            // Add current siblings to new sibling's list    
            Person mom = getPerson(person.MotherId);
            if (mom != null)
            {
                foreach (int siblingid in mom.ChildrenIds)
                {
                    newSibling.SiblingIds.Add(siblingid);
                }
            }
            else
            {
                newSibling.SiblingIds.Add(person.Id);
            }
            // Add mother to new sibling
            newSibling.MotherId = person.MotherId;            
            // Add new sibling to all other siblings' lists
            foreach (int siblingid in newSibling.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(newSibling.Id);
                }
            }
            // Add new sibling to mother's children    
            if (mom != null)
            {
                mom.ChildrenIds.Add(newSibling.Id);
            }

            AddPersonToFamily(newSibling);

        }

        public void AddSiblingByFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddSiblingByFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddSiblingByFatherToPerson(person);
        }

        private void AddSiblingByFatherToPerson(Person person)

        {
            // Create new sibling
            Person newSibling = new Person(GetNextID());
            newSibling.FirstName = "Sibling Of " + person.FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = person.GenerationIndex;
            // Add current siblings to new sibling's list    
            Person dad = getPerson(person.FatherId);
            if (dad != null)
            {
                foreach (int siblingid in dad.ChildrenIds)
                {
                    newSibling.SiblingIds.Add(siblingid);
                }
            }
            else
            {
                newSibling.SiblingIds.Add(person.Id);
            }
            // Add father to new sibling            
            newSibling.FatherId = person.FatherId;
            // Add new sibling to all other siblings' lists
            foreach (int siblingid in newSibling.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(newSibling.Id);
                }
            }
            // Add new sibling to father's children     
            if (dad != null)
            {
                dad.ChildrenIds.Add(newSibling.Id);
            }

            AddPersonToFamily(newSibling);

        }

        public void AddFriend_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {            
                e.CanExecute = true;          
        }

        public void AddFriend_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddFriendToPerson(person);
        }

        private void AddFriendToPerson(Person person)
        {
            Person friend = new Person(GetNextID());
            friend.FirstName = "Friend Of " + person.FirstName;
            friend.LastName = "";
            friend.Gender = "";
            friend.GenerationIndex = person.GenerationIndex;
            friend.FriendIds.Add(person.Id);

            AddPersonToFamily(friend);

            person.FriendIds.Add(friend.Id);
        }

        public void AddPartner_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddPartner_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddPartnerToPerson(person);
        }

        private void AddPartnerToPerson(Person person)
        {
            Person partner = new Person(GetNextID());
            partner.FirstName = "Partner Of " + person.FirstName;
            partner.LastName = "";
            partner.Gender = "";
            partner.GenerationIndex = person.GenerationIndex;
            partner.PartnerIds.Add(person.Id);

            AddPersonToFamily(partner);

            person.PartnerIds.Add(partner.Id);
        }

        public void AddChild_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Person parent = (Person)e.Parameter;
            if (parent.Gender == "Male" || parent.Gender == "Female")
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        public void AddChild_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddChildToPerson(person);
        }

        private void AddChildToPerson(Person parent)

        {
            // Create new child
            Person newChild = new Person(GetNextID());
            newChild.FirstName = "Child Of " + parent.FirstName;
            newChild.LastName = "";
            newChild.Gender = "Not Specified";
            newChild.GenerationIndex = parent.GenerationIndex + 1;
            // Add parent to new child   
            if (parent.Gender == "Male")
            {
                newChild.FatherId = parent.FatherId;
            }
            if (parent.Gender == "Female")
            {
                newChild.MotherId = parent.MotherId;
            }
            // Add current children to new child's sibling list                
            foreach (int childId in parent.ChildrenIds)
            {
                newChild.SiblingIds.Add(childId);
            }            
            // Add new child to all other childrens' sibling lists
            foreach (int siblingid in newChild.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(newChild.Id);
                }
            }
            // Add new child to parent  
            parent.ChildrenIds.Add(newChild.Id);

            AddPersonToFamily(newChild);

        }

        public void AddAbuser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddAbuser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddAbuserToPerson(person);
        }

        private void AddAbuserToPerson(Person victim)
        {
            Person abuser = new Person(GetNextID());
            abuser.FirstName = "Abuser Of " + abuser.FirstName;
            abuser.LastName = "";
            abuser.Gender = "";
            abuser.GenerationIndex = abuser.GenerationIndex - 1;
            abuser.VictimIds.Add(abuser.Id);

            AddPersonToFamily(abuser);

            abuser.AbuserIds.Add(abuser.Id);
        }

        public void AddVictim_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddVictim_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            AddVictimToPerson(person);
        }

        private void AddVictimToPerson(Person abuser)
        {
            Person victim = new Person(GetNextID());
            victim.FirstName = "Victim Of " + abuser.FirstName;
            victim.LastName = "";
            victim.Gender = "";
            victim.GenerationIndex = abuser.GenerationIndex + 1;
            victim.AbuserIds.Add(abuser.Id);

            AddPersonToFamily(victim);

            abuser.VictimIds.Add(victim.Id);
        }

        private Person getPerson(int ID)
        {
            return (Person)members.Where(m => m.Id == ID).FirstOrDefault();
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
