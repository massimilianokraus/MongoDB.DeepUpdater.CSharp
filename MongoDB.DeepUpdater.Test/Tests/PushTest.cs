using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class PushTest : BaseTestClass
    {
        [TestMethod]
        public void Push_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Push(new Person { Name = "Jack Frost" });

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByType, "PushUpdateDefinition", 1);
        }

        [TestMethod]
        public void Push_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(5, univ.Administration.Employees.Count);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Push(new Person { Name = "Jack Frost" });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual("Jack Frost", univ.Administration.Employees[5].Name);

            update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .Push(new Person { Name = "Al Middlewest" });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(7, univ.Administration.Employees.Count);
            Assert.AreEqual("Al Middlewest", univ.Administration.Employees[6].Name);
        }
    }
}
