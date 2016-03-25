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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyExplorer
{
    public class FamilyModel
    {
        public PersonSettings PersonSettings;
        public RelationshipSettings RelationshipSettings;
        public Tree Tree;
        public ObservableCollection<PersonModel> Members;
        public ObservableCollection<RelationshipModel> Relationships;

        public void CopyProperties(FamilyModel copyModel)
        {
            PersonSettings = new PersonSettings();
            PersonSettings.CopyProperties(copyModel.PersonSettings);
            RelationshipSettings = new RelationshipSettings();
            RelationshipSettings.CopyProperties(copyModel.RelationshipSettings);
            Tree = new Tree();
            Tree.CopyProperties(copyModel.Tree);
            Members = new ObservableCollection<PersonModel>() { };
            Members = copyModel.Members;
            Relationships = new ObservableCollection<RelationshipModel>() { };
            Relationships = copyModel.Relationships;
        }
    }
}
