using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class RelationshipData : INotifyPropertyChanged
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

        public RelationshipData(Relationship relationship, Person source, Person destination)
        {
            Relationship = relationship;
            PersonSource = source;
            PersonDestination = destination;
            if (relationship != null)
            {
                personSource.PropertyChanged += new PropertyChangedEventHandler(DataChanged);
                personDestination.PropertyChanged += new PropertyChangedEventHandler(DataChanged);
                relationship.PropertyChanged += new PropertyChangedEventHandler(DataChanged);
                ResetData();
            }
        }

        private void DataChanged(object sender, PropertyChangedEventArgs e)
        {
            ResetData();
        }

        public void ResetData()
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
            string sourceFirstName = PersonSource.FirstName;
            string sourceLastName = PersonSource.LastName;
            string sourceAgeStart = GetAgeAtRelationshipStart(PersonSource);
            string sourceAgeEnd = GetAgeAtRelationshipEnd(PersonSource);
            string sourceGender = PersonSource.Gender;
            string destinationFirstName = PersonDestination.FirstName;
            string destinationLastName = PersonDestination.LastName;
            string destinationAgeStart = GetAgeAtRelationshipStart(PersonDestination);
            string destinationAgeEnd = GetAgeAtRelationshipEnd(PersonDestination);
            string destinationGender = PersonDestination.Gender;

            switch (Relationship.Type)
            {
                case 1: // Mother
                    SourceDescription = "Mother: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAgeStart;

                    if (destinationGender == "Female") { DestinationDescription = "Daughter: " + destinationFirstName + " " + destinationLastName; }
                    else if (destinationGender == "Male") { DestinationDescription = "Son: " + destinationFirstName + " " + destinationLastName; }
                    else { DestinationDescription = "Child: " + destinationFirstName + " " + destinationLastName; }
                    break;

                case 2: // Father
                    SourceDescription = "Father: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAgeStart;

                    if (destinationGender == "Female") { DestinationDescription = "Daughter: " + destinationFirstName + " " + destinationLastName; }
                    else if (destinationGender == "Male") { DestinationDescription = "Son: " + destinationFirstName + " " + destinationLastName; }
                    else { DestinationDescription = "Child: " + destinationFirstName + " " + destinationLastName; }
                    break;

                case 3: // Sibling
                    if (sourceGender == "Female") { SourceDescription = "Sister: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAgeStart; }
                    else if (sourceGender == "Male") { SourceDescription = "Brother: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAgeStart; }
                    else { SourceDescription = "Sibling: " + sourceFirstName + " " + sourceLastName + " at age " + sourceAgeStart; }

                    if (destinationGender == "Female") { DestinationDescription = "Sister: " + destinationFirstName + " " + destinationLastName; }
                    else if (destinationGender == "Male") { DestinationDescription = "Brother: " + destinationFirstName + " " + destinationLastName; }
                    else { DestinationDescription = "Sibling: " + destinationFirstName + " " + destinationLastName; }
                    break;

                case 4: // Friend
                    if (relationship.Ended)
                    {
                        SourceDescription = sourceFirstName + " " + sourceLastName + " from age " + sourceAgeStart + " to " + sourceAgeEnd ;
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " from age " + destinationAgeStart + " to " + destinationAgeEnd;
                        break;
                    }
                    else
                    {
                        SourceDescription = sourceFirstName + " " + sourceLastName + " from age " + sourceAgeStart;
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " from age " + destinationAgeStart;
                        break;
                    }

                case 5: // Partner
                    if (relationship.Ended)
                    {
                        SourceDescription = sourceFirstName + " " + sourceLastName + " from age " + sourceAgeStart + " to " + sourceAgeEnd;
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " from age " + destinationAgeStart + " to " + destinationAgeEnd;
                        break;
                    }
                    else
                    {
                        SourceDescription = sourceFirstName + " " + sourceLastName + " at age " + sourceAgeStart;
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " at age " + destinationAgeStart;
                        break;
                    }
                    

                case 6: // Abuse
                    SourceDescription = "Abuser: " + sourceFirstName + " " + sourceLastName + " from age " + sourceAgeStart + " to " + sourceAgeEnd;
                    DestinationDescription = "Victim: " + destinationFirstName + " " + destinationLastName + " from age " + destinationAgeStart + " to " + destinationAgeEnd;
                    break;

                default:
                    break;


            }
        }

        private string GetAgeAtRelationshipStart(Person person)
        {

            int age = relationship.StartDate.Year - person.DOB.Year;
            if (person.DOB > relationship.StartDate.AddYears(-age) && age > 0) age--;

            return age.ToString();
        }

        private string GetAgeAtRelationshipEnd(Person person)
        {
            if (relationship.EndDate != null)
            {
                DateTime end = (DateTime)relationship.EndDate;
                int age = end.Year - person.DOB.Year;
                if (person.DOB > end.AddYears(-age) && age > 0) age--;
                return age.ToString();
            }
            else { return ""; }
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
