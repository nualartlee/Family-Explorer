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
            //SelectedRelationship = new RelationshipView();
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
            if (person.MotherRelationship != null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
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
            //mom.ChildrenIds.Add(child.Id);
            //child.MotherId = mom.Id;

            AddPersonToFamily(mom);
            CreateRelationship(1, mom, child, child.DOB, null);
        }

        public void AddFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            if (person.FatherRelationship != null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
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
            AddPersonToFamily(dad);
            CreateRelationship(2, dad, child, child.DOB, null);
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
            AddPersonToFamily(newSibling);

            // Create new relationships
            CreateRelationship(3, person, newSibling, newSibling.DOB, null);

            foreach (RelationshipView siblingRelationship in person.SiblingRelationships)
            {
                if (person != siblingRelationship.PersonDestination) { CreateRelationship(3, siblingRelationship.PersonDestination, newSibling, newSibling.DOB, null); }
                else {  }
            }

            if (person.MotherRelationship != null)
            {
                PersonView mom = person.MotherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in mom.ChildRelationships)
                {
                    CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                CreateRelationship(1, mom, newSibling, newSibling.DOB, null);
            }

            if (person.FatherRelationship != null)
            {
                PersonView dad = person.FatherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in dad.ChildRelationships)
                {
                    CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                CreateRelationship(2, dad, newSibling, newSibling.DOB, null);
            }
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
            AddPersonToFamily(newSibling);

            // Create new relationships
            if (person.MotherRelationship != null)
            {
                PersonView mom = person.MotherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in mom.ChildRelationships)
                {
                    CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                CreateRelationship(1, mom, newSibling, newSibling.DOB, null);
            }
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
            AddPersonToFamily(newSibling);

            // Create new relationships   
            if (person.FatherRelationship != null)
            {
                PersonView dad = person.FatherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in dad.ChildRelationships)
                {
                    CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                CreateRelationship(2, dad, newSibling, newSibling.DOB, null);
            }
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
                 
            AddPersonToFamily(friend);
            CreateRelationship(4, person, friend, DateTime.Now, null);
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

            AddPersonToFamily(partner);
            CreateRelationship(5, person, partner, DateTime.Now, null);
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
            AddPersonToFamily(newChild);

            // Create relationships               
            if (parent.Gender == "Female")
            {
                CreateRelationship(1, parent, newChild, newChild.DOB, null);                
            }
            if (parent.Gender == "Male")
            {
                CreateRelationship(2, parent, newChild, newChild.DOB, null);
            }

            foreach (RelationshipView childRelationship in parent.ChildRelationships)
            {
                CreateRelationship(3, childRelationship.PersonDestination, newChild, newChild.DOB, null);
            }            
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

            AddPersonToFamily(abuser);
            CreateRelationship(6, abuser, victim, DateTime.Now, null);
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
           
            AddPersonToFamily(victim);
            CreateRelationship(6, abuser, victim, DateTime.Now, null);
        }

        public void SetMother_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            if (person.MotherRelationship != null) { e.CanExecute = false; }
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
            // Create new relationships            
            foreach (RelationshipView childRelationship in mother.ChildRelationships)
            {
                PersonView sibling = childRelationship.PersonDestination;

                if (sibling.Id < person.Id)
                { CreateRelationship(3, sibling, person, person.DOB, null); }
                else
                { CreateRelationship(3, person, sibling, sibling.DOB, null); }
            }
            CreateRelationship(1, mother, person, person.DOB, null);
        }

        public void SetFather_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            PersonView person = (PersonView)e.Parameter;
            if (person.FatherRelationship != null) { e.CanExecute = false; }
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
            // Create new relationships            
            foreach (RelationshipView childRelationship in father.ChildRelationships)
            {
                PersonView sibling = childRelationship.PersonDestination;

                if (sibling.Id < person.Id)
                { CreateRelationship(3, sibling, person, person.DOB, null); }
                else
                { CreateRelationship(3, person, sibling, sibling.DOB, null); }
            }
            CreateRelationship(2, father, person, person.DOB, null);
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
            foreach (RelationshipView friendRelationship in person.FriendRelationships)
            {
                if (friendRelationship.PersonSource == setCommandTargetPerson) { return false; }
                if (friendRelationship.PersonDestination == setCommandTargetPerson) { return false; }
            }        
               
            return true;
        }

        private void SetFriend_Finalized(PersonView person, PersonView friend)
        {
            // Create relationship
            if (person.Id < friend.Id)
            { CreateRelationship(4, person, friend, DateTime.Now, null); }
            else
            { CreateRelationship(4, friend, person, DateTime.Now, null); }
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
            foreach (RelationshipView partnerRelationship in person.PartnerRelationships)
            {
                if (partnerRelationship.PersonSource == setCommandTargetPerson) { return false; }
                if (partnerRelationship.PersonDestination == setCommandTargetPerson) { return false; }
            }
            return true;
        }

        private void SetPartner_Finalized(PersonView person, PersonView partner)
        {
            // Create relationship
            if (person.Id < partner.Id)
            { CreateRelationship(5, person, partner, DateTime.Now, null); }
            else
            { CreateRelationship(5, partner, person, DateTime.Now, null); }
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
            foreach (RelationshipView childRelationship in setCommandTargetPerson.ChildRelationships)
            {                
                if (childRelationship.PersonDestination == child) { return false; }
            }
            // Not in the next generation
            if (setCommandTargetPerson.GenerationIndex + 1 != child.GenerationIndex) { return false; }
            return true;
        }

        private void SetChild_Finalized(PersonView person, PersonView child)
        {
            // Create relationships
            foreach (RelationshipView childRelationship in person.ChildRelationships)
            {
                PersonView sibling = childRelationship.PersonDestination;

                if (sibling.Id < person.Id)
                { CreateRelationship(3, sibling, person, person.DOB, null); }
                else
                { CreateRelationship(3, person, sibling, sibling.DOB, null); }
            }
            if (person.Gender == "Female") { CreateRelationship(1, person, child, child.DOB, null); }
            if (person.Gender == "Male") { CreateRelationship(2, person, child, child.DOB, null); }


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
            foreach (RelationshipView victimRelationship in person.VictimRelationships)
            {               
                if (victimRelationship.PersonDestination == setCommandTargetPerson) { return false; }
            }
            
            return true;
        }

        private void SetAbuser_Finalized(PersonView person, PersonView abuser)
        {
            // Create relationship                      
            CreateRelationship(6, person, setCommandTargetPerson, DateTime.Now, null);
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
            foreach (RelationshipView abuserRelationship in person.AbuserRelationships)
            {
                if (abuserRelationship.PersonSource == setCommandTargetPerson) { return false; }
            }
            return true;
        }

        private void SetVictim_Finalized(PersonView person, PersonView victim)
        {
            // Create relationship                      
            CreateRelationship(6, setCommandTargetPerson, person, DateTime.Now, null);
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
                //ResetAllRelationships();
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
            if (SelectedRelationship != null)
            {
                SelectedRelationship.Selected = false;                
            }
            if (relationship != null)
            {
                SelectedRelationship = relationship;
                SelectedRelationship.Selected = true;
            }
            else
            {
                SelectedRelationship = null;
            }
        }

        public void SelectPerson(PersonView person)
        {
            SelectedPerson.Selected = false;
            if (person != null)
            {
                SelectedPerson = person;
                SelectedPerson.Selected = true;                
            }
            else
            {
                SelectedPerson = null;
            }
        }        

        #endregion Commands
        
        public RelationshipView GetRelationship(int ID)
        {
            return (RelationshipView)relationships.Where(r => r.Id == ID).FirstOrDefault();
        }

        public PersonView GetPerson(int ID)
        {
            return (PersonView)members.Where(m => m.Id == ID).FirstOrDefault();
        }
                
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

        private void CreateRelationship(int type, PersonView personSource, PersonView personDestination, DateTime? startDate, DateTime? endDate)
        {

            int Id = type * (int)Math.Pow(10, 6) + personSource.Id * (int)Math.Pow(10, 3) + personDestination.Id;
            RelationshipView relationship = FamilyView.Instance.GetRelationship(Id);
            if (relationship != null)
            {
                relationship.ResetAllData();
            }
            else
            {
                RelationshipView newRelationship = new RelationshipView(Id, startDate, endDate);
                FamilyView.Instance.Relationships.Add(newRelationship);
            }
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
            //ResetAllRelationships();
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
                            RelationshipView relationshipView = new RelationshipView(relationshipModel.Id);
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
