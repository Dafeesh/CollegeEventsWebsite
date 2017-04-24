using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CollegeEvents;
using CollegeEvents.Database;

namespace CollegeEventsTests
{
    using db = DatabaseInterface;

    [TestClass]
    public class QueryEvents
    {
        [TestMethod]
        public void SelectEvents()
        {
            db.ResetDatabase();

            var e = PlannedEvent.Examples.First();
            var selA = db.SelectEvents(hostSchoolId: e.HostOrgId).FirstOrDefault();
            var selB = db.SelectEvents(hostSchoolId: e.HostSchoolId).FirstOrDefault();

            Assert.IsNotNull(selB);
            Assert.IsTrue(selA.About == e.About);
            Assert.IsTrue(selB.About == e.About);
        }
    }
}