using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System.Linq;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class PullAllTest : BaseTestClass
    {
        [TestMethod]
        public void PullAll_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .PullAll(new[] {
                    new Person { Name = "Alan Silvestri" },
                    new Person { Name = "Bob Marley" },
                    new Person { Name = "Bob Silvestri" },
                    new Person { Name = "Alicia Boys" }});

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByType, "PullUpdateDefinition", 1);
        }

        [TestMethod]
        public void PullAll_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(5, univ.Administration.Employees.Count);
            Assert.IsTrue(univ.Administration.Employees.Any(x => x.Name == "Alan Silvestri"));
            Assert.IsTrue(univ.Administration.Employees.Any(x => x.Name == "Bob Marley"));

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Administration.Employees)
                .PullAll(new[] {
                    new Person { Name = "Alan Silvestri" },
                    new Person { Name = "Bob Marley" },
                    new Person { Name = "Bob Silvestri" }, });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(3, univ.Administration.Employees.Count);
            Assert.IsFalse(univ.Administration.Employees.Any(x => x.Name == "Alan Silvestri"));
            Assert.IsFalse(univ.Administration.Employees.Any(x => x.Name == "Bob Marley"));
        }
    }
}
