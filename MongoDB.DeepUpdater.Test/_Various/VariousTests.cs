using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using MongoDB.Bson;
using System.Linq.Expressions;
using MongoDB.DeepUpdater;
using System.Linq;
using MongoDB.Bson.Serialization;

namespace MongoDB.DeepUpdater.Test.TestClasses
{
    [TestClass]
    public class VariousTests : BaseTestClass
    {
        [TestMethod]
        public void PopFirst_OnEmptyArray()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => true)
                .SelectArray(x => x.Programs)
                .PopFirst();

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Combine_Empty_List()
        //{
        //    var updates = new List<UpdateDefinition<University>>();

        //    // Exception here: at least 1 update is expected, but lest is empty.
        //    var combined = Builders<University>.Update.Combine(updates);

        //    _mongoCollection.UpdateOne(new BsonDocument(), combined);
        //}

        //[TestMethod]
        //public void Where_Set()
        //{
        //    Expression<Func<University, bool>> filter = x => x.TestReferenceName == "Complete";

        //    var completeUniversity = _mongoCollection.Find(filter).First();

        //    var update = Builders<University>.Update
        //        .Deep(completeUniversity)
        //        .SelectArray(x => x.Departments)
        //        .Where(x => x.MacroArea == "Science")
        //        .Set(new Department { Area = "Hacked!" });

        //    _mongoCollection.UpdateOne(filter, update);

        //    var newUniv = _mongoCollection.Find(filter).First();

        //    var hacked = newUniv.Departments.Count(x => x.Area == "Hacked!");
        //    Assert.AreEqual(3, hacked);
        //}

        //[TestMethod]
        //public void Array_AddToSet()
        //{
        //    Expression<Func<University, bool>> filter = x => x.TestReferenceName == "Complete";

        //    var completeUniversity = _mongoCollection.Find(filter).First();

        //    var update = Builders<University>.Update.Deep(completeUniversity)
        //        .SelectArray(x => x.Departments)
        //        .AddToSet(new Department { Area = "New Dep" });

        //    _mongoCollection.UpdateOne(filter, update);

        //    var newUniv = _mongoCollection.Find(filter).First();

        //    var hacked = newUniv.Departments.Count(x => x.Area == "Hacked!");
        //    Assert.AreEqual(3, hacked);
        //}

        //[TestMethod]
        //public void FieldDefinitions_Equal()
        //{
        //    Expression<Func<University, bool>> filter = x => x.TestReferenceName == "Complete";

        //    var completeUniversity = _mongoCollection.Find(filter).First();

        //    var f1 = (FieldDefinition<University, string>)"Departments.0.Area";
        //    var f2 = (FieldDefinition<University, string>)"Departments.0.Area";

        //    Assert.AreNotEqual(f1, f2);

        //    var s1 = f1.Render(_mongoCollection.DocumentSerializer, _mongoCollection.Settings.SerializerRegistry);
        //    Assert.AreEqual("Departments.0.Area", s1.FieldName);
        //}

        //[TestMethod]
        //public void AddToSet()
        //{
        //    Expression<Func<University, bool>> filter = x => x.TestReferenceName == "Complete";

        //    var univ = _mongoCollection.Find(filter).First();

        //    univ.Departments.Clear();

        //    _mongoCollection.ReplaceOne(x => x.TestReferenceName == "Complete", univ);

        //    univ = _mongoCollection.Find(filter).First();

        //    Assert.AreEqual(0, univ.Departments.Count);

        //    var d1 = new Department { Area = "SomeArea" };
        //    var d2 = new Department { Area = "SomeArea" };

        //    var update = Builders<University>.Update
        //        .Deep(univ)
        //        .SelectArray(x => x.Departments)
        //        .AddToSet(d1);

        //    _mongoCollection.UpdateOne(x => x.TestReferenceName == "Complete", update);

        //    univ = _mongoCollection.Find(filter).First();

        //    Assert.AreEqual(1, univ.Departments.Count);

        //    var update2 = Builders<University>.Update
        //        .Deep(univ)
        //        .SelectArray(x => x.Departments)
        //        .AddToSet(d2);

        //    _mongoCollection.UpdateOne(x => x.TestReferenceName == "Complete", update);

        //    univ = _mongoCollection.Find(filter).First();

        //    Assert.AreEqual(1, univ.Departments.Count);
        //}

        //[TestMethod]
        //public void a()
        //{
        //    Expression<Func<University, bool>> filter = x => x.TestReferenceName == "Complete";

        //    var univ = _mongoCollection.Find(filter).First();

        //    var update = Builders<University>.Update.Deep(univ)
        //        .SelectArray(x => x.Departments)
        //        .AddToSet(new Department { Area = "New Dep" });

        //    //_mongoCollection.UpdateOne(filter, update);
        //}
    }
}
