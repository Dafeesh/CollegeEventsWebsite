using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CollegeEvents;
using CollegeEvents.Database;

namespace CollegeEventsTests
{
    using db = DatabaseInterface;

    [TestClass]
    public class QueryOrganizations
    {
        [TestMethod]
        public void SelectOrg()
        {
            db.ResetDatabase();

            var o = Organization.Examples.First();
            var selA = db.SelectOrganizations(id: o.Id).FirstOrDefault();
            var selB = db.SelectOrganizations(name: o.Name).FirstOrDefault();

            Assert.IsNotNull(selA);
            Assert.IsNotNull(selB);
            Assert.IsTrue(selA.About == o.About);
            Assert.IsTrue(selB.About == o.About);
        }
    }
}
