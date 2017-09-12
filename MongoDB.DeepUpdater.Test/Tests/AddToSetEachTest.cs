using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class AddToSetEachTest : BaseTestClass
    {
        [TestMethod]
        public void AddToSetEach_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .AddToSetEach(new[] {
                    new Person { Name = "Alan Silvestri" },
                    new Person { Name = "Bob Marley" },
                    new Person { Name = "Bob Silvestri" }, });

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByType, "AddToSetUpdateDefinition", 1);
        }

        [TestMethod]
        public void AddToSetEach_CheckResult()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .AddToSetEach(new[] {
                    new Person { Name = "Alan Silvestri" },
                    new Person { Name = "Bob Marley" },
                    new Person { Name = "Bob Silvestri" }, });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(6, univ.Administration.Employees.Count);
            Assert.AreEqual("Bob Silvestri", univ.Administration.Employees[5].Name);
        }
    }
}
