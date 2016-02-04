using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class Family
    {
        public Tree Tree;
        public ObservableCollection<Person> Members;
    }
}
