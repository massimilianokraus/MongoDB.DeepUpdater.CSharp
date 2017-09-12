using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class BitwiseOrTest : BaseTestClass
    {
        [TestMethod]
        public void BitwiseOr_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor.Age)
                .BitwiseOr(52);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "or", 1);
        }

        [TestMethod]
        public void BitwiseOr_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(57, univ.Administration.Chancellor.Age);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor.Age)
                .BitwiseOr(52);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(61, univ.Administration.Chancellor.Age);
        }
    }
}