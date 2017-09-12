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
    public class MulTest : BaseTestClass
    {
        [TestMethod]
        public void Mul_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Engineering")
                .SelectArray(x => x.Programs)
                .Where(x => x.Name == "Marine Engineering")
                .Select(x => x.Cost)
                .Mul(1.2);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$mul", 1);
        }

        [TestMethod]
        public void Mul_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(25000, univ.Departments[0].Programs[1].Cost);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Engineering")
                .SelectArray(x => x.Programs)
                .Where(x => x.Name == "Marine Engineering")
                .Select(x => x.Cost)
                .Mul(1.2);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");
            Assert.AreEqual(25000 * 1.2, univ.Departments[0].Programs[1].Cost);
        }
    }
}
