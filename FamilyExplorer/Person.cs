using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class Person : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        private int id;
        public int Id
        { 
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged();
                }
            } 
        }
        private string firstName;
        public string FirstName
        { 
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    NotifyPropertyChanged();
                }
            } 
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string gender;
        public string Gender
        {
            get { return gender; }
            set
            {
                if (value != gender)
                {
                    gender = value;                    
                    NotifyPropertyChanged();
                }
            }
        }
        private DateTime dob;
        public DateTime DOB
        {
            get { return dob; }
            set
            {
                if (value != dob)
                {
                    dob = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int motherId;
        public int MotherId
        {
            get { return motherId; }
            set
            {
                if (value != motherId)
                {
                    motherId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int fatherId;
        public int FatherId
        {
            get { return fatherId; }
            set
            {
                if (value != fatherId)
                {
                    fatherId = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private List<int> siblingIds;
        public List<int> SiblingIds
        {
            get { return siblingIds; }
            set
            {
                if (value != siblingIds)
                {
                    siblingIds = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private List<int> partnerIds;
        public List<int> PartnerIds
        {
            get { return partnerIds; }
            set
            {
                if (value != partnerIds)
                {
                    partnerIds = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private List<int> friendIds;
        public List<int> FriendIds
        {
            get { return friendIds; }
            set
            {
                if (value != friendIds)
                {
                    friendIds = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private List<int> childrenIds;
        public List<int> ChildrenIds
        {
            get { return childrenIds; }
            set
            {
                if (value != childrenIds)
                {
                    childrenIds = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private List<int> abuserIds;
        public List<int> AbuserIds
        {
            get { return abuserIds; }
            set
            {
                if (value != abuserIds)
                {
                    abuserIds = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private List<int> victimIds;
        public List<int> VictimIds
        {
            get { return victimIds; }
            set
            {
                if (value != victimIds)
                {
                    victimIds = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int generationIndex;
        public int GenerationIndex
        {
            get { return generationIndex; }
            set
            {
                if (value != generationIndex)
                {
                    generationIndex = value;
                    //setPosition();
                    NotifyPropertyChanged();
                }
            }
        }
        private double siblingIndex;
        public double SiblingIndex
        {
            get { return siblingIndex; }
            set
            {
                if (value != siblingIndex)
                {
                    siblingIndex = value;
                    //setPosition();
                    NotifyPropertyChanged();
                }
            }
        }

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
        private string colorText;
        public string ColorText
        {
            get { return colorText; }
            set
            {
                if (value != colorText)
                {
                    colorText = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private int width;
        public int Width
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
        private int height;
        public int Height
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
        
    }
}
