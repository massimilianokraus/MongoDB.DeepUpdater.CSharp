using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class MaxTest : BaseTestClass
    {
        [TestMethod]
        public void Max_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Engineering")
                .SelectArray(x => x.Programs)
                .Where(x => x.Name == "Informatic Engineering")
                .SelectArray(x => x.Years)
                .Where(x => x.Order == 1)
                .SelectArray(x => x.Classes)
                .Where(x => true)
                .Select(x => x.Credits)
                .Max(10);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$max", 3);
        }

        [TestMethod]
        public void Max_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(12, univ.Departments[0].Programs[2].Years[0].Classes[0].Credits);
            Assert.AreEqual(9, univ.Departments[0].Programs[2].Years[0].Classes[1].Credits);
            Assert.AreEqual(3, univ.Departments[0].Programs[2].Years[0].Classes[2].Credits);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Engineering")
                .SelectArray(x => x.Programs)
                .Where(x => x.Name == "Informatic Engineering")
                .SelectArray(x => x.Years)
                .Where(x => x.Order == 1)
                .SelectArray(x => x.Classes)
                .Where(x => true)
                .Select(x => x.Credits)
                .Max(10);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(12, univ.Departments[0].Programs[2].Years[0].Classes[0].Credits);
            Assert.AreEqual(10, univ.Departments[0].Programs[2].Years[0].Classes[1].Credits);
            Assert.AreEqual(10, univ.Departments[0].Programs[2].Years[0].Classes[2].Credits);
        }
    }
}
