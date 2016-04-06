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
    public class PersonView :PersonBase, INotifyPropertyChanged
    {

        private RelationshipView motherRelationship;
        public RelationshipView MotherRelationship
        {
            get { return motherRelationship; }
            set
            {
                if (value != motherRelationship)
                {
                    motherRelationship = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private RelationshipView fatherRelationship;
        public RelationshipView FatherRelationship
        {
            get { return fatherRelationship; }
            set
            {
                if (value != fatherRelationship)
                {
                    fatherRelationship = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<RelationshipView> SiblingRelationships { get; set; }
        public ObservableCollection<RelationshipView> FriendRelationships { get; set; }
        public ObservableCollection<RelationshipView> PartnerRelationships { get; set; }
        public ObservableCollection<RelationshipView> ChildRelationships { get; set; }
        public ObservableCollection<RelationshipView> AbuserRelationships { get; set; }
        public ObservableCollection<RelationshipView> VictimRelationships { get; set; }

        private string borderBrush;
        public string BorderBrush
        {
            get { return borderBrush; }
            set
            {
                if (value != borderBrush)
                {
                    borderBrush = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string highlightBorderBrush;
        public string HighlightBorderBrush
        {
            get { return highlightBorderBrush; }
            set
            {
                if (value != highlightBorderBrush)
                {
                    highlightBorderBrush = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string background;
        public string Background
        {
            get { return background; }
            set
            {
                if (value != background)
                {
                    background = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string textColor;
        public string TextColor
        {
            get { return textColor; }
            set
            {
                if (value != textColor)
                {
                    textColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double borderThickness;
        public double BorderThickness
        {
            get { return borderThickness; }
            set
            {
                if (value != borderThickness)
                {
                    borderThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double highlightBorderThickness;
        public double HighlightBorderThickness
        {
            get { return highlightBorderThickness; }
            set
            {
                if (value != highlightBorderThickness)
                {
                    highlightBorderThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private double width;
        public double Width
        {
            get { return width; }
            set
            {
                if (value != width)
                {
                    width = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double height;
        public double Height
        {
            get { return height; }
            set
            {
                if (value != height)
                {
                    height = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double x;
        public double X
        {
            get { return x; }
            set
            {
                if (value != x)
                {
                    x = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                if (value != y)
                {
                    y = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (value != selected)
                {
                    selected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool mouseOver;
        public bool MouseOver
        {
            get { return mouseOver; }
            set
            {
                if (value != mouseOver)
                {
                    mouseOver = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public PersonView()
        {           
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);            
            Initialize();
        }

        public PersonView(int id)
        {
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            Initialize();
            Id = id;
        }
        
        public void Initialize()
        {

            InitiateAddCommands();
            InitiateSetCommands();

            FirstName = "First Name";
            LastName = "Last Name";
            Gender = "Not Specified";
            DOB = DateTime.Now;
            
            SiblingRelationships = new ObservableCollection<RelationshipView> { };
            FriendRelationships = new ObservableCollection<RelationshipView> { };
            PartnerRelationships = new ObservableCollection<RelationshipView> { };
            ChildRelationships = new ObservableCollection<RelationshipView> { };
            AbuserRelationships = new ObservableCollection<RelationshipView> { };
            VictimRelationships = new ObservableCollection<RelationshipView> { };           

            GenerationIndex = 0;
            SiblingIndex = 0;

            Refresh();
            //RefreshColors();
            //RefreshSize();            
        }
        
        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            Refresh();
        }
                
        public void Refresh()
        {
            RefreshColors();
            RefreshSize();
            RefreshAddCommandsCanExecute();
            RefreshSetCommandsCanExecute();
            RefreshTooltips();
        }

        public void RefreshColors()
        {
            Background = Settings.Instance.Person.BackgroundColor(Gender);
            BorderBrush = Settings.Instance.Person.BorderBrushColor(Gender);
            TextColor = Settings.Instance.Person.TextColor(Gender);
            if (Selected) { HighlightBorderBrush = Settings.Instance.Person.SelectedBorderBrushColor; }
            else if (MouseOver) { HighlightBorderBrush = Settings.Instance.Person.HighlightedBorderBrushColor; }
            else { HighlightBorderBrush = "Transparent"; }
        }

        public void RefreshSize()
        {
            Width = Settings.Instance.Person.Width;
            Height = Settings.Instance.Person.Height;

            BorderThickness = Settings.Instance.Person.BorderThickness;
            HighlightBorderThickness = Settings.Instance.Person.HighlightBorderThickness;
        }

        public void AddRelationship(RelationshipView relationship)
        {
            if (!IsNewRelationship(relationship)) { return; }

            switch (relationship.Type)
            {
                case 1:
                    if (this == relationship.PersonSource) { ChildRelationships.Add(relationship); }
                    else { MotherRelationship = relationship; }                  
                    break;
                case 2:
                    if (this == relationship.PersonSource) { ChildRelationships.Add(relationship); }
                    else { FatherRelationship = relationship; }
                    break;
                case 3:
                    SiblingRelationships.Add(relationship);
                    break;
                case 4:
                    FriendRelationships.Add(relationship);
                    break;
                case 5:
                    PartnerRelationships.Add(relationship);
                    break;
                case 6:
                    if (this == relationship.PersonSource)
                    {
                        VictimRelationships.Add(relationship);
                    }
                    else
                    {
                        AbuserRelationships.Add(relationship);
                    }
                    break;                    
            }
        }

        private bool IsNewRelationship(RelationshipView relationship)
        {
            bool IsNew = true;
            if (MotherRelationship != null)
            {
                if (relationship.Id == MotherRelationship.Id) { IsNew = false; }
            }
            if (FatherRelationship != null)
            {
                if (relationship.Id == FatherRelationship.Id) { IsNew = false; }
            }
            foreach (RelationshipView currentRelationship in SiblingRelationships)
            {
                if (relationship.Id == currentRelationship.Id) { IsNew = false; }
            }
            foreach (RelationshipView currentRelationship in FriendRelationships)
            {
                if (relationship.Id == currentRelationship.Id) { IsNew = false; }
            }
            foreach (RelationshipView currentRelationship in PartnerRelationships)
            {
                if (relationship.Id == currentRelationship.Id) { IsNew = false; }
            }
            foreach (RelationshipView currentRelationship in ChildRelationships)
            {
                if (relationship.Id == currentRelationship.Id) { IsNew = false; }
            }
            foreach (RelationshipView currentRelationship in AbuserRelationships)
            {
                if (relationship.Id == currentRelationship.Id) { IsNew = false; }
            }
            foreach (RelationshipView currentRelationship in VictimRelationships)
            {
                if (relationship.Id == currentRelationship.Id) { IsNew = false; }
            }
            return IsNew;
        }
        
        private void InitiateAddCommands()
        {
            MouseEnter = new RelayCommand(MouseEnter_Executed, MouseEnter_CanExecute);
            MouseLeave = new RelayCommand(MouseLeave_Executed, MouseLeave_CanExecute);
            MouseLeftButtonDown = new RelayCommand<MouseEventArgs>(MouseLeftButtonDown_Executed, MouseLeftButtonDown_CanExecute);
            Delete = new RelayCommand(Delete_Executed, Delete_CanExecute);
            AddMother = new RelayCommand(AddMother_Executed, AddMother_CanExecute);
            AddFather = new RelayCommand(AddFather_Executed, AddFather_CanExecute);
            AddSiblingUnknownParents = new RelayCommand(AddSiblingUnknownParents_Executed, AddSiblingUnknownParents_CanExecute);
            AddSiblingEqualParents = new RelayCommand(AddSiblingEqualParents_Executed, AddSiblingEqualParents_CanExecute);
            AddSiblingByMother = new RelayCommand(AddSiblingByMother_Executed, AddSiblingByMother_CanExecute);
            AddSiblingByFather = new RelayCommand(AddSiblingByFather_Executed, AddSiblingByFather_CanExecute);
            AddFriend = new RelayCommand(AddFriend_Executed, AddFriend_CanExecute);
            AddPartner = new RelayCommand(AddPartner_Executed, AddPartner_CanExecute);
            AddChild = new RelayCommand(AddChild_Executed, AddChild_CanExecute);
            AddAbuser = new RelayCommand(AddAbuser_Executed, AddAbuser_CanExecute);
            AddVictim = new RelayCommand(AddVictim_Executed, AddVictim_CanExecute);
        }

        private void RefreshAddCommandsCanExecute()
        {
            MouseEnter.RaiseCanExecuteChanged();
            MouseLeave.RaiseCanExecuteChanged();
            MouseLeftButtonDown.RaiseCanExecuteChanged();
            Delete.RaiseCanExecuteChanged();
            AddMother.RaiseCanExecuteChanged();
            AddFather.RaiseCanExecuteChanged();
            AddSiblingUnknownParents.RaiseCanExecuteChanged();
            AddSiblingEqualParents.RaiseCanExecuteChanged();
            AddSiblingByMother.RaiseCanExecuteChanged();
            AddSiblingByFather.RaiseCanExecuteChanged();
            AddFriend.RaiseCanExecuteChanged();
            AddPartner.RaiseCanExecuteChanged();
            AddChild.RaiseCanExecuteChanged();
            AddAbuser.RaiseCanExecuteChanged();
            AddVictim.RaiseCanExecuteChanged();
        }

        private void RefreshTooltips()
        {
            Delete_ToolTip = "Delete " + FirstName;
            AddMother_ToolTip = "Add " + FirstName + "'s mother";
            AddFather_ToolTip = "Add " + FirstName + "'s father";
            AddSiblingUnknownParents_ToolTip = "Add a sibling to " + FirstName + " from unknown parents";
            AddSiblingEqualParents_ToolTip = "Add a sibling sharing " + FirstName + "'s parents";
            AddSiblingByMother_ToolTip = "Add a sibling sharing " + FirstName + "'s mother";
            AddSiblingByFather_ToolTip = "Add a sibling sharing " + FirstName + "'s father";
            AddFriend_ToolTip = "Add a friend of " + FirstName;
            AddPartner_ToolTip = "Add a partner of " + FirstName;
            AddChild_ToolTip = "Add a child of " + FirstName;
            AddAbuser_ToolTip = "Add an abuser of " + FirstName;
            AddVictim_ToolTip = "Add a victim of " + FirstName;
            SelectMother_ToolTip = "Select " + FirstName + "'s mother from the tree";
            SelectFather_ToolTip = "Select " + FirstName + "'s father from the tree";
            SelectFriend_ToolTip = "Select " + FirstName + "'s friend from the tree";
            SelectPartner_ToolTip = "Select " + FirstName + "'s partner from the tree";
            SelectChild_ToolTip = "Select " + FirstName + "'s child from the tree";
            SelectAbuser_ToolTip = "Select " + FirstName + "'s abuser from the tree";
            SelectVictim_ToolTip = "Select " + FirstName + "'s victim from the tree";
        }
         
        public RelayCommand MouseEnter
        {
            get;
            private set;
        }
        private bool MouseEnter_CanExecute()
        {
            return true;
        }
        private void MouseEnter_Executed()
        {
            MouseOver = true;
            FamilyView.Instance.EnterSetCommandRelation(this);
        }

        public RelayCommand MouseLeave
        {
            get;
            private set;
        }
        private bool MouseLeave_CanExecute()
        {
            return true;
        }
        private void MouseLeave_Executed()
        {
            MouseOver = false;
            FamilyView.Instance.ExitSetCommandRelation();
        }

        public RelayCommand<MouseEventArgs> MouseLeftButtonDown
        {
            get;
            private set;
        }
        private bool MouseLeftButtonDown_CanExecute(MouseEventArgs e)
        {
            return true;
        }
        private void MouseLeftButtonDown_Executed(MouseEventArgs e)
        {
            if (FamilyView.Instance.SelectCommandInProgress) { FamilyView.Instance.FinalizeSetCommand(this); }
            else { FamilyView.Instance.SelectPerson(this); }            
            e.Handled = true;
        }

        public RelayCommand Delete
        {
            get;
            private set;
        }
        private bool Delete_CanExecute()
        {
            if (FamilyView.Instance.Members.Count() > 1) { return true; }
            else { return false; }
        }
        private void Delete_Executed()
        {
            // Delete all relationships
            if (MotherRelationship != null) { MotherRelationship.Delete.Execute(null); }
            if (FatherRelationship != null) { FatherRelationship.Delete.Execute(null); }
            foreach (RelationshipView siblingRelationship in SiblingRelationships.ToList()) { siblingRelationship.Delete.Execute(null); }
            foreach (RelationshipView friendRelationship in FriendRelationships.ToList()) { friendRelationship.Delete.Execute(null); }
            foreach (RelationshipView partnerRelationship in PartnerRelationships.ToList()) { partnerRelationship.Delete.Execute(null); }
            foreach (RelationshipView childRelationship in ChildRelationships.ToList()) {childRelationship.Delete.Execute(null); }
            foreach (RelationshipView abuserRelationship in AbuserRelationships.ToList()) { abuserRelationship.Delete.Execute(null); }
            foreach (RelationshipView victimRelationship in VictimRelationships.ToList()) { victimRelationship.Delete.Execute(null); }

            // Delete person from family members
            if (FamilyView.Instance.SelectedPerson == this) { FamilyView.Instance.SelectedPerson = null; }
            FamilyView.Instance.Members.Remove(this);            
        }
        private string delete_ToolTip;
        public string Delete_ToolTip
        {
            get { return delete_ToolTip; }
            set
            {
                if (value != delete_ToolTip)
                {
                    delete_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddMother
        {
            get;
            private set;            
        }
        private bool AddMother_CanExecute()
        {            
            if (MotherRelationship != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void AddMother_Executed()
        {
            PersonView mom = new PersonView(FamilyView.Instance.GetNextID());
            mom.FirstName = "Mother Of " + FirstName;
            mom.LastName = "";
            mom.Gender = "Female";
            mom.GenerationIndex = GenerationIndex - 1;
            FamilyView.Instance.AddPersonToFamily(mom);
            FamilyView.Instance.CreateRelationship(1, mom, this, DOB, null);
        }
        private string addMother_ToolTip;
        public string AddMother_ToolTip
        {
            get { return addMother_ToolTip; }
            set
            {
                if (value != addMother_ToolTip)
                {
                    addMother_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddFather
        {
            get;
            private set;
        }
        public bool AddFather_CanExecute()
        {            
            if (FatherRelationship != null)
            {
               return false;
            }
            else
            {
                return true;
            }
        }
        public void AddFather_Executed()
        {
            PersonView dad = new PersonView(FamilyView.Instance.GetNextID());
            dad.FirstName = "Father Of " + FirstName;
            dad.LastName = "";
            dad.Gender = "Male";
            dad.GenerationIndex = GenerationIndex - 1;
            FamilyView.Instance.AddPersonToFamily(dad);
            FamilyView.Instance.CreateRelationship(2, dad, this, DOB, null);
        }
        private string addFather_ToolTip;
        public string AddFather_ToolTip
        {
            get { return addFather_ToolTip; }
            set
            {
                if (value != addFather_ToolTip)
                {
                    addFather_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddSiblingUnknownParents
        {
            get;
            private set;
        }
        public bool AddSiblingUnknownParents_CanExecute()
        {
            return true;
        }
        public void AddSiblingUnknownParents_Executed()
        {
            // Create new sibling
            PersonView newSibling = new PersonView(FamilyView.Instance.GetNextID());
            newSibling.FirstName = "Sibling Of " + FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = GenerationIndex;
            FamilyView.Instance.AddPersonToFamily(newSibling);

            // Create new relationships

            // With me
            FamilyView.Instance.CreateRelationship(3, this, newSibling, newSibling.DOB, null);
            
        }
        private string addSiblingUnknownParents_ToolTip;
        public string AddSiblingUnknownParents_ToolTip
        {
            get { return addSiblingUnknownParents_ToolTip; }
            set
            {
                if (value != addSiblingUnknownParents_ToolTip)
                {
                    addSiblingUnknownParents_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddSiblingEqualParents
        {
            get;
            private set;
        }
        public bool AddSiblingEqualParents_CanExecute()
        {
            if (MotherRelationship != null && FatherRelationship != null) { return true; }
            else { return false; }
        }
        public void AddSiblingEqualParents_Executed()
        {
            // Create new sibling
            PersonView newSibling = new PersonView(FamilyView.Instance.GetNextID());
            newSibling.FirstName = "Sibling Of " + FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = GenerationIndex;
            FamilyView.Instance.AddPersonToFamily(newSibling);

            // Create new relationships

            // With me
            FamilyView.Instance.CreateRelationship(3, this, newSibling, newSibling.DOB, null);

            // With existing siblings
            foreach (RelationshipView siblingRelationship in SiblingRelationships)
            {
                if (this != siblingRelationship.PersonDestination) { FamilyView.Instance.CreateRelationship(3, siblingRelationship.PersonDestination, newSibling, newSibling.DOB, null); }
                else { FamilyView.Instance.CreateRelationship(3, siblingRelationship.PersonSource, newSibling, newSibling.DOB, null); }
            }

            // With mother & her children
            if (MotherRelationship != null)
            {
                PersonView mom = MotherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in mom.ChildRelationships)
                {
                    FamilyView.Instance.CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                FamilyView.Instance.CreateRelationship(1, mom, newSibling, newSibling.DOB, null);
            }

            // With father & his children
            if (FatherRelationship != null)
            {
                PersonView dad = FatherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in dad.ChildRelationships)
                {
                    FamilyView.Instance.CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                FamilyView.Instance.CreateRelationship(2, dad, newSibling, newSibling.DOB, null);
            }
        }
        private string addSiblingEqualParents_ToolTip;
        public string AddSiblingEqualParents_ToolTip
        {
            get { return addSiblingEqualParents_ToolTip; }
            set
            {
                if (value != addSiblingEqualParents_ToolTip)
                {
                    addSiblingEqualParents_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddSiblingByMother
        {
            get;
            private set;
        }
        public bool AddSiblingByMother_CanExecute()
        {
            if (MotherRelationship != null) { return true; }
            else { return false; }
        }
        public void AddSiblingByMother_Executed()
        {
            // Create new sibling
            PersonView newSibling = new PersonView(FamilyView.Instance.GetNextID());
            newSibling.FirstName = "Sibling Of " + FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = GenerationIndex;
            FamilyView.Instance.AddPersonToFamily(newSibling);

            // Create new relationships

            // With me
            FamilyView.Instance.CreateRelationship(3, this, newSibling, newSibling.DOB, null);
            
            // With mother & her children
            if (MotherRelationship != null)
            {
                PersonView mom = MotherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in mom.ChildRelationships)
                {
                    FamilyView.Instance.CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                FamilyView.Instance.CreateRelationship(1, mom, newSibling, newSibling.DOB, null);
            }
            
        }
        private string addSiblingByMother_ToolTip;
        public string AddSiblingByMother_ToolTip
        {
            get { return addSiblingByMother_ToolTip; }
            set
            {
                if (value != addSiblingByMother_ToolTip)
                {
                    addSiblingByMother_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddSiblingByFather
        {
            get;
            private set;
        }
        public bool AddSiblingByFather_CanExecute()
        {
            if (FatherRelationship != null) { return true; }
            else { return false; }
        }
        public void AddSiblingByFather_Executed()
        {
            // Create new sibling
            PersonView newSibling = new PersonView(FamilyView.Instance.GetNextID());
            newSibling.FirstName = "Sibling Of " + FirstName;
            newSibling.LastName = "";
            newSibling.Gender = "Not Specified";
            newSibling.GenerationIndex = GenerationIndex;
            FamilyView.Instance.AddPersonToFamily(newSibling);

            // Create new relationships

            // With me
            FamilyView.Instance.CreateRelationship(3, this, newSibling, newSibling.DOB, null);

            // With father & his children
            if (FatherRelationship != null)
            {
                PersonView dad = FatherRelationship.PersonSource;
                foreach (RelationshipView childRelationship in dad.ChildRelationships)
                {
                    FamilyView.Instance.CreateRelationship(3, childRelationship.PersonDestination, newSibling, newSibling.DOB, null);
                }
                FamilyView.Instance.CreateRelationship(2, dad, newSibling, newSibling.DOB, null);
            }

        }
        private string addSiblingByFather_ToolTip;
        public string AddSiblingByFather_ToolTip
        {
            get { return addSiblingByFather_ToolTip; }
            set
            {
                if (value != addSiblingByFather_ToolTip)
                {
                    addSiblingByFather_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddFriend
        {
            get;
            private set;
        }
        public bool AddFriend_CanExecute()
        {
            return true;
        }
        public void AddFriend_Executed()
        {
            PersonView friend = new PersonView(FamilyView.Instance.GetNextID());
            friend.FirstName = "Friend Of " + FirstName;
            friend.LastName = "";
            friend.Gender = "Not Specified";
            friend.GenerationIndex = GenerationIndex;

            FamilyView.Instance.AddPersonToFamily(friend);
            FamilyView.Instance.CreateRelationship(4, this, friend, DateTime.Now, null);
        }
        private string addFriend_ToolTip;
        public string AddFriend_ToolTip
        {
            get { return addFriend_ToolTip; }
            set
            {
                if (value != addFriend_ToolTip)
                {
                    addFriend_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddPartner
        {
            get;
            private set;
        }
        public bool AddPartner_CanExecute()
        {
           return true;
        }
        public void AddPartner_Executed()
        {
            PersonView partner = new PersonView(FamilyView.Instance.GetNextID());
            partner.FirstName = "Partner Of " + FirstName;
            partner.LastName = "";
            partner.Gender = "Not Specified";
            partner.GenerationIndex = GenerationIndex;

            FamilyView.Instance.AddPersonToFamily(partner);
            FamilyView.Instance.CreateRelationship(5, this, partner, DateTime.Now, null);
        }
        private string addPartner_ToolTip;
        public string AddPartner_ToolTip
        {
            get { return addPartner_ToolTip; }
            set
            {
                if (value != addPartner_ToolTip)
                {
                    addPartner_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddChild
        {
            get;
            private set;
        }
        public bool AddChild_CanExecute()
        {
            if (Gender == "Not Specified") { return false; }
            else { return true; }
        }
        public void AddChild_Executed()
        {
            // Create new child
            PersonView newChild = new PersonView(FamilyView.Instance.GetNextID());
            newChild.FirstName = "Child Of " + FirstName;
            newChild.LastName = "";
            newChild.Gender = "Not Specified";
            newChild.GenerationIndex = GenerationIndex + 1;
            FamilyView.Instance.AddPersonToFamily(newChild);

            // Create relationships               
            if (Gender == "Female")
            {
                FamilyView.Instance.CreateRelationship(1, this, newChild, newChild.DOB, null);
            }
            if (Gender == "Male")
            {
                FamilyView.Instance.CreateRelationship(2, this, newChild, newChild.DOB, null);
            }

            foreach (RelationshipView childRelationship in ChildRelationships)
            {
                FamilyView.Instance.CreateRelationship(3, childRelationship.PersonDestination, newChild, newChild.DOB, null);
            }
        }
        private string addChild_ToolTip;
        public string AddChild_ToolTip
        {
            get { return addChild_ToolTip; }
            set
            {
                if (value != addChild_ToolTip)
                {
                    addChild_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddAbuser
        {
            get;
            private set;
        }
        public bool AddAbuser_CanExecute()
        {
           return true;
        }
        public void AddAbuser_Executed()
        {
            PersonView abuser = new PersonView(FamilyView.Instance.GetNextID());
            abuser.FirstName = "Abuser Of " + FirstName;
            abuser.LastName = "";
            abuser.Gender = "Not Specified";
            abuser.GenerationIndex = GenerationIndex - 1;

            FamilyView.Instance.AddPersonToFamily(abuser);
            FamilyView.Instance.CreateRelationship(6, abuser, this, DateTime.Now, DateTime.Now);
        }
        private string addAbuser_ToolTip;
        public string AddAbuser_ToolTip
        {
            get { return addAbuser_ToolTip; }
            set
            {
                if (value != addAbuser_ToolTip)
                {
                    addAbuser_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand AddVictim
        {
            get;
            private set;
        }
        public bool AddVictim_CanExecute()
        {
            return true;
        }
        public void AddVictim_Executed()
        {
            PersonView victim = new PersonView(FamilyView.Instance.GetNextID());
            victim.FirstName = "Victim Of " + FirstName;
            victim.LastName = "";
            victim.Gender = "Not Specified";
            victim.GenerationIndex = GenerationIndex + 1;

            FamilyView.Instance.AddPersonToFamily(victim);
            FamilyView.Instance.CreateRelationship(6, this, victim, DateTime.Now, DateTime.Now);
        }
        private string addVictim_ToolTip;
        public string AddVictim_ToolTip
        {
            get { return addVictim_ToolTip; }
            set
            {
                if (value != addVictim_ToolTip)
                {
                    addVictim_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private void InitiateSetCommands()
        {
            SelectMother = new RelayCommand(SelectMother_Executed, SelectMother_CanExecute);
            SelectFather = new RelayCommand(SelectFather_Executed, SelectFather_CanExecute);
            SelectFriend = new RelayCommand(SelectFriend_Executed, SelectFriend_CanExecute);
            SelectPartner = new RelayCommand(SelectPartner_Executed, SelectPartner_CanExecute);
            SelectChild = new RelayCommand(SelectChild_Executed, SelectChild_CanExecute);
            SelectAbuser = new RelayCommand(SelectAbuser_Executed, SelectAbuser_CanExecute);
            SelectVictim = new RelayCommand(SelectVictim_Executed, SelectVictim_CanExecute);

        }

        private void RefreshSetCommandsCanExecute()
        {
            SelectMother.RaiseCanExecuteChanged();
            SelectFather.RaiseCanExecuteChanged();
            SelectFriend.RaiseCanExecuteChanged();
            SelectPartner.RaiseCanExecuteChanged();
            SelectChild.RaiseCanExecuteChanged();
            SelectAbuser.RaiseCanExecuteChanged();
            SelectVictim.RaiseCanExecuteChanged();

        }

        public RelayCommand SelectMother
        {
            get;
            private set;
        }
        public bool SelectMother_CanExecute()
        {            
            if (MotherRelationship != null) {return false; }
            else { return true; }
        }
        public void SelectMother_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 1;
            FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(1);
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Mother";
            FamilyView.Instance.SelectCommandInProgress = true;
        }
        private string selectMother_ToolTip;
        public string SelectMother_ToolTip
        {
            get { return selectMother_ToolTip; }
            set
            {
                if (value != selectMother_ToolTip)
                {
                    selectMother_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SelectFather
        {
            get;
            private set;
        }
        public bool SelectFather_CanExecute()
        {            
            if (FatherRelationship != null) { return false; }
            else {return true; }
        }
        public void SelectFather_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 2;
            FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(2);
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Father";
            FamilyView.Instance.SelectCommandInProgress = true;            
        }
        private string selectFather_ToolTip;
        public string SelectFather_ToolTip
        {
            get { return selectFather_ToolTip; }
            set
            {
                if (value != selectFather_ToolTip)
                {
                    selectFather_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SelectFriend
        {
            get;
            private set;
        }
        public bool SelectFriend_CanExecute()
        {
            return true;
        }
        public void SelectFriend_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 3;
            FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(4);
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Friend";
            FamilyView.Instance.SelectCommandInProgress = true;            
        }
        private string selectFriend_ToolTip;
        public string SelectFriend_ToolTip
        {
            get { return selectFriend_ToolTip; }
            set
            {
                if (value != selectFriend_ToolTip)
                {
                    selectFriend_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SelectPartner
        {
            get;
            private set;
        }
        public bool SelectPartner_CanExecute()
        {
            return true;
        }
        public void SelectPartner_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 4;
            FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(5);
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Partner";
            FamilyView.Instance.SelectCommandInProgress = true;            
        }
        private string selectPartner_ToolTip;
        public string SelectPartner_ToolTip
        {
            get { return selectPartner_ToolTip; }
            set
            {
                if (value != selectPartner_ToolTip)
                {
                    selectPartner_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SelectChild
        {
            get;
            private set;
        }
        public bool SelectChild_CanExecute()
        {            
            if (Gender == "Male" || Gender == "Female")
            { return true; }
            else { return false; }
        }
        public void SelectChild_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 5;
            if (Gender == "Female") { FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(1); }
            else { FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(2); }
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Child";
            FamilyView.Instance.SelectCommandInProgress = true;            
        }
        private string selectChild_ToolTip;
        public string SelectChild_ToolTip
        {
            get { return selectChild_ToolTip; }
            set
            {
                if (value != selectChild_ToolTip)
                {
                    selectChild_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SelectAbuser
        {
            get;
            private set;
        }
        public bool SelectAbuser_CanExecute()
        {
            return true;
        }
        public void SelectAbuser_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 6;
            FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(6);
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Abuser";
            FamilyView.Instance.SelectCommandInProgress = true;            
        }
        private string selectAbuser_ToolTip;
        public string SelectAbuser_ToolTip
        {
            get { return selectAbuser_ToolTip; }
            set
            {
                if (value != selectAbuser_ToolTip)
                {
                    selectAbuser_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SelectVictim
        {
            get;
            private set;
        }
        public bool SelectVictim_CanExecute()
        {
            return true;
        }
        public void SelectVictim_Executed()
        {
            FamilyView.Instance.SelectCommandTargetPerson = this;
            FamilyView.Instance.SelectCommandInProgressType = 7;
            FamilyView.Instance.SelectCommandInProgressColor = Settings.Instance.Relationship.PathColor(6);
            FamilyView.Instance.SelectCommandInProgressDescription = "Select " + FirstName + "'s Victim";
            FamilyView.Instance.SelectCommandInProgress = true;            
        }
        private string selectVictim_ToolTip;
        public string SelectVictim_ToolTip
        {
            get { return selectVictim_ToolTip; }
            set
            {
                if (value != selectVictim_ToolTip)
                {
                    selectVictim_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

    }
}
