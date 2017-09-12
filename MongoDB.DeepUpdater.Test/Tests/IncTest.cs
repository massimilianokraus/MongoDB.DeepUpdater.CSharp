using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class IncTest : BaseTestClass
    {
        [TestMethod]
        public void Inc_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor.Age)
                .Inc(-4);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$inc", 1);
        }

        [TestMethod]
        public void Inc_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(57, univ.Administration.Chancellor.Age);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor.Age)
                .Inc(-4);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(53, univ.Administration.Chancellor.Age);
        }
    }
}
