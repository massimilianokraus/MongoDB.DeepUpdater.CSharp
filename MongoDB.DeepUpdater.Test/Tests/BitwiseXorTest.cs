using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class BitwiseXorTest : BaseTestClass
    {
        [TestMethod]
        public void BitwiseXor_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor.Age)
                .BitwiseXor(52);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "xor", 1);
        }

        [TestMethod]
        public void BitwiseXor_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(57, univ.Administration.Chancellor.Age);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor.Age)
                .BitwiseXor(52);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(13, univ.Administration.Chancellor.Age); //bad age for a chancellor ahah
        }
    }
}