using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class RelationshipViewModel : INotifyPropertyChanged
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

        private Relationship relationship;
        public Relationship Relationship
        {
            get { return relationship; }
            set
            {
                if (value != relationship)
                {
                    relationship = value;
                    NotifyPropertyChanged();                    
                }
            }
        }

        private Person personSource;
        public Person PersonSource
        {
            get { return personSource; }
            set
            {
                if (value != personSource)
                {
                    personSource = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Person personDestination;
        public Person PersonDestination
        {
            get { return personDestination; }
            set
            {
                if (value != personDestination)
                {
                    personDestination = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string sourceDescription;
        public string SourceDescription
        {
            get { return sourceDescription; }
            set
            {
                if (value != sourceDescription)
                {
                    sourceDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string destinationDescription;
        public string DestinationDescription
        {
            get { return destinationDescription; }
            set
            {
                if (value != destinationDescription)
                {
                    destinationDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string startDateDescription;
        public string StartDateDescription
        {
            get { return startDateDescription; }
            set
            {
                if (value != startDateDescription)
                {
                    startDateDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string endDateDescription;
        public string EndDateDescription
        {
            get { return endDateDescription; }
            set
            {
                if (value != endDateDescription)
                {
                    endDateDescription = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool StartDateVisible
        {
            
            get
            {
                if (Relationship != null)
                {
                    switch (Relationship.Type)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return false;
                        case 4:
                        case 5:
                        case 6:
                            return true;
                        default:
                            return false;
                    }
                }
                else { return false; }
            }
        }

        public bool EndDateVisible
        {
            get
            {
                if (Relationship != null)
                {
                    switch (Relationship.Type)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return false;
                        case 4:
                        case 5:
                        case 6:
                            return true;
                        default:
                            return false;
                    }
                }
                else { return false; }
            }
        }

        private void ResetData()
        {
            SetDescription();
        }

        public void SetDescription()
        {
            SetHeaderDescription();
            SetPersonDescriptions();
            SetDateDescriptions();                    
        }
        
        private void SetHeaderDescription()
        {
            string source = (PersonSource != null) ? PersonSource.FirstName : "";
            string destination = (PersonDestination != null) ? PersonDestination.FirstName : "";
            string destinationGender = (PersonDestination != null) ? PersonDestination.Gender : "";
            string header = "";
            switch (Relationship.Type)
            {
                case 1:                   
                case 2:
                    if (destinationGender == "Female") { header = "daughter"; }
                    else if (destinationGender == "Male") { header = "son"; }
                    else { header = "child"; }
                    break;               
                case 3:
                    if (destinationGender == "Female") { header = "sister"; }
                    else if (destinationGender == "Male") { header = "brother"; }
                    else { header = "sibling"; }
                    break;
                case 4:
                    header = "friendship with";
                    break;
                case 5:
                    header = "partnership with";
                    break;
                case 6:
                    header = "abuse on";
                    break;
                default:
                    header = "";
                    break;

            }

            
            Description = source + "'s " + header + " " + destination;
        }

        private void SetPersonDescriptions()
        {
            string sourceFirstName = "";
            string sourceLastName = "";
            string sourceAge = "";
            string sourceGender = "";
            string destinationFirstName = "";
            string destinationLastName = "";
            string destinationAge = "";
            string destinationGender = "";

            if (PersonSource != null)
            {
                sourceFirstName = PersonSource.FirstName;
                sourceLastName = PersonSource.LastName;               

                DateTime now = DateTime.Today;
                int age = now.Year - PersonSource.DOB.Year;
                if (PersonSource.DOB > now.AddYears(-age)) age--;
                sourceAge = GetAgeAtRelationshipStart(PersonSource);
                sourceGender = PersonSource.Gender;

            }
            if (PersonDestination != null)
            {
                destinationFirstName = PersonDestination.FirstName;
                destinationLastName = PersonDestination.LastName;

                DateTime now = DateTime.Today;
                int age = now.Year - PersonDestination.DOB.Year;
                if (PersonDestination.DOB > now.AddYears(-age)) age--;
                destinationAge = GetAgeAtRelationshipStart(PersonDestination);
                destinationGender = PersonDestination.Gender;
            }
                switch (Relationship.Type)
                {
                    case 1:
                        SourceDescription = "Mother: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAge;

                        if (destinationGender == "Female") { DestinationDescription = "Daughter: " + destinationFirstName + " " + destinationLastName; }
                        else if (destinationGender == "Male") { DestinationDescription = "Son: " + destinationFirstName + " " + destinationLastName; }
                        else { DestinationDescription = "Child: " + destinationFirstName + " " + destinationLastName; }
                        break;

                    case 2:
                        SourceDescription = "Father: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAge;

                        if (destinationGender == "Female") { DestinationDescription = "Daughter: " + destinationFirstName + " " + destinationLastName; }
                        else if (destinationGender == "Male") { DestinationDescription = "Son: " + destinationFirstName + " " + destinationLastName; }
                        else { DestinationDescription = "Child: " + destinationFirstName + " " + destinationLastName; }
                        break;

                    case 3:
                        if (sourceGender == "Female") { SourceDescription = "Sister: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAge; }
                        else if (sourceGender == "Male") { SourceDescription = "Brother: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAge; }
                        else { SourceDescription = "Sibling: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAge; }

                        if (destinationGender == "Female") { DestinationDescription = "Sister: " + destinationFirstName + " " + destinationLastName; }
                        else if (destinationGender == "Male") { DestinationDescription = "Brother: " + destinationFirstName + " " + destinationLastName; }
                        else { DestinationDescription = "Sibling: " + destinationFirstName + " " + destinationLastName; }
                        break;

                    case 4:
                        SourceDescription = sourceFirstName + " " + sourceLastName + " at age " + sourceAge;
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " at age " + destinationAge;
                        break;

                    case 5:
                        SourceDescription = sourceFirstName + " " + sourceLastName + " at age " + sourceAge;
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " at age " + destinationAge;
                        break;

                    case 6:
                        SourceDescription = "Abuser: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAge;
                        DestinationDescription = "Victim: " + destinationFirstName + " " + destinationLastName + " at age " + destinationAge;
                        break;

                    default:
                        break;

                
            }
        }
        
        private string GetAgeAtRelationshipStart(Person person)
        {
            
            int age = relationship.StartDate.Year - person.DOB.Year;
            if (person.DOB > relationship.StartDate.AddYears(-age)) age--;
            
            return age.ToString();
        }

        private void SetDateDescriptions()
        {
            switch (Relationship.Type)
            {
                case 1:
                case 2:
                    
                case 3:
                    StartDateDescription = "";
                    EndDateDescription = "";
                    break;
                case 4:
                    StartDateDescription = "Friendship started:";
                    EndDateDescription = "Friendship ended:";
                    break;
                case 5:
                    StartDateDescription = "Partnership started:";
                    EndDateDescription = "Partnership ended:";
                    break;
                case 6:
                    StartDateDescription = "Abuse started:";
                    EndDateDescription = "Abuse ended:";
                    break;
                default:
                    StartDateDescription = "";
                    EndDateDescription = "";
                    break;

            }
        }

    }
}
