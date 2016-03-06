using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        private string reciprocal;
        public string Reciprocal
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
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            FamilyView.Instance.PropertyChanged += new PropertyChangedEventHandler(FamilyViewPropertyChangedHandler);
            GetDataFromId(id);
            SetHighlight();
        }

        public RelationshipView(int id, DateTime? start, DateTime? end)
        {
            PropertyChanged += new PropertyChangedEventHandler(PropertyChangedHandler);
            FamilyView.Instance.PropertyChanged += new PropertyChangedEventHandler(FamilyViewPropertyChangedHandler);
            GetDataFromId(id);           
            if (start != null) { StartDate = (DateTime)start; }
            if (end != null) { EndDate = (DateTime)end; }
            SetHighlight();
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
                SetZIndex();
                SetHighlight();
            }

            if (e.PropertyName == "MouseOver")
            {                
                SetHighlight();
            }

            if (PersonSource != null && PersonDestination != null) { ResetAllData(); }
        }

        private void SourcePersonPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (PersonSource != null && PersonDestination != null) { ResetAllData(); }
        }

        private void DestinationPersonPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (PersonSource != null && PersonDestination != null) { ResetAllData(); }
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
            PersonDestination.AddRelationship(this);
            PersonSource = FamilyView.Instance.GetPerson(sourceId);
            PersonSource.AddRelationship(this);

        }

        public void ResetAllData()
        {
            SetHeaderDescription();
            SetPersonDescriptions();
            SetDateDescriptions();
            SetReciprocal();
            SetPath();
            SetColors();            
            PathThickness = Settings.Instance.Relationship.PathThickness;
            HighlightPathThickness = Settings.Instance.Relationship.SelectedPathThickness;                     
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

        private void SetReciprocal()
        {
            PersonView selected = FamilyView.Instance.SelectedPerson;
            if (selected != null)
            {
                if (PersonSource != null)
                {
                    if (selected == PersonSource) { Reciprocal = PersonDestination.FirstName; }
                }
                if (PersonDestination != null)
                {
                    if (selected == PersonDestination) { Reciprocal = PersonSource.FirstName; }
                }
            }
        }

        private void SetColors()
        {
            PathColor = Settings.Instance.Relationship.PathColor(Type);         
        }

        private void SetHighlight()
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

        private void SetZIndex()
        {
            ZIndex = 1;
            if (PersonSource.Selected) { ZIndex = 2; }
            if (PersonDestination.Selected) { ZIndex = 2; }
            if (Selected) { ZIndex = 3; }
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
        
        public void MouseEnter()
        {
            MouseOver = true;            
        }

        public void MouseLeave()
        {
            MouseOver = false;            
        }

        public void MouseLeftButtonDown()
        {            
            FamilyView.Instance.SelectRelationship(this);
        }

    }
}
