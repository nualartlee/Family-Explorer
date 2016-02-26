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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace FamilyExplorer
{
    public sealed class FamilyView : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private static FamilyView instance = null;
        private static readonly object padlock = new object();

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (value != title)
                {
                    title = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<PersonView> members;
        public ObservableCollection<PersonView> Members
        {
            get { return members; }
            set
            {
                if (value != members)
                {
                    members = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private PersonView selectedPerson;
        public PersonView SelectedPerson
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

        private ObservableCollection<PersonView> selectedPersonSiblings;
        public ObservableCollection<PersonView> SelectedPersonSiblings
        {
            get { return selectedPersonSiblings; }
            set
            {
                if (value != selectedPersonSiblings)
                {
                    selectedPersonSiblings = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<RelationshipView> relationships;
        public ObservableCollection<RelationshipView> Relationships
        {
            get { return relationships; }
            set
            {
                if (value != relationships)
                {
                    relationships = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelationshipView selectedRelationship;
        public RelationshipView SelectedRelationship
        {
            get { return selectedRelationship; }
            set
            {
                if (value != selectedRelationship)
                {
                    selectedRelationship = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelationshipData selectedRelationshipData;
        public RelationshipData SelectedRelationshipData
        {
            get { return selectedRelationshipData; }
            set
            {
                if (value != selectedRelationshipData)
                {
                    selectedRelationshipData = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Tree tree;
        public Tree Tree
        {
            get { return tree; }
            set
            {
                if (value != tree)
                {
                    tree = value;
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
        private PersonView setCommandTargetPerson;

        public FamilyView()
        {
            Tree = new Tree();
            Members = new ObservableCollection<PersonView> { };
            Relationships = new ObservableCollection<RelationshipView> { };
            SelectedRelationship = new RelationshipView();
            SelectedPerson = new PersonView();
            SelectedPersonSiblings = new ObservableCollection<PersonView> { };            
        }

        public static FamilyView Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FamilyView();
                    }
                    return instance;
                }
            }
        }

        public void CreateNewFamily()
        {
            PersonView person = new PersonView(GetNextID());
            AddPersonToFamily(person);
            Tree.Scale = 1;
            CenterTreeInWindow();
            FamilyTreeCursor = Cursors.Arrow;
            setCommandInProgressType = 0;
            Title = "Family Explorer - NewFamily.fex";
        }

        #region Commands

        public void AddMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
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
            PersonView person = (PersonView)e.Parameter;
            AddMotherToPerson(person);
        }

        private void AddMotherToPerson(PersonView child)
        {
            PersonView mom = new PersonView(GetNextID());            
            mom.FirstName = "Mother Of " + child.FirstName;
            mom.LastName = "";
            mom.Gender = "Female";
            mom.GenerationIndex = child.GenerationIndex - 1;
            mom.ChildrenIds.Add(child.Id);

            child.MotherId = mom.Id;

            AddPersonToFamily(mom);            
        }

        public void AddFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
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
            PersonView person = (PersonView)e.Parameter;
            AddFatherToPerson(person);
        }

        private void AddFatherToPerson(PersonView child)
        {
            PersonView dad = new PersonView(GetNextID());            
            dad.FirstName = "Father Of " + child.FirstName;
            dad.LastName = "";
            dad.Gender = "Male";
            dad.GenerationIndex = child.GenerationIndex - 1;
            dad.ChildrenIds.Add(child.Id);

            child.FatherId = dad.Id;

            AddPersonToFamily(dad);

            
        }

        public void AddSiblingEqualParents_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddSiblingEqualParents_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            AddSiblingEqualParentsToPerson(person);
        }

        private void AddSiblingEqualParentsToPerson(PersonView person)
        {
            // Create new sibling
            PersonView newSibling = new PersonView(GetNextID());            
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
                PersonView otherSibling = GetPerson(siblingid);
                if (otherSibling != null)
                {
                    otherSibling.SiblingIds.Add(newSibling.Id);
                }
            }
            // Add new sibling to mother's children
            PersonView mom = GetPerson(person.MotherId);
            if (mom != null)
            {
                mom.ChildrenIds.Add(newSibling.Id);
            }
            // Add new sibling to father's children
            PersonView dad = GetPerson(person.FatherId);
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
            PersonView person = (PersonView)e.Parameter;
            AddSiblingByMotherToPerson(person);
        }

        private void AddSiblingByMotherToPerson(PersonView person)
        {
            // Create new sibling
            PersonView newSibling = new PersonView(GetNextID());            
            newSibling.FirstName = "Sibling Of " + person.FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = person.GenerationIndex;
            // Add current siblings to new sibling's list    
            PersonView mom = GetPerson(person.MotherId);
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
                PersonView otherSibling = GetPerson(siblingid);
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
            PersonView person = (PersonView)e.Parameter;
            AddSiblingByFatherToPerson(person);
        }

        private void AddSiblingByFatherToPerson(PersonView person)

        {
            // Create new sibling
            PersonView newSibling = new PersonView(GetNextID());            
            newSibling.FirstName = "Sibling Of " + person.FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = person.GenerationIndex;
            // Add current siblings to new sibling's list    
            PersonView dad = GetPerson(person.FatherId);
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
                PersonView otherSibling = GetPerson(siblingid);
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
            PersonView person = (PersonView)e.Parameter;
            AddFriendToPerson(person);
        }

        private void AddFriendToPerson(PersonView person)
        {
            PersonView friend = new PersonView(GetNextID());           
            friend.FirstName = "Friend Of " + person.FirstName;
            friend.LastName = "";
            friend.Gender = "Not Specified";
            friend.GenerationIndex = person.GenerationIndex;
            friend.FriendIds.Add(person.Id);

            person.FriendIds.Add(friend.Id);

            AddPersonToFamily(friend);            
        }

        public void AddPartner_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddPartner_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            AddPartnerToPerson(person);
        }

        private void AddPartnerToPerson(PersonView person)
        {
            PersonView partner = new PersonView(GetNextID());            
            partner.FirstName = "Partner Of " + person.FirstName;
            partner.LastName = "";
            partner.Gender = "Not Specified";
            partner.GenerationIndex = person.GenerationIndex;
            partner.PartnerIds.Add(person.Id);

            person.PartnerIds.Add(partner.Id);

            AddPersonToFamily(partner);            
        }

        public void AddChild_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddChild_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            AddChildToPerson(person);
        }

        private void AddChildToPerson(PersonView parent)

        {
            // Create new child
            PersonView newChild = new PersonView(GetNextID());           
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
                PersonView otherSibling = GetPerson(siblingid);
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
            PersonView person = (PersonView)e.Parameter;
            AddAbuserToPerson(person);
        }

        private void AddAbuserToPerson(PersonView victim)
        {
            PersonView abuser = new PersonView(GetNextID());            
            abuser.FirstName = "Abuser Of " + abuser.FirstName;
            abuser.LastName = "";
            abuser.Gender = "Not Specified";
            abuser.GenerationIndex = abuser.GenerationIndex - 1;
            abuser.VictimIds.Add(abuser.Id);

            abuser.AbuserIds.Add(abuser.Id);

            AddPersonToFamily(abuser);            
        }

        public void AddVictim_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void AddVictim_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            AddVictimToPerson(person);
        }

        private void AddVictimToPerson(PersonView abuser)
        {
            PersonView victim = new PersonView(GetNextID());            
            victim.FirstName = "Victim Of " + abuser.FirstName;
            victim.LastName = "";
            victim.Gender = "Not Specified";
            victim.GenerationIndex = abuser.GenerationIndex + 1;
            victim.AbuserIds.Add(abuser.Id);

            abuser.VictimIds.Add(victim.Id);

            AddPersonToFamily(victim);            
        }

        public void SetMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            if (person.MotherId > 0) { e.CanExecute = false; }
            else { e.CanExecute = true; }
        }

        public void SetMother_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 1;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Mother";
            SetCommandInProgress = true;
        }

        private bool SetMother_CanFinalize(PersonView person)
        {
            // Not in previous generation
            if (person.GenerationIndex != setCommandTargetPerson.GenerationIndex - 1) { return false; }
            // Not female
            if (person.Gender != "Female") { return false; }
            return true;
        }

        private void SetMother_Finalized(PersonView person, PersonView mother)
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
            foreach (int siblingid in person.SiblingIds.ToList())
            {
                PersonView otherSibling = GetPerson(siblingid);
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
            PersonView person = (PersonView)e.Parameter;
            if (person.FatherId > 0) { e.CanExecute = false; }
            else { e.CanExecute = true; }
        }

        public void SetFather_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 2;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Father";
            SetCommandInProgress = true;
        }

        private bool SetFather_CanFinalize(PersonView person)
        {
            // Not in previous generation
            if (person.GenerationIndex != setCommandTargetPerson.GenerationIndex - 1) { return false; }
            // Not male
            if (person.Gender != "Male") { return false; }
            return true;
        }

        private void SetFather_Finalized(PersonView person, PersonView father)
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
            foreach (int siblingid in person.SiblingIds.ToList())
            {
                PersonView otherSibling = GetPerson(siblingid);
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
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 3;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Friend";
            SetCommandInProgress = true;
        }

        private bool SetFriend_CanFinalize(PersonView person)
        {
            // Not itself
            if (person == setCommandTargetPerson) { return false; }
            // Not already a friend
            if (person.FriendIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetFriend_Finalized(PersonView person, PersonView partner)
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
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 4;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Partner";
            SetCommandInProgress = true;
        }

        private bool SetPartner_CanFinalize(PersonView person)
        {
            // Not itself
            if (person == setCommandTargetPerson) { return false; }
            // Not already a partner
            if (person.PartnerIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetPartner_Finalized(PersonView person, PersonView partner)
        {
            // Add partner to person's partner list                        
            person.PartnerIds.Add(partner.Id);
            // Add person to partners' partner list  
            partner.PartnerIds.Add(person.Id);
        }

        public void SetChild_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            if (person.Gender == "Male" || person.Gender == "Female")
            { e.CanExecute = true; }
            else { e.CanExecute = false; }
        }

        public void SetChild_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 5;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Child";
            SetCommandInProgress = true;
        }

        private bool SetChild_CanFinalize(PersonView child)
        {
            // Not already a child
            if (setCommandTargetPerson.ChildrenIds.Contains(child.Id)) { return false; }
            // Not in the next generation
            if (setCommandTargetPerson.GenerationIndex + 1 != child.GenerationIndex) { return false; }
            return true;
        }

        private void SetChild_Finalized(PersonView person, PersonView child)
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
                PersonView otherSibling = GetPerson(siblingid);
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
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 6;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Abuser";
            SetCommandInProgress = true;
        }

        private bool SetAbuser_CanFinalize(PersonView person)
        {
            // Not already an abuser
            if (person.VictimIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetAbuser_Finalized(PersonView person, PersonView abuser)
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
            setCommandTargetPerson = (PersonView)e.Parameter;
            setCommandInProgressType = 7;
            SetCommandInProgressDescription = "Select " + setCommandTargetPerson.FirstName + "'s Victim";
            SetCommandInProgress = true;
        }

        private bool SetVictim_CanFinalize(PersonView person)
        {
            // Not already a victim
            if (person.AbuserIds.Contains(setCommandTargetPerson.Id)) { return false; }
            return true;
        }

        private void SetVictim_Finalized(PersonView person, PersonView victim)
        {
            // Add victim to person's victim list                        
            person.VictimIds.Add(victim.Id);
            // Add person to victim's abuser list  
            victim.AbuserIds.Add(person.Id);
        }

        public void FinalizeSetCommand(PersonView setCommandRelationPerson)
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
                ResetAllRelationships();
            }
            
        }

        public void EnterSetCommandRelation(PersonView person)
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

        public void SelectRelationship(RelationshipView relationship)
        {
            SelectedRelationship.Selected = false;
            if (relationship != null)
            {
                SelectedRelationship = relationship;
                SelectedRelationship.Selected = true;
                SelectedRelationshipData = new RelationshipData(relationship, GetPerson(relationship.PersonSourceId), GetPerson(relationship.PersonDestinationId));
            }
            else
            {
                SelectedRelationshipData = new RelationshipData(null, null, null);
            }  
        }

        public void SelectPerson(PersonView person)
        {
            SelectedPerson.Selected = false;
            if (person != null)
            {
                SelectedPerson = person;
                SelectedPerson.Selected = true;
                PopulateSelectedPersonsRelationships();
            }
            else
            {
                SelectedPerson = null;
            }
        }

        private void PopulateSelectedPersonsRelationships()
        {
            SelectedPersonSiblings.Clear();
            foreach (int id in SelectedPerson.SiblingIds)
            {                
                SelectedPersonSiblings.Add(GetPerson(id));
            }
        }

        #endregion Commands

        private void ResetAllRelationships()
        {
            Relationships = new ObservableCollection<RelationshipView> { };
            foreach (PersonView person in Members)
            {
                ResetPersonRelationships(person);
            }
        }

        private void ResetPersonRelationships(PersonView person)
        {

            // Mother
            if (person.MotherId > 0)
            {
                PersonView mom = GetPerson(person.MotherId);
                ResetRelationship(1, mom, person, person.DOB, null);
            }

            // Father
            if (person.FatherId > 0)
            {
                PersonView dad = GetPerson(person.FatherId);
                ResetRelationship(2, dad, person, person.DOB, null);
            }

            // Siblings
            foreach (int siblingId in person.SiblingIds)
            {
                PersonView sibling = GetPerson(siblingId);
                PersonView sourcePerson = (person.Id > sibling.Id) ? person : sibling;
                PersonView destinationPerson = (person.Id > sibling.Id) ? sibling : person;
                DateTime startDate = (person.DOB < sibling.DOB) ? person.DOB : sibling.DOB;
                ResetRelationship(3, sourcePerson, destinationPerson, startDate, null);
            }

            // Friends
            foreach (int friendId in person.FriendIds)
            {
                PersonView friend = GetPerson(friendId);
                PersonView sourcePerson = (person.Id > friend.Id) ? person : friend;
                PersonView destinationPerson = (person.Id > friend.Id) ? friend : person;
                DateTime startDate = (person.DOB < friend.DOB) ? person.DOB : friend.DOB;
                ResetRelationship(4, sourcePerson, destinationPerson, startDate, null);
            }
            // Partners
            foreach (int partnerId in person.PartnerIds)
            {
                PersonView partner = GetPerson(partnerId);
                PersonView sourcePerson = (person.Id > partner.Id) ? person : partner;
                PersonView destinationPerson = (person.Id > partner.Id) ? partner : person;
                DateTime startDate = (person.DOB < partner.DOB) ? person.DOB : partner.DOB;
                ResetRelationship(5, sourcePerson, destinationPerson, startDate, null);
            }
            // Abusers
            foreach (int abuserId in person.AbuserIds)
            {
                PersonView abuser = GetPerson(abuserId);
                PersonView sourcePerson = (person.Id > abuser.Id) ? person : abuser;
                PersonView destinationPerson = (person.Id > abuser.Id) ? abuser : person;
                DateTime startDate = (person.DOB < abuser.DOB) ? person.DOB : abuser.DOB;
                ResetRelationship(6, sourcePerson, destinationPerson, startDate, null);
            }
            // Victims
            foreach (int victimId in person.VictimIds)
            {
                PersonView victim = GetPerson(victimId);
                PersonView sourcePerson = (person.Id > victim.Id) ? person : victim;
                PersonView destinationPerson = (person.Id > victim.Id) ? victim : person;
                DateTime startDate = (person.DOB < victim.DOB) ? person.DOB : victim.DOB;
                ResetRelationship(6, sourcePerson, destinationPerson, startDate, null);
            }
        }

        private void ResetRelationship(int type, PersonView personSource, PersonView personDestination, DateTime startDate, DateTime? endDate)
        {
            int Id = type * (int)Math.Pow(10,6) + personSource.Id * (int)Math.Pow(10, 3) + personDestination.Id;
            RelationshipView relationship = GetRelationship(Id);
            if (relationship != null)
            {
                relationship.Id = Id;
                relationship.Type = type;
                relationship.PersonSourceId = personSource.Id;
                relationship.PersonDestinationId = personDestination.Id;
                if (type < 4)
                {
                    relationship.StartDate = startDate;
                    relationship.EndDate = endDate;
                }
                relationship.Path = CreateRelationshipPath(relationship);
                relationship.PathThickness = Settings.Instance.Relationship.PathThickness;
                relationship.PathColor = Settings.Instance.Relationship.PathColor(type);
            }
            else
            {
                RelationshipView newRelationship = new RelationshipView();
                newRelationship.Id = Id;
                newRelationship.Type = type;
                newRelationship.PersonSourceId = personSource.Id;
                newRelationship.PersonDestinationId = personDestination.Id;
                newRelationship.StartDate = startDate;
                newRelationship.EndDate = endDate;
                newRelationship.Path = CreateRelationshipPath(newRelationship);
                newRelationship.PathThickness = Settings.Instance.Relationship.PathThickness;
                newRelationship.PathColor = Settings.Instance.Relationship.PathColor(type);
                Relationships.Add(newRelationship);
            }
        }

        private string CreateRelationshipPath(RelationshipView relationship)
        {
            string path = "";

            double width = Settings.Instance.Person.Width;
            double height = Settings.Instance.Person.Height;
            double horizontalSpace = Settings.Instance.Person.HorizontalSpace;
            double verticalSpace = Settings.Instance.Person.VerticalSpace;
            double offset = Settings.Instance.Relationship.PathOffset(relationship.Type);
            double margin = Settings.Instance.Person.Margin;
            double radius = Settings.Instance.Relationship.PathCornerRadius;

            PersonView sourcePerson = GetPerson(relationship.PersonSourceId);
            PersonView destinationPerson = GetPerson(relationship.PersonDestinationId);
            Point origin = new Point(sourcePerson.X + width / 2, sourcePerson.Y + height / 2);
            Point destination = new Point(destinationPerson.X + width / 2, destinationPerson.Y + height / 2);            

            int generationCrossings = destinationPerson.GenerationIndex - sourcePerson.GenerationIndex;
            bool descending = (generationCrossings > 0);
            bool level = (generationCrossings == 0);
            bool eastward = (origin.X < destination.X);
            bool centered = (origin.X == destination.X);                       

            #region Origin & Destination Points

            if (descending)
            {
                origin.Y += (height / 2 + margin);
                destination.Y -= (height / 2 + margin);                
                
            }
            else if (level)
            {
                origin.Y -= (height / 2 + margin);
                destination.Y -= (height / 2 + margin);                               
            }
            else // ascending
            {
                origin.Y -= (height / 2 + margin);
                destination.Y += (height / 2 + margin);                              
            }

            if (eastward)
            {
                origin.X += offset;
                destination.X -= offset;
            }
            else if (centered)
            {
                origin.X += offset;
                destination.X -= offset;
            }
            else // westward
            {
                origin.X -= offset;
                destination.X += offset;
            }

            #endregion Origin & Destination Points

            List<Point> points = new List<Point> { };
            points.Add(origin);

            while (points.Last() != destination)
            {
                if (descending) { points.Add(GetNextDownwardsPoint(points.Last(), destination, offset)); }
                else if (level) { points.Add(GetNextLevelPoint(points.Last(), destination, offset)); }
                else { { points.Add(GetNextUpwardsPoint(points.Last(), destination, offset)); } }
            }

            path = SmoothenPath(points);
            
            return path;
        }

        private string SmoothenPath(List<Point> points)
        {
            double radius = Settings.Instance.Relationship.PathCornerRadius;
            string path = "M" + points[0].ToString();

            for (int i=1; i < points.Count(); i++)
            {

                // Last point
                if (i == points.Count() - 1)
                {
                    path += " L" + points[i].ToString();
                    break;
                }

                bool startedHorizontal = points[i - 1].X != points[i].X;
                bool endedHorizontal = points[i].X != points[i + 1].X;
                bool startedVertical = points[i - 1].Y != points[i].Y;
                bool endedVertical = points[i].Y != points[i + 1].Y;                
                bool notCorner = (startedVertical && endedVertical) || (startedHorizontal && endedHorizontal);

                // This point is not a corner
                if (notCorner) { break; }

                Point tangent1;
                Point tangent2;

                if (startedHorizontal)
                {
                    var list = new[] { radius, Math.Abs(points[i - 1].X - points[i].X), Math.Abs(points[i].Y - points[i + 1].Y) };
                    radius = list.Min(); // Reduce corner radius if points are too close

                    bool startedGoingRight = points[i - 1].X < points[i].X;
                    if (startedGoingRight) { tangent1 = new Point(points[i].X - radius, points[i].Y); }
                    else { tangent1 = new Point(points[i].X + radius, points[i].Y); }
                    bool endedGoingDown = points[i].Y < points[i + 1].Y;
                    if (endedGoingDown) { tangent2 = new Point(points[i].X, points[i].Y + radius); }
                    else { tangent2 = new Point(points[i].X, points[i].Y - radius); }
                }
                else
                {
                    var list = new[] { radius, Math.Abs(points[i - 1].Y - points[i].Y), Math.Abs(points[i].X - points[i + 1].X) };
                    radius = list.Min(); // Reduce corner radius if points are too close

                    bool startedGoingDown = points[i - 1].Y < points[i].Y;
                    if (startedGoingDown) { tangent1 = new Point(points[i].X, points[i].Y - radius); }
                    else { tangent1 = new Point(points[i].X, points[i].Y + radius); }
                    bool endedGoingRight = points[i].X < points[i + 1].X;
                    if (endedGoingRight) { tangent2 = new Point(points[i].X + radius, points[i].Y); }
                    else { tangent2 = new Point(points[i].X - radius, points[i].Y); }                    
                }               
                path += " L" + tangent1.ToString() + " Q" + points[i].ToString() + " " + tangent2.ToString();
            }

            return path;
        }
        
        private Point GetNextDownwardsPoint(Point current, Point destination, double offset)
        {
            Point next = new Point();

            double width = Settings.Instance.Person.Width;
            double height = Settings.Instance.Person.Height;
            double horizontalSpace = Settings.Instance.Person.HorizontalSpace;
            double verticalSpace = Settings.Instance.Person.VerticalSpace;
            double margin = Settings.Instance.Person.Margin;
            //double radius = Settings.Instance.Relationship.PathCornerRadius;

            int location = GetVerticalLocationRelativeToPeople(current, offset);
            bool crossGeneration = (Math.Abs(destination.Y - current.Y) >= verticalSpace);

            if (location == 1) // Currently at origin, go down to center line
            {
                double Y = current.Y - margin + offset + verticalSpace / 2;
                next = new Point(current.X, Y);
                return next;
            }

            if (crossGeneration) // Go down to next level
            {                
                int generationIndex = GetGenerationIndex(current.Y);
                List<PersonView> peopleBelow = Members.Where(m => m.GenerationIndex == generationIndex + 1).OrderBy(m => m.X).ToList();
                
                bool spaceBelow = true;
                foreach (PersonView person in peopleBelow)
                {
                    if (current.X > person.X && current.X < person.X + width) { spaceBelow = false; break; }
                }


                if (spaceBelow) // Space below, go down
                {
                    double Y = current.Y + (height + verticalSpace);
                    next = new Point(current.X, Y);
                    return next;
                }
                else // Move sideways to vertical path opening
                {
                    bool moveRight = current.X <= destination.X;
                    double X = moveRight ? current.X + (width + horizontalSpace) / 2 : current.X - (width + horizontalSpace) / 2;
                    next = new Point(X, current.Y);
                    return next;
                }
            }
            else
            {

                if (current.X == destination.X) // Move down to destination
                {
                    next = new Point(current.X, destination.Y);
                    return next;
                }
                else // Move sideways over destination
                {
                    next = new Point(destination.X, current.Y);
                    return next;
                }
            }

        }

        private Point GetNextLevelPoint(Point current, Point destination, double offset)
        {
            Point next = new Point();           
            double verticalSpace = Settings.Instance.Person.VerticalSpace;
            double margin = Settings.Instance.Person.Margin;            
            int location = GetVerticalLocationRelativeToPeople(current, offset);            

            if (location == 3) // Currently at origin, go up to center line
            {                
                double Y = current.Y + margin + offset - verticalSpace / 2;                
                next = new Point(current.X, Y);
                return next;
            }            
            else
            {

                if (current.X == destination.X) // Currently over destination, move down
                {
                    next = new Point(current.X, destination.Y);
                    return next;
                }
                else // Move sideways over destination
                {
                    next = new Point(destination.X, current.Y);
                    return next;
                }
            }

        }

        private Point GetNextUpwardsPoint(Point current, Point destination, double offset)
        {
            Point next = new Point();

            double width = Settings.Instance.Person.Width;
            double height = Settings.Instance.Person.Height;
            double horizontalSpace = Settings.Instance.Person.HorizontalSpace;
            double verticalSpace = Settings.Instance.Person.VerticalSpace;
            double margin = Settings.Instance.Person.Margin;            

            int location = GetVerticalLocationRelativeToPeople(current, offset);
            bool crossGeneration = (Math.Abs(destination.Y - current.Y) >= verticalSpace);

            if (location == 3) // Currently at origin, go up to center line
            {
                double Y = current.Y + margin + offset - verticalSpace / 2;
                next = new Point(current.X, Y);
                return next;
            }

            if (crossGeneration) // Go up to next level
            {                
                int generationIndex = GetGenerationIndex(current.Y);
                List<PersonView> peopleAbove = Members.Where(m => m.GenerationIndex == generationIndex - 1).OrderBy(m => m.X).ToList();                
                bool spaceAbove = true;
                foreach (PersonView person in peopleAbove)
                {
                    if (current.X > person.X && current.X < person.X + width) { spaceAbove = false; break; }
                }
         
                if (spaceAbove) // Space above, go up
                {
                    double Y = current.Y - (height + verticalSpace);
                    next = new Point(current.X, Y);
                    return next;
                }
                else // Move sideways to vertical path opening
                {
                    bool moveRight = current.X <= destination.X;
                    double X = moveRight ? current.X + (width + horizontalSpace) / 2 : current.X - (width + horizontalSpace) / 2;
                    next = new Point(X, current.Y);
                    return next;
                }
            }
            else
            {

                if (current.X == destination.X) // Move down to destination
                {
                    next = new Point(current.X, destination.Y);
                    return next;
                }
                else // Move sideways over destination
                {
                    next = new Point(destination.X, current.Y);
                    return next;
                }
            }

        }

        private int GetVerticalLocationRelativeToPeople(Point point, double offset)
        {
            double height = Settings.Instance.Person.Height;
            double space = Settings.Instance.Person.VerticalSpace;
            double margin = Settings.Instance.Person.Margin;
            double location = point.Y % (height + space);
            bool positive = location >= 0;
            if (positive)
            {
                if (location == height + margin) { return 1; } // Person bottom
                if (location == height + space / 2 + offset) { return 2; } // On horizontal path
                if (location == height + space - margin) { return 3; } // Person top
            }
            else
            {
                if (location == - space + margin) { return 1; } // Person bottom
                if (location == - space / 2 + offset) { return 2; } // On horizontal path
                if (location == - margin) { return 3; } // Person top
            }

            //if (location == height/2 + margin) { return (positive) ? 1 : 3; } // Person bottom
            //if (location == (height + space) / 2 + offset) { return 2; } // On horizontal path
            //if (location == height/2 + space - margin) { return (positive) ? 3 : 1; } // Person top

            //if (positive)
            //{

            //}
            //else
            //{
            //    if (location == height/2 + margin) { return 1; } // Person bottom
            //    if (location == (height + space) / 2 + offset) { return 2; } // On horizontal path
            //    if (location == margin) { return 3; } // Person top
            //}


            return 0;
        }       

        private int GetGenerationIndex(double currentYPosition)
        {            
            double height = Settings.Instance.Person.Height;
            double horizontalSpace = Settings.Instance.Person.HorizontalSpace;
 
            if (currentYPosition > 0)
            {
                return (int)Math.Floor(currentYPosition / (height + horizontalSpace));
            }
            else
            {
                return (int)Math.Ceiling(currentYPosition / (height + horizontalSpace));
            }  
        }        
        
        public RelationshipView GetRelationship(int ID)
        {
            return (RelationshipView)relationships.Where(r => r.Id == ID).FirstOrDefault();
        }

        public PersonView GetPerson(int ID)
        {
            return (PersonView)members.Where(m => m.Id == ID).FirstOrDefault();
        }

        //public void InitalizePerson(PersonView person)
        //{
        //    person.Id = GetNextID();
        //    person.FirstName = "First Name";
        //    person.LastName = "Last Name";
        //    person.Gender = "Not Specified";
        //    person.DOB = DateTime.Now;
        //    person.SiblingIds = new List<int> { };
        //    person.PartnerIds = new List<int> { };
        //    person.FriendIds = new List<int> { };
        //    person.ChildrenIds = new List<int> { };
        //    person.AbuserIds = new List<int> { };
        //    person.VictimIds = new List<int> { };

        //    person.GenerationIndex = 0;
        //    person.SiblingIndex = 0;

        //    person.Width = Settings.Instance.Person.Width;
        //    person.Height = Settings.Instance.Person.Height;
        //}

        public int GetNextID()
        {
            if (Members.Count == 0) { return 1; }
            int? maxId = Members.Max(m => m.Id) + 1;
            return maxId ?? 1;
        }

        private void AddPersonToFamily(PersonView person)
        {
            if (Members == null)
            {
                Members = new ObservableCollection<PersonView> { };
            }
            Members.Add(person);
            OrderSiblings(person.GenerationIndex);
            SetTreeLayout();
        }

        private void SetPersonPosition(PersonView person)
        {
            person.X = Tree.Width / 2 + person.SiblingIndex * (Settings.Instance.Person.Width + Settings.Instance.Person.HorizontalSpace) - Settings.Instance.Person.Width / 2;
            person.Y = (person.GenerationIndex - Members.Min(m => m.GenerationIndex)) * (Settings.Instance.Person.Height + Settings.Instance.Person.VerticalSpace);
        }

        private void OrderSiblings(int generation)
        {
            List<PersonView> generationMembers = new List<PersonView> { };
            generationMembers = (List<PersonView>)members.Where(m => m.GenerationIndex == generation).OrderBy(m => m.DOB).ToList<PersonView>();
            // Center generation members about a zero index for easy positioning (i.e. -2,-1,0,1,2)           
            for (int i = 0; i < generationMembers.Count(); i++)
            {
                generationMembers[i].SiblingIndex = Convert.ToDouble(i) - (Convert.ToDouble(generationMembers.Count()) - 1) / 2;
            }

        }

        private void SetTreeLayout()
        {
            Tree.Width = (Members.Max(m => m.SiblingIndex) + 1 - Members.Min(m => m.SiblingIndex)) * (Settings.Instance.Person.Width + Settings.Instance.Person.HorizontalSpace) + Settings.Instance.Person.HorizontalSpace;
            Tree.Height = (Members.Max(m => m.GenerationIndex) + 1 - Members.Min(m => m.GenerationIndex)) * (Settings.Instance.Person.Height + Settings.Instance.Person.VerticalSpace) + Settings.Instance.Person.VerticalSpace;

            foreach (PersonView person in Members)
            {
                SetPersonPosition(person);
            }
            ResetAllRelationships();
            SetScaledTreeDimensions();
        }

        private void SetScaledTreeDimensions()
        {
            Tree.WidthScaled = Tree.Width * Tree.Scale;
            Tree.HeightScaled = Tree.Height * Tree.Scale;
        }

        public void SetWindowSize(double width, double height)
        {
            Tree.WindowWidth = width;
            Tree.WindowHeight = height;
        }

        public void CenterTreeInWindow()
        {

            SetTreeLayout();
            Tree.XPosition = (Tree.WindowWidth / 2) - (Tree.Width / 2);
            Tree.YPosition = (Tree.WindowHeight / 2) - (Tree.Height / 2);
        }

        public void MoveTreePositionInWindow(double deltaX, double deltaY)
        {
            Tree.XPosition += deltaX; /// TreeScale;
            Tree.YPosition += deltaY; /// TreeScale;
        }

        public void ScaleTree(double scaleIncrease, double windowCenterX, double windowCenterY)

        {

            //TreeScaleOrigin = new Point(windowCenterX / windowWidth, windowCenterY / windowHeight);
            Tree.ScaleOrigin = new Point(0.5, 0.5);

            Tree.ScaleCenterX = windowCenterX;
            Tree.ScaleCenterY = windowCenterY;

            if (scaleIncrease > 0)
            { Tree.Scale = Tree.Scale * (scaleIncrease / 100); }
            else { Tree.Scale = Tree.Scale * (-100 / scaleIncrease); }

            //XPosition = XPosition - (windowWidth * TreeScale) * scaleIncrease;
            //YPosition = YPosition - (windowHeight * TreeScale)* scaleIncrease;

            SetScaledTreeDimensions();
        }

        public void Save()
        {

            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "Family.fex";
            // set filters - this can be done in properties as well
            savefile.Filter = "Family Explorer files (*.fex)|*.fex|All files (*.*)|*.*";

            Nullable<bool> result = savefile.ShowDialog();

            if (result == true)
            {

                FamilyModel family = new FamilyModel();
                family.PersonSettings = Settings.Instance.Person;
                family.RelationshipSettings = Settings.Instance.Relationship;
                family.Tree = Tree;
                family.Members = new ObservableCollection<PersonModel>() { };
                foreach (PersonView personView in Members)
                {
                    PersonModel personModel = new PersonModel();
                    personModel.CopyBaseProperties(personView);
                    family.Members.Add(personModel);
                }
                family.Relationships = new ObservableCollection<RelationshipModel> { };
                foreach (RelationshipView relationshipView in Relationships)
                {
                    RelationshipModel relationshipModel = new RelationshipModel();
                    relationshipModel.CopyBaseProperties(relationshipView);
                    family.Relationships.Add(relationshipModel);
                }
                XmlSerializer xsSubmit = new XmlSerializer(typeof(FamilyModel));
                var subReq = family;
                using (StringWriter sww = new StringWriter())
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, subReq);
                    var xml = sww.ToString();
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xml);
                    xdoc.Save(savefile.FileName);
                    Title = "Family Explorer - " + savefile.FileName;
                }                
            }
        }

        public void Open()
        {
            OpenFileDialog openfile = new OpenFileDialog();
            // set a default file name
            openfile.FileName = "Family.fex";
            // set filters - this can be done in properties as well
            openfile.Filter = "Family Explorer files (*.fex)|*.fex|All files (*.*)|*.*";

            Nullable<bool> result = openfile.ShowDialog();

            if (result == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(FamilyModel));
                using (StreamReader reader = new StreamReader(openfile.FileName))
                {
                    FamilyModel family = new FamilyModel();
                    family = (FamilyModel)serializer.Deserialize(reader);
                    if (family.PersonSettings != null) { Settings.Instance.Person = family.PersonSettings; }
                    if (family.RelationshipSettings != null) { Settings.Instance.Relationship = family.RelationshipSettings; }       
                    if (family.Tree != null) { Tree = family.Tree; }
                    Members.Clear();
                    if (family.Members != null)
                    {
                        foreach (PersonModel personModel in family.Members)
                        {
                            PersonView personView = new PersonView(GetNextID());
                            personView.CopyBaseProperties(personModel);
                            Members.Add(personView);                                                        
                        }
                    }
                    Relationships.Clear();
                    if (family.Relationships != null)
                    {
                        foreach (RelationshipModel relationshipModel in family.Relationships)
                        {
                            RelationshipView relationshipView = new RelationshipView();
                            relationshipView.CopyBaseProperties(relationshipModel);
                            Relationships.Add(relationshipView);
                        }
                    }
                    Title = "Family Explorer - " + openfile.FileName;
                    
                    SetTreeLayout();
                }
            }
        }

    }
}
