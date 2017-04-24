using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CollegeEvents;
using CollegeEvents.Database;

namespace CollegeEventsTests
{
    using db = DatabaseInterface;

    [TestClass]
    public class QueryUsers
    {
        [TestMethod]
        public void SelectUsers()
        {
            db.ResetDatabase();

            var u = User.Examples.First();
            var selA = db.SelectUsers(id: u.Id).FirstOrDefault();
            var selB = db.SelectUsers(username: u.Username).FirstOrDefault();

            Assert.IsNotNull(selA);
            Assert.IsNotNull(selB);
            Assert.IsTrue(selA.FirstName == u.FirstName);
            Assert.IsTrue(selB.FirstName == u.FirstName);
        }
    }
}
