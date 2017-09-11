using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class SelectAndWhereTest : BaseTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Select_Expression_Null()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select<Department>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Select_SingleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Rector);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Select_DoubleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration)
                .Select(x => x.Rector);
        }
    }
}
