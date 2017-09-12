using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class MinTest : BaseTestClass
    {
        [TestMethod]
        public void Min_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Literature")
                .SelectArray(x => x.Programs)
                .Where(x => true)
                .Select(x => x.Cost)
                .Min(16000);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$min", 2);
        }

        [TestMethod]
        public void Min_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(17000, univ.Departments[3].Programs[0].Cost);
            Assert.AreEqual(14000, univ.Departments[3].Programs[1].Cost);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Literature")
                .SelectArray(x => x.Programs)
                .Where(x => true)
                .Select(x => x.Cost)
                .Min(16000);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(16000, univ.Departments[3].Programs[0].Cost);
            Assert.AreEqual(14000, univ.Departments[3].Programs[1].Cost);
        }
    }
}
