using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CollegeEvents;
using CollegeEvents.Database;

namespace CollegeEventsTests
{
    using db = DatabaseInterface;

    [TestClass]
    public class QuerySchools
    {
        [TestMethod]
        public void SelectSchools()
        {
            db.ResetDatabase();

            var u = School.Examples.First();
            var selA = db.SelectSchools(id: u.Id).FirstOrDefault();
            var selB = db.SelectSchools(name: u.Name).FirstOrDefault();

            Assert.IsNotNull(selA);
            Assert.IsNotNull(selB);
            Assert.IsTrue(selA.NumStudents == u.NumStudents);
            Assert.IsTrue(selB.NumStudents == u.NumStudents);
        }
    }
}
