using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class CurrentDateTest : BaseTestClass
    {
        [TestMethod]
        public void CurrentDate_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Where(x => x.Name.EndsWith("s"))
                .Select(x => x.Hired)
                .CurrentDate();

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$currentDate", 2);
        }

        [TestMethod]
        public void CurrentDate_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(new DateTime(2010, 09, 5, 00, 00, 00, DateTimeKind.Utc), univ.Administration.Employees[1].Hired);
            Assert.AreEqual(new DateTime(2015, 11, 30, 00, 00, 00, DateTimeKind.Utc), univ.Administration.Employees[3].Hired);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Where(x => x.Name.EndsWith("s"))
                .Select(x => x.Hired)
                .CurrentDate();

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            var now = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm");
            Assert.AreEqual(now, univ.Administration.Employees[1].Hired.ToString("yyyy-MM-dd hh:mm"));
            Assert.AreEqual(now, univ.Administration.Employees[3].Hired.ToString("yyyy-MM-dd hh:mm"));
        }
    }
}
