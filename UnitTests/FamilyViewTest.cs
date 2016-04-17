using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FamilyExplorer;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class FamilyViewTest
    {

        public FamilyView family;

        public FamilyViewTest()
        {            
            family = FamilyView.Instance;
                        
        }

        [TestMethod]
        public void TestRemoveMother(PersonView person)
        {
            // Select the person
            family.SelectedPerson = person;

            if (family.SelectedPerson.MotherRelationship != null)
            {                              
                // Count people & relationships
                int people = family.Members.Count;
                int relationships = family.Relationships.Count;
                int siblings = family.SelectedPerson.SiblingRelationships.Count;
                List<RelationshipView> siblingList = family.SelectedPerson.MotherRelationship.PersonSource.ChildRelationships.ToList();
                int motherChildren = family.SelectedPerson.MotherRelationship.PersonSource.ChildRelationships.Count;

                // Remove the relationship               
                family.SelectedPerson.MotherRelationship.Delete.Execute(this);
               
                // Should not have this relationship
                Assert.IsNull(family.SelectedPerson.MotherRelationship, "Should not have this relationship");

                // Should be able to add this relationship
                Assert.IsTrue(family.SelectedPerson.AddMother.CanExecute(this), "CanExecute should be true");
                
                // People count should remain
                Assert.AreEqual(people, family.Members.Count, "Wrong family member count");

                // Relationship count should have decreased
                Assert.AreEqual(relationships - 1, family.Relationships.Count, "Wrong family relationship count");

                // Sibling count could have decreased
                Assert.AreEqual(siblings - motherChildren + 1, family.SelectedPerson.SiblingRelationships.Count, "Wrong sibling count");

            }
            else
            {                
            }
        }

        [TestMethod]
        public void TestAddMother(PersonView person)
        {
            // Select the person
            family.SelectedPerson = person;

            if (family.SelectedPerson.MotherRelationship != null)
            {
                // Should not be able to add this relationship
                Assert.IsFalse(family.SelectedPerson.AddMother.CanExecute(this), "CanExecute should be false");
            }
            else
            {                
                // Count people & relationships
                int people = family.Members.Count;
                int relationships = family.Relationships.Count;

                // Add relationship
                family.SelectedPerson.AddMother.Execute(this);

                // Should have this relationship
                Assert.IsNotNull(family.SelectedPerson.MotherRelationship, "Should have this relationship");

                // People & relationship count should have increased
                Assert.AreEqual(people + 1, family.Members.Count, "Wrong family member count");
                Assert.AreEqual(relationships + 1, family.Relationships.Count, "Wrong family relationship count");
            }
        }

        [TestMethod]
        public void TestRemoveFather(PersonView person)
        {
            // Select the person
            family.SelectedPerson = person;

            if (family.SelectedPerson.FatherRelationship != null)
            {               
                // Count people & relationships
                int people = family.Members.Count;
                int relationships = family.Relationships.Count;
                int siblings = family.SelectedPerson.SiblingRelationships.Count;
                List<RelationshipView> siblingList = family.SelectedPerson.FatherRelationship.PersonSource.ChildRelationships.ToList();
                int fatherChildren = family.SelectedPerson.FatherRelationship.PersonSource.ChildRelationships.Count;

                // Remove the relationship               
                family.SelectedPerson.FatherRelationship.Delete.Execute(this);

                // Should not have this relationship
                Assert.IsNull(family.SelectedPerson.FatherRelationship, "Should not have this relationship");

                // Should be able to add this relationship
                Assert.IsTrue(family.SelectedPerson.AddFather.CanExecute(this), "CanExecute should be true");

                // People count should remain
                Assert.AreEqual(people, family.Members.Count, "Wrong family member count");

                // Relationship count should have decreased
                Assert.AreEqual(relationships - 1, family.Relationships.Count, "Wrong family relationship count");

                // Sibling count could have decreased
                Assert.AreEqual(siblings - fatherChildren + 1, family.SelectedPerson.SiblingRelationships.Count, "Wrong sibling count");

            }
            else
            {
            }
        }

        [TestMethod]
        public void TestAddFather(PersonView person)
        {
            // Select the person
            family.SelectedPerson = person;

            if (family.SelectedPerson.FatherRelationship != null)
            {
                // Should not be able to add this relationship
                Assert.IsFalse(family.SelectedPerson.AddFather.CanExecute(this), "CanExecute should be false");
            }
            else
            {               
                // Count people & relationships
                int people = family.Members.Count;
                int relationships = family.Relationships.Count;

                // Add relationship
                family.SelectedPerson.AddFather.Execute(this);

                // Should have this relationship
                Assert.IsNotNull(family.SelectedPerson.FatherRelationship, "Should have this relationship");

                // People & relationship count should have increased
                Assert.AreEqual(people + 1, family.Members.Count, "Wrong family member count");
                Assert.AreEqual(relationships + 1, family.Relationships.Count, "Wrong family relationship count");
            }
        }

        [TestMethod]
        public void TestAddMotherToAllMembers()
        {
            foreach (PersonView person in family.Members.ToList())
            {
                TestRemoveMother(person);
                TestAddMother(person);
            }
        }

        [TestMethod]
        public void TestAddFatherToAllMembers()
        {
            foreach (PersonView person in family.Members.ToList())
            {
                TestRemoveFather(person);
                TestAddFather(person);
            }
        }

        [TestMethod]
        public void TestAddSiblingEqualParents()
        {

            foreach (PersonView person in family.Members.ToList())
            {
                // Select the next person
                family.SelectedPerson = person;
                
                // Count people & relationships
                int siblings = family.SelectedPerson.SiblingRelationships.Count;
                int motherChildren = 0;
                if (family.SelectedPerson.MotherRelationship != null) { motherChildren = family.SelectedPerson.MotherRelationship.PersonSource.ChildRelationships.Count; }
                int fatherChildren = 0;
                if (family.SelectedPerson.FatherRelationship != null) { fatherChildren = family.SelectedPerson.FatherRelationship.PersonSource.ChildRelationships.Count; }
                int people = family.Members.Count;
                int relationships = family.Relationships.Count;

                // Add relationship
                family.SelectedPerson.AddSiblingEqualParents.Execute(this);

                // Should have this relationship
                Assert.IsNotNull(family.SelectedPerson.FatherRelationship, "Should have this relationship");

                // People & relationship count should have increased
                Assert.AreEqual(people + 1, family.Members.Count, "Wrong family member count");
                //Assert.AreEqual(relationships + 3, family.Relationships.Count, "Wrong family relationship count");
            }
        }
    }
}
