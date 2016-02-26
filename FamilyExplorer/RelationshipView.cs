using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class RelationshipView : RelationshipBase, INotifyPropertyChanged

    {
        private PersonView personSource;
        public PersonView PersonSource
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

        private PersonView personDestination;
        public PersonView PersonDestination
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

        private string path;
        public string Path
        {
            get { return path; }
            set
            {
                if (value != path)
                {
                    path = value;
                    NotifyPropertyChanged();                    
                }
            }
        }

        private double pathThickness = 6;
        public double PathThickness
        {
            get { return pathThickness; }
            set
            {
                if (value != pathThickness)
                {
                    pathThickness = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string pathColor;
        public string PathColor
        {
            get { return pathColor; }
            set
            {
                if (value != pathColor)
                {
                    pathColor = value;
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
                    switch (Type)
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
        }

        public bool EndDateVisible
        {
            get
            {
               
                    switch (Type)
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
        }

        public RelationshipView()
        {
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            Initialize();
        }

        public void Initialize()
        {
                       
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
           if (e.PropertyName == "PersonSourceId") { PersonSource = FamilyView.Instance.GetPerson(PersonSourceId); }
           if (e.PropertyName == "PersonDestinationId") { PersonDestination = FamilyView.Instance.GetPerson(PersonDestinationId); }
           if (PersonSource != null && PersonDestination != null) { ResetData(); }           
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
            switch (Type)
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

            switch (Type)
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
                    if (Ended)
                    {
                        SourceDescription = sourceFirstName + " " + sourceLastName + " from age " + sourceAgeStart + " to " + sourceAgeEnd;
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
                    if (Ended)
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

        private string GetAgeAtRelationshipStart(PersonView person)
        {

            int age = StartDate.Year - person.DOB.Year;
            if (person.DOB > StartDate.AddYears(-age) && age > 0) age--;

            return age.ToString();
        }

        private string GetAgeAtRelationshipEnd(PersonView person)
        {
            if (EndDate != null)
            {
                DateTime end = (DateTime)EndDate;
                int age = end.Year - person.DOB.Year;
                if (person.DOB > end.AddYears(-age) && age > 0) age--;
                return age.ToString();
            }
            else { return ""; }
        }

        private void SetDateDescriptions()
        {
            switch (Type)
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
