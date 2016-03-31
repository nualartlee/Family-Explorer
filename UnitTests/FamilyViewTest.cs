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
            

        }
    }
}
