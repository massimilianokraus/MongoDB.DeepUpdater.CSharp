using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class DoTest : BaseTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Do_UpdateCreator_Null()
        {
            var univ = FindByToken("Complete");

            var fluent = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments);

            var update = fluent.Do(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Do_Item_UpdateCreator_Null()
        {
            var univ = FindByToken("Complete");

            var fluent = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration);

            var update = fluent.Do(null, new InstitutionAdministration());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Do_Array_UpdateCreator_Null()
        {
            var univ = FindByToken("Complete");

            var fluent = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments);

            var update = fluent.Do(null, new List<Department>());
        }
    }
}
