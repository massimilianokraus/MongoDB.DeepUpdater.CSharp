using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System.Linq;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class PullTest : BaseTestClass
    {
        [TestMethod]
        public void Pull_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Pull(univ.Administration.Employees[1]);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByType, "PullUpdateDefinition", 1);
        }

        [TestMethod]
        public void Pull_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(5, univ.Administration.Employees.Count);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Pull(univ.Administration.Employees[1]);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");

            Assert.AreEqual(4, univ.Administration.Employees.Count);
            Assert.IsFalse(univ.Administration.Employees.Any(x => x.Name == "Phil Morris"));
        }
    }
}
