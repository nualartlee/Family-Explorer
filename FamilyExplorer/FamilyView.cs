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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace FamilyExplorer
{
    /// <summary>
    /// This is the main viewmodel. MainWindow intantiates and binds to a singleton of this type when the program is started.
    /// </summary>
    public sealed class FamilyView : INotifyPropertyChanged
    {                
               
        // TODO: Print window open maximized and centered        
        // TODO: Keep zoomborder contents within bounds regardless of scaling
        // TODO: Setup file names                     
        // TODO: Relationship path over various generations can cross person
        // TODO: Correct zoom blur        
        // TODO: Settings window           
        // TODO: Review & comment
        // TODO: Unit tests

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
                
        private FamilyModel savedFamilyModel;
        public FamilyModel SavedFamilyModel
        {
            get { return savedFamilyModel; }
            set
            {
                if (value != savedFamilyModel)
                {
                    savedFamilyModel = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        private FamilyModel currentFamilyModel;
        public FamilyModel CurrentFamilyModel
        {
            get { return currentFamilyModel; }
            set
            {
                if (value != currentFamilyModel)
                {
                    currentFamilyModel = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<FamilyModel> recordedFamilyModels;
        public ObservableCollection<FamilyModel> RecordedFamilyModels
        {
            get { return recordedFamilyModels; }
            set
            {
                if (value != recordedFamilyModels)
                {
                    recordedFamilyModels = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<String> undoDescriptions;
        public ObservableCollection<String> UndoDescriptions
        {
            get { return undoDescriptions; }
            set
            {
                if (value != undoDescriptions)
                {
                    undoDescriptions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<FamilyModel> undoneFamilyModels;
        public ObservableCollection<FamilyModel> UndoneFamilyModels
        {
            get { return undoneFamilyModels; }
            set
            {
                if (value != undoneFamilyModels)
                {
                    undoneFamilyModels = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<String> redoDescriptions;
        public ObservableCollection<String> RedoDescriptions
        {
            get { return redoDescriptions; }
            set
            {
                if (value != redoDescriptions)
                {
                    redoDescriptions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool disableChangeRecording;

        private DateTime lastChangeTime;

        private bool hasChanges = false;
        public bool HasChanges
        {
            get { return hasChanges; }
            set
            {
                if (value != hasChanges)
                {
                    hasChanges = value;
                    NotifyPropertyChanged();
                }
            }
        }       

        private FileStream currentFile;
        public FileStream CurrentFile
        {
            get { return currentFile; }
            set
            {
                if (value != currentFile)
                {
                    currentFile = value;
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

        private string selectCommandInProgressDescription;
        public string SelectCommandInProgressDescription
        {
            get { return selectCommandInProgressDescription; }
            set
            {
                if (value != selectCommandInProgressDescription)
                {
                    selectCommandInProgressDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private bool selectCommandInProgress;
        public bool SelectCommandInProgress
        {
            get { return selectCommandInProgress; }
            set
            {
                if (value != selectCommandInProgress)
                {
                    selectCommandInProgress = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int selectCommandInProgressType;
        public int SelectCommandInProgressType
        {
            get { return selectCommandInProgressType; }
            set
            {
                if (value != selectCommandInProgressType)
                {
                    selectCommandInProgressType = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string selectCommandInProgressColor;
        public string SelectCommandInProgressColor
        {
            get { return selectCommandInProgressColor; }
            set
            {
                if (value != selectCommandInProgressColor)
                {
                    selectCommandInProgressColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private PersonView selectCommandTargetPerson;
        public PersonView SelectCommandTargetPerson
        {
            get { return selectCommandTargetPerson; }
            set
            {
                if (value != selectCommandTargetPerson)
                {
                    selectCommandTargetPerson = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public FamilyView()
        {            
            Tree = new Tree();
            Members = new ObservableCollection<PersonView> { };
            Relationships = new ObservableCollection<RelationshipView> { };            
            SelectedPerson = new PersonView();
            RecordedFamilyModels = new ObservableCollection<FamilyModel> { };
            UndoDescriptions = new ObservableCollection<string> { };
            UndoneFamilyModels = new ObservableCollection<FamilyModel> { };
            RedoDescriptions = new ObservableCollection<string> { };
            InitiateCommands();
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            Members.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedHandler);
            Relationships.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChangedHandler);
            CreateNewFamily();

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

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            RefreshCommandsCanExecute();
            
            if (e.PropertyName == "SavedFamilyModel") { CheckForChanges(); };
            if (e.PropertyName == "CurrentFamilyModel") { CheckForChanges(); };

        }

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (!disableChangeRecording)
            {
                string changeDescription = "";

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    changeDescription += "Add";
                    if (sender.GetType() == typeof(ObservableCollection<PersonView>))
                    {
                        PersonView person = (PersonView)e.NewItems[0];
                        changeDescription += " Person: " + person.FirstName + " " + person.LastName;
                    }
                    if (sender.GetType() == typeof(ObservableCollection<RelationshipView>))
                    {
                        RelationshipView relationship = (RelationshipView)e.NewItems[0];
                        changeDescription += " Relationship: " + relationship.Description;
                    }
                }

                if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    changeDescription += "Delete";

                    if (sender.GetType() == typeof(ObservableCollection<PersonView>))
                    {
                        PersonView person = (PersonView)e.OldItems[0];
                        changeDescription += " Person: " + person.FirstName + " " + person.LastName;
                    }
                    if (sender.GetType() == typeof(ObservableCollection<RelationshipView>))
                    {
                        RelationshipView relationship = (RelationshipView)e.OldItems[0];
                        changeDescription += " Relationship: " + relationship.Description;
                    }
                }


                RecordFamilyChange(changeDescription);
            }
        }

        private void PersonPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DOB")
            {
                PersonView person = (PersonView)sender;
                OrderSiblings(person.GenerationIndex);
                RefreshTreeLayout();
            }
        }

        private void RelationshipPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {

        }

        private void BasePropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {

            if (!disableChangeRecording)
            {
                string changeDescription = "Edit ";
                if (sender.GetType() == typeof(PersonView))
                {
                    PersonView person = (PersonView)sender;
                    changeDescription += person.FirstName + "'s ";
                }
                if (sender.GetType() == typeof(RelationshipView))
                {
                    RelationshipView relationship = (RelationshipView)sender;
                    changeDescription += relationship.Description + " ";
                }

                changeDescription += Regex.Replace(e.PropertyName.ToString(), @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");

                RecordFamilyChange(changeDescription);
            }
        }

        private void InitiateCommands()
        {
            OpenFile = new RelayCommand(OpenFile_Executed, OpenFile_CanExecute);
            SaveFile = new RelayCommand(SaveFile_Executed, SaveFile_CanExecute);
            SaveFileAs = new RelayCommand(SaveFileAs_Executed, SaveFileAs_CanExecute);
            Undo = new RelayCommand(Undo_Executed, Undo_CanExecute);
            Redo = new RelayCommand(Redo_Executed, Redo_CanExecute);
            NewFamily = new RelayCommand(NewFamily_Executed, NewFamily_CanExecute);
            CenterTree = new RelayCommand(CenterTree_Executed, CenterTree_CanExecute);
        }

        private void RefreshCommandsCanExecute()
        {
            OpenFile.RaiseCanExecuteChanged();
            SaveFile.RaiseCanExecuteChanged();
            SaveFileAs.RaiseCanExecuteChanged();
            Undo.RaiseCanExecuteChanged();
            Redo.RaiseCanExecuteChanged();
            NewFamily.RaiseCanExecuteChanged();
            CenterTree.RaiseCanExecuteChanged();
        }

        #region Commands           

        public RelayCommand OpenFile
        {
            get;
            private set;
        }
        public bool OpenFile_CanExecute()
        {
            return true;
        }
        public void OpenFile_Executed()
        {
            // Set open file dialog
            OpenFileDialog openfile = new OpenFileDialog();
            // Set a default file name
            openfile.FileName = "Family.fex";
            // Set filters - this can be done in properties as well
            openfile.Filter = "Family Explorer files (*.fex)|*.fex|All files (*.*)|*.*";

            // Ask to select file
            Nullable<bool> result = openfile.ShowDialog();
            if (result == true)
            {
                // Ask to save any pending changes in current file
                if (CurrentFile != null && HasChanges)
                {
                    switch (SaveChangesDialog())
                    {
                        case MessageBoxResult.Yes:
                            Save();
                            CurrentFile.Dispose();
                            break;
                        case MessageBoxResult.Cancel:
                            return;
                    }
                }
                // Open selected file
                Open(openfile.FileName);
            }
        }
        private string openFile_ToolTip = "Open...";
        public string OpenFile_ToolTip
        {
            get { return openFile_ToolTip; }
            set
            {
                if (value != openFile_ToolTip)
                {
                    openFile_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SaveFile
        {
            get;
            private set;
        }
        public bool SaveFile_CanExecute()
        {
            if (HasChanges) { return true; }
            else { return false; }
        }
        public void SaveFile_Executed()
        {
            Save();
        }
        private string saveFile_ToolTip = "Save";
        public string SaveFile_ToolTip
        {
            get { return saveFile_ToolTip; }
            set
            {
                if (value != saveFile_ToolTip)
                {
                    saveFile_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand SaveFileAs
        {
            get;
            private set;
        }
        public bool SaveFileAs_CanExecute()
        {
            return true;
        }
        public void SaveFileAs_Executed()
        {
            SaveAs();

            //SaveFileDialog savefile = new SaveFileDialog();
            //// set a default file name
            //savefile.FileName = "Family.fex";
            //// set filters - this can be done in properties as well
            //savefile.Filter = "Family Explorer files (*.fex)|*.fex|All files (*.*)|*.*";

            //Nullable<bool> result = savefile.ShowDialog();

            //if (result == true)
            //{

            //    FamilyModel family = new FamilyModel();
            //    family.PersonSettings = Settings.Instance.Person;
            //    family.RelationshipSettings = Settings.Instance.Relationship;
            //    family.Tree = Tree;
            //    family.Members = new ObservableCollection<PersonModel>() { };
            //    foreach (PersonView personView in Members)
            //    {
            //        PersonModel personModel = new PersonModel();
            //        personModel.CopyBaseProperties(personView);
            //        family.Members.Add(personModel);
            //    }
            //    family.Relationships = new ObservableCollection<RelationshipModel> { };
            //    foreach (RelationshipView relationshipView in Relationships)
            //    {
            //        RelationshipModel relationshipModel = new RelationshipModel();
            //        relationshipModel.CopyBaseProperties(relationshipView);
            //        family.Relationships.Add(relationshipModel);
            //    }
            //    XmlSerializer xsSubmit = new XmlSerializer(typeof(FamilyModel));
            //    var subReq = family;
            //    using (StringWriter sww = new StringWriter())
            //    using (XmlWriter writer = XmlWriter.Create(sww))
            //    {
            //        xsSubmit.Serialize(writer, subReq);
            //        var xml = sww.ToString();
            //        XmlDocument xdoc = new XmlDocument();
            //        xdoc.LoadXml(xml);
            //        xdoc.Save(savefile.FileName);
            //        Title = "Family Explorer - " + savefile.FileName;
            //    }
            //}
        }
        private string saveFileAs_ToolTip = "Save As...";
        public string SaveFileAs_ToolTip
        {
            get { return saveFileAs_ToolTip; }
            set
            {
                if (value != saveFileAs_ToolTip)
                {
                    saveFileAs_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand Undo
        {
            get;
            private set;
        }
        public bool Undo_CanExecute()
        {
            if (RecordedFamilyModels.Count() < 1) { return false; }
            else { return true; }
        }
        public void Undo_Executed()
        {
            // Return if there are no recorded models
            if (RecordedFamilyModels.Count < 1) { return; }
            // Do not record changes during the undo
            disableChangeRecording = true;
            // Get the recorded model previous to the edit            
            FamilyModel modelToRestore = RecordedFamilyModels.Last();
            // Remove the undo records
            RecordedFamilyModels.Remove(modelToRestore);
            UndoDescriptions.Remove(UndoDescriptions.Last());
            // Record the current model in the undone list
            FamilyModel recordModel = new FamilyModel();
            recordModel.CopyProperties(currentFamilyModel);
            UndoneFamilyModels.Add(recordModel);
            RedoDescriptions.Add(Undo_ToolTip.Replace("Undo", "Redo"));
            // Refresh the undo-redo tooltips
            if (UndoDescriptions.Count() > 0) { Undo_ToolTip = UndoDescriptions.Last(); }
            else { Undo_ToolTip = "Undo..."; }

            if (RedoDescriptions.Count() > 0) { Redo_ToolTip = RedoDescriptions.Last(); }
            else { Redo_ToolTip = "Redo..."; }
            // Restore          
            RestoreFamilyModel(modelToRestore);

            disableChangeRecording = false;
        }
        private string undo_ToolTip = "Undo...";
        public string Undo_ToolTip
        {
            get { return undo_ToolTip; }
            set
            {
                if (value != undo_ToolTip)
                {
                    undo_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand Redo
        {
            get;
            private set;
        }
        public bool Redo_CanExecute()
        {
            if (UndoneFamilyModels.Count() < 1) { return false; }
            else { return true; }
        }
        public void Redo_Executed()
        {
            // Return if there are no recorded models
            if (UndoneFamilyModels.Count < 1) { return; }
            // Do not record changes during the redo
            disableChangeRecording = true;
            // Get the recorded model that was undone         
            FamilyModel modelToRestore = UndoneFamilyModels.Last();
            // Remove the records
            UndoneFamilyModels.Remove(modelToRestore);
            RedoDescriptions.Remove(RedoDescriptions.Last());
            // Record the current model in the done list
            FamilyModel recordModel = new FamilyModel();
            recordModel.CopyProperties(currentFamilyModel);
            RecordedFamilyModels.Add(recordModel);
            UndoDescriptions.Add(Redo_ToolTip.Replace("Redo", "Undo"));
            // Refresh the undo-redo tooltips
            if (UndoDescriptions.Count() > 0) { Undo_ToolTip = UndoDescriptions.Last(); }
            else { Undo_ToolTip = "Undo..."; }
            if (RedoDescriptions.Count() > 0) { Redo_ToolTip = RedoDescriptions.Last(); }
            else { Redo_ToolTip = "Redo..."; }
            // Restore
            RestoreFamilyModel(modelToRestore);            
            disableChangeRecording = false;
        }
        private string redo_ToolTip = "Redo...";
        public string Redo_ToolTip
        {
            get { return redo_ToolTip; }
            set
            {
                if (value != redo_ToolTip)
                {
                    redo_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand NewFamily
        {
            get;
            private set;
        }
        public bool NewFamily_CanExecute()
        {
            return true;
        }
        public void NewFamily_Executed()
        {
            // Ask to save any pending changes in current file
            if (HasChanges)
            {
                switch (SaveChangesDialog())
                {
                    case MessageBoxResult.Yes:
                        Save();
                        CurrentFile.Dispose();
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            // Create new family
            CreateNewFamily();
        }
        private string newFamily_ToolTip = "Create New Family";
        public string NewFamily_ToolTip
        {
            get { return newFamily_ToolTip; }
            set
            {
                if (value != newFamily_ToolTip)
                {
                    newFamily_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand CenterTree
        {
            get;
            private set;
        }
        public bool CenterTree_CanExecute()
        {
            return true;
        }
        public void CenterTree_Executed()
        {
            CenterTreeInWindow();
        }
        private string centerTree_ToolTip = "Center Tree";
        public string CenterTree_ToolTip
        {
            get { return centerTree_ToolTip; }
            set
            {
                if (value != centerTree_ToolTip)
                {
                    centerTree_ToolTip = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool SelectMother_CanFinalize(PersonView person)
        {
            // Not in previous generation
            if (person.GenerationIndex != SelectCommandTargetPerson.GenerationIndex - 1) { return false; }
            // Not female
            if (person.Gender != "Female") { return false; }
            return true;
        }
        private void SelectMother_Finalized(PersonView person, PersonView mother)
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
        
        private bool SelectFather_CanFinalize(PersonView person)
        {
            // Not in previous generation
            if (person.GenerationIndex != SelectCommandTargetPerson.GenerationIndex - 1) { return false; }
            // Not male
            if (person.Gender != "Male") { return false; }
            return true;
        }
        private void SelectFather_Finalized(PersonView person, PersonView father)
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
        
        private bool SelectFriend_CanFinalize(PersonView person)
        {
            // Not itself
            if (person == SelectCommandTargetPerson) { return false; }
            // Not already a friend
            foreach (RelationshipView friendRelationship in person.FriendRelationships)
            {
                if (friendRelationship.PersonSource == SelectCommandTargetPerson) { return false; }
                if (friendRelationship.PersonDestination == SelectCommandTargetPerson) { return false; }
            }        
               
            return true;
        }
        private void SelectFriend_Finalized(PersonView person, PersonView friend)
        {
            // Create relationship
            if (person.Id < friend.Id)
            { CreateRelationship(4, person, friend, DateTime.Now, null); }
            else
            { CreateRelationship(4, friend, person, DateTime.Now, null); }
        }
                
        private bool SelectPartner_CanFinalize(PersonView person)
        {
            // Not itself
            if (person == SelectCommandTargetPerson) { return false; }
            // Not already a partner
            foreach (RelationshipView partnerRelationship in person.PartnerRelationships)
            {
                if (partnerRelationship.PersonSource == SelectCommandTargetPerson) { return false; }
                if (partnerRelationship.PersonDestination == SelectCommandTargetPerson) { return false; }
            }
            return true;
        }
        private void SelectPartner_Finalized(PersonView person, PersonView partner)
        {
            // Create relationship
            if (person.Id < partner.Id)
            { CreateRelationship(5, person, partner, DateTime.Now, null); }
            else
            { CreateRelationship(5, partner, person, DateTime.Now, null); }
        }
        
        private bool SelectChild_CanFinalize(PersonView child)
        {
            // Not already a child
            foreach (RelationshipView childRelationship in SelectCommandTargetPerson.ChildRelationships)
            {                
                if (childRelationship.PersonDestination == child) { return false; }
            }
            // Not in the next generation
            if (SelectCommandTargetPerson.GenerationIndex + 1 != child.GenerationIndex) { return false; }
            return true;
        }
        private void SelectChild_Finalized(PersonView person, PersonView child)
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
        
        private bool SelectAbuser_CanFinalize(PersonView person)
        {
            // Not already an abuser
            foreach (RelationshipView victimRelationship in person.VictimRelationships)
            {               
                if (victimRelationship.PersonDestination == SelectCommandTargetPerson) { return false; }
            }
            
            return true;
        }
        private void SelectAbuser_Finalized(PersonView person, PersonView abuser)
        {
            // Create relationship                      
            CreateRelationship(6, abuser, person, DateTime.Now, DateTime.Now);
        }
        
        private bool SelectVictim_CanFinalize(PersonView person)
        {
            // Not already a victim
            foreach (RelationshipView abuserRelationship in person.AbuserRelationships)
            {
                if (abuserRelationship.PersonSource == SelectCommandTargetPerson) { return false; }
            }
            return true;
        }
        private void SelectVictim_Finalized(PersonView person, PersonView victim)
        {
            // Create relationship                      
            CreateRelationship(6, person, victim, DateTime.Now, DateTime.Now);
        }

        public void FinalizeSetCommand(PersonView setCommandRelationPerson)
        {
            if (SelectCommandInProgress)
            {
                switch (SelectCommandInProgressType)
                {
                    case 1: // Set mother
                        if (SelectMother_CanFinalize(setCommandRelationPerson))
                        { SelectMother_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 2: // Set father
                        if (SelectFather_CanFinalize(setCommandRelationPerson))
                        { SelectFather_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 3: // Set friend
                        if (SelectFriend_CanFinalize(setCommandRelationPerson))
                        { SelectFriend_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 4: // Set partner
                        if (SelectPartner_CanFinalize(setCommandRelationPerson))
                        { SelectPartner_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 5: // Set child
                        if (SelectChild_CanFinalize(setCommandRelationPerson))
                        { SelectChild_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 6: // Set abuser
                        if (SelectAbuser_CanFinalize(setCommandRelationPerson))
                        { SelectAbuser_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    case 7: // Set victim
                        if (SelectVictim_CanFinalize(setCommandRelationPerson))
                        { SelectVictim_Finalized(SelectCommandTargetPerson, setCommandRelationPerson); }
                        break;
                    default:
                        break;
                }
            }
            EndSetCommand();
        }

        public void EndSetCommand()
        {
            if (SelectCommandInProgress)
            {
                SelectCommandTargetPerson = null;
                SelectCommandInProgressType = 0;
                SelectCommandInProgressDescription = "";
                SelectCommandInProgress = false;
                FamilyTreeCursor = Cursors.Arrow;                
            }            
        }

        public void EnterSetCommandRelation(PersonView person)
        {

            switch (SelectCommandInProgressType)
            {
                case 0: // No command in progress
                    FamilyTreeCursor = Cursors.Arrow;                    
                    break;
                case 1: // Set mother
                    if (SelectMother_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 2: // Set father
                    if (SelectFather_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 3: // Set friend
                    if (SelectFriend_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 4: // Set partner
                    if (SelectPartner_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 5: // Set child
                    if (SelectChild_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 6: // Set abuser
                    if (SelectAbuser_CanFinalize(person))
                    { FamilyTreeCursor = Cursors.Hand; }
                    else { FamilyTreeCursor = Cursors.No; }
                    break;
                case 7: // Set victim
                    if (SelectVictim_CanFinalize(person))
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
            if (SelectCommandInProgress) { FamilyTreeCursor = Cursors.Arrow; }
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
            if (SelectedPerson != null)
            {
                SelectedPerson.Selected = false;
            }
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

        public void AddPersonToFamily(PersonView person)
        {
            if (Members == null)
            {
                Members = new ObservableCollection<PersonView> { };
            }
            Members.Add(person);
            OrderSiblings(person.GenerationIndex);
            RefreshTreeLayout();
            person.PropertyChanged += PersonPropertyChangedHandler;
            person.BasePropertyChanged += BasePropertyChangedHandler;
        }

        public void CreateRelationship(int type, PersonView personSource, PersonView personDestination, DateTime? startDate, DateTime? endDate)
        {

            if (personSource == personDestination) { return; }
            int Id = type * (int)Math.Pow(10, 6) + personSource.Id * (int)Math.Pow(10, 3) + personDestination.Id;

            RelationshipView relationship = FamilyView.Instance.GetRelationship(Id);
            if (relationship != null)
            {
                relationship.Refresh();
            }
            else
            {
                RelationshipView newRelationship = new RelationshipView(Id, startDate, endDate);
                FamilyView.Instance.Relationships.Add(newRelationship);
                newRelationship.PropertyChanged += RelationshipPropertyChangedHandler;
                newRelationship.BasePropertyChanged += BasePropertyChangedHandler;

            }
        }

        public void Open(string filename)
        {            
            // Close handle to old file
            if (CurrentFile != null) { CurrentFile.Dispose(); }

            // Grab handle to new file
            CurrentFile = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            CurrentFile.Seek(0, SeekOrigin.Begin);

            // Deserialize file
            XmlSerializer serializer = new XmlSerializer(typeof(FamilyModel));                            
            FamilyModel newFamilyModel = (FamilyModel)serializer.Deserialize(CurrentFile);
            SavedFamilyModel = newFamilyModel;

            // Restore the familymodel to the workspace
            RestoreFamilyModel(newFamilyModel);

            Title = "Family Explorer - " + filename;
            RecordedFamilyModels.Clear();
            UndoneFamilyModels.Clear();                          
        }

        public bool CanClose()
        {
            // Ask to save any pending changes in current file
            if (HasChanges)
            {
                switch (SaveChangesDialog())
                {
                    case MessageBoxResult.Yes:
                        Save();
                        CurrentFile.Dispose();
                        return true;
                    case MessageBoxResult.No:
                        if (CurrentFile != null) { CurrentFile.Dispose(); }
                        return true;
                    case MessageBoxResult.Cancel:
                        return false;
                    default:
                        return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void RecordFamilyChange(string changeDescription)
        {            

            // Do not record redo-undo commands
            if (disableChangeRecording) { return; }

            // If this change occurred within the last xxx ms of the previous change, assume it is a cascading change and ingnore
            if (lastChangeTime != null)
            {
                if (DateTime.Now - lastChangeTime < new TimeSpan(0, 0, 0, 0, 50))
                {
                    // Make note of the new status
                    CurrentFamilyModel = GetCurrentFamilyModel();
                    lastChangeTime = DateTime.Now;
                    return;
                }
            }

            // Record the status before the change       
            FamilyModel recordModel = new FamilyModel();
            recordModel.CopyProperties(currentFamilyModel);
            RecordedFamilyModels.Add(recordModel); //TODO: need to create a new object, currently storing the same object repeatedly
            UndoDescriptions.Add("Undo " + changeDescription);

            // Clear redo list on new changes
            UndoneFamilyModels.Clear();
            RedoDescriptions.Clear();            

            // Make note of the new status
            CurrentFamilyModel = GetCurrentFamilyModel();
            Undo_ToolTip = UndoDescriptions.Last();

            // Refresh undo-redo tooltips
            if (UndoDescriptions.Count() > 0) { Undo_ToolTip = UndoDescriptions.Last(); }
            else { Undo_ToolTip = "Undo..."; }
            Redo_ToolTip = "Redo...";
           
            // Check if different from saved copy
            //if (SavedFamilyModel.IsEqual(GetCurrentFamilyModel())) { HasChanges = false; }
            //else { HasChanges = true; }

            // Refresh can executes
            Undo.RaiseCanExecuteChanged();
            Redo.RaiseCanExecuteChanged();
            // Record change time
            lastChangeTime = DateTime.Now;            
        }

        private void RestoreFamilyModel(FamilyModel model)
        {
            
            disableChangeRecording = true;
            
            Settings.Instance.Person = new PersonSettings();
            if (model.PersonSettings != null) { Settings.Instance.Person.CopyProperties(model.PersonSettings); }

            Settings.Instance.Relationship = new RelationshipSettings();
            if (model.RelationshipSettings != null) { Settings.Instance.Relationship.CopyProperties(model.RelationshipSettings); }   
                    
            Members.Clear();
            if (model.Members != null)
            {
                foreach (PersonModel personModel in model.Members)
                {
                    PersonView personView = new PersonView(GetNextID());
                    personView.CopyBaseProperties(personModel);
                    Members.Add(personView);                                       
                }
            }

            Relationships.Clear();
            if (model.Relationships != null)
            {
                foreach (RelationshipModel relationshipModel in model.Relationships)
                {
                    RelationshipView relationshipView = new RelationshipView(relationshipModel.Id);
                    relationshipView.CopyBaseProperties(relationshipModel);
                    Relationships.Add(relationshipView);                                     
                }
            }
            Tree = new Tree();
            if (model.Tree != null)
            {
                Tree.CopyProperties(model.Tree);
            }
                 
            SelectedPerson = GetPerson(Tree.SelectedPersonId);
            if (SelectedPerson != null) { SelectedPerson.Selected = true; }

            SelectedRelationship = GetRelationship(Tree.SelectedRelationshipId);
            if (SelectedRelationship != null) { SelectedRelationship.Selected = true; }
            
            RefreshTreeLayout();
            SubscribeToEvents();
            CurrentFamilyModel = GetCurrentFamilyModel();            
            Undo.RaiseCanExecuteChanged();
            Redo.RaiseCanExecuteChanged();
            lastChangeTime = DateTime.Now;
            disableChangeRecording = false;

            FamilyTreeCursor = Cursors.Arrow;
            SelectCommandInProgressType = 0;
        }

        private void SubscribeToEvents()
        {
            if (Members != null)
            {
                foreach (PersonView person in Members)
                {                   
                    person.BasePropertyChanged += BasePropertyChangedHandler;
                }
            }            
            if (Relationships != null)
            {
                foreach (RelationshipView relationship in Relationships)
                {                    
                    relationship.BasePropertyChanged += BasePropertyChangedHandler;
                }
            }
        }

        private MessageBoxResult SaveChangesDialog()
        {           
            string msg = "Save changes to " + Title.Replace("Family Explorer - ", "") + "?";
            var result = MessageBox.Show(msg, "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);           
            return result;                            
        }
        
        private FamilyModel GetCurrentFamilyModel()
        {
            FamilyModel currentFamilyModel = new FamilyModel();

            currentFamilyModel.PersonSettings = Settings.Instance.Person;

            currentFamilyModel.RelationshipSettings = Settings.Instance.Relationship;

            currentFamilyModel.Tree = Tree;
            if (SelectedPerson != null) { currentFamilyModel.Tree.SelectedPersonId = SelectedPerson.Id; }
            if (SelectedRelationship != null) { currentFamilyModel.Tree.SelectedRelationshipId = SelectedRelationship.Id; }

            currentFamilyModel.Members = new ObservableCollection<PersonModel>() { };
            foreach (PersonView personView in Members)
            {
                PersonModel personModel = new PersonModel();
                personModel.CopyBaseProperties(personView);
                currentFamilyModel.Members.Add(personModel);
            }

            currentFamilyModel.Relationships = new ObservableCollection<RelationshipModel> { };
            foreach (RelationshipView relationshipView in Relationships)
            {
                RelationshipModel relationshipModel = new RelationshipModel();
                relationshipModel.CopyBaseProperties(relationshipView);
                currentFamilyModel.Relationships.Add(relationshipModel);
            }
            
            return currentFamilyModel;
        }

        private void CheckForChanges()
        {
            if (SavedFamilyModel == null) { return; }
            if (CurrentFamilyModel == null) { return; }
            if (SavedFamilyModel.IsEqual(CurrentFamilyModel)) { HasChanges = false; }
            else { HasChanges = true; }
        }

        private void UpdateCurrentFile()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(FamilyModel));            
            using (XmlWriter writer = XmlWriter.Create(CurrentFile))
            {                
                CurrentFile.SetLength(0);
                xsSubmit.Serialize(CurrentFile, GetCurrentFamilyModel());
            }
        }

        private void Save()
        {

            if (HasChanges == false) { return; }

            if (CurrentFile != null)
            {
                UpdateCurrentFile();
                CurrentFile.Flush();                
                SavedFamilyModel = GetCurrentFamilyModel();
            }
            else
            {
                SaveAs();
            }
        }

        private void SaveAs()
        {
            // Create a new dialog to select the file to save to
            SaveFileDialog savefile = new SaveFileDialog();

            // Get current file name
            if (CurrentFile != null)
            {
                savefile.FileName = Path.GetFileName(CurrentFile.Name);
                savefile.InitialDirectory = Path.GetDirectoryName(CurrentFile.Name);
            }
            else { savefile.FileName = "NewFamily"; }
          
            // Set dialog filters
            savefile.Filter = "Family Explorer files (*.fex)|*.fex|All files (*.*)|*.*";

            // Show the dialog to ask for the new file name
            Nullable<bool> result = savefile.ShowDialog();

            if (result == true)
            {

                if (CurrentFile != null)
                {
                    // If the user selects the same file name, save and exit
                    if (savefile.FileName == CurrentFile.Name)
                    {
                        Save();                        
                    }
                    else
                    {
                        // Copy current file data to new file
                        using (var fileStream = File.Create(savefile.FileName))
                        {
                            // Go to file origin
                            CurrentFile.Seek(0, SeekOrigin.Begin);
                            // Copy to new file
                            CurrentFile.CopyTo(fileStream);
                            // Close handle to old file
                            CurrentFile.Dispose();
                            // Set the new file as current
                            CurrentFile = fileStream;
                            // Update
                            UpdateCurrentFile();
                            // Save
                            Save();                            
                        }
                    }
                }
                else
                {
                    // Set the new file as current
                    CurrentFile = File.Create(savefile.FileName);
                    // Update
                    UpdateCurrentFile();
                    // Save
                    Save();
                }
            }
                     
        }
       
        private void CreateNewFamily()
        {
            // Create a personmodel
            PersonModel personModel = new PersonModel();
            personModel.FirstName = "First Name";
            personModel.LastName = "Last Name";
            personModel.Gender = "Not Specified";
            personModel.DOB = DateTime.Now;
            personModel.GenerationIndex = 0;
            personModel.SiblingIndex = 0;

            // Create a familymodel and add the personmodel
            FamilyModel newFamily = new FamilyModel();
            newFamily.PersonSettings = new PersonSettings();
            newFamily.RelationshipSettings = new RelationshipSettings();
            newFamily.Tree = new Tree();
            newFamily.Members = new ObservableCollection<PersonModel>() { };
            newFamily.Relationships = new ObservableCollection<RelationshipModel>() { };
            newFamily.Members.Add(personModel);

            // Mark the personmodel as selected
            newFamily.Tree.SelectedPersonId = personModel.Id;
            
            // Restore this new familymodel into the workspace
            RestoreFamilyModel(newFamily);
            newFamily.CopyProperties(GetCurrentFamilyModel());
            SavedFamilyModel = newFamily;           
            SavedFamilyModel.CopyProperties(GetCurrentFamilyModel());           
            
            // Set the title
            Title = "Family Explorer - NewFamily.fex";

            // Clear undo-redo records
            RecordedFamilyModels.Clear();
            UndoneFamilyModels.Clear();
        }

        private void SetPersonPosition(PersonView person)
        {
            person.X = Tree.Width / 2 + person.SiblingIndex * (Settings.Instance.Person.Width + Settings.Instance.Person.HorizontalSpace) - Settings.Instance.Person.Width / 2;
            person.Y = (person.GenerationIndex - Members.Min(m => m.GenerationIndex)) * (Settings.Instance.Person.Height + Settings.Instance.Person.VerticalSpace) + Settings.Instance.Person.Height / 2;
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

        private void RefreshTreeLayout()
        {
            Tree.Width = (Members.Max(m => m.SiblingIndex) + 1 - Members.Min(m => m.SiblingIndex)) * (Settings.Instance.Person.Width + Settings.Instance.Person.HorizontalSpace) + Settings.Instance.Person.HorizontalSpace;
            Tree.Height = (Members.Max(m => m.GenerationIndex) + 1 - Members.Min(m => m.GenerationIndex)) * (Settings.Instance.Person.Height + Settings.Instance.Person.VerticalSpace);

            foreach (PersonView person in Members)
            {
                SetPersonPosition(person);
            }           
            SetScaledTreeDimensions();
        }

        private void SetScaledTreeDimensions()
        {
            Tree.WidthScaled = Tree.Width * Tree.Scale;
            Tree.HeightScaled = Tree.Height * Tree.Scale;
        }

        private void SetWindowSize(double width, double height)
        {
            Tree.WindowWidth = width;
            Tree.WindowHeight = height;
        }

        private void CenterTreeInWindow()
        {
            RefreshTreeLayout();
            Tree.XPosition = (Tree.WindowWidth / 2) - (Tree.Width / 2);
            Tree.YPosition = (Tree.WindowHeight / 2) - (Tree.Height / 2);
        }

        private void MoveTreePositionInWindow(double deltaX, double deltaY)
        {
            Tree.XPosition += deltaX; /// TreeScale;
            Tree.YPosition += deltaY; /// TreeScale;
        }

        private void ScaleTree(double scaleIncrease, double windowCenterX, double windowCenterY)

        {
            Tree.ScaleOrigin = new Point(0.5, 0.5);

            Tree.ScaleCenterX = windowCenterX;
            Tree.ScaleCenterY = windowCenterY;

            if (scaleIncrease > 0)
            { Tree.Scale = Tree.Scale * (scaleIncrease / 100); }
            else { Tree.Scale = Tree.Scale * (-100 / scaleIncrease); }

            SetScaledTreeDimensions();
        }

    }
}
