using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private PersonView reciprocal;
        public PersonView Reciprocal
        {
            get { return reciprocal; }
            set
            {
                if (value != reciprocal)
                {
                    reciprocal = value;
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

        private double highlightPathThickness = 8;
        public double HighlightPathThickness
        {
            get { return highlightPathThickness; }
            set
            {
                if (value != highlightPathThickness)
                {
                    highlightPathThickness = value;
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

        private string highlightPathColor;
        public string HighlightPathColor
        {
            get { return highlightPathColor; }
            set
            {
                if (value != highlightPathColor)
                {
                    highlightPathColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string highlightTextColor;
        public string HighlightTextColor
        {
            get { return highlightTextColor; }
            set
            {
                if (value != highlightTextColor)
                {
                    highlightTextColor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int zIndex;
        public int ZIndex
        {
            get { return zIndex; }
            set
            {
                if (value != zIndex)
                {
                    zIndex = value;
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
       
        private bool ended = false;
        public bool Ended
        {
            get { return ended; }
            set
            {
                if (value != ended)
                {
                    if (ended == true) { EndDate = DateTime.Now; }
                    else { EndDate = null; }
                    ended = value;
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

        public RelationshipView(int id)
        {
            InitiateCommands();
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            FamilyView.Instance.PropertyChanged += new PropertyChangedEventHandler(FamilyViewPropertyChangedHandler);
            GetDataFromId(id);
            RefreshHighlight();
            
        }

        public RelationshipView(int id, DateTime? start, DateTime? end)
        {
            InitiateCommands();
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            FamilyView.Instance.PropertyChanged += new PropertyChangedEventHandler(FamilyViewPropertyChangedHandler);
            GetDataFromId(id);           
            if (start != null) { StartDate = (DateTime)start; }
            if (end != null) { EndDate = (DateTime)end; }
            RefreshHighlight();
            InitiateCommands();
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PersonSource")
            {
                PersonSource.PropertyChanged += new PropertyChangedEventHandler(SourcePersonPropertyChangedHandler);
                PersonSource.AddRelationship(this);
            }
            if (e.PropertyName == "PersonDestination")
            {
                PersonDestination.PropertyChanged += new PropertyChangedEventHandler(DestinationPersonPropertyChangedHandler);
                PersonDestination.AddRelationship(this);
            }

            if (e.PropertyName == "Selected")
            {
                RefreshZIndex();
                RefreshHighlight();
            }

            if (e.PropertyName == "MouseOver")
            {
                RefreshZIndex();
                RefreshHighlight();
            }

            if (PersonSource != null && PersonDestination != null) { Refresh(); }
        }

        private void SourcePersonPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (PersonSource != null && PersonDestination != null) { Refresh(); }
        }

        private void DestinationPersonPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (PersonSource != null && PersonDestination != null) { Refresh(); }
        }

        private void FamilyViewPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "SelectedPerson") { SetReciprocal(); }
        }       

        private void GetDataFromId(int id)
        {
            if (id < 1000000) { return; }
            Id = id;
            int destinationId = id % 1000;
            int sourceId = ((id - destinationId) / 1000) % 1000;
            int tp = (id - sourceId * 1000 - destinationId) / 1000000;
            Type = tp;
            PersonDestination = FamilyView.Instance.GetPerson(destinationId);
            if (PersonDestination != null) { PersonDestination.AddRelationship(this); }
            PersonSource = FamilyView.Instance.GetPerson(sourceId);
            if (PersonSource != null) { PersonSource.AddRelationship(this); }

        }

        public void Refresh()
        {
            RefreshHeaderDescription();
            RefreshPersonDescriptions();
            RefreshDateDescriptions();
            RefreshReciprocal();
            SetPath();
            RefreshColors();            
            PathThickness = Settings.Instance.Relationship.PathThickness;
            HighlightPathThickness = Settings.Instance.Relationship.SelectedPathThickness;
            RefreshCommandsCanExecute();
            RefreshTooltips();          
        }

        private void RefreshHeaderDescription()
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

        private void RefreshPersonDescriptions()
        {
            string sourceFirstName = PersonSource.FirstName;
            string sourceLastName = PersonSource.LastName;
            string sourceAgeStart = GetAgeAtDate(PersonSource, StartDate);
            string sourceAgeEnd = GetAgeAtDate(PersonSource, EndDate);
            string sourceGender = PersonSource.Gender;
            string destinationFirstName = PersonDestination.FirstName;
            string destinationLastName = PersonDestination.LastName;
            string destinationAgeStart = GetAgeAtDate(PersonDestination, StartDate);
            string destinationAgeEnd = GetAgeAtDate(PersonDestination, EndDate);
            string destinationGender = PersonDestination.Gender;
            string ageDescription = "";

            switch (Type)
            {
                case 1: // Mother
                    SourceDescription = "Mother: " + sourceFirstName + " " + sourceLastName + " at " + sourceAgeStart + " of age";

                    if (destinationGender == "Female") { DestinationDescription = "Daughter: " + destinationFirstName + " " + destinationLastName; }
                    else if (destinationGender == "Male") { DestinationDescription = "Son: " + destinationFirstName + " " + destinationLastName; }
                    else { DestinationDescription = "Child: " + destinationFirstName + " " + destinationLastName; }
                    break;

                case 2: // Father
                    SourceDescription = "Father: " + sourceFirstName + " " + sourceLastName + " at " + sourceAgeStart + " of age";

                    if (destinationGender == "Female") { DestinationDescription = "Daughter: " + destinationFirstName + " " + destinationLastName; }
                    else if (destinationGender == "Male") { DestinationDescription = "Son: " + destinationFirstName + " " + destinationLastName; }
                    else { DestinationDescription = "Child: " + destinationFirstName + " " + destinationLastName; }
                    break;

                case 3: // Sibling
                    if (sourceAgeStart != "from birth") { ageDescription = " at " + sourceAgeStart + " of age"; }
                    else { ageDescription = ""; }
                    if (sourceGender == "Female") { SourceDescription = "Sister: " + sourceFirstName + " " + sourceLastName + ageDescription; }
                    else if (sourceGender == "Male") { SourceDescription = "Brother: " + sourceFirstName + " " + sourceLastName + ageDescription; }
                    else { SourceDescription = "Sibling: " + sourceFirstName + " " + sourceLastName + ageDescription; }

                    if (destinationAgeStart != "from birth") { ageDescription = " at " + sourceAgeStart + " of age"; }
                    else { ageDescription = ""; }
                    if (destinationGender == "Female") { DestinationDescription = "Sister: " + destinationFirstName + " " + destinationLastName + ageDescription;}
                    else if (destinationGender == "Male") { DestinationDescription = "Brother: " + destinationFirstName + " " + destinationLastName + ageDescription; }
                    else { DestinationDescription = "Sibling: " + destinationFirstName + " " + destinationLastName + ageDescription; }
                    break;

                case 4: // Friend
                case 5: // Partner
                    if (Ended)
                    {
                        if (sourceAgeStart == sourceAgeEnd) { SourceDescription = sourceFirstName + " " + sourceLastName + " at " + sourceAgeStart + " of age"; }
                        else { SourceDescription = sourceFirstName + " " + sourceLastName + " from " + sourceAgeStart + " to " + sourceAgeEnd + " of age"; }
                        if (destinationAgeStart == destinationAgeEnd) { DestinationDescription = destinationFirstName + " " + destinationLastName + " at " + destinationAgeStart + " of age"; }
                        else { DestinationDescription = destinationFirstName + " " + destinationLastName + " from " + destinationAgeStart + " to " + destinationAgeEnd + " of age"; }
                        break;
                    }
                    else
                    {
                        SourceDescription = sourceFirstName + " " + sourceLastName + " from " + sourceAgeStart + " of age";
                        DestinationDescription = destinationFirstName + " " + destinationLastName + " from " + destinationAgeStart + " of age";
                        break;
                    }
               

                case 6: // Abuse
                    if (sourceAgeStart == sourceAgeEnd) { SourceDescription = "Abuser: " + sourceFirstName + " " + sourceLastName + " at " + sourceAgeStart + " of age"; }
                    else { SourceDescription = "Abuser: " + sourceFirstName + " " + sourceLastName + " from " + sourceAgeStart + " to " + sourceAgeEnd + " of age"; }
                    if (destinationAgeStart == destinationAgeEnd) { DestinationDescription = "Victim: " + destinationFirstName + " " + destinationLastName + " at " + destinationAgeStart + " of age"; }
                    else { DestinationDescription = "Victim: " + destinationFirstName + " " + destinationLastName + " from " + destinationAgeStart + " to " + destinationAgeEnd + " of age"; }
                    break;

                default:
                    break;


            }
        }

        private string GetAgeAtDate(PersonView person, DateTime? dateTime)
        {
            if (dateTime != null)
            {
                DateTime date = (DateTime)dateTime;

                int age = date.Year - person.DOB.Year;
                if (person.DOB > date.AddYears(-age) && age > 0) age--;
                if (age < 2)
                {
                    double days = (date - person.DOB).TotalDays;
                    double months = days / 30;                   
                    if (months <= 1)
                    {
                        if (Math.Floor(days) == 0) { return "from birth"; }
                        return Math.Floor(days).ToString() + " Days";
                    }
                    return "~" + Math.Floor(months).ToString() + " Months";
                }
                return age.ToString() + " Years";
            }
            else { return ""; }
        }
        
        private void RefreshDateDescriptions()
        {
            switch (Type)
            {
                case 1:
                case 2:
                case 3:
                    StartDate = (PersonSource.DOB > PersonDestination.DOB) ? PersonSource.DOB : PersonDestination.DOB;
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

        private void RefreshReciprocal()
        {
            PersonView selected = FamilyView.Instance.SelectedPerson;
            if (selected != null)
            {
                if (PersonSource != null)
                {
                    if (selected == PersonSource) { Reciprocal = PersonDestination; }
                }
                if (PersonDestination != null)
                {
                    if (selected == PersonDestination) { Reciprocal = PersonSource; }
                }
            }
        }

        private void RefreshColors()
        {
            PathColor = Settings.Instance.Relationship.PathColor(Type);         
        }

        private void RefreshHighlight()
        {
            if (Selected)
            {
                HighlightPathColor = Settings.Instance.Relationship.SelectedPathColor;
                HighlightTextColor = Settings.Instance.Relationship.SelectedPathColor;
            }
            else if (MouseOver)
            {
                HighlightPathColor = Settings.Instance.Relationship.HighlightedPathColor;
                HighlightTextColor = Settings.Instance.Relationship.HighlightedPathColor;
            }
            else
            {
                HighlightPathColor = "Transparent";
                HighlightTextColor = "Black";
            }
        }

        private void RefreshZIndex()
        {
            ZIndex = 1;
            if (PersonSource.Selected) { ZIndex = 2; }
            if (PersonDestination.Selected) { ZIndex = 2; }            
            if (Selected) { ZIndex = 3; }
            if (MouseOver) { ZIndex = 4; }
        }

        private void RefreshTooltips()
        {
            Delete_ToolTip = "Delete this relationship between " + PersonSource.FirstName + " and " + PersonDestination.FirstName;
        }

        #region Path

        public void SetPath()
        {
            if (PersonSource.X == PersonSource.Y && PersonSource.Y == PersonDestination.X && PersonDestination.X == PersonDestination.Y) { return; }
            double width = Settings.Instance.Person.Width;
            double height = Settings.Instance.Person.Height;
            double horizontalSpace = Settings.Instance.Person.HorizontalSpace;
            double verticalSpace = Settings.Instance.Person.VerticalSpace;
            double offset = Settings.Instance.Relationship.PathOffset(Type);
            double margin = Settings.Instance.Person.Margin;
            double radius = Settings.Instance.Relationship.PathCornerRadius;

            Point origin = new Point(PersonSource.X + width / 2, PersonSource.Y + height / 2);
            Point destination = new Point(PersonDestination.X + width / 2, PersonDestination.Y + height / 2);

            if (origin == destination) { return; }

            int generationCrossings = PersonDestination.GenerationIndex - PersonSource.GenerationIndex;
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
                if (points.Count() > 500) { return; }
            }

            Path = SmoothenPath(points);
        }

        private string SmoothenPath(List<Point> points)
        {
            double radius = Settings.Instance.Relationship.PathCornerRadius;
            string path = "M" + points[0].ToString();

            for (int i = 1; i < points.Count(); i++)
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
                List<PersonView> peopleBelow = FamilyView.Instance.Members.Where(m => m.GenerationIndex == generationIndex + 1).OrderBy(m => m.X).ToList();

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
                List<PersonView> peopleAbove = FamilyView.Instance.Members.Where(m => m.GenerationIndex == generationIndex - 1).OrderBy(m => m.X).ToList();
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
            bool positive = location >= -margin;
            if (positive)
            {
                if (location == height + margin) { return 1; } // Person bottom
                if (location == height + space / 2 + offset) { return 2; } // On horizontal path
                if (location == height + space - margin) { return 3; } // Person top
            }
            else
            {
                if (location == -space + margin) { return 1; } // Person bottom
                if (location == -space / 2 + offset) { return 2; } // On horizontal path
                if (location == -margin) { return 3; } // Person top
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

        #endregion Path

        private void InitiateCommands()
        {
            MouseEnter = new RelayCommand(MouseEnter_Executed, MouseEnter_CanExecute);
            MouseLeave = new RelayCommand(MouseLeave_Executed, MouseLeave_CanExecute);
            MouseLeftButtonDown = new RelayCommand<MouseEventArgs>(MouseLeftButtonDown_Executed, MouseLeftButtonDown_CanExecute);
            Delete = new RelayCommand(Delete_Executed, Delete_CanExecute);
        }

        private void RefreshCommandsCanExecute()
        {
            MouseEnter.RaiseCanExecuteChanged();
            MouseLeave.RaiseCanExecuteChanged();
            MouseLeftButtonDown.RaiseCanExecuteChanged();
            Delete.RaiseCanExecuteChanged();
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
            FamilyView.Instance.SelectRelationship(this);
            e.Handled = true;
        }

        public RelayCommand Delete
        {
            get;
            private set;
        }
        private bool Delete_CanExecute()
        {
            return true;
        }
        private void Delete_Executed()
        {
            switch (Type)
            {
                case 1: // Mother
                    PersonSource.ChildRelationships.Remove(this);
                    PersonDestination.MotherRelationship = null;
                    break;
                case 2: // Father
                    PersonSource.ChildRelationships.Remove(this);
                    PersonDestination.FatherRelationship = null;
                    break;
                case 3: // Sibling
                    PersonSource.SiblingRelationships.Remove(this);
                    PersonDestination.SiblingRelationships.Remove(this);
                    break;
                case 4: // Friend
                    PersonSource.FriendRelationships.Remove(this);
                    PersonDestination.FriendRelationships.Remove(this);
                    break;
                case 5: // Partner
                    PersonSource.PartnerRelationships.Remove(this);
                    PersonDestination.PartnerRelationships.Remove(this);
                    break;
                case 6: // Abuse
                    PersonSource.VictimRelationships.Remove(this);
                    PersonDestination.AbuserRelationships.Remove(this);
                    break;
                default:
                    break;

            }
            FamilyView.Instance.Relationships.Remove(this);
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
    }
}
