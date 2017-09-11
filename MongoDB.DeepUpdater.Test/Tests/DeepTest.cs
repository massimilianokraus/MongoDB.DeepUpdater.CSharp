using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class DeepTest : BaseTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Deep_Null()
        {
            University univ = null;

            var update = Builders<University>.Update.Deep(univ);
        }

        [TestMethod]
        public void Deep_Ok()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update.Deep(univ);

            Assert.AreSame(univ, update.Document);
        }
    }
}
