using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class SetTest : BaseTestClass
    {
        [TestMethod]
        public void Set_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor)
                .Set(new Person { Name = "Massimiliano Kraus" });

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$set", 1);
        }

        [TestMethod]
        public void Set_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual("Anders Hejlsberg", univ.Administration.Chancellor.Name);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor)
                .Set(new Person { Name = "Massimiliano Kraus" });

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual("Massimiliano Kraus", univ.Administration.Chancellor.Name);
        }
    }
}
