using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class AddToSetTest : BaseTestClass
    {
        [TestMethod]
        public void AddToSet_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .AddToSet(new Person { Name = "Jack Frost" });

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByType, "AddToSetUpdateDefinition", 1);
        }

        [TestMethod]
        public void AddToSet_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(5, univ.Administration.Employees.Count);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .AddToSet(new Person { Name = "Jack Frost" });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual("Jack Frost", univ.Administration.Employees[5].Name);

            update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .AddToSet(new Person { Name = "Al Middlewest" });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(6, univ.Administration.Employees.Count);
        }
    }
}
