using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FamilyExplorer;

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
        public void TestCreateNewFamily()
        {
            family.CreateNewFamily();

            Assert.AreEqual("Family Explorer - NewFamily.fex", family.Title);
            Assert.AreEqual(1, family.Members.Count);
            Assert.AreEqual(0, family.Relationships.Count);
            Assert.AreEqual(null, family.CurrentFile);
            Assert.AreEqual(0, family.UndoneFamilyModels.Count);
            Assert.AreEqual(0, family.RecordedFamilyModels.Count);

        }
    }
}
