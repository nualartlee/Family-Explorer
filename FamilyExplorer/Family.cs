using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private double xposition;
        public double XPosition
        {
            get { return xposition; }
            set
            {
                if (value != xposition)
                {
                    xposition = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double yposition;
        public double YPosition
        {
            get { return yposition; }
            set
            {
                if (value != yposition)
                {
                    yposition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double treewidth;
        public double TreeWidth
        {
            get { return treewidth; }
            set
            {
                if (value != treewidth)
                {
                    treewidth = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double treeheight;
        public double TreeHeight
        {
            get { return treeheight; }
            set
            {
                if (value != treeheight)
                {
                    treeheight = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double treewidthscaled;
        public double TreeWidthScaled
        {
            get { return treewidthscaled; }
            set
            {
                if (value != treewidthscaled)
                {
                    treewidthscaled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double treeheightscaled;
        public double TreeHeightScaled
        {
            get { return treeheightscaled; }
            set
            {
                if (value != treeheightscaled)
                {
                    treeheightscaled = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double windowWidth;
        private double windowHeight;
        private double treeScale;
        public double TreeScale
        {
            get { return treeScale; }
            set
            {
                if (value != treeScale)
                {
                    treeScale = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double treeScaleCenterX;
        public double TreeScaleCenterX
        {
            get { return treeScaleCenterX; }
            set
            {
                if (value != treeScaleCenterX)
                {
                    treeScaleCenterX = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double treeScaleCenterY;
        public double TreeScaleCenterY
        {
            get { return treeScaleCenterY; }
            set
            {
                if (value != treeScaleCenterY)
                {
                    treeScaleCenterY = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private Point treeScaleOrigin;
        public Point TreeScaleOrigin
        {
            get { return treeScaleOrigin; }
            set
            {
                if (value != treeScaleOrigin)
                {
                    treeScaleOrigin = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Cursor familyTreeCursor;
        public Cursor FamilyTreeCursor
        {
            get { return familyTreeCursor; }
            set
            {
                if (value != familyTreeCursor)
                {
                    familyTreeCursor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                if (value != selectedPerson)
                {
                    selectedPerson = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string setCommandInProgressDescription;
        public string SetCommandInProgressDescription
        {
            get { return setCommandInProgressDescription; }
            set
            {
                if (value != setCommandInProgressDescription)
                {
                    setCommandInProgressDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }        
        private bool setCommandInProgress;
        public bool SetCommandInProgress
        {
            get { return setCommandInProgress; }
            set
            {
                if (value != setCommandInProgress)
                {
                    setCommandInProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int setCommandInProgressType;
        private Person setCommandTargetPerson;
        
        public Family()
        {
            FamilyTreeCursor = Cursors.Arrow;
            setCommandInProgressType = 0;
            CreateNewFamily();
            CenterTreeInWindow();      
        }


        private void CreateNewFamily()
        {
            treeScale = 1;
            members = new ObservableCollection<Person> { };
            Person person = new Person(GetNextID());                                              
            AddPersonToFamily(person);            
        }

        #region Commands

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
                e.CanExecute = true;           
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

        public void SetMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            if (person.MotherId > 0) { e.CanExecute = false; }
            else { e.CanExecute = true; }            
        }

        public void SetMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 1;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Mother";
            SetCommandInProgress = true;            
        }

        private bool SetMother_CanFinalize(Person person)
        {
            // Not in previous generation
            if (person.GenerationIndex != setCommandTargetPerson.GenerationIndex - 1) { return false; }
            // Not female
            if (person.Gender != "Female") { return false; }
            return true;
        }

        private void SetMother_Finalized(Person person, Person mother)
        {
            
            if (mother.Gender != "Female") { return; }
            // Add mother to person                          
            person.MotherId = mother.Id;         
            // Add mother's current children to the person's sibling list                
            foreach (int childId in mother.ChildrenIds)
            {
                person.SiblingIds.Add(childId);
            }
            // Add person to mother's other childrens' sibling lists
            foreach (int siblingid in person.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(person.Id);
                }
            }
            // Add person to mother  
            mother.ChildrenIds.Add(person.Id);                       
        }

        public void SetFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            if (person.FatherId > 0) { e.CanExecute = false; }
            else { e.CanExecute = true; }
        }

        public void SetFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 2;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Father";
            SetCommandInProgress = true;            
        }

        private bool SetFather_CanFinalize(Person person)
        {
            // Not in previous generation
            if (person.GenerationIndex != setCommandTargetPerson.GenerationIndex - 1) { return false; }
            // Not male
            if (person.Gender != "Male") { return false; }
            return true;
        }

        private void SetFather_Finalized(Person person, Person father)
        {

            if (father.Gender != "Male") { return; }
            // Add father to person                          
            person.FatherId = father.Id;
            // Add father's current children to the person's sibling list                
            foreach (int childId in father.ChildrenIds)
            {
                person.SiblingIds.Add(childId);
            }
            // Add person to father's other childrens' sibling lists
            foreach (int siblingid in person.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(person.Id);
                }
            }
            // Add person to father  
            father.ChildrenIds.Add(person.Id);
        }

        public void SetFriend_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SetFriend_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 3;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Friend";
            SetCommandInProgress = true;            
        }

        private bool SetFriend_CanFinalize(Person person)
        {
            // Not itself
            if (person == setCommandTargetPerson) { return false; }
            // Not already a friend
            if (person.FriendIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetFriend_Finalized(Person person, Person partner)
        {
            // Add friend to person's friends list                        
            person.FriendIds.Add(partner.Id);
            // Add person to friend's friend list  
            partner.FriendIds.Add(person.Id);
        }

        public void SetPartner_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {            
             e.CanExecute = true;            
        }

        public void SetPartner_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 4;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Partner";
            SetCommandInProgress = true;           
        }

        private bool SetPartner_CanFinalize(Person person)
        {
            // Not itself
            if (person == setCommandTargetPerson) { return false; }
            // Not already a partner
            if (person.PartnerIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetPartner_Finalized(Person person, Person partner)
        {            
            // Add partner to person's partner list                        
            person.PartnerIds.Add(partner.Id);            
            // Add person to partners' partner list  
            partner.PartnerIds.Add(person.Id);
        }

        public void SetChild_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            Person person = (Person)e.Parameter;
            if (person.Gender == "Male" || person.Gender == "Female")
            { e.CanExecute = true; }
            else { e.CanExecute = false; }
        }

        public void SetChild_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 5;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Child";
            SetCommandInProgress = true;            
        }

        private bool SetChild_CanFinalize(Person child)
        {
            // Not already a child
            if (setCommandTargetPerson.ChildrenIds.Contains(child.Id)) { return false; }           
            // Not in the next generation
            if (setCommandTargetPerson.GenerationIndex + 1 != child.GenerationIndex) { return false; }            
           return true; 
        }

        private void SetChild_Finalized(Person person, Person child)
        {
            // Add siblings to child
            foreach (int childId in person.ChildrenIds)
            {
                if (!child.SiblingIds.Contains(childId))
                {
                    child.SiblingIds.Add(childId);
                }                
            }
            // add child to siblings
            foreach (int siblingid in person.SiblingIds)
            {
                Person otherSibling = getPerson(siblingid);
                if (otherSibling != null)
                {
                    if (!otherSibling.SiblingIds.Contains(child.Id))
                    {
                        otherSibling.SiblingIds.Add(child.Id);
                    }                    
                }
            }
            // Add child to person's children list                        
            person.ChildrenIds.Add(child.Id);
            // Add person as child's parent
            if (person.Gender == "Male") { child.FatherId = person.Id; }
            if (person.Gender == "Female") { child.MotherId = person.Id; }
            
        }

        public void SetAbuser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SetAbuser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 6;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Abuser";
            SetCommandInProgress = true;            
        }

        private bool SetAbuser_CanFinalize(Person person)
        {            
            // Not already an abuser
            if (person.VictimIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetAbuser_Finalized(Person person, Person abuser)
        {
            // Add abuser to person's abuser list                        
            person.AbuserIds.Add(abuser.Id);
            // Add person to abuser's victim list  
            abuser.VictimIds.Add(person.Id);
        }

        public void SetVictim_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void SetVictim_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (Person)e.Parameter;
            setCommandInProgressType = 7;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Victim";
            SetCommandInProgress = true;           
        }

        private bool SetVictim_CanFinalize(Person person)
        {
            // Not already a victim
            if (person.AbuserIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetVictim_Finalized(Person person, Person victim)
        {
            // Add victim to person's victim list                        
            person.VictimIds.Add(victim.Id);
            // Add person to victim's abuser list  
            victim.AbuserIds.Add(person.Id);
        }

        public void FinalizeSetCommand(Person setCommandRelationPerson)
        {
            if (SetCommandInProgress)
            {
                switch (setCommandInProgressType)
                {
                    case 1: // Set mother
                        if (SetMother_CanFinalize(setCommandRelationPerson))
                        { SetMother_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 2: // Set father
                        if (SetFather_CanFinalize(setCommandRelationPerson))
                        { SetFather_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 3: // Set friend
                        if (SetFriend_CanFinalize(setCommandRelationPerson))
                        { SetFriend_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 4: // Set partner
                        if (SetPartner_CanFinalize(setCommandRelationPerson))
                        { SetPartner_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 5: // Set child
                        if (SetChild_CanFinalize(setCommandRelationPerson))
                        { SetChild_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 6: // Set abuser
                        if (SetAbuser_CanFinalize(setCommandRelationPerson))
                        { SetAbuser_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 7: // Set victim
                        if (SetVictim_CanFinalize(setCommandRelationPerson))
                        { SetVictim_Finalized(setCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    default:
                        break;
                }
            }
            EndSetCommand();
        }        

        public void EndSetCommand()
        {
            if (SetCommandInProgress)
            {
                setCommandTargetPerson = null;
                setCommandInProgressType = 0;
                SetCommandInProgressDescription = "";
                SetCommandInProgress = false;
                FamilyTreeCursor = Cursors.Arrow;
            }
        }

        public void EnterSetCommandRelation(Person person)
        {
            
            switch (setCommandInProgressType)
            {
                case 0: // No command in progress
                    FamilyTreeCursor = Cursors.Arrow;
                    break;
                case 1: // Set mother
                    if (SetMother_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }                        
                    break;
                case 2: // Set father
                    if (SetFather_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 3: // Set friend
                    if (SetFriend_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 4: // Set partner
                    if (SetPartner_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 5: // Set child
                    if (SetChild_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }                    
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 6: // Set abuser
                    if (SetAbuser_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 7: // Set victim
                    if (SetVictim_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                default:
                    FamilyTreeCursor = Cursors.Arrow;
                    break;
            }                
        }

        public void ExitSetCommandRelation()
        {
            if (SetCommandInProgress) { FamilyTreeCursor = Cursors.Arrow; }           
            else { FamilyTreeCursor = Cursors.Arrow; }
        }

#endregion Commands

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
            OrderSiblings(person.GenerationIndex);
            SetTreeLayout();
        }

        private void SetPersonPosition(Person person)
        {            
            person.X = TreeWidth / 2 + person.SiblingIndex * 150 - (double)person.Width / 2;
            person.Y = (person.GenerationIndex - Members.Min(m => m.GenerationIndex)) * 120 ;
        }

        private void OrderSiblings(int generation)
        {            
            List<Person> generationMembers = new List<Person> { };
            generationMembers =(List<Person>)members.Where(m => m.GenerationIndex == generation).OrderBy(m => m.DOB).ToList<Person>();
            // Center generation members about a zero index for easy positioning (i.e. -2,-1,0,1,2)           
            for (int i = 0; i < generationMembers.Count(); i++)
            {                
                generationMembers[i].SiblingIndex = Convert.ToDouble(i) - (Convert.ToDouble(generationMembers.Count()) - 1) / 2;                
            }
            
        }       
        

        private void SetTreeLayout()
        {            
            TreeWidth = (Members.Max(m => m.SiblingIndex) + 1 - Members.Min(m => m.SiblingIndex)) * 150 -40;
            TreeHeight = (Members.Max(m => m.GenerationIndex) + 1 - Members.Min(m => m.GenerationIndex)) * 120 - 30;

            foreach (Person person in Members)
            {
                SetPersonPosition(person);
            }
            
            SetScaledTreeDimensions();
        }

        private void SetScaledTreeDimensions()
        {
            TreeWidthScaled = TreeWidth * TreeScale;
            TreeHeightScaled = TreeHeight * TreeScale;
        }

        public void SetWindowSize(double width, double height)
        {
            windowWidth = width;
            windowHeight = height;
        }

        private void CenterTreeInWindow()
        {
            
            SetTreeLayout();
            XPosition = (windowWidth / 2) - (TreeWidth / 2);
            YPosition = (windowHeight / 2) - (TreeHeight / 2);
        }
        
        public void MoveTreePositionInWindow(double deltaX, double deltaY)
        {
            XPosition += deltaX; /// TreeScale;
            YPosition += deltaY; /// TreeScale;
        }

        public void ScaleTree(double scaleIncrease, double windowCenterX, double windowCenterY)

        {

            //TreeScaleOrigin = new Point(windowCenterX / windowWidth, windowCenterY / windowHeight);
            TreeScaleOrigin = new Point(0.5,0.5);

            TreeScaleCenterX = windowCenterX;
            TreeScaleCenterY = windowCenterY;

            if (scaleIncrease > 0)
            { TreeScale = TreeScale * (scaleIncrease / 100); }
            else { TreeScale = TreeScale * (-100 / scaleIncrease); }

            //XPosition = XPosition - (windowWidth * TreeScale) * scaleIncrease;
            //YPosition = YPosition - (windowHeight * TreeScale)* scaleIncrease;

            SetScaledTreeDimensions();
        }

    }
}
