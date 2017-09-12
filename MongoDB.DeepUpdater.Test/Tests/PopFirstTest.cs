using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class PopFirstTest : BaseTestClass
    {
        [TestMethod]
        public void PopFirst_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.MacroArea == "Science")
                .SelectArray(x => x.Programs)
                .PopFirst();

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$pop", 3);
        }

        [TestMethod]
        public void PopFirst_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(3, univ.Departments[0].Programs.Count);
            Assert.AreEqual(1, univ.Departments[1].Programs.Count);
            Assert.AreEqual(0, univ.Departments[2].Programs.Count);
            Assert.AreEqual("Civil Engineering", univ.Departments[0].Programs[0].Name);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.MacroArea == "Science")
                .SelectArray(x => x.Programs)
                .PopFirst();

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");

            Assert.AreEqual(2, univ.Departments[0].Programs.Count);
            Assert.AreEqual(0, univ.Departments[1].Programs.Count);
            Assert.AreEqual(0, univ.Departments[2].Programs.Count);

            Assert.AreEqual("Marine Engineering", univ.Departments[0].Programs[0].Name);
        }
    }
}
